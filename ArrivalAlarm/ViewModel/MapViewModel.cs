using ArrivalAlarm.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;

namespace ArrivalAlarm.ViewModel
{
    public class MapViewModel : ViewModelBase, INavigable
    {
        /// <summary>
        /// Navigation service used for navigation between pages
        /// </summary>
        private INavigationService _navigationService;

        private MapModel _model;

        private string _selectedLocation;

        private Geopoint _actualLocation;

        /// <summary>
        /// Actual user location on map
        /// </summary>
        public Geopoint ActualLocation
        {
            get { return _actualLocation; }
            private set { Set(nameof(ActualLocation), ref _actualLocation, value); }
        }

        /// <summary>
        /// Map zoom level (from 1 to 20 )
        /// </summary>
        public double ZoomLevel
        {
            get; private set;
        }

        /// <summary>
        /// Text from auto suggest box
        /// </summary>
        public string AutoSuggestBoxText { get; set; } = "";

        private readonly ObservableCollection<string> _foundLocations = new ObservableCollection<string>();

        public INotifyCollectionChanged FoundLocations
        {
            get { return _foundLocations; }
        }

        private bool _pushpinVisible;

        public bool PushpinVisible
        {
            get { return _pushpinVisible; }
            private set { Set(nameof(PushpinVisible), ref _pushpinVisible, value); }
        }

        public MapViewModel()
        {
            _model = new MapModel(TimeSpan.FromMinutes(2));
            _navigationService = ServiceLocator.Current.GetInstance<INavigationService>();

            CreateCommands();
        }

        /// <summary>
        /// Updates actual user location
        /// </summary>
        private async void UpdateUserLocation()
        {
            var actualLocation = await _model.GetActualLocationAsync();
            ActualLocation = actualLocation.Coordinate.Point;
            ZoomLevel = 12;
            Messenger.Default.Send(ActualLocation, Messages.Tokens.MapViewToken);
            PushpinVisible = true;
        }

        private void SetProvidedLocation(MapLocation location)
        {
            if (location == null)
                return;

            ActualLocation = location.Point;
            ZoomLevel = 12;
            Messenger.Default.Send(ActualLocation, Messages.Tokens.MapViewToken);
            PushpinVisible = true;
        }

        #region Commands

        /// <summary>
        /// Creates all commands
        /// </summary>
        private void CreateCommands()
        {
            FindMeCommand = new RelayCommand(UpdateUserLocation);
            TextChangeCommand = new RelayCommand<bool>(TextChangedCommandExecute);
            SuggestionChosenCommand = new RelayCommand<object>(SuggestionChosenExecute);
        }

        /// <summary>
        /// Command launched when user wants update of his current location
        /// </summary>
        public ICommand FindMeCommand { get; private set; }

        /// <summary>
        /// Command launched when user clicked combo box
        /// </summary>
        public ICommand TextChangeCommand { get; private set; }

        private async void TextChangedCommandExecute(bool isUserInputReason)
        {
            _foundLocations.Clear();
            if (isUserInputReason && !string.IsNullOrEmpty(AutoSuggestBoxText))
            {
                var userInput = AutoSuggestBoxText;
                var locations = await _model.FindLocationAsync(userInput).ConfigureAwait(true);

                if (locations?.Count == 0)
                    return;

                var foundLocations = locations?.Where(location => GetReadableName(location).Contains(userInput)) ?? new List<MapLocation>();

                foreach (var location in foundLocations)
                {
                    _foundLocations.Add(GetReadableName(location));
                }
            }
        }

        private string GetReadableName(MapLocation location)
        {
            var address = location.Address;

            return $"{address.Town} {address.Street} {address.StreetNumber}";
        }

        /// <summary>
        /// </summary>
        public ICommand SuggestionChosenCommand { get; private set; }

        private async void SuggestionChosenExecute(object selectedItem)
        {
            if (selectedItem == null)
                return;

            _selectedLocation = (string)selectedItem;
            var locations = await _model.FindLocationAsync(_selectedLocation).ConfigureAwait(true);
            SetProvidedLocation(locations.First());
        }

        #endregion Commands

        #region INavigable

        public void OnNavigatedFrom(object parameter)
        {
            //MyProperty = (parameter as string) ?? string.Empty;
        }

        public void GoBack()
        {
            _navigationService.GoBack();
        }

        public void OnNavigatedTo(object parameter)
        {
            UpdateUserLocation();
        }

        #endregion INavigable
    }
}