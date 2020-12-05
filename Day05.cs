using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day05 : Day
    {
        string[] Instructions;
        List<int> SeatIds;
        int LargestNumber;
        public Day05(string _input) : base(_input)
        {
            Instructions = this.parseStringArray(_input);
            SeatIds = new List<int>();
        }
        public override Tuple<string, string> getResult()
        {
            LargestNumber = getPartOne();
            return Tuple.Create(LargestNumber.ToString(), getPartTwo());
        }
        public int getPartOne()
        {
            int ReturnValue = 0;
            foreach (string s in Instructions)
            {
                string binaryPass = s.Replace("F", "0").Replace("B", "1").Replace("L", "0").Replace("R", "1");
                int row = Convert.ToInt32(binaryPass.Substring(0, 7), 2);
                int column = Convert.ToInt32(binaryPass.Substring(7, 3), 2);
                int seatId = row * 8 + column;
                SeatIds.Add(seatId);
                if (seatId > ReturnValue)
                    ReturnValue = seatId;
            }
            return ReturnValue;
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            for (int i = 0; i < LargestNumber; i++)
            {
                if (!SeatIds.Contains(i) && SeatIds.Contains(i + 1) && SeatIds.Contains(i - 1))
                {
                    ReturnValue = i;
                    break;
                }
            }
            return ReturnValue.ToString();
        }
    }
}
