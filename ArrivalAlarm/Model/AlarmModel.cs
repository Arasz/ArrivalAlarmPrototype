using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Windows.Devices.Geolocation;
using Windows.Foundation.Metadata;

namespace ArrivalAlarm.Model
{
    /// <summary>
    /// Location alarm data model
    /// </summary>
    [DataContract]
    public class AlarmModel
    {
        /// <summary>
        /// Days in which alarm is active
        /// </summary>
        [DataMember]
        public ISet<DayOfWeek> ActiveDays { get; set; } = new HashSet<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Friday, DayOfWeek.Saturday };

        /// <summary>
        /// Alarm label
        /// </summary>
        [DataMember]
        public string Label { get; set; } = "DefaultLabel";

        /// <summary>
        /// Alarm state
        /// </summary>
        [DataMember]
        public bool IsActive { get; set; }

        /// <summary>
        /// Alarm cyclic
        /// </summary>
        [DataMember]
        public bool IsCyclic { get; set; }

        /// <summary>
        /// User is informed only with notification
        /// </summary>
        [DataMember]
        public bool OnlyNotifications { get; set; }

        /// <summary>
        /// Location where alarm will be triggered
        /// </summary>
        [DataMember]
        public AlarmLocation AlarmLocation { get; set; } = new AlarmLocation("DefaultLocation", new BasicGeoposition());

        /// <summary>
        /// Ringtone
        /// </summary>
        [DataMember, Deprecated("Will change in the future", DeprecationType.Deprecate, 1)]
        public string Ringtone { get; set; } = "defaultRingtone";

        /// <summary>
        /// Activates alarm
        /// </summary>
        public void Activate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="alarmLocation">Location in which alarm will be triggered</param>
        public AlarmModel(AlarmLocation alarmLocation)
        {
            AlarmLocation = alarmLocation;
        }

        public AlarmModel()
        {
        }

        public override string ToString()
        {
            StringBuilder textRepresentation = new StringBuilder();

            textRepresentation.AppendLine($"Label: {Label}");
            textRepresentation.AppendLine($"IsActive: {IsActive}");
            textRepresentation.AppendLine($"Location:\n{AlarmLocation}");
            textRepresentation.Append($"Active days: ");
            foreach (var activeDay in ActiveDays)
                textRepresentation.Append($"{activeDay} ");
            textRepresentation.AppendLine();
            return textRepresentation.ToString();
        }
    }
}