using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace ArrivalAlarm.Model
{
    public class MapModel
    { 
        private Geolocator _geolocator;

        /// <summary>
        /// Distance after which position changed event will be raised
        /// </summary>
        private double _movementThreshold = 500;

        /// <summary>
        /// Minimum time interval between location updates in milliseconds
        /// </summary>
        private uint _reportInterval = 1000;

        private TimeSpan _getPositionTimeout = TimeSpan.FromSeconds(10);

        public MapModel()
        {
            _geolocator = new Geolocator()
            {
                DesiredAccuracy = PositionAccuracy.Default,
                MovementThreshold = _movementThreshold,
                ReportInterval = _reportInterval,
            };

            _geolocator.PositionChanged += PositionChangedHandler;
            _geolocator.StatusChanged += StatusChangedHandler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locationFetchInterval">Time after which location data will be discarded</param>
        public MapModel(TimeSpan locationFetchInterval):this()
        {
            _locationFetchInterval = locationFetchInterval;
        }

        private TimeSpan _locationFetchInterval;
        /// <summary>
        /// Time after which location data will be discarded
        /// </summary>
        public TimeSpan LocationFetchInterval
        {
            get { return _locationFetchInterval; }
            private set { _locationFetchInterval = value; }
        }

        public async Task<Geoposition> GetActualLocationAsync()
        {
            if (_locationFetchInterval != null)
                return await _geolocator.GetGeopositionAsync(_locationFetchInterval, _getPositionTimeout);
            else
                return await _geolocator.GetGeopositionAsync();
        }


        private void StatusChangedHandler(Geolocator sender, StatusChangedEventArgs args)
        {
            //await Common.Logger.WriteLogAsync($"Geolocator status changed from {sender.LocationStatus} to {args.Status}");
        }

        private void PositionChangedHandler(Geolocator sender, PositionChangedEventArgs args)
        {
 
        }
    }
}
