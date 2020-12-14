using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day14 : Day
    {
        string[] Instructions;
        Dictionary<int, long> Memory;
        List<DataSet> Sets = new List<DataSet>();

        public Day14(string _input) : base(_input)
        {
            Instructions = this.parseStringArray(_input.Replace("\r\nmem", "mem"));
            Memory = new Dictionary<int, long>();
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            long ReturnValue = 0;
            foreach (string s in Instructions)
            {
                Sets.Add(new DataSet(s));
            }
            foreach (DataSet s in Sets)
            {
                s.MemoryWrite(ref Memory);
            }
            foreach (KeyValuePair<int, long> m in Memory)
            {
                ReturnValue += m.Value;
            }
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            long ReturnValue = 0;
            foreach (DataSet s in Sets)
            {
                ReturnValue += s.FLoatingSum();
            }
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
        }
        public void MemoryWrite(ref Dictionary<int, long> Memory)
        {
            foreach (Tuple<int, int> t in Values)
            {
                string ValueString = Convert.ToString(t.Item2, 2);
                ValueString = ValueString.PadLeft(36, '0');
                string Maskedvalue = "";
                for (int i = 0; i < Mask.Length; i++)
                {
                    string switch_on = ValueString[i].ToString() + Mask[i].ToString();
                    switch (switch_on)
                    {
                        case "0X":
                        case "00":
                        case "10":
                            Maskedvalue += "0";
                            break;
                        case "1X":
                        case "11":
                        case "01":
                            Maskedvalue += "1";
                            break;
                        default:
                            break;
                    }
                }
                long masked = Convert.ToInt64(Maskedvalue, 2);
                if (Memory.ContainsKey(t.Item1))
                    Memory[t.Item1] = masked;
                else
                    Memory.Add(t.Item1, masked);
            }
        }
        public FLoatingSum()
        {
            long ReturnValue = 0;
            foreach (Tuple<int, int> t in Values)
            {
                string ValueString = Convert.ToString(t.Item2, 2);
                ValueString = ValueString.PadLeft(36, '0');
                double Maskedvalue = 0;
                int HighestValue = 0;
                for (int i = 0; i < Mask.Length; i++)
                {
                    int inv = Mask.Length - (i + 1);
                    string switch_on = ValueString[inv].ToString() + Mask[inv].ToString();
                    switch (switch_on)
                    {
                        case "00":
                            break;
                        case "0X":
                        case "1X":
                            Maskedvalue += Math.Pow(2, i) / 2;
                            HighestValue++;
                            break;
                        case "11":
                        case "01":
                            Maskedvalue += Math.Pow(2, i);
                            break;
                        default:
                            break;
                    }
                }
                ReturnValue += (long)Maskedvalue * (int)Math.Pow(2, HighestValue);
            }
            return ReturnValue;
        }
    }

}
