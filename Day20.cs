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
            KeyValuePair<int, Square> Current = new KeyValuePair<int, Square>(Corners.First().Key, Squares[Corners.First().Key]);
            List<int> Directions = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                if (Corners[Current.Key][i])
                    Directions.Add(i);
            }
            for (int i = Directions[0]; i > 0; i--) //right,down,left,upp
            {
                Squares[Current.Key].Turn();
            }
            if (Directions[1] == 3)
                Squares[Current.Key].InvertY();
            int Shore = 0;
            int SquaresMatched = 0;
            int ContentLenght = Current.Value.Content.GetLength(0);
            while (true)
            {
                KeyValuePair<int, Square> FirstInRow = new KeyValuePair<int, Square>(Current.Key, Current.Value);
                for (int i = 0; i < ContentLenght; i++)
                {
                    if (Sea.Count <= Shore + i)
                        Sea.Add(new List<char>());
                    for (int ii = 0; ii < ContentLenght; ii++)
                        Sea[Shore + i].Add(Current.Value.Content[i, ii]);
                }
                Dictionary<int, Square> Next = new Dictionary<int, Square>(Squares);
                bool Match = false;
                for (int n = 0; n < 2; n++)
                {
                    int side = n;
                    foreach (KeyValuePair<int, Square> s in Squares)
                    {
                        if (s.Key != Current.Key)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                bool Anti = false;
                                if (s.Value.Sides[i] == side || s.Value.AntiSides[i] == side)
                                {
                                    if (s.Value.AntiSides[i] == side)
                                        Anti = true;
                                    Match = true;
                                    if (n == 0)
                                    {
                                        int b = i + 2;
                                        if (b > 3)
                                            b -= 4;
                                        for (int k = b; k > 0; k--) //right,down,left,upp
                                        {
                                            Squares[Current.Key].Turn();
                                        }
                                    }
                                    if (n == 1)
                                    {
                                        int b = i + 3;
                                        if (b > 3)
                                            b -= 4;
                                        for (int k = b; k > 0; k--) //right,down,left,upp
                                        {
                                            Squares[Current.Key].Turn();
                                        }
                                        FirstInRow = new KeyValuePair<int, Square>(s.Key, s.Value);
                                    }
                                }
                                if (Anti)
                                {
                                    s.Value.InvertY();
                                }
                            }
                        }
                        if (Match)
                        {
                            Next.Remove(s.Key);
                            Current = s;
                            SquaresMatched++;
                            if (n == 1)
                                Shore += ContentLenght;
                            break;
                        }
                    }
                    if (Match)
                        break;
                    if (n == 0)
                        Current = new KeyValuePair<int, Square>(FirstInRow.Key, FirstInRow.Value);
                }
                if (!Match)
                {
                    for (int i = 0; i < ContentLenght; i++)
                    {
                        if (Sea.Count <= Shore + i)
                            Sea.Add(new List<char>());
                        for (int ii = 0; ii < ContentLenght; ii++)
                            Sea[Shore + i].Add(Current.Value.Content[i, ii]);
                    }
                    break;
                }
                Squares = new Dictionary<int, Square>(Next);
            }
            string SeaString = "\r\n";
            foreach (List<char> l in Sea)
            {
                SeaString += new string(l.ToArray()) + "\r\n";
            }
            return SeaString;
            //return ReturnValue.ToString();
        }
    }
    public class Square
    {
        public List<int> Sides; //right,down,left,upp
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
                    Content[x - 1, y - 1] = splitted[Size - x][Size - y];
                }
            }
            string l = "";
            string r = "";
            foreach (string s in splitted)
            {
                l += s[0];
                r += s.Last();
            }
            SideStrings.Add(r);
            SideStrings.Add(splitted.Last());
            SideStrings.Add(l);
            SideStrings.Add(splitted[0]); //right,down,left,upp
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
                Sides[2],
                AntiSides[0],
                Sides[3],
                AntiSides[1]
            }; //top,down,left,right
            List<int> NewAntiSides = new List<int>()
            {
                AntiSides[2],
                Sides[0],
                AntiSides[3],
                Sides[1]
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
                    New[x, y] = Content[x, Size - (y + 1)];
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
                    New[x, y] = Content[Size - (x + 1), y];
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
