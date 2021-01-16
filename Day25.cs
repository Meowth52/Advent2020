using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day25 : Day
    {
        long CardKey;
        long DoorKey;
        public Day25(string _input) : base(_input)
        {
            CardKey = 8421034;
            DoorKey = 15993936;
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            long ReturnValue = 0;
            int LoopSize = 0;
            int SubjectNumber = 7;
            long Value = 1;
            int CardLoop = 0;
            int DoorLoop = 0;
            while (CardLoop == 0 || DoorLoop == 0)
            {
                LoopSize++;
                Value *= SubjectNumber;
                Value %= 20201227;
                if (Value == CardKey)
                {
                    CardLoop = LoopSize;
                }
                if (Value == DoorKey)
                {
                    DoorLoop = LoopSize;
                }
            }
            Value = 1;
            for (int i = 0; i < CardLoop; i++)
            {
                Value *= DoorKey;
                Value %= 20201227;
            }
            ReturnValue = Value;
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            return ReturnValue.ToString();
        }
    }
}
