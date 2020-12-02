using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day02 : Day
    {
        string[] Instructions;
        public Day02(string _input) : base(_input)
        {
            Instructions = this.parseStringArray(_input);
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            int ReturnValue = 0;
            foreach (string s in Instructions)
            {
                MatchCollection Matches = new Regex("\\d+").Matches(s);
                int Min = Int32.Parse(Matches[0].Value);
                int Max = Int32.Parse(Matches[1].Value);
                Matches = new Regex("[a-z]+").Matches(s);
                string c = Matches[0].Value;
                string Password = Matches[1].Value;
                int PasswordMatches = (Password.Length - Password.Replace(c, "").Length);
                if (PasswordMatches >= Min && PasswordMatches <= Max)
                    ReturnValue++;
            }
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            foreach (string s in Instructions)
            {
                MatchCollection Matches = new Regex("\\d+").Matches(s);
                int Min = Int32.Parse(Matches[0].Value);
                int Max = Int32.Parse(Matches[1].Value);
                Matches = new Regex("[a-z]+").Matches(s);
                char c = Matches[0].Value[0];
                string Password = Matches[1].Value;
                if (Password[Min - 1] == c ^ Password[Max - 1] == c)
                    ReturnValue++;
            }
            return ReturnValue.ToString();
        }
    }
}
