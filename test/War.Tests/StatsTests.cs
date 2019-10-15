//-----------------------------------------------------------------------
// <copyright file="StatsTests.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace War.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;

    using Xunit;

    public class StatsTests
    {
        private Random random = new Random();

        public static IEnumerable<object[]> FormatProviderData()
        {
            yield return new[] { (IFormatProvider)null };
            yield return new[] { CultureInfo.InvariantCulture };
            yield return new[] { new CultureInfo("tr-TR") };
        }

        [Fact]
        public void ConstructorShouldWorkWithNullTheSameAsEmptyList()
        {
            var statsWithNull = new Stats(null);
            var statsWithEmpty = new Stats(Enumerable.Empty<long>());

            Assert.Equal(statsWithEmpty.Count, statsWithNull.Count);
            Assert.Equal(statsWithEmpty.Min, statsWithNull.Min);
            Assert.Equal(statsWithEmpty.Max, statsWithNull.Max);
            Assert.Equal(statsWithEmpty.Median, statsWithNull.Median);
            Assert.Equal(statsWithEmpty.Average, statsWithNull.Average);
            Assert.Equal(statsWithEmpty.StandardOfDeviation, statsWithNull.StandardOfDeviation);
        }

        [Fact]
        public void ConstructorShouldFindMedianCorrectlyOnOdd()
        {
            var stats = new Stats(Enumerable.Range(0, 9).Select(x => (long)x));

            Assert.Equal(9, stats.Count);
            Assert.Equal(0, stats.Min);
            Assert.Equal(8, stats.Max);
            Assert.Equal(4.0d, stats.Median);
            Assert.Equal(4.0d, stats.Average);
            Assert.Equal(2.582d, Math.Round(stats.StandardOfDeviation, 3));
        }

        [Fact]
        public void ConstructorShouldFindMedianCorrectlyOnEven()
        {
            var stats = new Stats(Enumerable.Range(0, 10).Select(x => (long)x));

            Assert.Equal(10, stats.Count);
            Assert.Equal(0, stats.Min);
            Assert.Equal(9, stats.Max);
            Assert.Equal(4.5d, stats.Median);
            Assert.Equal(4.5d, stats.Average);
            Assert.Equal(2.872d, Math.Round(stats.StandardOfDeviation, 3));
        }

        [Fact]
        public void ConstructorShouldFindAverageAndStandardOfDeviation()
        {
            var stats = new Stats(Enumerable.Range(0, 10).Select(_ => 4L));

            Assert.Equal(10, stats.Count);
            Assert.Equal(4, stats.Min);
            Assert.Equal(4, stats.Max);
            Assert.Equal(4.0d, stats.Median);
            Assert.Equal(4.0d, stats.Average);
            Assert.Equal(0.00d, Math.Round(stats.StandardOfDeviation, 3));
        }

        [Theory]
        [MemberData(nameof(FormatProviderData))]
        [SuppressMessage(
            "Microsoft.Globalization",
            "CA1305:SpecifyIFormatProvider",
            Justification = "Need to test the ToString() method without an IFormatProvider.")]
        public void ToStringShouldHaveAllValues(IFormatProvider provider)
        {
            var size = this.random.Next(40) + 10;
            var stats = new Stats(Enumerable.Range(0, size).Select(_ => (long)this.random.Next(50) + 975));

            string str;

            if (provider == null)
            {
                str = stats.ToString();
                provider = CultureInfo.InvariantCulture;
            }
            else
            {
                str = stats.ToString(provider);
            }

            Assert.Contains("Count: " + stats.Count, str, StringComparison.Ordinal);
            Assert.Contains("Min: " + stats.Min, str, StringComparison.Ordinal);
            Assert.Contains("Max: " + stats.Max.ToString("n0", provider), str, StringComparison.Ordinal);
            Assert.Contains("Median: " + stats.Median.ToString("n1", provider), str, StringComparison.Ordinal);
            Assert.Contains("Average: " + stats.Average.ToString("n", provider), str, StringComparison.Ordinal);
            Assert.Contains(
                "Std of Dev: " + stats.StandardOfDeviation.ToString("n", provider),
                str,
                StringComparison.Ordinal);
        }
    }
}
