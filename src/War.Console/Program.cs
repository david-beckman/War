//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace War.Console
{
    using Console = System.Console;

    internal class Program
    {
        private static void Main()
        {
            using (var game = new Game())
            {
                while (game.Metadata.Battles < 100000 && game.MoveNext())
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
