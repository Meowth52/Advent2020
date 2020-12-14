using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Numerics;

namespace Advent2020
{
    public class Day14 : Day
    {
        string[] Instructions;
        Dictionary<ulong, ulong> Memory;
        List<DataSet> Sets = new List<DataSet>();

        public Day14(string _input) : base(_input)
        {
            Instructions = this.parseStringArray(_input.Replace("\r\nmem", "mem"));
            Memory = new Dictionary<ulong, ulong>();
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            ulong ReturnValue = 0;
            foreach (string s in Instructions)
            {
                Sets.Add(new DataSet(s));
            }
            foreach (DataSet s in Sets)
            {
                s.MemoryWrite(ref Memory);
            }
            foreach (KeyValuePair<ulong, ulong> m in Memory)
            {
                ReturnValue += m.Value;
            }
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            BigInteger ReturnValue = 0;
            Memory = new Dictionary<ulong, ulong>();
            foreach (DataSet s in Sets)
            {
                s.FLoatingSum(ref Memory);
            }
            foreach (KeyValuePair<ulong, ulong> m in Memory)
            {
                ReturnValue = BigInteger.Add(ReturnValue, m.Value);
            }
            return ReturnValue.ToString();
        }
    }
    public class DataSet
    {
        string Mask;
        List<Tuple<ulong, ulong>> Values;
        public DataSet(string _input)
        {
            string[] input = _input.Split(new[] { "mem" }, StringSplitOptions.RemoveEmptyEntries);
            Values = new List<Tuple<ulong, ulong>>();
            foreach (string s in input)
            {
                if (s.Contains("mask"))
                {
                    Mask = Regex.Match(s, @"[10X]+").Value;
                }
                else
                {
                    MatchCollection Matches = Regex.Matches(s, @"-?\d+");
                    Values.Add(new Tuple<ulong, ulong>(ulong.Parse(Matches[0].Value), ulong.Parse(Matches[1].Value)));
                }
            }
        }
        public void MemoryWrite(ref Dictionary<ulong, ulong> Memory)
        {
            foreach (Tuple<ulong, ulong> t in Values)
            {
                string ValueString = Convert.ToString((long)t.Item2, 2);
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
                ulong masked = Convert.ToUInt64(Maskedvalue, 2);
                if (Memory.ContainsKey(t.Item1))
                    Memory[t.Item1] = masked;
                else
                    Memory.Add(t.Item1, masked);
            }
        }
        public void FLoatingSum(ref Dictionary<ulong, ulong> Memory)
        {
            foreach (Tuple<ulong, ulong> t in Values)
            {
                string ValueString = Convert.ToString((long)t.Item1, 2);
                ValueString = ValueString.PadLeft(36, '0');
                List<ulong> Adresses = new List<ulong>();
                Adresses.Add(0);
                ulong Solid = 0;
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
                            int count = Adresses.Count;
                            for (int a = 0; a < count; a++)
                            {
                                Adresses.Add((ulong)a);
                                Adresses[a] += (ulong)Math.Pow(2, i);
                            }
                            break;
                        case "11":
                        case "01":
                        case "10":
                            Solid += (ulong)Math.Pow(2, i);
                            break;
                        default:
                            break;
                    }
                }
                foreach (ulong l in Adresses)
                {
                    Memory[l + Solid] = t.Item2;
                }
                ;
            }
            ;
        }
    }

}
