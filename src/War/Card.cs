//-----------------------------------------------------------------------
// <copyright file="Card.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace War
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    /// <summary>A representation of the 52 standard playing cards.</summary>
    public class Card : IEquatable<Card>, IComparable<Card>
    {
        /// <summary>A flag indicating if an <see cref="Face.Ace" /> should be considered high by default during comparisons.</summary>
        /// <seealso cref="Face.DefaultAceHigh" />
        public const bool DefaultAceHigh = Face.DefaultAceHigh;

        /// <summary>Initializes a new instance of the <see cref="Card" /> class.</summary>
        /// <param name="face">The face of the card.</param>
        /// <param name="suit">The suit of the card.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="face" /> or <paramref name="suit" /> are <value>null</value>.
        /// </exception>
        public Card(Face face, Suit suit)
        {
            this.Face = face ?? throw new ArgumentNullException(nameof(face));
            this.Suit = suit ?? throw new ArgumentNullException(nameof(suit));
            this.Indicator = string.Format(CultureInfo.InvariantCulture, "{0}{1}", this.Face.Indicator, this.Suit.Indicator);
        }

        /// <summary>Gets the <see cref="Face" /> of the <see cref="Card" />.</summary>
        /// <returns>The <see cref="Face" /> of the <see cref="Card" />.</returns>
        public Face Face { get; }

        /// <summary>Gets the <see cref="Suit" /> of the <see cref="Card" />.</summary>
        /// <returns>The <see cref="Suit" /> of the <see cref="Card" />.</returns>
        public Suit Suit { get; }

        /// <summary>Gets a shortened string that represents the card.</summary>
        /// <returns>
        ///     A 2 character string, <see cref="Face.Indicator" /> followed by <see cref="Suit.Indicator" />, that represents
        ///     the card.
        /// </returns>
        public string Indicator { get; }

        /// <summary>Gets the full name of the card.</summary>
        /// <returns>The full name of the card.</returns>
        public string Name => this.Face.Name + " of " + this.Suit.Name;

        /// <summary>Determines whether two specified <see cref="Card" /> objects have the same value.</summary>
        /// <param name="left">The first card to compare.</param>
        /// <param name="right">The second card to compare.</param>
        /// <returns>
        ///     <value>true</value> if the value of <paramref name="left" /> is the same as the value of <paramref name="right" />;
        ///     otherwise, <value>false</value>. If both <paramref name="left" /> and <paramref name="right" /> are <value>null</value>,
        ///     the method returns <value>true</value>.
        /// </returns>
        public static bool operator ==(Card left, Card right)
        {
            return Equals(left, right);
        }

        /// <summary>Determines whether two specified <see cref="Card" /> objects have different values.</summary>
        /// <param name="left">The first card to compare.</param>
        /// <param name="right">The second card to compare.</param>
        /// <returns>
        ///     <value>true</value> if the value of <paramref name="left" /> is different from the value of <paramref name="right" />;
        ///     otherwise, <value>false</value>.
        /// </returns>
        public static bool operator !=(Card left, Card right)
        {
            return !Equals(left, right);
        }

        /// <summary>Indicates whether a specified <see cref="Card" /> is less than another specified <see cref="Card" />.</summary>
        /// <param name="left">The first card to compare.</param>
        /// <param name="right">The second card to compare.</param>
        /// <returns>
        ///     <value>true</value>if the value of <paramref name="left" /> is less than the value of <paramref name="right" />;
        ///     otherwise, <value>false</value>.
        /// </returns>
        public static bool operator <(Card left, Card right)
        {
            return CompareTo(left, right) < 0;
        }

        /// <summary>
        ///     Indicates whether a specified <see cref="Card" /> is less than or equal to another specified <see cref="Card" />.
        /// </summary>
        /// <param name="left">The first card to compare.</param>
        /// <param name="right">The second card to compare.</param>
        /// <returns>
        ///     <value>true</value>if the value of <paramref name="left" /> is less than or equal to the value of
        ///     <paramref name="right" />; otherwise, <value>false</value>.
        /// </returns>
        public static bool operator <=(Card left, Card right)
        {
            return CompareTo(left, right) <= 0;
        }

        /// <summary>Indicates whether a specified <see cref="Card" /> is greater than another specified <see cref="Card" />.</summary>
        /// <param name="left">The first card to compare.</param>
        /// <param name="right">The second card to compare.</param>
        /// <returns>
        ///     <value>true</value>if the value of <paramref name="left" /> is greater than the value of <paramref name="right" />;
        ///     otherwise, <value>false</value>.
        /// </returns>
        public static bool operator >(Card left, Card right)
        {
            return CompareTo(left, right) > 0;
        }

        /// <summary>
        ///     Indicates whether a specified <see cref="Card" /> is greater than or equal to another specified <see cref="Card" />.
        /// </summary>
        /// <param name="left">The first card to compare.</param>
        /// <param name="right">The second card to compare.</param>
        /// <returns>
        ///     <value>true</value>if the value of <paramref name="left" /> is greater than or equal to the value of
        ///     <paramref name="right" />; otherwise, <value>false</value>.
        /// </returns>
        public static bool operator >=(Card left, Card right)
        {
            return CompareTo(left, right) >= 0;
        }

        /// <summary>Determines whether two specified <see cref="Card" /> objects have the same value.</summary>
        /// <param name="left">The first card to compare.</param>
        /// <param name="right">The second card to compare.</param>
        /// <returns>
        ///     <value>true</value> if the value of <paramref name="left" /> is the same as the value of <paramref name="right" />;
        ///     otherwise, <value>false</value>. If both <paramref name="left" /> and <paramref name="right" /> are <value>null</value>,
        ///     the method returns <value>true</value>.
        /// </returns>
        public static bool Equals(Card left, Card right)
        {
            return left is null ? right is null : left.Equals(right);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Name;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (this.Face.GetHashCode() * Suit.Suits.Count) + this.Suit.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Card);
        }

        /// <inheritdoc />
        public bool Equals(Card other)
        {
            return other != null && this.Face == other.Face && this.Suit == other.Suit;
        }

        /// <inheritdoc />
        public int CompareTo(Card other)
        {
            return this.CompareTo(other, DefaultAceHigh);
        }

        /// <summary>
        ///     Compares the current instance with another object of the same type and returns an integer that indicates whether the
        ///     current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <param name="aceHigh">A flag indicating whether the <see cref="Face.Ace" /> is high.</param>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has these meanings:
        ///     <list type="table">
        ///         <listheader><term>Value</term><term>Meaning</term></listheader>
        ///         <item>
        ///             <term>Less than zero</term>
        ///             <term>This instance precedes <paramref name="other" /> in the sort order.</term>
        ///         </item>
        ///         <item>
        ///             <term>Zero</term>
        ///             <term>This instance occurs in the same position in the sort order as <paramref name="other" />.</term>
        ///         </item>
        ///         <item>
        ///             <term>Greater than zero</term>
        ///             <term>This instance follows <paramref name="other" /> in the sort order.</term>
        ///         </item>
        ///     </list>
        /// </returns>
        public int CompareTo(Card other, bool aceHigh)
        {
            // Null should come first
            if (other == null)
            {
                return 1;
            }

            var result = this.Face.CompareTo(other.Face, aceHigh);
            if (result != 0)
            {
                return result;
            }

            return this.Suit.CompareTo(other.Suit);
        }

        private static int CompareTo(Card left, Card right)
        {
            if (left is null)
            {
                return right is null ? 0 : -right.CompareTo(null);
            }

            return left.CompareTo(right);
        }
    }
}
