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
            Dictionary<int, Queue<int>> Spoken = new Dictionary<int, Queue<int>>();
            int Turn = 1;
            int Last = 0;
            foreach (int i in Instructions)
            {
                Spoken.Add(i, new Queue<int>(new int[] { Turn }));
                Last = i;
                Turn++;
            }
            ;
            while (Turn <= 2020)
            {
                if (!Spoken.ContainsKey(Last))
                    Spoken.Add(Last, new Queue<int>(new int[] { }));
                if (Spoken[Last].Count > 1)
                {
                    ReturnValue = (Turn - 1) - Spoken[Last].Dequeue();
                }
                else
                    ReturnValue = 0;
                if (Turn != Instructions.Count)
                    Spoken[Last].Enqueue(Turn);
                if (Spoken[Last].Count > 2)
                    Spoken[Last].Dequeue();

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
