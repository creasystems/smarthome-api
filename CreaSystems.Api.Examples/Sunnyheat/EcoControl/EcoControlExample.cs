//-------------------------------------------------------------------------------------------------------
// <copyright file="EcoControlExample.cs" company="CREA SYSTEMS Electronic GmbH">
// Copyright (c) CREA SYSTEMS Electronic GmbH. All rights reserved.
//
// CREA SYSTEMS Electronic GmbH licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
//
// <author>CREA SYSTEMS Electronic GmbH</author>
//-------------------------------------------------------------------------------------------------------

#region Namespaces

using System;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text;

#endregion Namespaces

namespace CreaSystems.Api.Examples.Sunnyheat.EcoControl
{
    /// <summary>
    /// Example program to smart control the SUNNYHEAT infrared heaters.
    /// </summary>
    internal class EcoControlExample
    {
        #region Variables

        /// <summary>
        /// The heater list
        /// </summary>
        List<SunnyheatInfraredHeater> heaterList;

        #endregion Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EcoControlExample"/> class.
        /// </summary>
        internal EcoControlExample()
        {
            Console.WriteLine("#################################");
            Console.WriteLine("# CREA SYSTEMS Electronic GmbH: #");
            Console.WriteLine("#################################");
            Console.WriteLine(string.Empty);
            Console.WriteLine("Start ECOcontrol example...");
            Console.WriteLine(string.Empty);
            Console.WriteLine("Initialize the example heaters...");

            InitializeHeaters();

            Console.ReadLine();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Initializes the heaters.
        /// </summary>
        private void InitializeHeaters()
        {
            //Create a new list of SUNNYHEAT infrared heaters
            //and add all SUNNYHEAT infrared heaters you need or want with their mac address.
            //You will find the mac address of each SUNNYHEAT infrared heater in the SUNNYHEAT APP.
            heaterList = new List<SunnyheatInfraredHeater>
            {
                new SunnyheatInfraredHeater("03af0f3c09af", "Living room"),
                new SunnyheatInfraredHeater("04bf0f5c09af", "Kitchen")
            };

            if (heaterList.Count == 0)
            {
                Console.WriteLine("No heaters initialized!");
                return;
            }

            //Get all informations for the SUNNYHEAT infrared heaters
            foreach (SunnyheatInfraredHeater heater in heaterList)
            {
                Console.WriteLine("SUNNYHEAT infrared heater mac: {0}", heater.MacAddress);
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("Name: {0}", heater.HeaterName);
                Console.WriteLine(string.Empty);
            }

            ConsoleKey key = ConsoleKey.Escape;

            while (key != ConsoleKey.Q)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("Please choose the next action:");
                Console.WriteLine("1 - Set new target temperature");
                Console.WriteLine("2 - Set new light state");
                Console.WriteLine("3 - Get the current light state");
                Console.WriteLine("4 - Get the current room temperature");
                Console.WriteLine("5 - Get the current target temperature");
                Console.WriteLine("Q - Quite example application");
                Console.WriteLine(string.Empty);
                Console.Write("Choose: ");

                key = Console.ReadKey().Key;

                Console.WriteLine(string.Empty);
                Console.WriteLine("Please enter the heater mac address.");
                string macInput = Console.ReadLine();

                switch (key)
                {
                    case ConsoleKey.Q:
                        break;
                    case ConsoleKey.D1:
                        //Set the setpoint temperature
                        Console.WriteLine("Please enter the new setpoint temperature.");
                        Console.WriteLine("Valid temperatures are between 0 and 45 C° in 0.5 steps.");
                        string input1 = Console.ReadLine();
                        double value1;

                        if (double.TryParse(input1, out value1))
                        {
                            HttpClient client1 = new()
                            {
                                BaseAddress = new Uri("http://localhost:8000/setTargetTemperature?id=" + macInput + "&value=" + value1)
                            };

                            HttpResponseMessage response1 = client1.GetAsync(string.Empty).Result;

                            if (response1.IsSuccessStatusCode)
                            {
                                Console.ForegroundColor = response1.Content.ReadAsStringAsync().Result.ToLower().Contains("error") ? ConsoleColor.Red : ConsoleColor.Green;
                                Console.WriteLine(response1.Content.ReadAsStringAsync().Result);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Error during set target temperature process.");
                            }

                            // Dispose once all HttpClient calls are complete.
                            client1.Dispose();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Input {0} is not valid!", input1);
                        }

                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case ConsoleKey.D2:
                        //Set the setpoint temperature
                        Console.WriteLine("Please enter the new light state.");
                        Console.WriteLine("Valid light state values are 0 (off) and 1 (on).");
                        string input2 = Console.ReadLine();
                        int value2;

                        if (int.TryParse(input2, out value2))
                        {
                            HttpClient client2 = new()
                            {
                                BaseAddress = new Uri("http://localhost:8000/setLightState?id=" + macInput + "&value=" + value2)
                            };

                            HttpResponseMessage response2 = client2.GetAsync(string.Empty).Result;

                            if (response2.IsSuccessStatusCode)
                            {
                                Console.ForegroundColor = response2.Content.ReadAsStringAsync().Result.ToLower().Contains("error") ? ConsoleColor.Red : ConsoleColor.Green;
                                Console.WriteLine(response2.Content.ReadAsStringAsync().Result);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Error during set light state process.");
                            }

                            // Dispose once all HttpClient calls are complete.
                            client2.Dispose();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Input {0} is not valid!", input2);
                        }

                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case ConsoleKey.D3:
                        HttpClient client3 = new()
                        {
                            BaseAddress = new Uri("http://localhost:8000/getCurrentLightState?id=" + macInput)
                        };

                        HttpResponseMessage response3 = client3.GetAsync(string.Empty).Result;

                        if (response3.IsSuccessStatusCode)
                        {
                            Console.ForegroundColor = response3.Content.ReadAsStringAsync().Result.ToLower().Contains("error") ? ConsoleColor.Red : ConsoleColor.Green;
                            Console.WriteLine(response3.Content.ReadAsStringAsync().Result);
                        }
                        else
                        {
                            Console.WriteLine("Error during get current light state process.");
                        }

                        // Dispose once all HttpClient calls are complete.
                        client3.Dispose();

                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case ConsoleKey.D4:
                        HttpClient client4 = new()
                        {
                            BaseAddress = new Uri("http://localhost:8000/getCurrentTemperatur?id=" + macInput)
                        };

                        HttpResponseMessage response4 = client4.GetAsync(string.Empty).Result;

                        if (response4.IsSuccessStatusCode)
                        {
                            Console.ForegroundColor = response4.Content.ReadAsStringAsync().Result.ToLower().Contains("error") ? ConsoleColor.Red : ConsoleColor.Green;
                            Console.WriteLine(response4.Content.ReadAsStringAsync().Result);
                        }
                        else
                        {
                            Console.WriteLine("Error during get current room temperature process.");
                        }

                        // Dispose once all HttpClient calls are complete.
                        client4.Dispose();

                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case ConsoleKey.D5:
                        HttpClient client5 = new()
                        {
                            BaseAddress = new Uri("http://localhost:8000/getCurrentTargetTemperatur?id=" + macInput)
                        };

                        HttpResponseMessage response5 = client5.GetAsync(string.Empty).Result;

                        if (response5.IsSuccessStatusCode)
                        {
                            Console.ForegroundColor = response5.Content.ReadAsStringAsync().Result.ToLower().Contains("error") ? ConsoleColor.Red : ConsoleColor.Green;
                            Console.WriteLine(response5.Content.ReadAsStringAsync().Result);
                        }
                        else
                        {
                            Console.WriteLine("Error during get current target temperature process.");
                        }

                        // Dispose once all HttpClient calls are complete.
                        client5.Dispose();

                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion Methods
    }
}
