//-----------------------------------------------------------------------
// <copyright file="CardTests.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace War.Tests
{
    using System;

    using Xunit;

    public class CardTests
    {
        private readonly Random random = new Random();

        public static Card Next(Random random)
        {
            var face = FaceTests.Next(random);
            var suit = SuitTests.Next(random);

            return new Card(face, suit);
        }

        [Fact]
        public void ConstructorShouldErrorWithNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Card(null, Suit.Spades));
            Assert.Throws<ArgumentNullException>(() => new Card(Face.Ace, null));
        }

        [Fact]
        public void ConstructorShouldWork()
        {
            var face = FaceTests.Next(this.random);
            var suit = SuitTests.Next(this.random);

            var card = new Card(face, suit);

            Assert.Equal(face, card.Face);
            Assert.Equal(suit, card.Suit);
        }

        [Fact]
        public void IndicatorShouldBeConcatenationOfIndicators()
        {
            var card = Next(this.random);
            var indicator = card.Indicator;
            Assert.Equal(card.Face.Indicator, indicator[0]);
            Assert.Equal(card.Suit.Indicator, indicator[1]);
            Assert.Equal(2, indicator.Length);
        }

        [Fact]
        public void NameShouldBeConcatenationOfNames()
        {
            var card = Next(this.random);
            var name = card.Name;
            Assert.StartsWith(card.Face.Name, name, StringComparison.Ordinal);
            Assert.EndsWith(card.Suit.Name, name, StringComparison.Ordinal);
        }

        [Fact]
        public void ToStringShouldMatchName()
        {
            var card = Next(this.random);
            Assert.Equal(card.Name, card.ToString());
        }

        [Fact]
        public void GetHashCodeShouldHaveSameValueForEqualInstances()
        {
            var card1 = Next(this.random);
            var card2 = new Card(card1.Face, card1.Suit);

            Assert.Equal(card1.GetHashCode(), card2.GetHashCode());
        }

        [Fact]
        public void GetHashCodeShouldBeDifferentForDifferentInstances()
        {
            var card1 = Next(this.random);
            Card card2;

            do
            {
                card2 = Next(this.random);
            }
            while (card1.Equals(card2));

            Assert.NotEqual(card1.GetHashCode(), card2.GetHashCode());
        }

        [Fact]
        public void EqualsShouldBeFalseForNull()
        {
            var card = Next(this.random);
            Assert.False(card.Equals((Card)null));
        }

        [Fact]
        public void EqualsShouldBeFalseForNonCards()
        {
            var card = Next(this.random);
            Assert.False(card.Equals(card.ToString()));
        }

        [Fact]
        public void EqualsShouldBeTrueForEqualInstances()
        {
            var card1 = Next(this.random);
            var card2 = new Card(card1.Face, card1.Suit);

            Assert.True(card1.Equals(card2));
        }

        [Fact]
        public void EqualsShouldBeFalseForDifferentInstances()
        {
            var card1 = Next(this.random);
            Card card2;

            do
            {
                card2 = Next(this.random);
            }
            while (card1.Equals(card2));

            Assert.False(card1.Equals(card2));
        }

        [Fact]
        public void CompareToShouldHaveNullFirst()
        {
            Assert.True(Next(this.random).CompareTo(null) > 0);
        }

        [Fact]
        public void CompareToShouldFollowFacesFirst()
        {
            // Hearts > Spades but 2 < 3
            var lower = new Card(Face.Two, Suit.Hearts);
            var upper = new Card(Face.Three, Suit.Spades);

            Assert.True(lower.CompareTo(upper) < 0);
        }

        [Fact]
        public void CompareToShouldFollowSuitsWhenFacesMatch()
        {
            // Hearts > Spades
            var lower = new Card(Face.Two, Suit.Hearts);
            var upper = new Card(Face.Two, Suit.Spades);

            Assert.True(lower.CompareTo(upper) > 0);
        }

        [Fact]
        public void CompareToShouldBeEqualForEqualInstances()
        {
            var card1 = Next(this.random);
            var card2 = new Card(card1.Face, card1.Suit);

            Assert.True(card1.CompareTo(card2) == 0);
        }

        [Fact]
        public void CompareToShouldFollowAceRules()
        {
            var ace = new Card(Face.Ace, Suit.Hearts);
            var two = new Card(Face.Two, Suit.Hearts);

            Assert.True(ace.CompareTo(two) < 0);
            Assert.True(ace.CompareTo(two, false) < 0);
            Assert.True(ace.CompareTo(two, true) > 0);
        }
    }
}
