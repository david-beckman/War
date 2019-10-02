//-----------------------------------------------------------------------
// <copyright file="SimpleBattle.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace War
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>A simple battle where the card faces do not match and there is a clear winner.</summary>
    public class SimpleBattle : Battle
    {
        /// <summary>Initializes a new instance of the <see cref="SimpleBattle" /> class.</summary>
        /// <param name="left">The card from the left player.</param>
        /// <param name="right">The card from the right player.</param>
        /// <exception cref="ArgumentException">The faces match.</exception>
        public SimpleBattle(Card left, Card right)
            : base(left, right)
        {
            if (left == null && right == null)
            {
                this.Winner = Winner.Tie;
            }
            else if (left == null)
            {
                this.Winner = Winner.Right;
            }
            else if (right == null)
            {
                this.Winner = Winner.Left;
            }
            else if (left.Face == right.Face)
            {
                throw new ArgumentException(Strings.Arg_FaceMatch, nameof(right));
            }
            else if (Face.CompareTo(left.Face, right.Face, true) > 0)
            {
                this.Winner = Winner.Left;
            }
            else
            {
                // left.Face < right.Face
                this.Winner = Winner.Right;
            }
        }

        /// <inheritdoc />
        public override Winner Winner { get; }

        /// <inheritdoc />
        internal override IEnumerable<Card> AllCards => new[] { this.Left, this.Right };

        /// <inheritdoc />
        public override string ToString(IFormatProvider provider)
        {
            return string.Format(provider, "{0} {1} {2}", this.Left?.Indicator ?? "xx", this.Right?.Indicator ?? "xx", this.Winner.ToString()[0]);
        }
    }
}
