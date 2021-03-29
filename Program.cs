using System;
using System.Linq;
using System.Collections.Generic;

namespace Lesson2_5
{
    static class Extensions
    {
        public static int CountWords(this string input)
        {
            return (from c in input
                    select c).Count();
        }

        public static void Add<TKey, TValue>(this Dictionary<TKey, TValue> obj, KeyValuePair<TKey, TValue> element)
        {
            obj.Add(element.Key, element.Value);
        }

        public static Dictionary<T, int> Frequency<T>(this List<T> input)
        {
            Dictionary<T, int> result = new Dictionary<T, int>();

            var query2 = from el in input
                         group el by el into arr
                         select new KeyValuePair<T, int>(arr.Key, arr.Count()) into arr2
                         orderby arr2.Value descending
                         select arr2;
            foreach(var el in query2)
            {
                result.Add(el);
            }

            return result;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Some test string".CountWords());

            List<int> input = new List<int>() { 1, 5, 10, 1, 10, 1 };
            Console.WriteLine("\n");
            foreach(var el in input.Frequency())
            {
                Console.WriteLine($"{el.Key} : {el.Value}");
            }

            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                { "four", 4 },
                { "two", 2 },
                { "one", 1 },
                { "three", 3 }
            };
            var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
            d = dict.OrderBy(p => p.Value);
            d = dict.OrderBy(GetValue);
            Console.WriteLine("\n");
            foreach(var pair in d)
            {
                Console.WriteLine($"{pair.Key} - {pair.Value}");
            }
        }

        public static int GetValue(KeyValuePair<string, int> p)
        {
            return p.Value;
        }
    }
}
