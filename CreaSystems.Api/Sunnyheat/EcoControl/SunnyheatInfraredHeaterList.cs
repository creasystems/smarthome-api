//-------------------------------------------------------------------------------------------------------
// <copyright file="SunnyheatInfraredHeaterList.cs" company="CREA SYSTEMS Electronic GmbH">
// Copyright (c) CREA SYSTEMS Electronic GmbH. All rights reserved.
//
// CREA SYSTEMS Electronic GmbH licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
//
// <author>CREA SYSTEMS Electronic GmbH</author>
//-------------------------------------------------------------------------------------------------------

namespace CreaSystems.Api.Sunnyheat.EcoControl
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion Namespaces

    /// <summary>
    /// This class represent a list of all available SUNNYHEAT infrared heaters and can control them.
    /// </summary>
    public class SunnyheatInfraredHeaterList
    {
        #region Variables

        /// <summary>
        /// The list of all available SUNNYHEAT infrared heaters.
        /// </summary>
        private readonly List<SunnyheatInfraredHeater> heaterList = new List<SunnyheatInfraredHeater>();

        #endregion Variables

        #region Methods

        /// <summary>
        /// Add a SUNNYHEAT infrared heater to the list.
        /// </summary>
        /// <param name="heater">The heater.</param>
        public void AddHeater(SunnyheatInfraredHeater heater)
        {
            if (heaterList.Exists(x => x.IpAddress == heater.IpAddress))
            {
                Console.WriteLine("A heater with this ip address {0} already exists in the list.", heater.IpAddress);
                return;
            }

            RefreshHeaterData(heater);

            heaterList.Add(heater);

            Console.WriteLine("Heater with ip address {0} ({1}) has been added to the list.", heater.IpAddress, heater.Name);
            Console.WriteLine(string.Empty);
        }

        /// <summary>
        /// Tries to get mac information from the SUNNYHEAT infrared heater.
        /// </summary>
        /// <param name="heater">The heater.</param>
        private async Task TryToGetMacInformationAsync(SunnyheatInfraredHeater heater)
        {
            using (var client = new HttpClient())
            {
                var content = await client.GetAsync(string.Concat("http://", heater.IpAddress, "/mesh_info"));

                string macs = content.Headers.GetValues("mesh-node-mac").ToArray()[0];

                if (content == null || macs.Length == 0)
                {
                    return;
                }

                heater.Mac = macs.Split(',')[0];

                Console.WriteLine("Heater with ip address {0} has MAC: {1}.", heater.IpAddress, heater.Mac);
            }
        }

        /// <summary>
        /// Removes a SUNNYHEAT infrared heater from the list.
        /// </summary>
        /// <param name="heater">The heater.</param>
        public void RemoveHeater(SunnyheatInfraredHeater heater)
        {
            if (!heaterList.Exists(x => x.IpAddress == heater.IpAddress))
            {
                Console.WriteLine("A heater with this ip address {0} not exists in the list.", heater.IpAddress);
                return;

            }

            heaterList.Remove(heater);

            Console.WriteLine("Heater with ip address {0} is removed from the list.", heater.IpAddress);
        }

        /// <summary>
        /// Refreshes all SUNNYHEAT infrared heater data.
        /// </summary>
        public void RefreshAllHeaterData()
        {
            foreach (SunnyheatInfraredHeater heater in heaterList)
            {
                RefreshHeaterData(heater);
            }
        }

        /// <summary>
        /// Refreshes the data from one SUNNYHEAT infrared heater.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        public void RefreshHeaterData(string ipAddress)
        {
            SunnyheatInfraredHeater heater = heaterList.Find(x => x.IpAddress == ipAddress);

            if (heater == null)
            {
                Console.WriteLine("Heater with ip address {0} not found!", heater.IpAddress);
                return;
            }

            RefreshHeaterData(heater);
        }

        /// <summary>
        /// Refreshes the data from one SUNNYHEAT infrared heater.
        /// </summary>
        /// <param name="heater">The heater.</param>
        public void RefreshHeaterData(SunnyheatInfraredHeater heater)
        {
            if (string.IsNullOrEmpty(heater.Mac))
            {
                TryToGetMacInformationAsync(heater);

                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(10);

                    if (!string.IsNullOrEmpty(heater.Mac))
                    {
                        break;
                    }
                }
            }

            Console.WriteLine("Refresh the heater data for SUNNYHEAT infrared heater {0} ({1})", heater.Mac, heater.Name);
        }

        /// <summary>
        /// Reboots all heaters.
        /// </summary>
        public void RebootAllHeaters()
        {
            foreach (SunnyheatInfraredHeater heater in heaterList)
            {
                heater.RebootHeater();
            }
        }

        #endregion Methods
    }
}
