﻿//  Copyright 2014 Craig Courtney
//  Copyright 2020 Helios Contributors
//    
//  Helios is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Helios is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using GadrocsWorkshop.Helios.Interfaces.DCS.F5E;

// ReSharper disable once CheckNamespace
namespace GadrocsWorkshop.Helios.Gauges.F5E
{
    using System.Windows;

    internal abstract class F5Eevice : CompositeVisualWithBackgroundImage
    {
        protected F5Eevice(string name, Size size)
            : base(name, size)
        {
            SupportedInterfaces = new[] { typeof(F5EInterface) };
        }
    }
}
