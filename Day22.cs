using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day22 : Day
    {
        List<string[]> Instructions;
        Queue<int> Deck1;
        Queue<int> Deck2;

        public Day22(string _input) : base(_input)
        {
            Instructions = this.parseListOfStringArrays(_input);
            Deck1 = new Queue<int>();
            foreach (string s in Instructions[0])
                if (!s.Contains("Player"))
                    Deck1.Enqueue(Int32.Parse(s));
            Deck2 = new Queue<int>();
            foreach (string s in Instructions[1])
                if (!s.Contains("Player"))
                    Deck2.Enqueue(Int32.Parse(s));
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            int ReturnValue = 0;
            while (true)
            {

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
