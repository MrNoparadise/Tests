using System;
using System.Collections.Generic;
using System.Linq;

namespace ArrayTask
{
    public class Multiplicator
    {
        public List<int> List { get; set; }

        public void GenerateList()
        {
            List = new List<int>();
            var rand = new Random();
            for (int i = 1; i < 10; i++)
            {
                List.Add(rand.Next(1000));
            }
        }

        public void EasySort()
        {
            List.Sort();
        }

        public void Execute()
        {
            GenerateList();
            EasySort();
            Console.WriteLine($"EASY SORT(MAX x2): {List.Last() * 2}");
            Console.WriteLine($"EASY SORT(MIN x2): {List.First() * 2}");
        }
    }
}
