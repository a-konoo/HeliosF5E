
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;


namespace GadrocsWorkshop.Helios.Controls
{
    public class FlipAnimUtil
    {
        public static string ImageFileName(string baseImageName, int index, char substitutionChar)
        {
            return baseImageName.Replace($"{substitutionChar}.", $"{index}.");
        }

        public static (String, String) GetDirPathAndFileName(string path)
        {
            var dirPath = Path.GetDirectoryName(path);
            var fileName = Path.GetFileName(path);

            return (dirPath, fileName);
        }


        public static bool IsPatternHasPngExt(string filename)
        {
            return filename?.IndexOf(".png", StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

        public static String CreateExampleFileDescription(string path, int pattern)
        {
            var trimedPath = path?.Trim();

            if (String.IsNullOrEmpty(trimedPath)) { return "empty path"; }
            if (pattern <= 0) { return "invalid pattern number."; }

            (string dirPath, string fileName) = FlipAnimUtil.GetDirPathAndFileName(trimedPath);


            var description = $"path:\r\n{dirPath}\r\n base filename:{fileName}\r\n";
            (bool isValidFileName, MatchCollection matches) = CheckReplaceNumberFormat(trimedPath, 2);

            string[] exampleArray = new string[] { };
            if (isValidFileName)
            {
                (string prev, string aft) = GetFilenameParts(fileName, matches,2);
                exampleArray = GetExamplesDescFor2PH(1, pattern, prev, aft);

                description += String.Join("", exampleArray);
                description += "...\r\n";
                description += "each frame must have above format rule's file name file.\r\n";
                if (pattern > 1)
                {
                    description += "and current positon pattern number parameter will be valid.\r\n";
                    description += "when pattern number change, it shows position image at assigned pattern number.\r\n";
                    description += "ex1) ***_01_01.png is first angle(button not pushed).\r\n";
                    description += "ex2) ***_01_02.png is first angle(button pushed).\r\n";
                    description += "ex3) ***_02_01.png is second angle(button not pushed).\r\n";
                }
                else
                {
                    description += "and current positon pattern number parameter will be valid.\r\n";
                    description += "when pattern number change, it shows position image at assigned pattern number.\r\n";
                    description += "ex1) ***_01_01.png is first angle(no button).\r\n";
                    description += "ex2) ***_02_01.png is second angle(no button).\r\n";
                    description += "ex3) ***_03_01.png is third angle(no button).\r\n";
                }
            }
            else
            {
                description += "file name must be ****_01_01.png at first frame.";
            }

            return description;
        }

        public static String CreatePositionExampleFor2PH(
            string path, int pattern, int frame)
        {
            var trimedPath = path?.Trim();
            if (String.IsNullOrEmpty(trimedPath)) { return "empty path"; }
            if (pattern <= 0) { return "invalid pattern"; }

            (string dirPath, string fileName) = FlipAnimUtil.GetDirPathAndFileName(trimedPath);

            (bool isValidFileName, MatchCollection matches) = CheckReplaceNumberFormat(trimedPath, 2);
            string description = "";
            if (isValidFileName)
            {
                (string prev, string aft) = GetFilenameParts(fileName, matches, 2);
                string[] exampleArrayStart = GetExamplesDescFor2PH(frame, pattern, prev, aft);
                string[] exampleArrayEnd = GetExamplesDescFor2PH(frame, pattern, prev, aft);
                var first = String.Join("", exampleArrayStart);
                var last = String.Join("", exampleArrayEnd);

                description += first;
                if (!first.Equals(last))
                {
                    description += "...\r\n";
                    description += last;
                }
            }
            else
            {
                description += "file name must be ****_01_01.png at first frame.";
            }
            Console.WriteLine(description);
            return description;
        }

        public static String CreatePositionExampleFor1PH(
            string path, int frame)
        {
            var trimedPath = path?.Trim();
            if (String.IsNullOrEmpty(trimedPath)) { return "empty path"; }

            (string dirPath, string fileName) = FlipAnimUtil.GetDirPathAndFileName(trimedPath);

            (bool isValidFileName, MatchCollection matches) = CheckReplaceNumberFormat(trimedPath, 1);
            string description = "";
            if (isValidFileName)
            {
                (string prev, string aft) = GetFilenameParts(fileName, matches,1);
                string exampleArrayStart = GetExamplesDescFor1PH(frame, prev, aft)[0];
                string exampleArrayEnd = GetExamplesDescFor1PH(frame, prev, aft)[0];
                var first = String.Join("", exampleArrayStart);
                var last = String.Join("", exampleArrayEnd);

                description += first;
                if (!first.Equals(last))
                {
                    description += "...\r\n";
                    description += last;
                }
            }
            else
            {
                description += "file name must be ****_01.png at first frame.";
            }
            Console.WriteLine(description);
            return description;
        }

        public static String CreateAnimPositionExampleFor1PH(
            string path, int startFrame, int endFrame)
        {
            var trimedPath = path?.Trim();
            if (String.IsNullOrEmpty(trimedPath)) { return "empty path"; }

            (string dirPath, string fileName) = FlipAnimUtil.GetDirPathAndFileName(trimedPath);

            (bool isValidFileName, MatchCollection matches) = CheckReplaceNumberFormat(trimedPath, 1);
            string description = "";
            if (isValidFileName)
            {
                (string prev, string aft) = GetFilenameParts(fileName, matches, 1);
                string exampleArrayStart = GetAnimExamplesDescFor1PH(startFrame, startFrame, prev, aft)[0];
                string exampleArrayEnd = GetAnimExamplesDescFor1PH(endFrame, endFrame, prev, aft)[0];
                var first = String.Join("", exampleArrayStart);
                var last = String.Join("", exampleArrayEnd);

                description += first;
                if (!first.Equals(last))
                {
                    description += "...\r\n";
                    description += last;
                }
            }
            else
            {
                description += "file name must be ****_01.png at first frame.";
            }
            Console.WriteLine(description);
            return description;
        }

        public static (bool, MatchCollection) CheckReplaceNumberFormat(String path, int count)
        {
            if (path == null || path.Trim().Length == 0)
            {
                return (false, null);
            }
            var patternFileName = Path.GetFileName(path);
            Regex ptn = new Regex("_01");
            var matches = ptn.Matches(patternFileName);
            if (matches.Count != count)
            {
                return (false, null);
            }
            return (true, matches);
        }

        public static String[] GetExamplesDescFor2PH(
            int frame, int pattern, string prev, string aft)
        {
            List<String> desc = new List<String>();

            for (var j = 1; j <= pattern; j++)
            {
                desc.Add($"frame {frame}:{prev}_{ZeroPadNum(frame)}_{ZeroPadNum(j)}{aft}\r\n");
            }

            return desc?.ToArray();
        }

        public static String[] GetExamplesDescFor1PH(
            int frame, string prev, string aft)
        {
            List<String> desc = new List<String>();

            desc.Add($"frame {frame}:{prev}_{ZeroPadNum(frame)}{aft}\r\n");
            return desc?.ToArray();
        }

        public static String[] GetAnimExamplesDescFor1PH(
             int startPos, int endPos, string prev, string aft)
        {
            List<String> desc = new List<String>();
            for (var i = startPos; i <= endPos; i++)
            {
                desc.Add($"frame {i}:{prev}_{ZeroPadNum(i)}{aft}\r\n");
            }
            return desc?.ToArray();
        }


        public static List<String[]> GetPathArrayFor2PH(
            int frame, int pattern, string prev, string aft)
        {
            List<String[]> pathList = new List<String[]>();

            List<String> framePaterns = new List<String>();
            for (var j = 1; j <= pattern; j++)
            {
                framePaterns.Add($"{prev}_{ZeroPadNum(frame)}_{ZeroPadNum(j)}{aft}");
            }
            pathList.Add(framePaterns.ToArray());
            return pathList;
        }

        public static List<String> GetPathArrayFor1PH(
            int frame, string prev, string aft)
        {
            List<String> framePaterns = new List<String>();

            framePaterns.Add($"{prev}_{ZeroPadNum(frame)}{aft}");

            return framePaterns;
        }

        public static List<String> GetAnimPathArrayFor1PH(
            int startPos, int endPos, string prev, string aft)
        {
            List<String> framePaterns = new List<String>();
            for (var i = startPos; i <= endPos; i++)
            {
                framePaterns.Add($"{prev}_{ZeroPadNum(i)}{aft}");
            }
            return framePaterns;
        }

        public static (String, String) GetFilenameParts(String filename, MatchCollection matches,int count)
        {
            if (matches == null || matches.Count < count) { return (String.Empty, String.Empty); }
            var prev = filename.Substring(0, matches[0].Index);
            var matchLastIndex = matches.Count -1 < 0 ? 0 : matches.Count - 1;
            var aft = matches[matchLastIndex].Index + 3 < filename.Length ?
                filename.Substring(matches[matchLastIndex].Index + 3) : String.Empty;
            return (prev, aft);
        }

        public static String ZeroPadNum(int x)
        {
            var temp = $"00{x}";
            return temp.Substring(temp.Length - 2);
        }

    }
}
