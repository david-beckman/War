//-----------------------------------------------------------------------
// <copyright file="SuitTests.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace War.Tests
{
    using System;

    using Xunit;

    public class SuitTests
    {
        private readonly Random random = new Random();

        public static Suit Next(Random random)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            return random.Next(Suit.Suits);
        }
    }
}
