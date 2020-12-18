using System;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day18 : Day
    {
        string[] Instructions;
        public Day18(string _input) : base(_input)
        {
            Instructions = this.parseStringArray(_input);
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            BigInteger ReturnValue = 0;
            foreach (string s in Instructions)
            {
                string Mutable = s;
                while (true)
                {
                    MatchCollection Matches = Regex.Matches(Mutable, @"\(([^\()]+?)\)");
                    if (Matches.Count == 0)
                    {
                        ReturnValue += SumFromString(Mutable);
                        break;
                    }
                    foreach (Match m in Matches)
                    {
                        int Pos = Mutable.IndexOf(m.Value);
                        string s1 = Mutable.Substring(0, Pos);
                        string s2 = SumFromString(m.Value).ToString();
                        string s3 = Mutable.Substring(Pos + m.Value.Length);
                        Mutable = Mutable.Substring(0, Pos) + SumFromString(m.Value).ToString() + Mutable.Substring(Pos + m.Value.Length);
                    }
                }
            }
            return ReturnValue.ToString();
        }
        public long SumFromString(string s)
        {
            long ReturnValue = 0;
            string[] Strings = s.Replace("(", "").Replace(")", "").Split(' ');
            bool Odd = true;
            string Operator = "";
            long Now = 0;
            foreach (string st in Strings)
            {
                if (Odd)
                {
                    Now = Int64.Parse(st);
                    if (ReturnValue == 0)
                    {
                        ReturnValue = Now;
                    }
                    else
                    {
                        if (Operator != "")
                        {
                            switch (Operator)
                            {
                                case "+":
                                    ReturnValue += Now;
                                    break;
                                case "*":
                                    ReturnValue *= Now;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    Operator = st;
                }

                Odd = !Odd;
            }
            return ReturnValue; //1547914458 short 
            //114509670993114 long
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            return ReturnValue.ToString();
        }
    }
}
