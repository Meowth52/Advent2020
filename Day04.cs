using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day04 : Day
    {
        string[] Instructions;
        public Day04(string _input) : base(_input)
        {
            string cleaned = _input.Replace("\r\n\r\n", "_").Replace("\r\n", "=").Replace(" ", "=");
            Instructions = this.parseStringArray(cleaned);
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            int ReturnValue = 0;
            string[] Pass = { "byr:", "iyr:", "eyr:", "hgt:", "hcl:", "ecl:", "pid:" };
            foreach (string s in Instructions)
            {
                bool mhm = true;
                foreach (string ss in Pass)
                {
                    if (!s.Contains(ss))
                        mhm = false;
                }
                if (mhm)
                    ReturnValue++;
            }
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            foreach (string s in Instructions)
            {
                int Validness = 0;
                string[] Document = s.Split('=');
                foreach (string ss in Document)
                {
                    string[] DicItem = ss.Split(':');
                    int Number = 0;
                    switch (DicItem[0])
                    {
                        case "byr":
                            Int32.TryParse(DicItem[1], out Number);
                            if (Number >= 1920 && Number <= 2002)
                                Validness++;
                            break;
                        case "iyr":
                            Int32.TryParse(DicItem[1], out Number);
                            if (Number >= 2010 && Number <= 2020)
                                Validness++;
                            break;
                        case "eyr":
                            Int32.TryParse(DicItem[1], out Number);
                            if (Number >= 2020 && Number <= 2030)
                                Validness++;
                            break;
                        case "hgt":
                            if (DicItem[1].Contains("cm"))
                            {
                                Int32.TryParse(DicItem[1].Replace("cm", ""), out Number);
                                if (Number >= 150 && Number <= 193)
                                    Validness++;
                            }
                            if (DicItem[1].Contains("in")) //Should we really let these guy in?
                            {
                                Int32.TryParse(DicItem[1].Replace("in", ""), out Number);
                                if (Number >= 59 && Number <= 76)
                                    Validness++;
                            }
                            break;
                        case "hcl":
                            Int32.TryParse(DicItem[1].Replace("#", ""), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out Number);
                            if (Number != )
                                break;
                        case "ecl":
                            ;
                            break;
                        case "pid":
                            ;
                            break;
                        case "cid":
                            ; //sssh
                            break;
                        default:
                            break;
                    }
                }
                if (Validness >= 7)
                    ReturnValue++;
            }
            return ReturnValue.ToString();
        }
    }
}
