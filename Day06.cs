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
        List<string[]> Instructions;
        public Day06(string _input) : base(_input)
        {
            Instructions = this.parseListOfStringArrays(_input);
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            int ReturnValue = 0;
            foreach (string[] g in Instructions)
            {
                List<char> Answers = new List<char>();
                foreach (string s in g)
                {
                    foreach (char c in s)
                    {
                        if (c != ',' && !Answers.Contains(c))
                            Answers.Add(c);
                    }
                }
                ReturnValue += Answers.Count;
            }
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            foreach (string[] g in Instructions)
            {
                Dictionary<char, int> Answers = new Dictionary<char, int>();
                foreach (string s in g)
                {
                    foreach (char c in s)
                    {
                        if (Answers.ContainsKey(c))
                            Answers[c]++;
                        else
                            Answers.Add(c, 1);
                    }
                }
                foreach (KeyValuePair<char, int> k in Answers)
                    if (k.Value == g.Length)
                        ReturnValue++;
            }
            return ReturnValue.ToString();
        }
    }
}
