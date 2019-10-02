//-----------------------------------------------------------------------
// <copyright file="Battle.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace War
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>An abstract representation of a battle.</summary>
    public abstract class Battle
    {
        /// <summary>Initializes a new instance of the <see cref="Battle" /> class.</summary>
        /// <param name="left">The card from the left player.</param>
        /// <param name="right">The card from the right player.</param>
        internal Battle(Card left, Card right)
        {
            this.Left = left;
            this.Right = right;
        }

        /// <summary>Gets the card from the left player.</summary>
        /// <returns>The card from the left player.</returns>
        public Card Left { get; }

        /// <summary>Gets the card from the right player.</summary>
        /// <returns>The card from the right player.</returns>
        public Card Right { get; }

        /// <summary>Gets the winner of this battle.</summary>
        /// <returns>The winner of this battle.</returns>
        public abstract Winner Winner { get; }

        /// <summary>Gets the set of all cards used in the battle.</summary>
        /// <returns>The set of all cards used in the battle.</returns>
        internal abstract IEnumerable<Card> AllCards { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        public abstract string ToString(IFormatProvider provider);
    }
}
