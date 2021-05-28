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

namespace CreaSystems.Api.Examples.Sunnyheat.EcoControl
{
    #region Namespaces

    using CreaSystems.Api.Sunnyheat.EcoControl;
    using System;

    #endregion Namespaces

    /// <summary>
    /// Example program to smart control the SUNNYHEAT infrared heaters.
    /// </summary>
    internal class EcoControlExample
    {
        #region Variables

        /// <summary>
        /// The heater list
        /// </summary>
        private SunnyheatInfraredHeaterList heaterList;

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
            heaterList = new SunnyheatInfraredHeaterList();

            //Add all SUNNYHEAT infrared heaters you need or want
            //heaterList.AddHeater(new SunnyheatInfraredHeater("192.168.20.127", "Living room"));
            //heaterList.AddHeater(new SunnyheatInfraredHeater("192.168.20.131", "Kitchen"));
            //heaterList.AddHeater(new SunnyheatInfraredHeater("192.168.10.112", "IT"));
            heaterList.AddHeater(new SunnyheatInfraredHeater("192.168.10.101", "IT"));

            if (heaterList.Heaters.Count == 0)
            {
                Console.WriteLine("No heaters connected!");
                return;
            }

            //Get all informations for the SUNNYHEAT infrared heaters
            foreach (SunnyheatInfraredHeater heater in heaterList.Heaters)
            {
                Console.WriteLine("SUNNYHEAT infrared heater mac: {0}", heater.Mac);
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("IP Address: {0}", heater.IpAddress);
                Console.WriteLine("Name: {0}", heater.Name);
                Console.WriteLine("Has light: {0}", heater.LightFunctionIsActivated.ToString());
                Console.WriteLine("Light state: {0}", heater.Light.ToString());
                Console.WriteLine("Setpoint temperature: {0}", heater.SetpointTemperature);
                Console.WriteLine("Room temperature: {0}", heater.RoomTemperature);
                Console.WriteLine(string.Empty);
            }

            ConsoleKey key = ConsoleKey.Escape;

            while (key != ConsoleKey.Q)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("Please choose the next action:");
                Console.WriteLine("1 - Reboot all heaters");
                Console.WriteLine("2 - Check for new version and start upgrade");
                Console.WriteLine("3 - Set new setpoint temperature");
                Console.WriteLine("4 - Set new light state");
                Console.WriteLine("5 - Get the current light state");
                Console.WriteLine("6 - Get the current room temperature");
                Console.WriteLine("7 - Get the current setpoint temperature");
                Console.WriteLine("Q - Quite example application");
                Console.WriteLine(string.Empty);
                Console.Write("Choose: ");

                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.Q:
                        break;
                    case ConsoleKey.D1:
                        //Reboot all devices
                        Console.WriteLine(string.Empty);

                        //Reboot all SUNNYHEAT infrared heaters
                        heaterList.RebootAllHeaters();
                        break;
                    case ConsoleKey.D2:
                        //Check for a new version and start upgrade
                        heaterList.CheckForNewUpgradeAndStart();
                        break;
                    case ConsoleKey.D3:
                        //Set the setpoint temperature
                        Console.WriteLine("Please enter the new setpoint temperature.");
                        Console.WriteLine("Valid temperatures are between 0 and 45 C° in 0.5 steps.");
                        string input = Console.ReadLine();
                        double newTemp;

                        if (double.TryParse(input, out newTemp))
                        {
                            heaterList.Heaters[0].SetpointTemperature = newTemp;

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("The temperature was set to {0} C°.", input);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Input {0} is not valid!", input);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        break;
                    case ConsoleKey.D4:
                        //Set the light state
                        Console.WriteLine(string.Empty);

                        if (!heaterList.Heaters[0].LightFunctionIsActivated)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The light function is not activated on the ECOcontrol thermostat, so it is not possible to switch the light.");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            continue;
                        }

                        Console.WriteLine("Please enter the new light state (0 = off, 1 = on).");

                        ConsoleKey newLightState = Console.ReadKey().Key;

                        if (newLightState == ConsoleKey.D0)
                        {
                            heaterList.Heaters[0].Light = EnumLightState.Off;
                        }
                        else if (newLightState == ConsoleKey.D1)
                        {
                            heaterList.Heaters[0].Light = EnumLightState.On;
                        }
                        break;
                    case ConsoleKey.D5:
                        //Get the light state
                        Console.WriteLine(string.Empty);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("The light function is {0} - The current light state is: {1}", heaterList.Heaters[0].LightFunctionIsActivated ? "activated" : "deactivated", heaterList.Heaters[0].Light == EnumLightState.On ? "on" : "off");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case ConsoleKey.D6:
                        //Get the room temperature
                        Console.WriteLine(string.Empty);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("The current room temperature is {0} C°.", heaterList.Heaters[0].RoomTemperature);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case ConsoleKey.D7:
                        //Get the setpoint temperature
                        Console.WriteLine(string.Empty);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("The current setpoint temperature is {0} C°.", heaterList.Heaters[0].SetpointTemperature);
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
