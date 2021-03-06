// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using BenchmarkDotNet.Attributes;
using MicroBenchmarks;

namespace Microsoft.Extensions.Caching.Memory.Tests
{
    [BenchmarkCategory(Categories.Libraries)]
    public class MemoryCacheTests
    {
        private MemoryCache _memCache;

        [GlobalSetup]
        public void Setup()
        {
            _memCache = new MemoryCache(new MemoryCacheOptions());
            for (var i = 0; i < 1024; i++)
            {
                _memCache.Set(i.ToString(), i.ToString());
            }
        }

        [GlobalCleanup]
        public void Cleanup() => _memCache.Dispose();

        [Benchmark]
        public object GetHit() => _memCache.Get("256");

        [Benchmark]
        public bool TryGetValueHit() => _memCache.TryGetValue("256", out _);

        [Benchmark]
        public object GetMiss() => _memCache.Get("-1");

        [Benchmark]
        public bool TryGetValueMiss() => _memCache.TryGetValue("-1", out _);

        [Benchmark]
        public object SetOverride() => _memCache.Set("512", "512");
    }
}
