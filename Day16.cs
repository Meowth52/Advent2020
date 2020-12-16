using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day16 : Day
    {
        Dictionary<string, List<Tuple<int, int>>> Rules;
        List<List<int>> Tickets;
        public Day16(string _input) : base(_input)
        {
            string[] Inputs = _input.Split(new[] { "your ticket:" }, StringSplitOptions.RemoveEmptyEntries);
            Rules = new Dictionary<string, List<Tuple<int, int>>>();
            foreach (string s in this.parseStringArray(Inputs[0]))
            {
                string Rulename = s.Substring(0, s.IndexOf(':'));
                Rules.Add(Rulename, new List<Tuple<int, int>>());
                MatchCollection matches = Regex.Matches(s, @"\d+-\d+");
                foreach (Match m in matches)
                {
                    string[] numbers = m.Value.Split('-');
                    Rules[Rulename].Add(new Tuple<int, int>(Int32.Parse(numbers[0]), Int32.Parse(numbers[1])));
                }
            }
            Tickets = this.parseListOfIntegerLists(Inputs[1]);
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            int ReturnValue = 0;
            foreach (List<int> t in Tickets)
            {
                foreach (int i in t)
                {
                    bool ok = false;
                    foreach (KeyValuePair<string, List<Tuple<int, int>>> r in Rules)
                    {
                        foreach (Tuple<int, int> pair in r.Value)
                        {
                            if (i >= pair.Item1 && i <= pair.Item2)
                                ok = true;
                            else
                                ;
                        }
                    }
                    if (!ok)
                        ReturnValue += i;
                }
            }
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            return ReturnValue.ToString();
        }
    }
}
