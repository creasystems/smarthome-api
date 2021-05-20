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
            InitializeHeaters();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Initializes the heaters.
        /// </summary>
        private void InitializeHeaters()
        {
            heaterList = new SunnyheatInfraredHeaterList();
            heaterList.AddHeater(new SunnyheatInfraredHeater("192.168.20.127", "Living room"));
        }

        #endregion Methods
    }
}
