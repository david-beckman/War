//-----------------------------------------------------------------------
// <copyright file="Stats.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace War
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>The general statistics from a set of values.</summary>
    public class Stats
    {
        private static readonly string DefaultLinePrefix = string.Empty;

        /// <summary>Initializes a new instance of the <see cref="Stats" /> class.</summary>
        /// <param name="values">The set of values to get the statistics for.</param>
        public Stats(IEnumerable<long> values)
        {
            IList<long> list = (values ?? Enumerable.Empty<long>()).OrderBy(x => x).ToArray();

            this.Count = list.Count;
            this.Min = list.Count == 0 ? 0 : list[0];
            this.Max = list.Count == 0 ? 0 : list[list.Count - 1];
            this.Median = list.Count == 0 ? 0 :
                list.Count % 2 == 1 ? list[list.Count / 2] : (list[(list.Count / 2) - 1] + list[list.Count / 2]);

            var average = this.Average = this.Count == 0 ? 0d : list.Sum() * 1.0d / this.Count;
            this.StandardOfDeviation = list.Count == 0 ? 0 :
                Math.Sqrt(list.Select(x => Math.Pow(x - average, 2)).Sum() / list.Count);
        }

        /// <summary>Gets the number of elements in the set.</summary>
        /// <returns>The number of elements in the set.</returns>
        public long Count { get; }

        /// <summary>Gets the minimum value of the set.</summary>
        /// <returns>The minimum value of the set.</returns>
        public long Min { get; }

        /// <summary>Gets the maximum value of the set.</summary>
        /// <returns>The maximum value of the set.</returns>
        public long Max { get; }

        /// <summary>Gets the median of the set.</summary>
        /// <returns>The median of the set.</returns>
        public long Median { get; }

        /// <summary>Gets the average of the set.</summary>
        /// <returns>The average of the set.</returns>
        public double Average { get; }

        /// <summary>Gets the standard of deviation from <see cref="Average" />.</summary>
        /// <returns>The standard of deviation from <see cref="Average" />.</returns>
        public double StandardOfDeviation { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.ToString(CultureInfo.InvariantCulture, DefaultLinePrefix);
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>A string that represents the current object.</returns>
        public string ToString(IFormatProvider provider)
        {
            return this.ToString(provider, DefaultLinePrefix);
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="linePrefix">The string to include prior to every line.</param>
        /// <returns>A string that represents the current object.</returns>
        public string ToString(IFormatProvider provider, string linePrefix)
        {
            const string format = @"{0}Count: {1:n0}
{0}Min: {2:n0}
{0}Max: {3:n0}
{0}Median: {4:n0}
{0}Average: {5:n}
{0}Std of Dev: {6:n}";
            return string.Format(
                provider,
                format,
                linePrefix,
                this.Count,
                this.Min,
                this.Max,
                this.Median,
                this.Average,
                this.StandardOfDeviation);
        }
    }
}
