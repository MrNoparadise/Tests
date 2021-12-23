using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HugeArrayTask
{
    public class MaxFinder
    {
        public List<int> Giant { get; set; }

        public void GenerateGiant()
        {
            Giant = new List<int>();
            var rand = new Random();
            for (int i = 1; i < 1000000; i++)
            {
                Giant.Add(rand.Next());
            }
        }

		public Pair DefaultMethod(List<int> arr)
        {
			if (arr == null || arr.Count < 1)
				return new Pair() { Max = 0, Min = 0 };

			int min = arr[0];
			int max = arr[0];
			arr.ForEach(a =>
			{
				if (max < a)
				{
					max = a;
				}
				else if (min > a)
				{
					min = a;
				}
			});
			return new Pair() { Max = max, Min = min };
		}
		public Pair GetMinMax(List<int> arr, int low, int high)
		{
			Pair result = new Pair();
			if (low == high)
			{
				result.Max = arr[low];
				result.Min = arr[low];
				return result;
			}
			if (high == low + 1)
			{
				if (arr[low] > arr[high])
				{
					result.Max = arr[low];
					result.Min = arr[high];
				}
				else
				{
					result.Max = arr[high];
					result.Min = arr[low];
				}
				return result;
			}
			int mid = (low + high) / 2;
			var left = GetMinMax(arr, low, mid);
			var right = GetMinMax(arr, mid + 1, high);
			if (left.Min < right.Min)
				result.Min = left.Min;
			else
				result.Min = right.Min;

			if (left.Max > right.Max)
				result.Max = left.Max;
			else
				result.Max = right.Max;
			return result;
		}

		public void Execute()
        {
			var stopwatch = new Stopwatch();
			GenerateGiant();
			stopwatch.Start();
			var defaultRes = DefaultMethod(Giant);
			Console.WriteLine($"DEFAULT: MIN = {defaultRes.Min} MAX = {defaultRes.Max} ELAPSED: {stopwatch.ElapsedMilliseconds} ms");
			stopwatch.Stop();
			stopwatch.Start();
			var fasten =  GetMinMax(Giant, 0, Giant.Count - 1);
			Console.WriteLine($"FAST(RECCURSION): MIN = {fasten.Min} MAX = {fasten.Max} ELAPSED: {stopwatch.ElapsedMilliseconds} ms");
			stopwatch.Stop();
			stopwatch.Start();
			var fastest = new Pair() { Max = Giant.Max(), Min = Giant.Min() };
			Console.WriteLine($"FASTEST(MIN/MAX)(TIME X2): MIN = {fastest.Min} MAX = {fastest.Max} ELAPSED: {stopwatch.ElapsedMilliseconds} ms");
			stopwatch.Stop();
		}
	}
}
