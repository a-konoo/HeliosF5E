// Copyright 2020 Ammo Goettsch
// 
// Helios is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Helios is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Collections.Generic;
using GadrocsWorkshop.Helios.Util;

namespace GadrocsWorkshop.Helios.Windows.ViewModel
{
    /// <summary>
    /// A view model for StatusReportItem viewed in the Interface Status view
    /// </summary>
    public class InterfaceStatusViewItem : HeliosStaticViewModel<StatusReportItem>
    {
        public InterfaceStatusViewItem(StatusReportItem item) : base(item)
        {
            // no code
        }

        public bool HasRecommendation => Data.Recommendation != null;

        public string Status => Data.Severity.ToString();
        public string TextLine1 => Data.Status;
        public string TextLine2 => Data.Recommendation;

        // optional verbatim code
        public bool HasCode => (Data.CodeLines?.Count ?? 0) > 0;
        public IList<CodeLine> Code => Data.CodeLines;
    }
}