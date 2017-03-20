using DeviceTrackingService.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DeviceTrackingService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RestService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RestService.svc or RestService.svc.cs at the Solution Explorer and start debugging.
    public class RestService : IRestService
    {
        public TrackingDevice getDevice(string deviceSerial)
        {
            TrackingDevice TD = new TrackingDevice();
            string connectionString = System.Configuration.ConfigurationManager.
                                    ConnectionStrings["DTSDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("select * from dbo.TrackingDeviceMaster where DeviceSerial = '" + deviceSerial +"'", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TD.deviceSerial = reader["DeviceSerial"].ToString();
                        TD.driverId = reader["DriverId"].ToString();
                        TD.EW = reader["EW"].ToString();
                        TD.firmWareVersion = reader["FirmWareVersion"].ToString();
                        TD.latitude = float.Parse(reader["Latitude"].ToString());
                        TD.longitude = float.Parse(reader["Longitude"].ToString());
                        TD.NS = reader["NS"].ToString();
                        TD.heading = reader["Heading"].ToString();
                    }
                }
                connection.Close();
            }
            return TD;
        }

        public TrackingDevice[] allDevices()
        {
            List<TrackingDevice> allTds = new List<TrackingDevice>();
            string connectionString = System.Configuration.ConfigurationManager.
                                    ConnectionStrings["DTSDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("select * from dbo.TrackingDeviceMaster", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    TrackingDevice TD = new TrackingDevice();
                    while (reader.Read())
                    {
                        TD.deviceSerial = reader["DeviceSerial"].ToString();
                        TD.driverId = reader["DriverId"].ToString();
                        TD.EW = reader["EW"].ToString();
                        TD.firmWareVersion = reader["FirmWareVersion"].ToString();
                        TD.latitude = float.Parse(reader["Latitude"].ToString());
                        TD.longitude = float.Parse(reader["Longitude"].ToString());
                        TD.NS = reader["NS"].ToString();
                        TD.heading = reader["Heading"].ToString();
                        allTds.Add(TD);
                    }
                }
                connection.Close();
            }
            return allTds.ToArray();
        }
    }
}
