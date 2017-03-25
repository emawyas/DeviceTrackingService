using DeviceTrackingService.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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
                        //TD.GpsDateTime = Convert.ToDateTime(reader["UpdateTime"].ToString());
                        allTds.Add(TD);
                    }
                }
                connection.Close();
            }
            return allTds.ToArray();
        }

        public int addDevice(TrackingDevice device)
        {
            Debug.WriteLine("Received POST request");
            string connectionString = System.Configuration.ConfigurationManager.
                                    ConnectionStrings["DTSDB-Home"].ConnectionString;
            string values = "(@DriverId, @MaxSpeed, @DeviceSerial, @FirmWareVersion,@Speed, @Latitude, @longitude,@EW,@NS,@Heading,@UpdateTime,@CompleteRoute,@StartCoords,@EndCoords)";
            int result = 0;

            //insert in the DB
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("insert into dbo.DeviceMaster values " + values, connection))
            {
                connection.Open();
                //Check if the device exists
                int recordCout = (int)new SqlCommand("SELECT COUNT(*) from dbo.DeviceMaster where DeviceSerial = '" +
                                                    device.deviceSerial+"'", connection).ExecuteScalar();
                if (recordCout > 0)
                {
                    connection.Close();
                    return result;
                }
                Debug.WriteLine("RestService: Posting to DB");
                //command.Parameters.AddWithValue("@Id", getCurrId(connectionString)+1);
                command.Parameters.AddWithValue("@DriverId", device.driverId);
                command.Parameters.AddWithValue("@MaxSpeed", device.maxSpeed);
                command.Parameters.AddWithValue("@DeviceSerial", device.deviceSerial);
                command.Parameters.AddWithValue("@FirmWareVersion", device.firmWareVersion);
                command.Parameters.AddWithValue("@Speed", device.Speed);
                command.Parameters.AddWithValue("@Latitude", (float)device.latitude);
                command.Parameters.AddWithValue("@Longitude", (float)device.longitude);
                command.Parameters.AddWithValue("@EW", device.EW);
                command.Parameters.AddWithValue("@NS", device.NS);
                command.Parameters.AddWithValue("@Heading", device.heading);
                command.Parameters.AddWithValue("@UpdateTime", Convert.ToDateTime(device.GpsDateTime));
                command.Parameters.AddWithValue("@CompleteRoute", device.CompleteRoute);
                command.Parameters.AddWithValue("@StartCoords", device.Source);
                command.Parameters.AddWithValue("@EndCoords", device.Destination);
                result = command.ExecuteNonQuery();
                Debug.WriteLine("Result = " + result);
                connection.Close();
            }
            return result;
        }

        public int updateDevice(TrackingDevice device)
        {
            Debug.WriteLine("RestService: Received PUT request " + device.Speed);
            string connectionString = System.Configuration.ConfigurationManager.
                        ConnectionStrings["DTSDB-Home"].ConnectionString;
            string values = "Speed = @Speed, Latitude = @Latitude, Longitude =  @longitude, EW = @EW, NS = @NS, Heading = @Heading, UpdateTime = @UpdateTime," + 
                            "CompleteRoute = @CompleteRoute, StartCoords = @StartCoords, EndCoords = @EndCoords";
            int result = 0;

            //update the DB
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command =
                            new SqlCommand("update dbo.DeviceMaster SET " + values + " where DeviceSerial = '" 
                            + device.deviceSerial+"'", connection))
            {
                connection.Open();

                //Check if the device exists
                int recordCout = (int)new SqlCommand("SELECT COUNT(*) from dbo.DeviceMaster where DeviceSerial = '" +
                                                    device.deviceSerial + "'", connection).ExecuteScalar();

                if (recordCout == 0)
                {
                    connection.Close();
                    return 0;
                }

                Debug.WriteLine("RestService: updating DB");
                command.Parameters.AddWithValue("@Speed", device.Speed);
                command.Parameters.AddWithValue("@Latitude", (float)device.latitude);
                command.Parameters.AddWithValue("@Longitude", (float)device.longitude);
                command.Parameters.AddWithValue("@EW", device.EW);
                command.Parameters.AddWithValue("@NS", device.NS);
                command.Parameters.AddWithValue("@Heading", device.heading);
                command.Parameters.AddWithValue("@UpdateTime", Convert.ToDateTime(device.GpsDateTime));
                command.Parameters.AddWithValue("@CompleteRoute", device.CompleteRoute);
                command.Parameters.AddWithValue("@StartCoords", device.Source);
                command.Parameters.AddWithValue("@EndCoords", device.Destination);
                result = command.ExecuteNonQuery();
                connection.Close();
            }
            return result;
        }

    }
}
