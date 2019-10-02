//-----------------------------------------------------------------------
// <copyright file="DeepBattle.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace War
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    ///     A complex battle where the initial card faces match, there are casualties, and a second battle to determine the
    ///     <see cref="Winner" />. Note that in the case that there is no second battle (ie <see cref="Next" /> is <value>null</value>),
    ///     the <see cref="Winner" /> is a <see cref="Winner.Tie" />.
    /// </summary>
    public class DeepBattle : Battle
    {
        /// <summary>Initializes a new instance of the <see cref="DeepBattle" /> class.</summary>
        /// <param name="left">The card from the left player.</param>
        /// <param name="right">The card from the right player.</param>
        /// <param name="leftCasualties">The casualties from the left player or <value>null</value> if the player is out of cards.</param>
        /// <param name="rightCasualties">
        ///     The casualties from the right player or <value>null</value> if the player is out of cards.
        /// </param>
        /// <param name="next">The next battle or <value>null</value> if both players are out of cards.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="left" /> or <paramref name="right" /> is <value>null</value>.
        /// </exception>
        /// <exception cref="ArgumentException">The <paramref name="left" /> and <paramref name="right" /> faces do not match.</exception>
        public DeepBattle(Card left, Card right, IReadOnlyList<Card> leftCasualties, IReadOnlyList<Card> rightCasualties, Battle next)
            : base(left, right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            if (left.Face != right.Face)
            {
                throw new ArgumentException(Strings.Arg_FaceMismatch, nameof(right));
            }

            this.LeftCasualties = leftCasualties ?? Enumerable.Empty<Card>().ToArray();
            this.RightCasualties = rightCasualties ?? Enumerable.Empty<Card>().ToArray();
            this.Next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <inheritdoc />
        public override Winner Winner => this.Next?.Winner ?? Winner.Tie;

        /// <summary>Gets the casualties from the left player.</summary>
        /// <returns>The casualties from the left player.</returns>
        public IReadOnlyList<Card> LeftCasualties { get; }

        /// <summary>Gets the casualties from the right player.</summary>
        /// <returns>The casualties from the right player.</returns>
        public IReadOnlyList<Card> RightCasualties { get; }

        /// <summary>Gets the next battle.</summary>
        /// <returns>The next battle.</returns>
        public Battle Next { get; }

        /// <summary>Gets the depth of this battle.</summary>
        /// <returns>
        ///     The depth of this battle: this is one more than the depth of <see cref="Next" /> or <value>1</value> if <see cref="Next" />
        ///     is <value>null</value>.
        /// </returns>
        internal int Depth => 1 + ((this.Next as DeepBattle)?.Depth ?? 0);

        /// <inheritdoc />
        internal override IEnumerable<Card> AllCards =>
            new[] { this.Left, this.Right }
            .Union(this.LeftCasualties)
            .Union(this.RightCasualties)
            .Union(this.Next.AllCards);

        /// <inheritdoc />
        public override string ToString(IFormatProvider provider)
        {
            const string format = @"{0} {1} T
  -> ({2}) ({3})
  -> {4}";

            return string.Format(
                provider,
                format,
                this.Left.Indicator,
                this.Right.Indicator,
                string.Join(", ", this.LeftCasualties.Select(c => c.Indicator)),
                string.Join(", ", this.RightCasualties.Select(c => c.Indicator)),
                this.Next);
        }
    }
}
