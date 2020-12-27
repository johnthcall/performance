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
        [Params(10, 200, 3000, 50000)]
        public int N;

        private static List<int> Ints;

        private static List<string> Strings;

        private static List<long> Longs;

        private static List<byte> Bytes;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var random = new Random(17850950);
            Ints = new List<int>();
            Strings = new List<string>();
            Longs = new List<long>();
            Bytes = new List<byte>();
            for (int i = 0; i < N; i++)
            {
                var item = random.Next();
                Strings.Add($"Test{item}");
                Ints.Add(item);
                Longs.Add(item);
                Bytes.Add((byte)item);
            }
        }

        [Benchmark]
        public List<string> StringToList()
        {
            return Strings.OrderBy(s => s).ToList();
        }

        [Benchmark]
        public string[] StringToArray()
        {
            return Strings.OrderBy(s => s).ToArray();
        }

        [Benchmark]
        public List<int> IntToList()
        {
            return Ints.OrderBy(s => s).ToList();
        }

        [Benchmark]
        public int[] IntToArray()
        {
            return Ints.OrderBy(s => s).ToArray();
        }

        [Benchmark]
        public List<long> LongToList()
        {
            return Longs.OrderBy(s => s).ToList();
        }

        [Benchmark]
        public long[] LongToArray()
        {
            return Longs.OrderBy(s => s).ToArray();
        }

        [Benchmark]
        public List<byte> ByteToList()
        {
            return Bytes.OrderBy(s => s).ToList();
        }

        [Benchmark]
        public byte[] ByteToArray()
        {
            return Bytes.OrderBy(s => s).ToArray();
        }
    }
}
