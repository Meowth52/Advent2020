using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day08 : Day
    {
        List<string[]> Instructions;
        Dictionary<int, Tuple<string, int>> Code;
        public Day08(string _input) : base(_input)
        {
            Instructions = this.parseListOfStringArrays2(_input);
            Code = new Dictionary<int, Tuple<string, int>>();
            int i = 1;
            foreach (string[] ss in Instructions)
            {
                Code.Add(i, new Tuple<string, int>(ss[0], Int32.Parse(ss[1])));
                i++;
            }
            ;
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            return IntMachine(Code).Item1.ToString();
        }
        public Tuple<int, bool> IntMachine(Dictionary<int, Tuple<string, int>> code)
        {
            int Accumulator = 0;
            bool EndedWell = false;
            int i = 1;
            List<int> ExecutedCodes = new List<int>();
            while (true) //we where testing infinit loops right?
            {
                if (i >= code.Count)
                {
                    EndedWell = true;
                    break;
                }
                string OpCode = code[i].Item1;
                switch (OpCode)
                {
                    case "acc":
                        Accumulator += code[i].Item2;
                        i++;
                        break;
                    case "jmp":
                        i += code[i].Item2;
                        break;
                    case "nop":
                        i++;
                        break;
                    default:
                        break;
                }
                if (ExecutedCodes.Contains(i))
                    break;
                else
                    ExecutedCodes.Add(i);
            }
            return new Tuple<int, bool>(Accumulator, EndedWell);
        }
        public string getPartTwo()
        {
            int ReturnValue = 0;
            return ReturnValue.ToString();
        }
    }
}
