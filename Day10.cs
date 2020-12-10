using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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
            int Last1 = -20;
            int last2 = -10;
            int last3 = 0;
            foreach (int i in Instructions)
            {
                if (i - Last1 <= 3)
                    ReturnValue++;
                Last1 = last2;
                last2 = last3;
                last3 = i;
            }
            return ReturnValue.ToString();
        }
    }
}
