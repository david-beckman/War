//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace War.Console
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using Console = System.Console;

    internal class Program
    {
        /*
         * Running 1,000,000 games with a battles limit of 1,000,000 resulted in a max of 29,527 battles in a completed game. Bumping that
         * by ~50% to play it safe.
         */
        private const int DefaultMaxBattles = 45000;

        private int maxBattles;

        private Program(int maxBattles)
        {
            this.maxBattles = maxBattles;
        }

        private static async Task Main(string[] args)
        {
            var dictionary = (args ?? Enumerable.Empty<string>())
                .Where(arg => !string.IsNullOrEmpty(arg) && arg.Contains("=", StringComparison.Ordinal))
                .Select(arg => arg.Split("=", StringSplitOptions.RemoveEmptyEntries))
                .Select(arg => arg.Length == 2 && int.TryParse(arg[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var value) ?
                    (key: arg[0].Trim(), value) :
                    (key: null, 0))
                .Where(arg => !string.IsNullOrEmpty(arg.key))
                .GroupBy(arg => arg.key, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(arg => arg.Key, arg => arg.First().value, StringComparer.OrdinalIgnoreCase);

            int maxBattles = DefaultMaxBattles;
            if (dictionary.TryGetValue(nameof(maxBattles), out var max) && max > 0)
            {
                maxBattles = max;
            }

            int games = 1;
            if (dictionary.TryGetValue(nameof(games), out var count) && count >= 1)
            {
                games = count;
            }

            int? seed = null;
            if (dictionary.TryGetValue(nameof(seed), out var seedValue))
            {
                seed = seedValue;
            }

            await new Program(maxBattles).StartAsync(games, seed).ConfigureAwait(false);
        }

        private async Task StartAsync(int games, int? seed = null)
        {
            if (games == 1)
            {
                Console.WriteLine(this.RunGame(true, seed));
            }
            else
            {
                var tasks = Enumerable
                    .Range(0, games)
                    .Select(offset => seed == null ? (int?)null : seed.Value + offset)
                    .Select(s => Task.Run(() => this.RunGame(false, s)));
                Console.WriteLine(new GamesMetadata(await Task.WhenAll(tasks).ConfigureAwait(false)));
            }
        }

        private GameMetadata RunGame(bool writeBattles, int? seed = null)
        {
            using (var game = seed == null ? new Game() : new Game(seed.Value))
            {
                while (game.Metadata.Battles < this.maxBattles && game.MoveNext())
                {
                    if (writeBattles)
                    {
                        Console.Write(game.Metadata.Battles);
                        Console.Write(Strings.BattleCountSeparator);
                        Console.WriteLine(game.Current);
                    }
                }

                return game.Metadata;
            }
        }
    }
}
