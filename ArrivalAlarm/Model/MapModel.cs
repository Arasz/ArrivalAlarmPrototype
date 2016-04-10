using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;

namespace ArrivalAlarm.Model
{
    public class MapModel
    {
        /// <summary>
        /// Object which contains all logic responsible for localization
        /// </summary>
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
        public MapModel(TimeSpan locationFetchInterval) : this()
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

        /// <summary>
        /// Gets actual position on map from GPS
        /// </summary>
        /// <returns>Actual position on map</returns>
        public async Task<Geoposition> GetActualLocationAsync()
        {
            if (_locationFetchInterval != null)
                return await _geolocator.GetGeopositionAsync(_locationFetchInterval, _getPositionTimeout);
            else
                return await _geolocator.GetGeopositionAsync();
        }

        /// <summary>
        /// Finds location on map given location name
        /// </summary>
        /// <param name="locationName">Searched location name</param>
        /// <returns>List of locations matched with given location name</returns>
        async public Task<IReadOnlyList<MapLocation>> FindLocationAsync(string locationName)
        {
            // Give as a hint actual user location because we don't have any informations
            // about searched location
            var hint = await GetActualLocationAsync();

            // Find given location
            MapLocationFinderResult mapLocationFinderResult = await MapLocationFinder.FindLocationsAsync(locationName, hint.Coordinate.Point, 3);

            var locationsList = mapLocationFinderResult.Locations;

            //TODO: Do something with this switch or simplify it
            switch (mapLocationFinderResult.Status)
            {
                case MapLocationFinderStatus.Success:
                    break;

                case MapLocationFinderStatus.BadLocation:
                    locationsList = null;
                    break;

                case MapLocationFinderStatus.IndexFailure:
                    locationsList = null;
                    break;

                case MapLocationFinderStatus.NetworkFailure:
                    locationsList = null;
                    break;

                case MapLocationFinderStatus.InvalidCredentials:
                    locationsList = null;
                    break;

                case MapLocationFinderStatus.UnknownError:
                    locationsList = null;
                    break;
            }

            return locationsList;
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