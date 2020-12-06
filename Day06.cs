using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day06 : Day
    {
        string[] Instructions;
        public Day06(string _input) : base(_input)
        {
            string clean = _input.Replace("\r\n\r\n", "_").Replace("\r\n", ",");
            Instructions = this.parseStringArray(clean);
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            int ReturnValue = 0;
            foreach (string s in Instructions)
            {
                List<char> Answeres = new List<char>();
                foreach (char c in s)
                {
                    if (c != ',' && !Answeres.Contains(c))
                        Answeres.Add(c);
                }
                ReturnValue += Answeres.Count;
            }
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            List<string[]> Passengers = new List<string[]>();
            foreach (string s in Instructions)
            {
                Passengers.Add(s.Split(','));
            }
            foreach (string[] g in Passengers)
            {
                Dictionary<char, int> Answeres = new Dictionary<char, int>();
                foreach (string s in g)
                {
                    foreach (char c in s)
                    {
                        if (Answeres.ContainsKey(c))
                            Answeres[c]++;
                        else
                            Answeres.Add(c, 1);
                    }
                }
                foreach (KeyValuePair<char, int> k in Answeres)
                    if (k.Value == g.Length)
                        ReturnValue++;
            }
            return ReturnValue.ToString();
        }
    }
}
