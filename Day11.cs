using System;
using System.Collections.Generic;
using System.Linq;

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
                    ChairMaybe next = new ChairMaybe(x, y, Instructions[y][x] == 'L'); //I dont thing we need them non chair spaces around here
                    Area.Add(next.ToString(), next);
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
            int Last = -1;
            int Iterations = 0;
            while (true)
            {
                Iterations++;
                ReturnValue = 0;
                Dictionary<string, ChairMaybe> NextState = new Dictionary<string, ChairMaybe>();
                foreach (KeyValuePair<string, ChairMaybe> c in Area)
                {
                    if (c.Value.IsChair)
                    {
                        int NumberOfNeighbours = 0;
                        foreach (string s in c.Value.GetAllNeighbourKeys())
                        {
                            if (Area.ContainsKey(s) && Area[s].IsOccupied)
                                NumberOfNeighbours++;
                        }
                        ChairMaybe Next = new ChairMaybe(c.Value.x, c.Value.y, c.Value.IsChair);
                        if (c.Value.IsOccupied)
                        {
                            Next.IsOccupied = (NumberOfNeighbours < 4);
                        }
                        else
                            Next.IsOccupied = NumberOfNeighbours == 0;
                        NextState.Add(Next.ToString(), Next);
                        if (Next.IsOccupied)
                            ReturnValue++;
                    }
                }
                if (ReturnValue == Last)
                    break;
                Area = new Dictionary<string, ChairMaybe>(NextState);
                Last = ReturnValue;
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
                ReturnList.Add(_x.ToString() + "," + _y.ToString());
            }
            return ReturnList;
        }
    }
}
