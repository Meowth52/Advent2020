using System;
using System.Collections.Generic;

namespace Advent2020
{
    public class Day21 : Day
    {
        List<Food> Foods;
        Dictionary<string, List<string>> Allergens;
        List<string> Keys;
        public Day21(string _input) : base(_input)
        {
            Foods = new List<Food>();
            Allergens = new Dictionary<string, List<string>>();
            string[] input = this.parseStringArray(_input);
            foreach (string s in input)
            {
                Foods.Add(new Food(s));
            }
        }
        public override Tuple<string, string> getResult()
        {
            return Tuple.Create(getPartOne(), getPartTwo());
        }
        public string getPartOne()
        {
            int ReturnValue = 0;
            foreach (Food f in Foods)
            {
                foreach (string a in f.Allergens)
                {
                    if (!Allergens.ContainsKey(a))
                    {
                        Allergens.Add(a, new List<string>(f.Ingridients));
                    }
                }
            }
            Keys = new List<string>(Allergens.Keys);
            foreach (string k in Keys)
            {
                foreach (Food f in Foods)
                {
                    if (f.Allergens.Contains(k))
                    {
                        List<string> Next = new List<string>();
                        foreach (string s in Allergens[k])
                        {
                            if (f.Ingridients.Contains(s))
                            {
                                Next.Add(s);
                            }
                        }
                        Allergens[k] = new List<string>(Next);
                    }
                }
            }
            while (true)
            {
                bool GetOut = true;
                foreach (string k in Keys)
                {
                    if (Allergens[k].Count == 1)
                    {
                        string Current = Allergens[k][0];
                        foreach (string k2 in Keys)
                        {
                            if (Allergens[k2].Count > 1 && Allergens[k2].Contains(Current))
                            {
                                Allergens[k2].Remove(Current);
                            }
                        }
                    }
                    else
                        GetOut = false;

                }
                if (GetOut)
                    break;
            }
            List<string> AllergenIngridients = new List<string>();
            foreach (KeyValuePair<string, List<string>> k in Allergens)
            {
                AllergenIngridients.AddRange(k.Value);
            }
            foreach (Food f in Foods)
            {
                foreach (string s in f.Ingridients)
                {
                    if (!AllergenIngridients.Contains(s))
                    {
                        ReturnValue++;
                    }
                }
            }
            return ReturnValue.ToString();
        }
        public string getPartTwo()
        {
            string ReturnValue = "";
            Keys.Sort();
            foreach (string k in Keys)
            {
                ReturnValue += Allergens[k][0] + ",";
            }
            ReturnValue = ReturnValue.TrimEnd(new[] { ',' });
            return ReturnValue.ToString();
        }
        public class Food
        {
            public List<string> Ingridients;
            public List<string> Allergens;
            public Food(string input)
            {
                string[] splitted = input.Replace(")", "").Split(new[] { " (contains " }, StringSplitOptions.RemoveEmptyEntries);
                Ingridients = new List<string>(splitted[0].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries));
                Allergens = new List<string>(splitted[1].Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries));
            }
        }
    }
}
