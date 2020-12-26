using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day24 : Day
    {
        string[] Instructions;
        Dictionary<Tile, bool> Floor;
        public Day24(string _input) : base(_input)
        {
            // se -> 3
            // sw -> 1
            // nw -> 7
            // ne -> 9
            string Cleaned = _input.Replace("se", "3").Replace("sw", "1").Replace("nw", "7").Replace("ne", "9");
            Instructions = parseStringArray(Cleaned);
            Floor = new Dictionary<Tile, bool>();
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            int ReturnValue = 0;
            foreach (string i in Instructions)
            {
                Tile Current = new Tile(0, 0);
                foreach (char c in i)
                {
                    Current.MoveHexSteps(c);
                }
                if (Floor.ContainsKey(Current))
                    Floor[Current] = !Floor[Current];
                else
                    Floor.Add(new Tile(Current), true);
            }
            foreach (KeyValuePair<Tile, bool> t in Floor)
            {
                if (t.Value)
                    ReturnValue++;
            }
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            for (int cycle = 1; cycle <= 100; cycle++)
            {
                Dictionary<Tile, bool> NextFloor = new Dictionary<Tile, bool>();
                foreach (KeyValuePair<Tile, bool> k in Floor)
                {
                    List<Tile> Neigbhours = k.Key.GetNeihbours();
                    int Blacks = 0;
                    foreach (Tile c in Neigbhours)
                    {
                        if (!Floor.ContainsKey(c) & !NextFloor.ContainsKey(c))
                        {
                            List<Tile> Neigbhours2 = c.GetNeihbours();
                            int Blacks2 = 0;
                            bool NextState2 = false;
                            foreach (Tile c2 in Neigbhours2)
                            {
                                if (Floor.ContainsKey(c2) && Floor[c2])
                                {
                                    Blacks2++;
                                }

                            }
                            if (Blacks2 == 2)
                                NextState2 = true;
                            NextFloor.Add(c, NextState2);
                        }
                        else if (Floor.ContainsKey(c) && Floor[c])
                            Blacks++;
                    }
                    bool NextState = k.Value;
                    if (k.Value && (Blacks != 1 && Blacks != 2))
                        NextState = false;
                    else if (!k.Value && Blacks == 2)
                        NextState = true;
                    NextFloor.Add(k.Key, NextState);
                }
                Floor = new Dictionary<Tile, bool>(NextFloor);
            }
            foreach (KeyValuePair<Tile, bool> k in Floor)
                if (k.Value)
                    ReturnValue++;
            return ReturnValue.ToString();
        }
    }
    public class Tile : Coordinate
    {
        public Tile(int x, int y) : base(x, y)
        {
            this.x = x;
            this.y = y;
        }
        public Tile(Tile t) : base(t.x, t.y)
        {
            this.x = t.x;
            this.y = t.y;
        }

        public void MoveHexSteps(char c)
        {
            switch (c)
            {
                case 'e':
                    this.AddTo(new Coordinate(2, 0));
                    break;
                case '3':
                    this.AddTo(new Coordinate(1, -2));
                    break;
                case '1':
                    this.AddTo(new Coordinate(-1, -2));
                    break;
                case 'w':
                    this.AddTo(new Coordinate(-2, 0));
                    break;
                case '7':
                    this.AddTo(new Coordinate(-1, 2));
                    break;
                case '9':
                    this.AddTo(new Coordinate(1, 2));
                    break;
            }
        }
        public List<Tile> GetNeihbours()
        {
            List<Tile> ReturnList = new List<Tile>() {
                new Tile(2,0) ,
                new Tile(1,-2) ,
                new Tile(-1,-2) ,
                new Tile(-2,0) ,
                new Tile(-1,2) ,
                new Tile(1,2) };
            foreach (Tile c in ReturnList)
                c.AddTo(this);
            return ReturnList;
        }
    }
}
