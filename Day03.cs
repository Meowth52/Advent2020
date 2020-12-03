using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day03 : Day
    {
        string[] Instructions;
        public Day03(string _input) : base(_input)
        {
            Instructions = this.parseStringArray(_input);
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            return TraverseSlope(3,1).ToString(); //because Threee is the magick number.. and also one
        }
        public string getPartTwo()
        {
            long ReturnValue = 1;
            ReturnValue *= TraverseSlope(1, 1);
            ReturnValue *= TraverseSlope(3, 1);
            ReturnValue *= TraverseSlope(5, 1);
            ReturnValue *= TraverseSlope(7, 1);
            ReturnValue *= TraverseSlope(1, 2);
            return ReturnValue.ToString();
        }
        public int TraverseSlope(int _x, int _y) //Weee
        {
            int ReturnValue = 0;
            int x = 0;
            for (int i = 0; i<Instructions.Length;i+=_y)
            {
                if (Instructions[i][x] == '#')
                    ReturnValue++; //Swoosh
                x += _x;
                if (x >= Instructions[i].Length)
                    x -= Instructions[i].Length;
            }
            return ReturnValue;
        }
    }
}
