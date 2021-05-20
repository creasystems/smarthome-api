//-------------------------------------------------------------------------------------------------------
// <copyright file="SmartHomeInterface.cs" company="CREA SYSTEMS Electronic GmbH">
// Copyright (c) CREA SYSTEMS Electronic GmbH. All rights reserved.
//
// CREA SYSTEMS Electronic GmbH licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
//
// <author>CREA SYSTEMS Electronic GmbH</author>
//-------------------------------------------------------------------------------------------------------

namespace CreaSystems.Api.SmartHome
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    #endregion Namespaces

    /// <summary>
    /// All methods for the communication with the esp32 mesh network
    /// </summary>
    public class SmartHomeInterface
    {
        #region Variables

        /// <summary>
        /// The singleton object
        /// </summary>
        private static SmartHomeInterface singleton;

        #endregion Variables

        #region Properties

        /// <summary>
        /// Gets or sets the ESP32 mesh ip address.
        /// </summary>
        /// <value>
        /// The ESP32 mesh ip address.
        /// </value>
        internal IPAddress ESP32MeshIpAddress { set; get; }

        /// <summary>
        /// Gets or sets the es P32 mesh node number.
        /// </summary>
        /// <value>
        /// The es P32 mesh node number.
        /// </value>
        private int ESP32MeshNodeNum { set; get; }

        /// <summary>
        /// Gets or sets the ESP32 mesh node macs.
        /// </summary>
        /// <value>
        /// The es P32 mesh node macs.
        /// </value>
        private string ESP32MeshNodeMacs { set; get; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="SmartHomeInterface"/> class from being created.
        /// </summary>
        private SmartHomeInterface()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>SmartHomeInterface</returns>
        internal static SmartHomeInterface GetInstance()
        {
            if (singleton == null)
            {
                singleton = new SmartHomeInterface();
            }

            return singleton;
        }

        /// <summary>
        /// Refresh the smart home network devices asynchronous.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        internal async Task RefreshESP32NetworkDevicesAsync(IPAddress ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress.ToString()))
            {
                return;
            }

            HttpClient client = new HttpClient();
            client.Timeout = new TimeSpan(0, 0, 1);

            try
            {
                var content = await client.GetAsync(string.Concat("http://", ipAddress.ToString(), "/mesh_info"));

                ESP32MeshNodeNum = Convert.ToInt32(content.Headers.GetValues("mesh-node-num").ToArray()[0]);
                ESP32MeshNodeMacs = content.Headers.GetValues("mesh-node-mac").ToArray()[0];
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Gets the web request object for the ESP32.
        /// </summary>
        /// <param name="specificMac">The specific mac.</param>
        /// <returns>
        /// The http web request object for communicate with the ESP32
        /// </returns>
        private HttpWebRequest GetWebRequestObjectForESP32(string specificMac)
        {
            if (string.IsNullOrEmpty(ESP32MeshIpAddress.ToString()))
            {
                return null;
            }

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Concat("http://", ESP32MeshIpAddress, "/device_request"));
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 2000;

            if (string.IsNullOrEmpty(specificMac))
            {
                httpWebRequest.Headers.Add("Mesh-Node-Num", ESP32MeshNodeNum.ToString());
                httpWebRequest.Headers.Add("Mesh-Node-Mac", ESP32MeshNodeMacs);
            }
            else
            {
                httpWebRequest.Headers.Add("Mesh-Node-Num", "1");
                httpWebRequest.Headers.Add("Mesh-Node-Mac", specificMac);
            }

            return httpWebRequest;
        }

        /// <summary>
        /// Gets the ESP32 device information.
        /// </summary>
        /// <param name="specificMac">The specific mac.</param>
      /*  internal List<IESP32MeshDevice> GetESP32DeviceInformation(string specificMac)
        {
            HttpWebRequest webRequest = GetWebRequestObjectForESP32(specificMac);

            if (webRequest == null)
            {
                return null;
            }

            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                string json = "{\"request\":\"get_device_info\"}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)webRequest.GetResponse();
            var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();
            string[] resultValues = result.Split("HTTP/1.1 200 OK");
            SmartHomeDebugWindow.Instance.Add(result);

            List<IESP32MeshDevice> resultDevices = new List<IESP32MeshDevice>();

            foreach (string r in resultValues)
            {
                if (string.IsNullOrEmpty(r))
                {
                    continue;
                }

                string oneDevice = r.Substring(r.IndexOf("{"));
                string[] deviceValues = oneDevice.Replace("{", string.Empty).Replace("}", string.Empty).Split(",");
                EnumESP32MeshDeviceType DeviceType = (EnumESP32MeshDeviceType)Enum.Parse(typeof(EnumESP32MeshDeviceType), deviceValues[0].Split(":")[1].Replace("\"", string.Empty));
                IESP32MeshDevice d = null;
                switch (DeviceType)
                {
                    case EnumESP32MeshDeviceType.SunnyheatDevice:
                        d = new SmartHomeSunnyheatDevice(deviceValues[2].Split(":")[1].Replace("\"", string.Empty), deviceValues[1].Split(":")[1].Replace("\"", string.Empty));
                        break;
                    case EnumESP32MeshDeviceType.SmartHomeConnector:
                        d = new SmartHomeConnectorDevice(deviceValues[2].Split(":")[1].Replace("\"", string.Empty), deviceValues[1].Split(":")[1].Replace("\"", string.Empty));
                        break;
                    case EnumESP32MeshDeviceType.SmartHomeWeatherstation:
                        break;
                    case EnumESP32MeshDeviceType.SmartHomeTimer:
                        break;
                    case EnumESP32MeshDeviceType.SmartHomeTransmitter:
                        break;
                }


                ((ESP32MeshDevice)d).SoftwareVersion = deviceValues[5].Split(":")[1].Replace("\"", string.Empty);
                ((ESP32MeshDevice)d).DeviceType = (EnumESP32MeshDeviceType)Enum.Parse(typeof(EnumESP32MeshDeviceType), deviceValues[0].Split(":")[1].Replace("\"", string.Empty));
                ((ESP32MeshDevice)d).HardwareVersion = deviceValues[25].Split(":")[1].Replace("\"", string.Empty);
                ((ESP32MeshDevice)d).AllDeviceValues = deviceValues;
                ((ESP32MeshDevice)d).SetCharacteristicObjects(oneDevice);


                resultDevices.Add(d);
            }

            return resultDevices;
        }

        /// <summary>
        /// Reboots the ESP32 network, if the param is empty.
        /// Reboots the RSP32 of the param address
        /// </summary>
        internal void RebootESP32Network(string specificMac)
        {
            HttpWebRequest webRequest = GetWebRequestObjectForESP32(specificMac);

            if (webRequest == null)
            {
                return;
            }

            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                string json = "{\"request\":\"reboot\"," +
                              "\"delay\":50}";

                streamWriter.Write(json);
            }

            _ = (HttpWebResponse)webRequest.GetResponse();
        }

        /// <summary>
        /// Upgrade the ESP32 network.
        /// </summary>
        internal void UpgradeESP32Network()
        {
            HttpWebRequest webRequest = GetWebRequestObjectForESP32(string.Empty);

            if (webRequest == null)
            {
                return;
            }

            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                string json = "{\"request\":\"start_upgrade\"}";

                streamWriter.Write(json);
            }

            try
            {
                _ = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Resets the ESP32 device to first initialize.
        /// </summary>
        /// <param name="specificMac">The specific mac.</param>
        internal void ResetESP32DeviceToFirstInit(string specificMac)
        {
            if (string.IsNullOrEmpty(specificMac))
            {
                return;
            }

            HttpWebRequest webRequest = GetWebRequestObjectForESP32(specificMac);

            if (webRequest == null)
            {
                return;
            }

            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                string json = "{\"request\":\"factory_settings\"}, {\"firstInit\":1}";

                streamWriter.Write(json);
            }

            try
            {
                _ = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Upgrade the ESP32 network.
        /// </summary>
        internal void ResetESP32DeviceToFactorySettings(string specificMac)
        {
            if (string.IsNullOrEmpty(specificMac))
            {
                return;
            }

            HttpWebRequest webRequest = GetWebRequestObjectForESP32(specificMac);

            if (webRequest == null)
            {
                return;
            }

            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                string json = "{\"request\":\"factory_settings\"}, {\"firstInit\":0}";

                streamWriter.Write(json);
            }

            try
            {
                _ = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Gets the ESP32 network configuration.
        /// </summary>
        internal void GetESP32NetworkConfiguration()
        {
            HttpWebRequest webRequest = GetWebRequestObjectForESP32(ESP32MeshNodeMacs.Split(",")[0]);

            if (webRequest == null)
            {
                return;
            }

            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                string json = "{\"request\":\"get_mesh_config\"}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)webRequest.GetResponse();
            var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();
            string[] resultValues = result.Replace("{", string.Empty).Replace("}", string.Empty).Split(",");
            SmartHomeDebugWindow.Instance.Add(result);

            ESP32MeshId = resultValues[0].Split(":")[1].Replace("\"", string.Empty);
            ESP32SignalStrength = 2 * (resultValues[7].Split(":")[1].Replace("\"", string.Empty).ToInt32() + 100);
            ESP32MaxLayer = resultValues[1].Split(":")[1].Replace("\"", string.Empty).ToInt32();
            ESP32MaxExternalConnections = resultValues[2].Split(":")[1].Replace("\"", string.Empty).ToInt32();
            ESP32FreeHeap = Convert.ToInt64(resultValues[11].Split(":")[1].Replace("\"", string.Empty));
            ESP32RunningTime = Convert.ToInt64(resultValues[12].Split(":")[1].Replace("\"", string.Empty));
        }

        /// <summary>
        /// Save new Characteristics in Device
        /// </summary>
        /// <param name="specificMac"></param>
        /// <param name="jsonCommand"></param>
        internal void SetESP32DeviceInformation(string specificMac, string jsonCommand)
        {
            HttpWebRequest webRequest = GetWebRequestObjectForESP32(specificMac);

            if (webRequest == null)
            {
                return;
            }

            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonCommand);
            }

            var httpResponse = (HttpWebResponse)webRequest.GetResponse();
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();
            string[] resultValues = result.Split("HTTP/1.1 200 OK");
            SmartHomeDebugWindow.Instance.Add(result);

            if (result == "{\"status_msg\":\"MDF_OK\",\"status_code\":0}")
            {
                MsgBox.GetInstanceWitoutDB().ShowInformation("Daten wurden übertragen");
            }
            else
            {
                MsgBox.GetInstanceWitoutDB().ShowError("Fehler beim Daten Übetragen");
            }
        }*/

        #endregion Methods
    }
}