using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2020
{
    public class Day11 : Day
    {
        string[] Instructions;
        Dictionary<Tuple<int, int>, bool> Area;
        Tuple<int,int>[] AllDirections;
        int MaxX;
        int MaxY;

        public Day11(string _input) : base(_input)
        {
            Instructions = this.parseStringArray(_input);
            Area = new Dictionary<Tuple<int, int>, bool>();
            MaxX = Instructions[0].Length - 1;
            MaxY = Instructions.Count() - 1;
            for (int y = 0; y < Instructions.Count(); y++)
            {
                for (int x = 0; x < Instructions[y].Length; x++)
                {
                    if (Instructions[y][x] == 'L')
                    {
                        Area.Add(new Tuple<int, int>(x, y), false);
                    }
                }
            }
            AllDirections = new Tuple<int,int>[8] { new Tuple<int, int>( 1, -1 ),
                                                    new Tuple<int, int>( 0, 1 ),    
                                                    new Tuple<int, int>( 1, 0 ), 
                                                    new Tuple<int, int>( 1, 1 ), 
                                                    new Tuple<int, int>( 0, -1 ),
                                                    new Tuple<int, int>( -1, -1 ),
                                                    new Tuple<int, int>( -1, 0 ), 
                                                    new Tuple<int, int>( -1, 1 ) };
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
            Dictionary<Tuple<int, int>, bool> Part1Area = new Dictionary<Tuple<int, int>, bool>(Area);
            while (true)
            {
                Iterations++;
                ReturnValue = 0;
                Dictionary<Tuple<int, int>, bool> NextState = new Dictionary<Tuple<int, int>, bool>();
                foreach (KeyValuePair<Tuple<int, int>, bool> c in Part1Area)
                {
                    int NumberOfNeighbours = 0;
                    foreach (Tuple<int, int> d in GetListOfOffsets(c.Key, AllDirections))
                    {
                        if (Part1Area.ContainsKey(d) && Part1Area[d])
                            NumberOfNeighbours++;
                    }
                    Tuple<int, int> Next = new Tuple<int, int>(c.Key.Item1, c.Key.Item2);
                    bool IsOccupied = false;
                    if (c.Value)
                    {
                        IsOccupied = (NumberOfNeighbours < 4);
                    }
                    else
                        IsOccupied = NumberOfNeighbours == 0;
                    NextState.Add(Next, IsOccupied);
                    if (IsOccupied)
                        ReturnValue++;
                }
                if (ReturnValue == Last)
                    break;
                Part1Area = new Dictionary<Tuple<int, int>, bool>(NextState);
                Last = ReturnValue;
            }
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            int Last = -1;
            int Iterations = 0;
            Dictionary<Tuple<int, int>, bool> Part2Area = new Dictionary<Tuple<int, int>, bool>(Area);
            while (true)
            {
                Iterations++;
                ReturnValue = 0;
                Dictionary<Tuple<int, int>, bool> NextState = new Dictionary<Tuple<int, int>, bool>();
                foreach (KeyValuePair<Tuple<int, int>, bool> c in Part2Area)
                {
                    int NumberOfNeighbours = 0;
                    foreach (Tuple<int, int> d in AllDirections)
                    {
                        int i = 1;
                        while (true)
                        {
                            Tuple<int, int> D =  this.GetListOfOffsets(c.Key,new Tuple<int, int>[]{new Tuple<int, int>(d.Item1 *i, d.Item2 *i)}).FirstOrDefault();
                            if (D.Item1 < 0 ||
                                D.Item2 < 0 ||
                                D.Item1 > MaxX ||
                                D.Item2 > MaxY)
                                break;
                            if (Part2Area.ContainsKey(D))
                            {
                                if (Part2Area[D])
                                    NumberOfNeighbours++;
                                break;
                            }
                            i++;
                        }
                    }
                    Tuple<int, int> Next = new Tuple<int, int>(c.Key.Item1, c.Key.Item2);
                    bool IsOccupied = false;
                    if (c.Value)
                    {
                        IsOccupied = (NumberOfNeighbours < 5);
                    }
                    else
                        IsOccupied = NumberOfNeighbours == 0;
                    NextState.Add(Next, IsOccupied);
                    if (IsOccupied)
                        ReturnValue++;
                }
                if (ReturnValue == Last)
                    break;
                Part2Area = new Dictionary<Tuple<int, int>, bool>(NextState);
                Last = ReturnValue;
                ;
            }
            return ReturnValue.ToString();
        }
        public List<Tuple<int, int>> GetListOfOffsets(Tuple<int, int> c, Tuple<int,int>[] Offsets)
        {
            List<Tuple<int, int>> ReturnList = new List<Tuple<int, int>>();
            for (int i = 0; i < Offsets.Length; i++)
            {
                int _x = c.Item1 + Offsets[i].Item1;
                int _y = c.Item2 + Offsets[i].Item2;
                ReturnList.Add(new Tuple<int, int>(_x, _y));
            }
            return ReturnList;
        }
    }
}
