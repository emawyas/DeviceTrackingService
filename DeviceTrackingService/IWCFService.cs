using DeviceTrackingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DeviceTrackingService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWCFService" in both code and config file together.
    [ServiceContract]
    public interface IWCFService
    {
        [OperationContract]
        List<TrackingDevice> getAllTrackingDevices();

        [OperationContract]
        TrackingDevice getTrackingDevice(string serial);
    }
}
