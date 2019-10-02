//-----------------------------------------------------------------------
// <copyright file="Suit.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace War
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>A representation of the 4 standard playing card suits.</summary>
    public class Suit : IEquatable<Suit>, IComparable<Suit>
    {
        /// <summary>A spade.</summary>
        public static readonly Suit Spades = new Suit(0, "Spades");

        /// <summary>A club.</summary>
        public static readonly Suit Clubs = new Suit(1, "Clubs");

        /// <summary>A diamond.</summary>
        public static readonly Suit Diamonds = new Suit(2, "Diamonds");

        /// <summary>A heart.</summary>
        public static readonly Suit Hearts = new Suit(3, "Hearts");

        /// <summary>The set of all 4 suits.</summary>
        public static readonly IReadOnlyList<Suit> Suits = new Suit[]
        {
            Spades,
            Clubs,
            Diamonds,
            Hearts,
        };

        private readonly int ordinal;

        private Suit(int ordinal, string name)
        {
            this.ordinal = ordinal;
            this.Name = name;
        }

        /// <summary>Gets a single character indicator for the suit.</summary>
        /// <returns>A single character representing this instance:
        ///     <list type="table">
        ///         <listheader><term>Value</term><term>Indicator</term></listheader>
        ///         <item><term>2-9</term><term>The numeric character.</term></item>
        ///         <item><term><see cref="Spades" /></term><term><value>S</value></term></item>
        ///         <item><term><see cref="Clubs" /></term><term><value>C</value></term></item>
        ///         <item><term><see cref="Diamonds" /></term><term><value>D</value></term></item>
        ///         <item><term><see cref="Hearts" /></term><term><value>H</value></term></item>
        ///     </list>
        /// </returns>
        public char Indicator => this.Name[0];

        /// <summary>Gets the name of this face.</summary>
        /// <returns>The name of the face.</returns>
        public string Name { get; }

        /// <summary>Determines whether two specified <see cref="Suit" /> objects have the same value.</summary>
        /// <param name="left">The first face to compare.</param>
        /// <param name="right">The second face to compare.</param>
        /// <returns>
        ///     <value>true</value> if the value of <paramref name="left" /> is the same as the value of <paramref name="right" />;
        ///     otherwise, <value>false</value>. If both <paramref name="left" /> and <paramref name="right" /> are <value>null</value>,
        ///     the method returns <value>true</value>.
        /// </returns>
        public static bool operator ==(Suit left, Suit right)
        {
            return Equals(left, right);
        }

        /// <summary>Determines whether two specified <see cref="Suit" /> objects have different values.</summary>
        /// <param name="left">The first face to compare.</param>
        /// <param name="right">The second face to compare.</param>
        /// <returns>
        ///     <value>true</value> if the value of <paramref name="left" /> is different from the value of <paramref name="right" />;
        ///     otherwise, <value>false</value>.
        /// </returns>
        public static bool operator !=(Suit left, Suit right)
        {
            return !Equals(left, right);
        }

        /// <summary>Indicates whether a specified <see cref="Suit" /> is less than another specified <see cref="Suit" />.</summary>
        /// <param name="left">The first face to compare.</param>
        /// <param name="right">The second face to compare.</param>
        /// <returns>
        ///     <value>true</value>if the value of <paramref name="left" /> is less than the value of <paramref name="right" />;
        ///     otherwise, <value>false</value>.
        /// </returns>
        public static bool operator <(Suit left, Suit right)
        {
            return CompareTo(left, right) < 0;
        }

        /// <summary>
        ///     Indicates whether a specified <see cref="Suit" /> is less than or equal to another specified <see cref="Suit" />.
        /// </summary>
        /// <param name="left">The first face to compare.</param>
        /// <param name="right">The second face to compare.</param>
        /// <returns>
        ///     <value>true</value>if the value of <paramref name="left" /> is less than or equal to the value of
        ///     <paramref name="right" />; otherwise, <value>false</value>.
        /// </returns>
        public static bool operator <=(Suit left, Suit right)
        {
            return CompareTo(left, right) <= 0;
        }

        /// <summary>Indicates whether a specified <see cref="Suit" /> is greater than another specified <see cref="Suit" />.</summary>
        /// <param name="left">The first face to compare.</param>
        /// <param name="right">The second face to compare.</param>
        /// <returns>
        ///     <value>true</value>if the value of <paramref name="left" /> is greater than the value of <paramref name="right" />;
        ///     otherwise, <value>false</value>.
        /// </returns>
        public static bool operator >(Suit left, Suit right)
        {
            return CompareTo(left, right) > 0;
        }

        /// <summary>
        ///     Indicates whether a specified <see cref="Suit" /> is greater than or equal to another specified <see cref="Suit" />.
        /// </summary>
        /// <param name="left">The first face to compare.</param>
        /// <param name="right">The second face to compare.</param>
        /// <returns>
        ///     <value>true</value>if the value of <paramref name="left" /> is greater than or equal to the value of
        ///     <paramref name="right" />; otherwise, <value>false</value>.
        /// </returns>
        public static bool operator >=(Suit left, Suit right)
        {
            return CompareTo(left, right) >= 0;
        }

        /// <summary>Determines whether two specified <see cref="Suit" /> objects have the same value.</summary>
        /// <param name="left">The first face to compare.</param>
        /// <param name="right">The second face to compare.</param>
        /// <returns>
        ///     <value>true</value> if the value of <paramref name="left" /> is the same as the value of <paramref name="right" />;
        ///     otherwise, <value>false</value>. If both <paramref name="left" /> and <paramref name="right" /> are <value>null</value>,
        ///     the method returns <value>true</value>.
        /// </returns>
        public static bool Equals(Suit left, Suit right)
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
            return this.ordinal;
        }

        /// <inheritdoc />
        public override bool Equals(object other)
        {
            return this.Equals(other as Suit);
        }

        /// <inheritdoc />
        public bool Equals(Suit other)
        {
            return !(other is null) && other.ordinal == this.ordinal;
        }

        /// <inheritdoc />
        public int CompareTo(Suit other)
        {
            // Null should come first
            if (other is null)
            {
                return 1;
            }

            return this.ordinal - other.ordinal;
        }

        private static int CompareTo(Suit left, Suit right)
        {
            if (left is null)
            {
                return right is null ? 0 : -right.CompareTo(null);
            }

            return left.CompareTo(right);
        }
    }
}
