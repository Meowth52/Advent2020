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
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            int ReturnValue = 0;
            Dictionary<int, int> Spoken = new Dictionary<int, int>();
            int Turn = 1;
            int Last = 0;
            foreach (int i in Instructions)
            {
                Spoken.Add(i, 0);
                Last = i;
                Turn++;
            }
            ;
            while (Turn <= 2020)
            {
                if (Spoken.ContainsKey(Last) && Spoken[Last] > 0)
                {
                    ReturnValue = Turn - Spoken[Last];
                    Spoken[Last] = Turn;
                }
                else if (!Spoken.ContainsKey(Last))
                {
                    Spoken[Last] = 0;
                    ReturnValue = 0;
                }
                else
                    Spoken[Last] = Turn;
                Last = ReturnValue;
                Turn++;
            }
            ReturnValue = Last;
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            return ReturnValue.ToString();
        }
    }
}
