using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day14 : Day
    {
        string[] Instructions;
        Dictionary<int, int> Memory;

        public Day14(string _input) : base(_input)
        {
            Instructions = this.parseStringArray(_input.Replace("\r\nmem", "mem"));
            Memory = new Dictionary<int, int>();
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            int ReturnValue = 0;
            List<DataSet> Sets = new List<DataSet>();
            foreach (string s in Instructions)
            {
                Sets.Add(new DataSet(s));
            }
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            return ReturnValue.ToString();
        }
    }
    public class DataSet
    {
        string Mask;
        List<Tuple<int, int>> Values;
        public DataSet(string _input)
        {
            string[] input = _input.Split(new[] { "mem" }, StringSplitOptions.RemoveEmptyEntries);
            Values = new List<Tuple<int, int>>();
            foreach (string s in input)
            {
                if (s.Contains("mask"))
                {
                    Mask = Regex.Match(s, @"[10X]+").Value;
                }
                else
                {
                    MatchCollection Matches = Regex.Matches(s, @"-?\d+");
                    Values.Add(new Tuple<int, int>(Int32.Parse(Matches[0].Value), Int32.Parse(Matches[1].Value)));
                }
            }
            ;
        }
        public void MemoryWrite(ref Dictionary<int, int> Memory)
        {
            foreach (Tuple<int, int> t in Values)
            {
                int masked = 0;
                string ValueString = Convert.ToString(t.Item2, 2);
                for (int i = 0; i < Mask.Length; i++)
                {

                }
                if (Memory.ContainsKey(t.Item1))
                    Memory[t.Item1] = masked;
                else
                    Memory.Add(t.Item1, masked);
            }
        }
    }

}
