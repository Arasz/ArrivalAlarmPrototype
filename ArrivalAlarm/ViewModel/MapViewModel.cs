using ArrivalAlarm.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace ArrivalAlarm.ViewModel
{
    public class MapViewModel: ViewModelBase, INavigable
    {
        private INavigationService _navigationService;

        private MapModel _model;

        private Geopoint _userPosition;
        public Geopoint UserPosition
        {
            get { return _userPosition; }
            private set { Set(nameof(UserPosition), ref _userPosition, value); }
        }
        

        private double _zoomLevel;                    
        public double ZoomLevel
        {
            get { return _zoomLevel; }
            set { Set(nameof(ZoomLevel), ref _zoomLevel, value); }
        }


        public MapViewModel()
        {
            _model = new MapModel(TimeSpan.FromMinutes(2));
            _navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
        }


        #region INavigable 
        public void OnNavigatedFrom(object parameter)
        {
            //MyProperty = (parameter as string) ?? string.Empty;
        }

        public void GoBack()
        {
            _navigationService.GoBack();
        }

        async public void OnNavigatedTo(object parameter)
        {
            var actualLocation = await _model.GetActualLocationAsync();
            UserPosition = actualLocation.Coordinate.Point;
            ZoomLevel = 12;
        }
        #endregion
    }
}
