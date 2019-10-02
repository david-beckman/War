//-----------------------------------------------------------------------
// <copyright file="Hand.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace War
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     A representation of a hand in War. Cards are removed from the top of the hand via the <see cref="Take()" /> and
    ///     <see cref="Take(int)" /> methods and added to the bottom via the <see cref="Add(IEnumerable{Card})" /> method.
    /// </summary>
    public class Hand
    {
        private readonly Queue<Card> cards = new Queue<Card>();

        /// <summary>Initializes a new instance of the <see cref="Hand" /> class.</summary>
        public Hand()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Hand" /> class.</summary>
        /// <param name="cards">The set of cards that constitute the initial hand.</param>
        public Hand(IEnumerable<Card> cards)
        {
            this.Add(cards);
        }

        /// <summary>Gets a value indicating whether ths hand is empty.</summary>
        /// <returns><value>true</value> if the hand is empty; otherwise, <value>false</value>.</returns>
        public bool IsEmpty => this.cards.Count == 0;

        /// <summary>Takes the next card from the top of the hand.</summary>
        /// <returns>The top card from the hand or <value>null</value> if <see cref="IsEmpty" />.</returns>
        public Card Take()
        {
            return this.IsEmpty ? null : this.cards.Dequeue();
        }

        /// <summary>Takes the top cards up-to the specified <paramref name="amount" /> from the hand and returns them.</summary>
        /// <param name="amount">The amount of cards to take. Note that a negative amount is interpreted to be the same as zero.</param>
        /// <returns>
        ///     The top cards up-to the specified <paramref name="amount" />. If the hand is empty, an empty set is returned. If there are
        ///     fewer cards than the specified <paramref name="amount" />, all remaining cards are returned.
        /// </returns>
        public IReadOnlyList<Card> Take(int amount)
        {
            var result = new List<Card>(amount);

            for (var i = 0; i < amount; i++)
            {
                var next = this.Take();
                if (next == null)
                {
                    break;
                }

                result.Add(next);
            }

            return result;
        }

        /// <summary>Add a set of cards to the bottom of the hand. Note that <value>null</value> cards are ignored.</summary>
        /// <param name="cards">The set of cards to add.</param>
        public void Add(IEnumerable<Card> cards)
        {
            if (cards == null)
            {
                return;
            }

            foreach (var card in cards.Where(c => c != null))
            {
                this.cards.Enqueue(card);
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Join(" ", this.cards.Select(card => card.Indicator));
        }
    }
}
