using System;
using System.Collections.Generic;
using System.Linq;

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
            int Lenght = 25;
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
            Queue<long> DataSet = new Queue<long>();
            foreach (long l in Instructions)
            {
                DataSet.Enqueue(l);
            }
            foreach (long l in Instructions)
            {
                DataSet.Dequeue();
                long sum = l;
                List<long> Range = new List<long>();
                Range.Add(l);
                foreach (long ll in DataSet)
                {
                    if (l != ll)
                    {
                        sum += ll;
                        Range.Add(ll);
                        if (sum == Invalid)
                        {
                            Range.Sort();
                            ReturnValue = Range.First() + Range.Last();
                            GetOut = true;
                            break;
                        }
                        if (sum >= Invalid)
                        {
                            break;
                        }
                    }
                    if (GetOut)
                        break;
                }
            }

            return ReturnValue.ToString();
        }
    }
}
