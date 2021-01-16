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
        public Day20(string _input) : base(_input)
        {
            string[] splitted = _input.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Squares = new Dictionary<int, Square>();
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
                foreach (KeyValuePair<int, Square> s2 in Squares)
                {
                    if (s.Key != s2.Key)
                    {
                        foreach (int side in s.Value.Sides.Keys)
                        {
                            if (s2.Value.Sides.ContainsKey(side))
                            {
                                s2.Value.Sides[side] = s.Key;
                                NumberOfNeighbours++;
                            }
                            else if (s2.Value.AntiSides.ContainsKey(side))
                            {
                                s2.Value.AntiSides[side] = s.Key;
                                NumberOfNeighbours++;
                            }
                        }
                    }
                }
                if (NumberOfNeighbours == 2)
                    ReturnValue *= s.Key;
            }
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            return ReturnValue.ToString();
        }
    }
    public class Square
    {
        public Dictionary<int, int> Sides;
        public Dictionary<int, int> AntiSides;
        public Square(string input)
        {
            Sides = new Dictionary<int, int>();
            AntiSides = new Dictionary<int, int>();
            input = input.Replace(".", "0").Replace("#", "1");
            string[] splitted = input.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<string> SideStrings = new List<string>();
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
                Sides.Add(Convert.ToInt32(s, 2), 0);
                string NextS = "";
                for (int i = s.Length - 1; i >= 0; i--)
                {
                    NextS += s[i];
                }
                AntiSides.Add(Convert.ToInt32(NextS, 2), 0);
            }
        }
    }
}
