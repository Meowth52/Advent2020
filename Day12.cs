using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day12 : Day
    {
        List<Tuple<char, int>> Instructions;
        public Day12(string _input) : base(_input)
        {
            string[] instructions = this.parseStringArray(_input);
            Instructions = new List<Tuple<char, int>>();
            foreach (string s in instructions)
            {
                Instructions.Add(new Tuple<char, int>(s[0], Int32.Parse(s.Substring(1))));
            }
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            int ReturnValue = 0;
            Coordinate Ferry = new Coordinate(0, 0);
            char Direction = 'E';
            List<char> Directions = new List<char> { 'E', 'S', 'W', 'N' };
            foreach (Tuple<char, int> t in Instructions)
            {
                switch (t.Item1)
                {
                    case 'E':
                    case 'S':
                    case 'W':
                    case 'N':
                        Ferry.MoveNSteps(t.Item1, t.Item2);
                        break;
                    case 'L':
                        int Schmegres = t.Item2 / 90;
                        int l = Directions.IndexOf(Direction) - Schmegres;
                        if (l < 0)
                            l = l + 4;
                        Direction = Directions[l];
                        break;
                    case 'R':
                        Schmegres = t.Item2 / 90;
                        int r = Directions.IndexOf(Direction) + Schmegres;
                        if (r > 3)
                            r = r - 4;
                        Direction = Directions[r];
                        break;
                    case 'F':
                        Ferry.MoveNSteps(Direction, t.Item2);
                        break;
                    default:
                        break;
                }
            }
            ReturnValue = Ferry.ManhattanDistance(new Coordinate(0, 0));
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            Coordinate Ferry = new Coordinate(0, 0);
            char Direction = 'E';
            Coordinate Waypoint = new Coordinate(10, 1);
            List<char> Directions = new List<char> { 'E', 'S', 'W', 'N' };
            foreach (Tuple<char, int> t in Instructions)
            {
                switch (t.Item1)
                {
                    case 'E':
                    case 'S':
                    case 'W':
                    case 'N':
                        Waypoint.MoveNSteps(t.Item1, t.Item2);
                        break;
                    case 'L':
                        int Schmegres = t.Item2 / 90;
                        for (int i = 0; i < Schmegres; i++)
                        {
                            Waypoint = new Coordinate(Waypoint.y * -1, Waypoint.x);
                        }
                        break;
                    case 'R':
                        Schmegres = t.Item2 / 90;
                        for (int i = 0; i < Schmegres; i++)
                        {
                            Waypoint = new Coordinate(Waypoint.y, Waypoint.x * -1);
                        }
                        break;
                    case 'F':
                        Ferry.AddTo(new Coordinate(Waypoint.x * t.Item2, Waypoint.y * t.Item2));
                        break;
                    default:
                        break;

                }
                ;
            }
            ReturnValue = Ferry.ManhattanDistance(new Coordinate(0, 0));
            return ReturnValue.ToString();
        }
    }
}
