using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public abstract class Day
    {
        public MainView _mainView;
        public Day(string _input)
        {

        }
        public void SetMainView(MainView mainView)
        {
            this._mainView = mainView;
        }
        public abstract Tuple<string, string> getResult();

        public string parseJustOneLine(string input)
        {
            return input.Replace("\r\n", "");
        }
        public string[] parseStringArray(string input)
        {
            string Input = input.Replace("\r\n", "_");
            return Input.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
        }
        public List<string[]> parseListOfStringArrays(string input)
        {
            List<string[]> ReturnList = new List<string[]>();
            string[] RawInstructions = input.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in RawInstructions)
            {
                ReturnList.Add(s.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
            }
            return ReturnList;
        }
        public List<string[]> parseListOfStringArrays2(string input)
        {
            List<string[]> ReturnList = new List<string[]>();
            string[] RawInstructions = input.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in RawInstructions)
            {
                ReturnList.Add(s.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            }
            return ReturnList;
        }
        public List<List<int>> parseListOfIntegerLists(string input)
        {
            List<List<int>> ReturnList = new List<List<int>>();
            string Input = input.Replace("\r\n", "_");
            string[] RawInstructions = Input.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in RawInstructions)
            {
                MatchCollection Matches = Regex.Matches(s, @"-?\d+");
                List<int> IntList = new List<int>();
                foreach (Match m in Matches)
                {
                    int ParseInt = 0;
                    Int32.TryParse(m.Value, out ParseInt);
                    IntList.Add(ParseInt);
                }
                if (IntList.Count > 0)
                    ReturnList.Add(IntList);
            }
            return ReturnList;
        }
        public List<int> parseListOfInteger(string input)
        {
            List<int> ReturnList = new List<int>();
            MatchCollection Matches = Regex.Matches(input, @"-?\d+");
            foreach (Match m in Matches)
            {
                ReturnList.Add(Int32.Parse(m.Value));
            }
            return ReturnList;
        }
        public List<long> parseListOfLong(string input)
        {
            List<long> ReturnList = new List<long>();
            MatchCollection Matches = Regex.Matches(input, @"-?\d+");
            foreach (Match m in Matches)
            {
                ReturnList.Add(Int64.Parse(m.Value));
            }
            return ReturnList;
        }
    }
}
