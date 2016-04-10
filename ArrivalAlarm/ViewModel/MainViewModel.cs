using ArrivalAlarm.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using Windows.Devices.Geolocation;

namespace ArrivalAlarm.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase, INavigable
    {
        private INavigationService _navigationService;

        private readonly ObservableCollection<AlarmModel> _alarmsCollection = new ObservableCollection<AlarmModel>()
        {
            new AlarmModel(new AlarmLocation("Poznan", new BasicGeoposition()))
            {
                Label = "Alarm praca",
                ActiveDays = new HashSet<DayOfWeek>() {DayOfWeek.Monday, DayOfWeek.Tuesday},
                IsActive = true,
                IsCyclic = true,
            },

            new AlarmModel(new AlarmLocation("Wroclaw", new BasicGeoposition()))
            {
                Label = "Sex",
                ActiveDays = new HashSet<DayOfWeek>() {DayOfWeek.Friday, DayOfWeek.Wednesday},
                IsActive = true,
                IsCyclic = true,
            },
        };

        public INotifyCollectionChanged AlarmsCollection => _alarmsCollection;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            _navigationService = ServiceLocator.Current.GetInstance<INavigationService>();

            CreateNavigateToSelectLocationPageCommand();
        }

        /// <summary>
        /// Returns command which navigates to selection page
        /// </summary>
        public ICommand NavigateToSelectLocationPage => _navigateToSelectLocationPage;

        private RelayCommand _navigateToSelectLocationPage;

        private void ExecuteNavigateToLocationPage()
        {
            _navigationService.NavigateTo(nameof(View.MapPage), "Graf acykliczny - mo¿e byæ reprezentowany jako drzewo.");
        }

        private void CreateNavigateToSelectLocationPageCommand()
        {
            _navigateToSelectLocationPage = new RelayCommand(ExecuteNavigateToLocationPage);
        }

        #region INavigable

        public void OnNavigatedTo(object parameter)
        {
            //Common.Logger.CreateLoggerAsync();
        }

        public void OnNavigatedFrom(object parameter)
        {
        }

        public void GoBack()
        {
            _navigationService.GoBack();
        }

        #endregion INavigable
    }
}