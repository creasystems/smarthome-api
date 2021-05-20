//-------------------------------------------------------------------------------------------------------
// <copyright file="SunnyheatInfraredHeater.cs" company="CREA SYSTEMS Electronic GmbH">
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
    using System.Net;

    #endregion Namespaces

    /// <summary>
    /// This class represent the data from one SUNNYHEAT infrared heater.
    /// </summary>
    /// <seealso cref="IEquatable{CreaSystems.Sunnyheat.Api.SunnyheatInfraredHeater}" />
    public class SunnyheatInfraredHeater : IEquatable<SunnyheatInfraredHeater>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the ip address of the SUNNYHEAT infrared heater.
        /// </summary>
        /// <value>
        /// The ip address.
        /// </value>
        public string IpAddress { private set; get; }

        /// <summary>
        /// Gets or sets the name of the SUNNYHEAT infrared heater.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { set; get; }

        /// <summary>
        /// Gets the room temperature of the SUNNYHEAT infrared heater.
        /// </summary>
        /// <value>
        /// The room temperature.
        /// </value>
        public double RoomTemperature { internal set; get; }

        /// <summary>
        /// Gets the setpoint temperature of the SUNNYHEAT infrared heater.
        /// </summary>
        /// <value>
        /// The setpoint temperature.
        /// </value>
        public double SetpointTemperature { internal set; get; }

        /// <summary>
        /// Gets the light state of the SUNNYHEAT infrared heater.
        /// </summary>
        /// <value>
        /// The light state.
        /// </value>
        public EnumLightState Light { internal set; get; }

        /// <summary>
        /// Gets the mac of the SUNNYHEAT infrared heater.
        /// </summary>
        /// <value>
        /// The mac.
        /// </value>
        public string Mac { internal set; get; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SunnyheatInfraredHeater"/> class.
        /// </summary>
        /// <param name="ipAddress">The ip address of the SUNNYHEAT infrared heater.</param>
        public SunnyheatInfraredHeater(string ipAddress) : this(ipAddress, ipAddress)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SunnyheatInfraredHeater"/> class.
        /// </summary>
        /// <param name="ipAddress">The ip address of the SUNNYHEAT infrared heater.</param>
        /// <param name="name">The name of the SUNNYHEAT infrared heater.</param>
        public SunnyheatInfraredHeater(string ipAddress, string name)
        {
            IpAddress = ipAddress;
            Name = name;
            RoomTemperature = 0.0;
            SetpointTemperature = 0.00;
            Mac = string.Empty;
            Light = EnumLightState.Off;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Concat("IPAddress: ", IpAddress, ", Name: ", Name);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return IPAddress.Parse(IpAddress).GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }


            if (!(obj is SunnyheatInfraredHeater objAsHeater))
            {
                return false;
            }
            else
            {
                return Equals(objAsHeater);
            }
        }

        /// <summary>
        /// Check, if the current object has the same type like another object.
        /// </summary>
        /// <param name="other">The object, that should checked with another one.</param>
        /// <returns>
        ///   <see langword="true" />, if the current obejct is the same like <paramref name="other" />-parameter, otherwise <see langword="false" />.
        /// </returns>
        public bool Equals(SunnyheatInfraredHeater other)
        {
            if (other == null)
            {
                return false;
            }

            return (IpAddress.Equals(other.IpAddress));
        }

        #endregion Methods
    }
}
