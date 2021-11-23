using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day20 : Day
    {
        Dictionary<int, Square> Squares;
        Dictionary<int, List<bool>> Corners;
        public Day20(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            string[] splitted = Input.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Squares = new Dictionary<int, Square>();
            Corners = new Dictionary<int, List<bool>>();
            foreach (string s in splitted)
            {
                string[] Splittted = s.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (Splittted.Length > 1)
                {
                    int Id = Int32.Parse(Regex.Match(Splittted[0], @"-?\d+").Value);
                    MatchCollection Matches = Regex.Matches(s, @"-?\d+");
                    Squares.Add(Id, new Square(Splittted[1]));
                }
            }
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            long ReturnValue = 1;
            foreach (KeyValuePair<int, Square> s in Squares)
            {
                int NumberOfNeighbours = 0;
                List<bool> Matches = new List<bool>() { false, false, false, false };
                foreach (KeyValuePair<int, Square> s2 in Squares)
                {
                    if (s.Key != s2.Key)
                    {
                        foreach (int side in s.Value.Sides)
                        {
                            if (s2.Value.Sides.Contains(side) || s2.Value.AntiSides.Contains(side))
                            {
                                NumberOfNeighbours++;
                                Matches[s.Value.Sides.IndexOf(side)] = true;
                            }
                        }
                    }
                }
                if (NumberOfNeighbours == 2)
                {
                    ReturnValue *= s.Key;
                    Corners.Add(s.Key, Matches);
                }
            }
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            List<List<char>> Sea = new List<List<char>>();
            int Current = Corners.First().Key;
            List<int> Directions = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                if (Corners[Current][i])
                    Directions.Add(i);
            }
            while (true)
            {
                int side = Squares[Current].Sides[Directions[0]];
                //match first direction and fill the sea if no match go to second direction
            }
            foreach (KeyValuePair<int, Square> s in Squares)
            {
                foreach (KeyValuePair<int, Square> s2 in Squares)
                {
                    if (s.Key != s2.Key)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            int side = s.Value.Sides[i];
                            if (s2.Value.Sides.Contains(side) || s2.Value.AntiSides.Contains(side))
                            {
                                switch (i)
                                {
                                    case 3://right
                                        s.Value.MatchRight = s2.Key;
                                        break;
                                    case 1://down
                                        s.Value.MatchDown = s2.Key;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (s2.Value.AntiSides.Contains(side))
                            {
                                switch (i)
                                {
                                    case 3://right
                                        s.Value.InvertRight = true;
                                        break;
                                    case 1://down
                                        s.Value.InvertDown = true;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            return ReturnValue.ToString();
        }
    }
    public class Square
    {
        public List<int> Sides; //top,down,left,right
        public List<int> AntiSides;
        public int MatchRight;
        public bool InvertRight;
        public int MatchDown;
        public bool InvertDown;
        public char[,] Content;
        int Size;
        public Square(string input)
        {
            Sides = new List<int>();
            AntiSides = new List<int>();
            input = input.Replace(".", "0").Replace("#", "1");
            string[] splitted = input.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<string> SideStrings = new List<string>();
            Size = splitted.Length - 2;
            Content = new char[Size, Size];
            for (int y = 1; y < splitted.Length - 1; y++)
            {
                for (int x = 1; x < splitted.Length - 1; x++)
                {
                    Content[x - 1, y - 1] = splitted[y][x];
                }
            }
            SideStrings.Add(splitted[0]);
            SideStrings.Add(splitted.Last());
            string l = "";
            string r = "";
            foreach (string s in splitted)
            {
                l += s[0];
                r += s.Last();
            }
            SideStrings.Add(l);
            SideStrings.Add(r);
            foreach (string s in SideStrings)
            {
                Sides.Add(Convert.ToInt32(s, 2));
                string NextS = "";
                for (int i = s.Length - 1; i >= 0; i--)
                {
                    NextS += s[i];
                }
                AntiSides.Add(Convert.ToInt32(NextS, 2));
            }
        }
        public void Turn() //Dont forget to invert
        {
            char[,] New = new char[Size, Size];
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    New[x, y] = Content[y, x];
                }
            }
            Content = New;
            List<int> NewSides = new List<int>()
            {
                AntiSides[2],
                Sides[0],
                AntiSides[3],
                Sides[1]
            }; //top,down,left,right
            List<int> NewAntiSides = new List<int>()
            {
                Sides[2],
                AntiSides[0],
                Sides[3],
                AntiSides[1]
            };
            Sides = new List<int>(NewSides);
            AntiSides = new List<int>(NewAntiSides);
        }
        public void InvertX()
        {
            char[,] New = new char[Size, Size];
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    New[x, y] = Content[Size - x, y];
                }
            }
            Content = New;
            List<int> NewSides = new List<int>()
            {
                AntiSides[0],
                AntiSides[1],
                Sides[3],
                Sides[2]
            }; //top,down,left,right
            List<int> NewAntiSides = new List<int>()
            {
                Sides[0],
                Sides[1],
                AntiSides[3],
                AntiSides[2]
            };
            Sides = new List<int>(NewSides);
            AntiSides = new List<int>(NewAntiSides);
        }
        public void InvertY()
        {
            char[,] New = new char[Size, Size];
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    New[x, y] = Content[x, Size - y];
                }
            }
            Content = New;
            List<int> NewSides = new List<int>()
            {
                Sides[1],
                Sides[0],
                AntiSides[2],
                AntiSides[3]
            }; //top,down,left,right
            List<int> NewAntiSides = new List<int>()
            {
                AntiSides[1],
                AntiSides[0],
                Sides[2],
                Sides[3]
            };
            Sides = new List<int>(NewSides);
            AntiSides = new List<int>(NewAntiSides);
        }
    }
}
