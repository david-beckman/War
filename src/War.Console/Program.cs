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

    using Console = System.Console;

    internal class Program
    {
        private const int DefaultMaxIterations = 100000;

        private int? seed;
        private int maxIterations;

        private Program(int maxIterations, int? seed)
        {
            this.maxIterations = maxIterations;
            this.seed = seed;
        }

        private static void Main(string[] args)
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

            int? seed = null;
            if (dictionary.TryGetValue(nameof(seed), out var seedValue))
            {
                seed = seedValue;
            }

            int maxIterations = DefaultMaxIterations;
            if (dictionary.TryGetValue(nameof(maxIterations), out var max) && max > 0)
            {
                maxIterations = max;
            }

            new Program(maxIterations, seed).Start();
        }

        private void Start()
        {
            using (var game = this.seed == null ? new Game() : new Game(this.seed.Value))
            {
                while (game.Metadata.Battles < this.maxIterations && game.MoveNext())
                {
                    Console.Write(game.Metadata.Battles);
                    Console.Write(Strings.BattleCountSeparator);
                    Console.WriteLine(game.Current);
                }

                Console.WriteLine(game);
            }
        }
    }
}
