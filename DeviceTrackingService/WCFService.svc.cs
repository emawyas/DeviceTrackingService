using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DeviceTrackingService.Models;
using System.Data.SqlClient;

namespace DeviceTrackingService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WCFService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WCFService.svc or WCFService.svc.cs at the Solution Explorer and start debugging.
    public class WCFService : IWCFService
    {

        public List<TrackingDevice> getAllTrackingDevices()
        {
            List<TrackingDevice> allTds = new List<TrackingDevice>();
            string connectionString = System.Configuration.ConfigurationManager.
                                    ConnectionStrings["DTSDB-Home"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("select * from dbo.DeviceMaster", connection))
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
                        TD.GpsDateTime = reader["UpdateTime"].ToString();
                        TD.CompleteRoute = reader["CompleteRoute"].ToString();
                        TD.Source = reader["StartCoords"].ToString();
                        TD.Destination = reader["EndCoords"].ToString();
                        allTds.Add(TD);
                    }
                }
                connection.Close();
            }
            return allTds;
        }

        public TrackingDevice getTrackingDevice(string serial)
        {
            TrackingDevice TD = new TrackingDevice();
            string connectionString = System.Configuration.ConfigurationManager.
                                    ConnectionStrings["DTSDB-Home"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("select * from dbo.DeviceMaster where DeviceSerial = '" + serial + "'", connection))
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
                        TD.GpsDateTime = reader["UpdateTime"].ToString();
                        TD.CompleteRoute = reader["CompleteRoute"].ToString();
                        TD.Source = reader["StartCoords"].ToString();
                        TD.Destination = reader["EndCoords"].ToString();
                    }
                }
                connection.Close();
            }
            return TD;
        }
    }
}
