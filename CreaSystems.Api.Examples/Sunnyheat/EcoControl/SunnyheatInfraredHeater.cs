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

#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreaSystems.Api.Examples.Sunnyheat.EcoControl
{
    /// <summary>
    /// Class with all properties for control a SUNNYHEAT infrared heater
    /// </summary>
    internal class SunnyheatInfraredHeater
    {
        #region Properties

        /// <summary>
        /// Gets or sets the mac address.
        /// </summary>
        /// <value>
        /// The mac address.
        /// </value>
        internal string MacAddress { private set; get; }

        /// <summary>
        /// Gets or sets the name of the heater.
        /// </summary>
        /// <value>
        /// The name of the heater.
        /// </value>
        internal string HeaterName { private set; get; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SunnyheatInfraredHeater"/> class.
        /// </summary>
        /// <param name="macAddress">The mac address.</param>
        /// <param name="heaterName">Name of the heater.</param>
        internal SunnyheatInfraredHeater(string macAddress,  string heaterName)
        {
            MacAddress = macAddress;
            HeaterName = heaterName;
        }

        #endregion Constructors
    }
}
