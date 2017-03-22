using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DeviceTrackingService.Models
{
    [DataContract(Namespace = "")]
    public class TrackingDevice
    {
        [DataMember (Name = "DriverId")]
        public string driverId { get; set; }

        [DataMember(Name = "MaxSpeed")]
        public int maxSpeed { get; set; }

        [DataMember(Name = "DeviceSerial")]
        public string deviceSerial { get; set; }

        [DataMember(Name = "FirmWareVersion")]
        public string firmWareVersion { get; set; }

        [DataMember(Name = "latitude")]
        public double latitude { get; set; }

        [DataMember(Name = "longitude")]
        public double longitude { get; set; }

        [DataMember(Name = "heading")]
        public string heading { get; set; }

        [DataMember(Name ="EW")]
        public string EW { get; set; }

        [DataMember(Name = "NS")]
        public string NS { get; set; }

        [DataMember(Name = "GpsDateTime")]
        public string GpsDateTime { get; set; }

        [DataMember(Name = "Speed")]
        public int Speed { get; set; }
    }
}