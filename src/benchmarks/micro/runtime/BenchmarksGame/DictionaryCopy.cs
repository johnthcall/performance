using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using MicroBenchmarks;

namespace MicroBenchmarks.runtime
{
    [BenchmarkCategory(Categories.Libraries, Categories.Collections, Categories.GenericCollections)]
    public class DictionaryCopy
    {
        private static Dictionary<string, int> FullDictionary;
        
        private static Dictionary<string, int> HalfEmptyDictionary;

        static DictionaryCopy()
        {
            var size = 3000;
            FullDictionary = new Dictionary<string, int>(size, StringComparer.OrdinalIgnoreCase);
            HalfEmptyDictionary = new Dictionary<string, int>(size, StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < size; i++)
            {
                HalfEmptyDictionary[$"Test{i}"] = i;
                FullDictionary[$"Test{i}"] = i;
            }
            for (int i = 0; i < size; i += 2)
            {
                HalfEmptyDictionary.Remove($"Test{i}");
            }
        }

        [Benchmark]
        public void CopyConstructorFull()
        {
            new Dictionary<string, int>(FullDictionary, StringComparer.OrdinalIgnoreCase);
        }

        [Benchmark]
        public void CopyConstructorHalfEmpty()
        {
            new Dictionary<string, int>(HalfEmptyDictionary, StringComparer.OrdinalIgnoreCase);
        }
    }
}
