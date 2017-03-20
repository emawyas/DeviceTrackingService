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
        [DataMember]
        public string driverId { get; set; }

        [DataMember]
        public int maxSpeed { get; set; }

        [DataMember]
        public string deviceSerial { get; set; }

        [DataMember]
        public string firmWareVersion { get; set; }

        [DataMember]
        public double latitude { get; set; }

        [DataMember]
        public double longitude { get; set; }

        [DataMember]
        public string heading { get; set; }

        [DataMember]
        public string EW { get; set; }

        [DataMember]
        public string NS { get; set; }
    }
}