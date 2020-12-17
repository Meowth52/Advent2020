using System;
using System.Collections.Generic;

namespace Advent2020
{
    public class Day17 : Day
    {
        Dictionary<Cooordinate, bool> Grid;
        public Day17(string _input) : base(_input)
        {
            Grid = new Dictionary<Cooordinate, bool>();
            string[] input = this.parseStringArray(_input);
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    Grid.Add(new Cooordinate(x, y, 0), input[y][x] == '#');
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
            for (int cycle = 1; cycle <= 6; cycle++)
            {
                Dictionary<Cooordinate, bool> NextGrid = new Dictionary<Cooordinate, bool>();
                foreach (KeyValuePair<Cooordinate, bool> k in Grid)
                {
                    List<Cooordinate> Neigbhours = k.Key.GetNeihbours();
                    int Cubes = 0;
                    foreach (Cooordinate c in Neigbhours)
                    {
                        if (!Grid.ContainsKey(c) & !NextGrid.ContainsKey(c))
                        {
                            List<Cooordinate> Neigbhours2 = c.GetNeihbours();
                            int Cubes2 = 0;
                            bool NextState2 = false;
                            Cooordinate b = new Cooordinate(0, 2, 1);
                            if (c.Equals(b))
                                ;
                            foreach (Cooordinate c2 in Neigbhours2)
                            {

                                if (Grid.ContainsKey(c2) && Grid[c2])
                                {
                                    Cubes2++;
                                }

                            }
                            if (Cubes2 == 3)
                                NextState2 = true;
                            NextGrid.Add(c, NextState2);
                        }
                        else if (Grid.ContainsKey(c) && Grid[c])
                            Cubes++;
                    }
                    bool NextState = k.Value;
                    if (k.Value && (Cubes != 2 && Cubes != 3))
                        NextState = false;
                    else if (!k.Value && Cubes == 3)
                        NextState = true;
                    NextGrid.Add(k.Key, NextState);
                }
                Grid = new Dictionary<Cooordinate, bool>(NextGrid);
            }
            foreach (KeyValuePair<Cooordinate, bool> k in Grid)
                if (k.Value)
                    ReturnValue++;
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            return ReturnValue.ToString();
        }
    }
    public class Cooordinate : Coordinate, IEquatable<Cooordinate>, IComparable<Cooordinate>
    {
        public int z;
        public Cooordinate(int x, int y, int z) : base(x, y)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public List<Cooordinate> GetNeihbours()
        {
            List<Cooordinate> ReturnList = new List<Cooordinate>() {
                new Cooordinate(0,0,1) ,
                new Cooordinate(0,1,0) ,
                new Cooordinate(0,1,1) ,
                new Cooordinate(1,0,0) ,
                new Cooordinate(1,0,1) ,
                new Cooordinate(1,1,0) ,
                new Cooordinate(1,1,1) ,
                new Cooordinate(0,0,-1) ,
                new Cooordinate(0,-1,0) ,
                new Cooordinate(0,-1,-1) ,
                new Cooordinate(-1,0,0) ,
                new Cooordinate(-1,0,-1) ,
                new Cooordinate(-1,-1,0) ,
                new Cooordinate(-1,-1,-1) ,
                new Cooordinate(0,1,-1) ,
                new Cooordinate(0,-1,1) ,
                new Cooordinate(1,-1,0) ,
                new Cooordinate(-1,1,0) ,
                new Cooordinate(1,1,-1) ,
                new Cooordinate(1,-1,1) ,
                new Cooordinate(-1,-1,1) ,
                new Cooordinate(-1,1,-1) ,
                new Cooordinate(1,0,-1) ,
                new Cooordinate(-1,0,1) ,
                new Cooordinate(-1,1,1) ,
                new Cooordinate(1,-1,-1) };
            foreach (Cooordinate c in ReturnList)
                c.AddTo(this);
            return ReturnList;
        }
        public void AddTo(Cooordinate A)
        {
            x += A.x;
            y += A.y;
            z += A.z;
        }

        public Cooordinate GetSum(Cooordinate A)
        {
            int x2 = x + A.x;
            int y2 = y + A.y;
            int z2 = z + A.z;
            return new Cooordinate(x2, y2, z2);
        }
        public override string ToString()
        {
            return x.ToString() + "," + y.ToString() + "," + z.ToString();
        }

        public override int GetHashCode()
        {
            int hCode = x ^ y ^ z;
            return hCode.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as Cooordinate);
        }
        public bool Equals(Cooordinate obj)
        {
            return obj != null && obj.x == x && obj.y == y && obj.z == z;
        }
        public int CompareTo(Cooordinate other)
        {
            if (this.x == other.x)
            {
                if (this.y == other.y)
                    return this.z.CompareTo(other.z);
                return this.y.CompareTo(other.y);
            }
            return this.x.CompareTo(other.x);
        }
    }
    class CooordinateEqualityComparer : IEqualityComparer<Cooordinate>
    {
        public bool Equals(Cooordinate b1, Cooordinate b2)
        {
            if (b2 == null && b1 == null)
                return true;
            else if (b1 == null | b2 == null)
                return false;
            else if (b1.x == b2.x && b1.y == b2.y && b1.z == b2.z)
                return true;
            else
                return false;
        }

        public int GetHashCode(Cooordinate bx)
        {
            int hCode = bx.x ^ bx.y ^ bx.z;
            return hCode.GetHashCode();
        }
    }
}
