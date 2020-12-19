using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day19 : Day
    {
        Dictionary<int, RuleSet> RuleSets;
        string[] Messages;
        public Day19(string _input) : base(_input)
        {
            string[] Inputs = _input.Replace("\r\n\r\n", "_").Split('_');
            RuleSets = new Dictionary<int, RuleSet>();
            foreach (string s in this.parseStringArray(Inputs[0]))
            {
                string[] Splitted = s.Split(':');
                RuleSets.Add(Int32.Parse(Splitted[0]), new RuleSet(Splitted[1]));
            }
            Messages = this.parseStringArray(Inputs[1]);
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            int ReturnValue = 0;
            foreach (string m in Messages)
            {
                string Message = m;
                int Index = 0;
                while (true)
                {
                    //Actually solve the day
                }
            }
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            return ReturnValue.ToString();
        }
        public class RuleSet
        {
            List<List<string>> Rules;
            public RuleSet(string input)
            {
                Rules = new List<List<string>>();
                string[] Splitted = input.Split('|');
                foreach (string s in Splitted)
                {
                    string[] Schplitted = s.Replace("\"", "").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    Rules.Add(Schplitted.ToList());
                }
            }
        }
    }
}
