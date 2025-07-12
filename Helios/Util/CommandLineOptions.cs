﻿// Copyright 2020 Ammo Goettsch
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
// 

using System.Collections.Generic;
using System.Windows;
using CommandLine;
using CommandLine.Text;

namespace GadrocsWorkshop.Helios.Util
{
    /// <summary>
    /// helper to work around issues with CommandLine package in a graphical application
    /// </summary>
    public class CommandLineOptions
    {
        public static T Parse<T>(T defaults, string[] args, out int exitCode) where T : CommandLineOptions
        {
            T options = defaults;

            // parse command line: we can't use the simple form of this because we need to display
            // help ourselves as the library just prints help to the console which we don't see
            ParserResult<T> result = Parser.Default.ParseArguments<T>(args);
            exitCode = result.MapResult(success =>
                {
                    options = success;
                    return 0;
                },
                errors => HandleCommandLineErrors(result, errors));
            return options;
        }

        public static int HandleCommandLineErrors<T>(ParserResult<T> result, IEnumerable<Error> _)
            where T : CommandLineOptions
        {
            // WARNING: this is broken in the command line utility.  It prints help automatically
            // to a console we can't see and then returns a parser error instead of a IsHelp item
            // so we just treat all parser errors the same and just show the help
            MessageBox.Show(HelpText.AutoBuild(result), "Command Line Options");
            return -1;
        }
    }
}