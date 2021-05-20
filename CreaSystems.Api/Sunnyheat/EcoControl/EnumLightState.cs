//-------------------------------------------------------------------------------------------------------
// <copyright file="EnumLightState.cs" company="CREA SYSTEMS Electronic GmbH">
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
    /// <summary>
    /// The available states for the SUNNYHEAT infrared heater light (only you have an installed light).
    /// </summary>
    public enum EnumLightState
    {
        #region Enums

        /// <summary>
        /// The light is off
        /// </summary>
        Off = 0,

        /// <summary>
        /// The light is on
        /// </summary>
        On = 1

        #endregion Enums
    }
}
