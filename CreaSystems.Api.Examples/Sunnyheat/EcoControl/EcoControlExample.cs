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
            //heaterList.AddHeater(new SunnyheatInfraredHeater("192.168.20.128", "Living room"));
            //heaterList.AddHeater(new SunnyheatInfraredHeater("192.168.20.131", "Kitchen"));
            heaterList.AddHeater(new SunnyheatInfraredHeater("192.168.10.112", "IT"));

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

            Console.WriteLine(string.Empty);
            Console.WriteLine("Please press 1, if you want to reboot all heaters:");
            Console.Write("Input: ");

            if (Console.ReadKey().Key == ConsoleKey.D1)
            {
                Console.WriteLine(string.Empty);

                //Reboot all SUNNYHEAT infrared heaters
                heaterList.RebootAllHeaters();
            }
            else
            {
                Console.WriteLine("No reboot.");
            }
        }

        #endregion Methods
    }
}
