//-----------------------------------------------------------------------
// <copyright file="SuitTests.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace War.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Xunit;

    public class SuitTests
    {
        private readonly Random random;

        private readonly Suit defaultSuit;

        public SuitTests()
        {
            this.random = new Random();
            this.defaultSuit = Next(this.random);
        }

        public static Suit Next(Random random)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            return random.Next(Suit.Suits);
        }

        public static IEnumerable<object[]> ListAllSuits()
        {
            return (new Suit[] { null }).Union(Suit.Suits).Select(suit => new Suit[] { suit });
        }

        [Theory]
        [MemberData(nameof(ListAllSuits))]
        public void ToStringShouldBeName(Suit suit)
        {
            Assert.Equal(suit?.Name, suit?.ToString());
        }

        [Fact]
        public void GetHashCodeShouldReturnADifferentValueForEachSuit()
        {
            Suit.Suits.Select(suit => new { Suit = suit, Hash = suit.GetHashCode() })
                .GroupBy(item => item.Hash)
                .All(groupItem =>
                {
                    Assert.Single(groupItem);
                    return true;
                });
        }

        [Fact]
        public void EqualsShouldBeFalseForNonSuits()
        {
            Assert.False(this.defaultSuit.Equals(this.defaultSuit.Name));
        }

        [Fact]
        public void EqualsShouldBeTrueForTheSameSuit()
        {
            Assert.True(this.defaultSuit.Equals((object)this.defaultSuit));
            Assert.True(Suit.Equals(this.defaultSuit, this.defaultSuit));
        }

        [Fact]
        public void CompareToShouldPutSpadesLow()
        {
            Assert.True(Suit.Spades.CompareTo(this.defaultSuit) <= 0);
            Assert.True(Suit.CompareTo(Suit.Spades, this.defaultSuit) <= 0);
        }

        [Fact]
        public void CompareToShouldPutNullFirst()
        {
            Assert.True(this.defaultSuit.CompareTo(null) > 0);
            Assert.True(Suit.CompareTo(this.defaultSuit, null) > 0);
            Assert.True(Suit.CompareTo(null, this.defaultSuit) < 0);
        }

        [Fact]
        public void CompareToShoulMatchNulls()
        {
            Assert.Equal(0, Suit.CompareTo(null, null));
        }

        [Theory]
        [MemberData(nameof(ListAllSuits))]
        public void LessThanShouldMatchCompareTo(Suit suit)
        {
            Assert.Equal(Suit.CompareTo(this.defaultSuit, suit) < 0, this.defaultSuit < suit);
            Assert.Equal(Suit.CompareTo(suit, this.defaultSuit) < 0, suit < this.defaultSuit);
        }

        [Theory]
        [MemberData(nameof(ListAllSuits))]
        public void LessThanOrEqualToShouldMatchCompareTo(Suit suit)
        {
            Assert.Equal(Suit.CompareTo(this.defaultSuit, suit) <= 0, this.defaultSuit <= suit);
            Assert.Equal(Suit.CompareTo(suit, this.defaultSuit) <= 0, suit <= this.defaultSuit);
        }

        [Theory]
        [MemberData(nameof(ListAllSuits))]
        public void EqualToShouldMatchCompareToAndEquals(Suit suit)
        {
            Assert.Equal(Suit.CompareTo(this.defaultSuit, suit) == 0, this.defaultSuit == suit);
            Assert.Equal(Suit.CompareTo(suit, this.defaultSuit) == 0, suit == this.defaultSuit);
            Assert.Equal(Suit.Equals(suit, this.defaultSuit), suit == this.defaultSuit);
        }

        [Theory]
        [MemberData(nameof(ListAllSuits))]
        public void NotEqualToShouldMatchCompareToAndEquals(Suit suit)
        {
            Assert.Equal(Suit.CompareTo(this.defaultSuit, suit) != 0, this.defaultSuit != suit);
            Assert.Equal(Suit.CompareTo(suit, this.defaultSuit) != 0, suit != this.defaultSuit);
            Assert.Equal(!Suit.Equals(suit, this.defaultSuit), suit != this.defaultSuit);
        }

        [Theory]
        [MemberData(nameof(ListAllSuits))]
        public void GreaterThanShouldMatchCompareTo(Suit suit)
        {
            Assert.Equal(Suit.CompareTo(this.defaultSuit, suit) > 0, this.defaultSuit > suit);
            Assert.Equal(Suit.CompareTo(suit, this.defaultSuit) > 0, suit > this.defaultSuit);
        }

        [Theory]
        [MemberData(nameof(ListAllSuits))]
        public void GreaterThanOrEqualToShouldMatchCompareTo(Suit suit)
        {
            Assert.Equal(Suit.CompareTo(this.defaultSuit, suit) >= 0, this.defaultSuit >= suit);
            Assert.Equal(Suit.CompareTo(suit, this.defaultSuit) >= 0, suit >= this.defaultSuit);
        }
    }
}
