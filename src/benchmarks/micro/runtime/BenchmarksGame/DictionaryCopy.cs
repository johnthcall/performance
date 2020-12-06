using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using MicroBenchmarks;

namespace MicroBenchmarks.runtime
{
    [BenchmarkCategory(Categories.Libraries, Categories.Collections, Categories.GenericCollections)]
    public class DictionaryCopy
    {
        [Params(10, 200, 3000)]
        public int N;

        private static SortedDictionary<int, int> IntDictionary;
        
        private static SortedDictionary<string, string> StringDictionary;

        private static SortedDictionary<int, int> IntDictionaryHalf;
        
        private static SortedDictionary<string, string> StringDictionaryHalf;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var random = new Random(17850950);
            IntDictionary = new SortedDictionary<int, int>();
            StringDictionary = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            IntDictionaryHalf = new SortedDictionary<int, int>();
            StringDictionaryHalf = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var list = new List<int>(N);
            for (int i = 0; i <N; i++)
            {
                list.Add(random.Next());
            }
            for (int i = 0; i < N; i++)
            {
                var item = list[i];
                StringDictionary.Add($"Test{item}", $"TestValue{item}");
                IntDictionary.Add(item, -item);
                StringDictionaryHalf.Add($"Test{item}", $"TestValue{item}");
                IntDictionaryHalf.Add(item, -item);
            }
            for (int i = 1; i < N; i += 2)
            {
                var item = list[i];
                StringDictionaryHalf.Remove($"Test{item}");
                IntDictionaryHalf.Remove(item);
            }
        }

        [Benchmark]
        public void FullString()
        {
            new SortedDictionary<string, string>(StringDictionary, StringComparer.OrdinalIgnoreCase);
        }

        [Benchmark]
        public void FullInt()
        {
            new SortedDictionary<int, int>(IntDictionary);
        }

        [Benchmark]
        public void HalfFullString()
        {
            new SortedDictionary<string, string>(StringDictionaryHalf, StringComparer.OrdinalIgnoreCase);
        }

        [Benchmark]
        public void HalfFullInt()
        {
            new SortedDictionary<int, int>(IntDictionaryHalf);
        }
    }
}
