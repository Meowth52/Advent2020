using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day09 : Day
    {
        List<long> Instructions;
        long Invalid;
        public Day09(string _input) : base(_input)
        {
            Instructions = this.parseListOfLong(_input);
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            long ReturnValue = 0;
            int Lenght = 5;
            Queue<long> Preamble = new Queue<long>();
            Queue<long> DataSet = new Queue<long>();
            int i = 0;
            foreach (long l in Instructions)
            {
                if (i < Lenght)
                    Preamble.Enqueue(l);
                else
                    DataSet.Enqueue(l);
                i++;
            }
            foreach (long l in DataSet)
            {
                bool Pair = false;
                foreach (long ll in Preamble)
                {
                    foreach (long lll in Preamble)
                    {
                        if (ll != lll && l == (ll + lll))
                        {
                            Pair = true;
                            break;
                        }
                    }
                }
                if (!Pair)
                {
                    ReturnValue = l;
                    break;
                }
                Preamble.Enqueue(l);
                Preamble.Dequeue();
            }
            Invalid = ReturnValue;
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            long ReturnValue = 0;
            bool GetOut = false;
            foreach (long l in Instructions)
            {
                long sum = 0;
                List<long> Range = new List<long>();
                foreach (long ll in Instructions)
                {
                    sum += ll;
                    Range.Add(ll);
                    if (sum == Invalid)
                    {
                        GetOut = true;
                        Range.Sort();
                        ReturnValue = Range.First() + Range.Last();
                        break;
                    }
                    if (GetOut)
                        break;
                }
            }

            return ReturnValue.ToString();
        }
    }
}
