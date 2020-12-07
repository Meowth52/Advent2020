using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day07 : Day
    {
        string[] Instructions;
        public Day07(string _input) : base(_input)
        {
            Instructions = this.parseStringArray(_input);
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            int ReturnValue = 0;
            Dictionary<string, Dictionary<string, int>> Bags = new Dictionary<string, Dictionary<string, int>>();
            foreach (string s in Instructions)
            {
                string[] wordSplit = s.Split(' ');
                string Bag = wordSplit[0] + wordSplit[1];
                Bags.Add(Bag, new Dictionary<string, int>());
                for (int i = 7; i < wordSplit.Length; i++)
                {
                    if (wordSplit[i].Contains("bag"))
                        Bags[Bag].Add(wordSplit[i - 2] + wordSplit[i - 1], Int32.Parse(wordSplit[i - 3]));
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
