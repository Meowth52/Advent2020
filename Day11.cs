using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day11 : Day
    {
        string[] Instructions;
        Dictionary<string, ChairMaybe> Area;

        public Day11(string _input) : base(_input)
        {
            Instructions = this.parseStringArray(_input);
            Area = new Dictionary<string, ChairMaybe>();
            for (int y = 0; y < Instructions.Count(); y++)
            {
                for (int x = 0; x < Instructions[y].Length; x++)
                {
                    Area.Add(x.ToString() + y.ToString(), new ChairMaybe(x, y, Instructions[y][x] == 'L'));
                }
            }
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            int ReturnValue = 0;
            while (true)
            {
                Dictionary<string, ChairMaybe> NextState = new Dictionary<string, ChairMaybe>(Area);
                foreach (KeyValuePair<string, ChairMaybe> c in Area)
                {
                    foreach (string s in c.ge)
                }
            }
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            return ReturnValue.ToString();
        }
    }
    public class ChairMaybe : Coordinate
    {
        public bool IsChair;
        public bool IsOccupied;
        int[,] AllDirections = new int[8, 2] { { 1, -1 }, { 0, 1 }, { 1, 0 }, { 1, 1 }, { 0, -1 }, { -1, -1 }, { -1, 0 }, { -1, 1 } };
        public ChairMaybe(int x, int y, bool isChair) : base(x, y)
        {
            this.x = x;
            this.y = y;
            IsChair = isChair;
            IsOccupied = false;
        }
        public List<string> GetAllNeighbourKeys() //Use hashcode instead?
        {
            List<string> ReturnList = new List<string>();
            for (int i = 0; i < 8; i++)
            {
                int _x = x + AllDirections[i, 0];
                int _y = y + AllDirections[i, 1];
                ReturnList.Add(x.ToString() + y.ToString());
            }
            return ReturnList;
        }
    }
}
