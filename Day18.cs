using System;
using System.Collections.Generic;
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
            return Tuple.Create(getPartSomething(), getPartSomething(true));
        }
        public string getPartSomething(bool YouMeenTwo = false)
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
                        ReturnValue += SumFromString(Mutable, YouMeenTwo);
                        break;
                    }
                    foreach (Match m in Matches)
                    {
                        int Pos = Mutable.IndexOf(m.Value);
                        string s1 = Mutable.Substring(0, Pos);
                        string s2 = SumFromString(m.Value, YouMeenTwo).ToString();
                        string s3 = Mutable.Substring(Pos + m.Value.Length);
                        Mutable = Mutable.Substring(0, Pos) + SumFromString(m.Value, YouMeenTwo).ToString() + Mutable.Substring(Pos + m.Value.Length);
                    }
                }
            }
            return ReturnValue.ToString();
        }
        public long SumFromString(string s, bool NotThisWay)
        {
            if (NotThisWay)
                return Suminzer2(s);
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
            return ReturnValue;
        }

        public long Suminzer2(string s)
        {
            long ReturnValue = 0;
            List<WhyDoTuplesHaveToSuck> NotAString = new List<WhyDoTuplesHaveToSuck>();
            string[] Strings = s.Replace("(", "").Replace(")", "").Split(' ');
            bool Odd = true;
            char NextOperator = '#';
            long NextNumber = 0;
            foreach (string st in Strings)
            {
                if (Odd)
                {
                    NextNumber = Int64.Parse(st);
                    NotAString.Add(new WhyDoTuplesHaveToSuck(NextOperator, NextNumber));
                }
                else
                {
                    NextOperator = st[0];
                }
                Odd = !Odd;
            }
            for (int i = 1; i < NotAString.Count; i++)
            {
                if (NotAString[i].C == '+')
                {
                    NotAString[i - 1].I += NotAString[i].I;
                    NotAString.RemoveAt(i);
                    i -= 1;
                }
            }
            for (int i = 1; i < NotAString.Count; i++)
            {
                if (NotAString[i].C == '*')
                {
                    NotAString[i - 1].I *= NotAString[i].I;
                    NotAString.RemoveAt(i);
                    i -= 1;
                }
            }
            return NotAString[0].I;
        }
        public class WhyDoTuplesHaveToSuck
        {
            public char C;
            public long I;
            public WhyDoTuplesHaveToSuck(char c, long i)
            {
                C = c;
                I = i;
            }
        }
    }
}
