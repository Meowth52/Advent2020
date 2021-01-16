using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day22 : Day
    {
        List<string[]> Instructions;
        Queue<int> Deck1;
        Queue<int> Deck2;
        Queue<int> RDeck1;
        Queue<int> RDeck2;

        public Day22(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            Instructions = this.parseListOfStringArrays(Input);
            Deck1 = new Queue<int>();
            foreach (string s in Instructions[0])
                if (!s.Contains("Player"))
                    Deck1.Enqueue(Int32.Parse(s));
            Deck2 = new Queue<int>();
            foreach (string s in Instructions[1])
                if (!s.Contains("Player"))
                    Deck2.Enqueue(Int32.Parse(s));
            RDeck1 = new Queue<int>(Deck1);
            RDeck2 = new Queue<int>(Deck2);
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            int ReturnValue = 0;
            while (Deck1.Count > 0 && Deck2.Count > 0)
            {
                int PlayerOneCard = Deck1.Dequeue();
                int PlayerTwoCard = Deck2.Dequeue();
                if (PlayerOneCard > PlayerTwoCard)
                {
                    Deck1.Enqueue(PlayerOneCard);
                    Deck1.Enqueue(PlayerTwoCard);
                }
                else
                {
                    Deck2.Enqueue(PlayerTwoCard);
                    Deck2.Enqueue(PlayerOneCard);
                }

            }
            ReturnValue = CountScore(Deck1.Count > 0 ? Deck1 : Deck2);
            return ReturnValue.ToString();
        }
        public int CountScore(Queue<int> Deck)
        {
            int ReturnValue = 0;
            for (int i = Deck.Count; i >= 1; i--)
            {
                ReturnValue += Deck.Dequeue() * i;
            }
            return ReturnValue;

        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            Crab(RDeck1, RDeck2);
            ReturnValue = CountScore(RDeck1.Count > 0 ? RDeck1 : RDeck2);
            return ReturnValue.ToString();
        }
        public bool Crab(Queue<int> Deck1, Queue<int> Deck2)
        {
            int ReturnValue = 0;
            HashSet<string> AllTheRounds = new HashSet<string>();
            while (Deck1.Count > 0 && Deck2.Count > 0)
            {
                bool PlayerOneWins = false;
                int PlayerOneCard = Deck1.Dequeue();
                int PlayerTwoCard = Deck2.Dequeue();
                if (PlayerOneCard <= Deck1.Count && PlayerTwoCard <= Deck2.Count)
                {
                    PlayerOneWins = Crab(new Queue<int>(Deck1.Take(PlayerOneCard)), new Queue<int>(Deck2.Take(PlayerTwoCard)));
                }
                else //Not recursive
                {
                    PlayerOneWins = (PlayerOneCard > PlayerTwoCard);
                }
                if (PlayerOneWins)
                {
                    Deck1.Enqueue(PlayerOneCard);
                    Deck1.Enqueue(PlayerTwoCard);
                }
                else
                {
                    Deck2.Enqueue(PlayerTwoCard);
                    Deck2.Enqueue(PlayerOneCard);
                }
                string Round = string.Join(",", Deck1) + string.Join(",", Deck2);
                if (AllTheRounds.Contains(Round))
                {
                    break;
                }
                else
                    AllTheRounds.Add(Round);
            }
            return (Deck1.Count > 0);
        }
    }
}
