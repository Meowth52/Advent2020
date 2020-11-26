using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roy_T.AStar;

namespace Advent2020
{
    public class Coordinate : IEquatable<Coordinate>, IComparable<Coordinate>
    {
        public int x;
        public int y;
        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Coordinate(Coordinate c)
        {
            x = c.x;
            y = c.y;
        }
        public Coordinate()
        {

        }
        public bool IsOn(Coordinate c)
        {
            return (c.x == this.x && c.y == this.y);
        }
        public void AddTo(Coordinate A)
        {
            x += A.x;
            y += A.y;
        }
        public void MoveOneStep(char c)
        {
            switch (c)
            {
                case 'E':
                case 'R':
                    this.AddTo(new Coordinate(0, 1));
                    break;
                case 'W':
                case 'L':
                    this.AddTo(new Coordinate(0, -1));
                    break;
                case 'N':
                case 'U':
                    this.AddTo(new Coordinate(1, 0));
                    break;
                case 'S':
                case 'D':
                    this.AddTo(new Coordinate(-1, 0));
                    break;
            }
        }
        public Coordinate GetSum(Coordinate A)
        {
            int x2 = x + A.x;
            int y2 = y + A.y;
            return new Coordinate(x2, y2);
        }
        public bool IsInPositiveBounds(int x2, int y2)
        {
            return (x >= 0 && y >= 0 && x <= x2 && y <= y2);
        }
        public int ManhattanDistance(Coordinate coo)
        {
            return Math.Abs(this.x - coo.x) + Math.Abs(this.y - coo.y);
        }
        public Coordinate RelativePosition(Coordinate coo)
        {
            return new Coordinate(this.x - coo.x, this.y - coo.y);
        }
        //public Position GetPosition()
        //{
        //    return new Position(x, y);
        //}
        public override string ToString()
        {
            return x.ToString() + "," + y.ToString();
        }

        public override int GetHashCode()
        {
            int hCode = x ^ y;
            return hCode.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as Coordinate);
        }
        public bool Equals(Coordinate obj)
        {
            return obj != null && obj.x == x && obj.y == y;
        }
        public int CompareTo(Coordinate other)
        {
            if (this.x == other.x)
            {
                return this.y.CompareTo(other.y);
            }
            return this.x.CompareTo(other.x);
        }
        public void Assimilate(Coordinate c)
        {
            x = c.x;
            y = c.y;
        }
    }
    class CoordinateEqualityComparer : IEqualityComparer<Coordinate>
    {
        public bool Equals(Coordinate b1, Coordinate b2)
        {
            if (b2 == null && b1 == null)
                return true;
            else if (b1 == null | b2 == null)
                return false;
            else if (b1.x == b2.x && b1.y == b2.y)
                return true;
            else
                return false;
        }

        public int GetHashCode(Coordinate bx)
        {
            int hCode = bx.x ^ bx.y;
            return hCode.GetHashCode();
        }
    }
}
