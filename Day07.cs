using System;
using System.Collections.Generic;

namespace Advent2020
{
    public class Day07 : Day
    {
        string[] Instructions;
        Dictionary<string, Dictionary<string, int>> Bags = new Dictionary<string, Dictionary<string, int>>();
        Dictionary<string, List<string>> InvertedBags = new Dictionary<string, List<string>>();
        public Day07(string _input) : base(_input)
        {
            Instructions = this.parseStringArray(_input);
            foreach (string s in Instructions)
            {
                string[] wordSplit = s.Split(' ');
                string Bag = wordSplit[0] + wordSplit[1];
                Bags.Add(Bag, new Dictionary<string, int>());
                if (!InvertedBags.ContainsKey(Bag))
                    InvertedBags.Add(Bag, new List<string>());
                for (int i = 7; i < wordSplit.Length; i++)
                {
                    if (wordSplit[i].Contains("bag"))
                        Bags[Bag].Add(wordSplit[i - 2] + wordSplit[i - 1], Int32.Parse(wordSplit[i - 3]));
                }
            }
            foreach (KeyValuePair<string, List<string>> ib in InvertedBags)
            {
                foreach (KeyValuePair<string, Dictionary<string, int>> b in Bags)
                {
                    if (b.Value.ContainsKey(ib.Key) & !ib.Value.Contains(b.Key))
                        ib.Value.Add(b.Key);
                }
            }
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            int ReturnValue = 0;
            List<string> ShinyGoldParents = GetParentBags("shinygold");
            ReturnValue = ShinyGoldParents.Count;
            return ReturnValue.ToString();
        }
        public List<string> GetParentBags(string bag)
        {
            List<string> ReturnList = new List<string>();
            foreach (string b in InvertedBags[bag])
                if (!ReturnList.Contains(b))
                    ReturnList.Add(b);
            List<string> tempList = new List<string>();
            foreach (string b in ReturnList)
            {
                tempList.AddRange(GetParentBags(b));
            }
            foreach (string s in tempList)
                if (!ReturnList.Contains(s))
                    ReturnList.Add(s);
            return ReturnList;
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            ReturnValue = GetChildBags("shinygold") - 1;
            return ReturnValue.ToString();
        }
        public int GetChildBags(string bag)
        {
            int ReturnValue = 1;
            foreach (KeyValuePair<string, int> b in Bags[bag])
                ReturnValue += (b.Value) * GetChildBags(b.Key);
            return ReturnValue;
        }
    }
}
