using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace Advent2020
{
    public class Day23 : Day
    {
        Dictionary<int, int> Cups;
        int StartCup;
        int LargestCup;
        public Day23(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            Cups = new Dictionary<int, int>();
            Input = Regex.Match(Input, @"\d+").Value;
            StartCup = Int32.Parse(Input[0].ToString());
            LargestCup = 0;
            for (int i = 0; i < Input.Length; i++)
            {
                int Next;
                if (i == Input.Length - 1)
                    Next = Int32.Parse(Input[0].ToString());
                else
                    Next = Int32.Parse(Input[i + 1].ToString());
                if (Next > LargestCup)
                    LargestCup = Next;
                Cups.Add(Int32.Parse(Input[i].ToString()), Next);
            }
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            string ReturnValue = "";
            Dictionary<int, int> Cups1 = new Dictionary<int, int>(Cups);
            MoveCups(ref Cups1, 100);
            int Current = 1;
            while (true)
            {
                Current = Cups1[Current];
                if (Current == 1)
                    break;
                ReturnValue += Current;
            }
            return ReturnValue;
        }
        public void MoveCups(ref Dictionary<int, int> Cups, int Rounds)
        {
            int Current = StartCup;
            for (int i = 1; i <= Rounds; i++)
            {
                int Next = Current;
                List<int> PickUp = new List<int>();
                for (int c = 0; c < 3; c++)
                {
                    Next = Cups[Next];
                    PickUp.Add(Next);
                }
                Cups[Current] = Cups[Next];
                int Destination = Current;
                while (true)
                {
                    Destination--;
                    if (Destination < 1)
                        Destination = LargestCup;
                    if (!PickUp.Contains(Destination))
                        break;
                }
                Cups[PickUp.Last()] = Cups[Destination];
                Cups[Destination] = PickUp.First();
                Current = Cups[Current];
            }
        }
        public string getPartTwo()
        {
            long ReturnValue = 0;
            int OneMillion = 1000000;
            int LastCup = Cups.Last().Key;
            for (int i = LargestCup + 1; i <= OneMillion; i++)
            {
                Cups.Add(i, i + 1);
            }
            Cups[LastCup] = LargestCup + 1;
            LargestCup = OneMillion;
            Cups[OneMillion] = StartCup;
            MoveCups(ref Cups, 10 * OneMillion);
            ReturnValue = Cups[1];
            ReturnValue = ReturnValue * Cups[(int)ReturnValue];
            return ReturnValue.ToString();
        }
    }
}
