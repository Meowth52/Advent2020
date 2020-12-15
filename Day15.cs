using System;
using System.Collections.Generic;

namespace Advent2020
{
    public class Day15 : Day
    {
        List<int> Instructions;
        public Day15(string _input) : base(_input)
        {
            Instructions = this.parseListOfInteger(_input);
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(2020), getPartOne(30000000));
        }
        public string getPartOne(int BigNumber)
        {
            int ReturnValue = 0;
            Dictionary<int, int> Spoken = new Dictionary<int, int>();
            int Turn = 1;
            int LastSpoken = 0;
            int NextNumber = 0;
            foreach (int i in Instructions)
            {
                Spoken.Add(i, Turn);
                LastSpoken = i;
                Turn++;
            }
            ;
            while (Turn <= BigNumber)
            {
                if (!Spoken.ContainsKey(LastSpoken))
                    Spoken.Add(LastSpoken, 0);
                if (Spoken[LastSpoken] > 0 && Turn != Instructions.Count + 1)
                {
                    NextNumber = (Turn - 1) - Spoken[LastSpoken];
                }
                else
                    NextNumber = 0;
                if (Turn != Instructions.Count + 1)
                    Spoken[LastSpoken] = Turn - 1;
                LastSpoken = NextNumber;
                Turn++;
            }
            ReturnValue = LastSpoken;
            return ReturnValue.ToString();
        }
        public string getPartTwo() //Nope
        {
            int ReturnValue = 0;
            return ReturnValue.ToString();
        }
    }
}
