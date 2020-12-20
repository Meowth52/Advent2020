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
        List<string> ValidMessages;
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
            ValidMessages = RuleSets[0].PossibleStrings(ref RuleSets, 0);
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
                if (ValidMessages.Contains(m))
                    ReturnValue++;
            }
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            //RuleSets[8] = new RuleSet("42 | 42 8");
            //RuleSets[11] = new RuleSet("42 31 | 42 11 31");
            //RuleSets[8] = new RuleSet("42 | 42 42| 42 42 42| 42 42 42 42 | 42 42 42 42 42 | 42 42 42 42 42 42 | 42 42 42 42 42 42 42 | 42 42 42 42 42 42 42 42 | 42 42 42 42 42 42 42 42 42 | 42 42 42 42 42 42 42 42 42 42");
            //RuleSets[11] = new RuleSet("42 31 | 42 42 31 31 | 42 42 42 31 31 31 | 42 42 42 42 31 31 31 31  | 42 42 42 42 42 31 31 31 31 31 | 42 42 42 42 42 42 31 31 31 31 31 31 | 42 42 42 42 42 42 42 31 31 31 31 31 31 31 | 42 42 42 42 42 42 42 42 31 31 31 31 31 31 31 31 | 42 42 42 42 42 42 42 42 42 31 31 31 31 31 31 31 31 31 | 42 42 42 42 42 42 42 42 42 42 31 31 31 31 31 31 31 31 31 31");
            //ValidMessages = RuleSets[0].PossibleStrings(ref RuleSets, 0);
            //foreach (string m in Messages)
            //{
            //    if (ValidMessages.Contains(m))
            //        ReturnValue++;
            //}
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
            public List<string> PossibleStrings(ref Dictionary<int, RuleSet> RuleSets, int Depth)
            {
                Depth++;
                List<string> ReturnList = new List<string>();
                if (Depth > 15)
                    return ReturnList;
                if (Rules[0][0] == "a" || Rules[0][0] == "b")
                {
                    ReturnList.Add(Rules[0][0]);
                    return ReturnList;
                }
                foreach (List<string> l in Rules)
                {
                    HashSet<string> Hashis = new HashSet<string>();
                    foreach (string s in l)
                    {
                        List<string> Childs = RuleSets[Int32.Parse(s)].PossibleStrings(ref RuleSets, Depth);
                        if (Hashis.Count == 0)
                            foreach (string c in Childs)
                                Hashis.Add(c);
                        else
                        {
                            HashSet<string> Hashiser = new HashSet<string>();
                            foreach (string a in Hashis)
                                foreach (string c in Childs)
                                    if (a.Length + c.Length < 98)
                                        Hashiser.Add(a + c);
                            Hashis = new HashSet<string>(Hashiser);
                        }

                    }
                    ReturnList.AddRange(Hashis);
                }
                return ReturnList;
            }
        }
    }
}
