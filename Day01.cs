using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day01 : Day
    {
        List<int> Instructions;
        public Day01(string _input) : base(_input)
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
            foreach (int i in Instructions)
            {
                foreach (int i2 in Instructions)
                {
                    if (i + i2 == 2020)
                        ReturnValue = i * i2;
                }
            }
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            foreach (int i in Instructions)
                foreach (int i2 in Instructions)
                    foreach (int i3 in Instructions)
                        if (i + i2 +i3 == 2020)
                            ReturnValue = i * i2 *i3;
            return ReturnValue.ToString();
        }
    }
}
