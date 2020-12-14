using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day13 : Day
    {
        List<int> Instructions;
        List<int> Instructions2;
        public Day13(string _input) : base(_input)
        {
            Instructions = this.parseListOfInteger(_input);
            Instructions2 = this.parseListOfInteger(_input.Replace("x", "-1"));
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            int ReturnValue = 0;
            int Shortest = 1000000;
            int Depart = Instructions.First();
            Instructions.RemoveAt(0);
            foreach (int i in Instructions)
            {
                int WaitingTime = i - (Depart % i);
                if (WaitingTime < Shortest)
                {
                    Shortest = WaitingTime;
                    ReturnValue = WaitingTime * i;
                }
            }
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            long ReturnValue = 0;
            Instructions2.RemoveAt(0);
            int Largest = 0;
            foreach (int i in Instructions2)
                if (i > Largest)
                    Largest = i;
            long Depart = 0;
            while (true)
            {
                int Last = 1;
                bool Yes = true;
                long WaitingTime = 0;
                foreach (int i in Instructions2)
                {
                    if (i != -1)
                    {
                        WaitingTime = i - (Depart % i);
                        if (WaitingTime != Last)
                        {
                            Yes = false;//Noo!
                            break;
                        }
                        else
                            ;
                    }
                    Last++;
                }
                if (Yes)
                {
                    ReturnValue = Depart + 1;
                    break;
                }
                Depart += Largest;
            }
            return ReturnValue.ToString();
        }
    }
}
