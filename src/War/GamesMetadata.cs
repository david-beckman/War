//-----------------------------------------------------------------------
// <copyright file="GamesMetadata.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace War
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>Summary data from multiple games.</summary>
    public class GamesMetadata
    {
        /// <summary>Initializes a new instance of the <see cref="GamesMetadata" /> class.</summary>
        /// <param name="items">The collection of game metadata the aggregate.</param>
        public GamesMetadata(IEnumerable<GameMetadata> items)
        {
            var metadata = (items ?? Enumerable.Empty<GameMetadata>()).Where(items => items != null).ToArray();
            this.Games = metadata.Length;
            this.BattlesPerCompletedGame = new Stats(metadata.Where(item => item.IsComplete).Select(item => item.Battles));
        }

        /// <summary>Gets the number of games played.</summary>
        /// <returns>The number of games player.</returns>
        public long Games { get; }

        /// <summary>Gets the percentage of games that were completed.</summary>
        /// <returns>The percentage, as an value between <value>0</value> and <value>1</value> inclusively, of games completed.</returns>
        public double CompletionRate => this.Games == 0 ? 1.0d : this.BattlesPerCompletedGame.Count * 1.0d / this.Games;

        /// <summary>Gets the summary statistics of the number of battles per game.</summary>
        /// <returns>The summary statistics of the number of battles per game.</returns>
        public Stats BattlesPerCompletedGame { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        public string ToString(IFormatProvider provider)
        {
            const string format = @"Games: {0:n0}
Completion Rate: {1:p}
Battles per Game:
{2}";
            return string.Format(provider, format, this.Games, this.CompletionRate, this.BattlesPerCompletedGame.ToString(provider, "  "));
        }
    }
}
