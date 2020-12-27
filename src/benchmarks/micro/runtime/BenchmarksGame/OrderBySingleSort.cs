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
        public List<string> StringList()
        {
            return Strings.OrderBy(s => s).ToList();
        }

        [Benchmark]
        public string[] StringArray()
        {
            return Strings.OrderBy(s => s).ToArray();
        }

        [Benchmark]
        public List<int> IntList()
        {
            return Ints.OrderBy(s => s).ToList();
        }

        [Benchmark]
        public int[] IntArray()
        {
            return Ints.OrderBy(s => s).ToArray();
        }

        [Benchmark]
        public List<long> LongList()
        {
            return Longs.OrderBy(s => s).ToList();
        }

        [Benchmark]
        public long[] LongArray()
        {
            return Longs.OrderBy(s => s).ToArray();
        }

        [Benchmark]
        public List<byte> ByteList()
        {
            return Bytes.OrderBy(s => s).ToList();
        }

        [Benchmark]
        public byte[] ByteArray()
        {
            return Bytes.OrderBy(s => s).ToArray();
        }
    }
}
