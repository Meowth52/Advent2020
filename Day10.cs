using System;
using System.Collections.Generic;

namespace Advent2020
{
    public class Day10 : Day
    {
        List<int> Instructions;
        public Day10(string _input) : base(_input)
        {
            Instructions = this.parseListOfInteger(_input);
            Instructions.Sort();
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            int ReturnValue = 0;
            int Jolt1 = 0;
            int Jolt3 = 0;
            int Last = 0;
            foreach (int i in Instructions)
            {
                if (i - Last == 1)
                    Jolt1++;
                if (i - Last == 3)
                    Jolt3++;
                Last = i;
            }
            Jolt3++;
            ReturnValue = Jolt1 * Jolt3;
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            int Lastest = -20;
            int laster = -10;
            int last = 0;
            List<int> Sequenses = new List<int>();
            bool InSequense = false;
            foreach (int i in Instructions)
            {
                if (!InSequense && i == last + 1 && last == laster + 1)
                {
                    InSequense = true;
                    Sequenses.Add(0);
                }
                if (InSequense && i == last + 1)
                {
                    Sequenses[Sequenses.Count - 1]++;
                }
                else
                    InSequense = false;
                Lastest = laster;
                laster = last;
                last = i;
            }
            Dictionary<int, int> NumberSequense = new Dictionary<int, int>();
            NumberSequense.Add(0, 0);
            for (int i = 1; i <= 100; i++)
            {
                NumberSequense.Add(i, NumberSequense[i - 1] + i - 1);
            }
            return ReturnValue.ToString();
        }
    }
}
