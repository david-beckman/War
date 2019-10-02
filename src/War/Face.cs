//-----------------------------------------------------------------------
// <copyright file="Face.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace War
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>A representation of the 13 standard playing card faces.</summary>
    public class Face : IEquatable<Face>, IComparable<Face>
    {
        /// <summary>A flag indicating if an <see cref="Face.Ace" /> should be considered high by default during comparisons.</summary>
        public const bool DefaultAceHigh = false;

        /// <summary>The 2.</summary>
        public static readonly Face Two = new Face(2, '2', "Two");

        /// <summary>The 3.</summary>
        public static readonly Face Three = new Face(3, '3', "Three");

        /// <summary>The 4.</summary>
        public static readonly Face Four = new Face(4, '4', "Four");

        /// <summary>The 5.</summary>
        public static readonly Face Five = new Face(5, '5', "Five");

        /// <summary>The 6.</summary>
        public static readonly Face Six = new Face(6, '6', "Six");

        /// <summary>The 7.</summary>
        public static readonly Face Seven = new Face(7, '7', "Seven");

        /// <summary>The 8.</summary>
        public static readonly Face Eight = new Face(8, '8', "Eight");

        /// <summary>The 9.</summary>
        public static readonly Face Nine = new Face(9, '9', "Nine");

        /// <summary>The 10.</summary>
        public static readonly Face Ten = new Face(10, 'T', "Ten");

        /// <summary>The Jack.</summary>
        public static readonly Face Jack = new Face(11, 'J', "Jack");

        /// <summary>The Queen.</summary>
        public static readonly Face Queen = new Face(12, 'Q', "Queen");

        /// <summary>The King.</summary>
        public static readonly Face King = new Face(13, 'K', "King");

        /// <summary>The Ace.</summary>
        public static readonly Face Ace = new Face(1, 14, 'A', "Ace");

        /// <summary>The set of all 13 faces.</summary>
        public static readonly IReadOnlyList<Face> Faces = new Face[]
        {
            Ace,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
        };

        private readonly int defaultOrdinal;

        private readonly int alternateOrdinal;

        private Face(int ordinal, char indicator, string name)
        {
            this.defaultOrdinal = this.alternateOrdinal = ordinal;
            this.Indicator = indicator;
            this.Name = name;
        }

        private Face(int defaultOrdinal, int alternateOrdinal, char indicator, string name)
        {
            this.defaultOrdinal = defaultOrdinal;
            this.alternateOrdinal = alternateOrdinal;
            this.Indicator = indicator;
            this.Name = name;
        }

        /// <summary>Gets a single character indicator for the face.</summary>
        /// <returns>A single character representing this instance:
        ///     <list type="table">
        ///         <listheader><term>Value</term><term>Indicator</term></listheader>
        ///         <item><term>2-9</term><term>The numeric character.</term></item>
        ///         <item><term><see cref="Ten" /></term><term><value>T</value></term></item>
        ///         <item><term><see cref="Jack" /></term><term><value>J</value></term></item>
        ///         <item><term><see cref="Queen" /></term><term><value>Q</value></term></item>
        ///         <item><term><see cref="King" /></term><term><value>K</value></term></item>
        ///         <item><term><see cref="Ace" /></term><term><value>A</value></term></item>
        ///     </list>
        /// </returns>
        public char Indicator { get; }

        /// <summary>Gets the name of this face.</summary>
        /// <returns>The name of the face.</returns>
        public string Name { get; }

        /// <summary>Determines whether two specified <see cref="Face" /> objects have the same value.</summary>
        /// <param name="left">The first face to compare.</param>
        /// <param name="right">The second face to compare.</param>
        /// <returns>
        ///     <value>true</value> if the value of <paramref name="left" /> is the same as the value of <paramref name="right" />;
        ///     otherwise, <value>false</value>. If both <paramref name="left" /> and <paramref name="right" /> are <value>null</value>,
        ///     the method returns <value>true</value>.
        /// </returns>
        public static bool operator ==(Face left, Face right)
        {
            return Equals(left, right);
        }

        /// <summary>Determines whether two specified <see cref="Face" /> objects have different values.</summary>
        /// <param name="left">The first face to compare.</param>
        /// <param name="right">The second face to compare.</param>
        /// <returns>
        ///     <value>true</value> if the value of <paramref name="left" /> is different from the value of <paramref name="right" />;
        ///     otherwise, <value>false</value>.
        /// </returns>
        public static bool operator !=(Face left, Face right)
        {
            return !Equals(left, right);
        }

        /// <summary>Indicates whether a specified <see cref="Face" /> is less than another specified <see cref="Face" />.</summary>
        /// <param name="left">The first face to compare.</param>
        /// <param name="right">The second face to compare.</param>
        /// <returns>
        ///     <value>true</value>if the value of <paramref name="left" /> is less than the value of <paramref name="right" />;
        ///     otherwise, <value>false</value>.
        /// </returns>
        public static bool operator <(Face left, Face right)
        {
            return CompareTo(left, right) < 0;
        }

        /// <summary>
        ///     Indicates whether a specified <see cref="Face" /> is less than or equal to another specified <see cref="Face" />.
        /// </summary>
        /// <param name="left">The first face to compare.</param>
        /// <param name="right">The second face to compare.</param>
        /// <returns>
        ///     <value>true</value>if the value of <paramref name="left" /> is less than or equal to the value of
        ///     <paramref name="right" />; otherwise, <value>false</value>.
        /// </returns>
        public static bool operator <=(Face left, Face right)
        {
            return CompareTo(left, right) <= 0;
        }

        /// <summary>Indicates whether a specified <see cref="Face" /> is greater than another specified <see cref="Face" />.</summary>
        /// <param name="left">The first face to compare.</param>
        /// <param name="right">The second face to compare.</param>
        /// <returns>
        ///     <value>true</value>if the value of <paramref name="left" /> is greater than the value of <paramref name="right" />;
        ///     otherwise, <value>false</value>.
        /// </returns>
        public static bool operator >(Face left, Face right)
        {
            return CompareTo(left, right) > 0;
        }

        /// <summary>
        ///     Indicates whether a specified <see cref="Face" /> is greater than or equal to another specified <see cref="Face" />.
        /// </summary>
        /// <param name="left">The first face to compare.</param>
        /// <param name="right">The second face to compare.</param>
        /// <returns>
        ///     <value>true</value>if the value of <paramref name="left" /> is greater than or equal to the value of
        ///     <paramref name="right" />; otherwise, <value>false</value>.
        /// </returns>
        public static bool operator >=(Face left, Face right)
        {
            return CompareTo(left, right) >= 0;
        }

        /// <summary>Determines whether two specified <see cref="Face" /> objects have the same value.</summary>
        /// <param name="left">The first face to compare.</param>
        /// <param name="right">The second face to compare.</param>
        /// <returns>
        ///     <value>true</value> if the value of <paramref name="left" /> is the same as the value of <paramref name="right" />;
        ///     otherwise, <value>false</value>. If both <paramref name="left" /> and <paramref name="right" /> are <value>null</value>,
        ///     the method returns <value>true</value>.
        /// </returns>
        public static bool Equals(Face left, Face right)
        {
            return left is null ? right is null : left.Equals(right);
        }

        /// <summary>
        ///     Compares the two specified <see cref="Face" /> objects and returns an integer that indicates whether
        ///     <paramref name="left" /> precedes, follows, or occurs in the same position in the sort order as <paramref name="right" />.
        /// </summary>
        /// <param name="left">The first face to compare.</param>
        /// <param name="right">The second face to compare.</param>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has these meanings:
        ///     <list type="table">
        ///         <listheader><term>Value</term><term>Meaning</term></listheader>
        ///         <item>
        ///             <term>Less than zero</term>
        ///             <term><paramref name="left" /> precedes <paramref name="right" /> in the sort order.</term>
        ///         </item>
        ///         <item>
        ///             <term>Zero</term>
        ///             <term><paramref name="left" /> occurs in the same position in the sort order as <paramref name="right" />.</term>
        ///         </item>
        ///         <item>
        ///             <term>Greater than zero</term>
        ///             <term><paramref name="left" /> follows <paramref name="right" /> in the sort order.</term>
        ///         </item>
        ///     </list>
        /// </returns>
        /// <seealso cref="DefaultAceHigh" />
        public static int CompareTo(Face left, Face right)
        {
            return CompareTo(left, right, DefaultAceHigh);
        }

        /// <summary>
        ///     Compares the two specified <see cref="Face" /> objects and returns an integer that indicates whether
        ///     <paramref name="left" /> precedes, follows, or occurs in the same position in the sort order as <paramref name="right" />.
        /// </summary>
        /// <param name="left">The first face to compare.</param>
        /// <param name="right">The second face to compare.</param>
        /// <param name="aceHigh">A flag indicating whether the <see cref="Face.Ace" /> is high.</param>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has these meanings:
        ///     <list type="table">
        ///         <listheader><term>Value</term><term>Meaning</term></listheader>
        ///         <item>
        ///             <term>Less than zero</term>
        ///             <term><paramref name="left" /> precedes <paramref name="right" /> in the sort order.</term>
        ///         </item>
        ///         <item>
        ///             <term>Zero</term>
        ///             <term><paramref name="left" /> occurs in the same position in the sort order as <paramref name="right" />.</term>
        ///         </item>
        ///         <item>
        ///             <term>Greater than zero</term>
        ///             <term><paramref name="left" /> follows <paramref name="right" /> in the sort order.</term>
        ///         </item>
        ///     </list>
        /// </returns>
        public static int CompareTo(Face left, Face right, bool aceHigh)
        {
            if (left is null)
            {
                return right is null ? 0 : -right.CompareTo(null);
            }

            return left.CompareTo(right, aceHigh);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Name;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.GetOrdinal(DefaultAceHigh);
        }

        /// <inheritdoc />
        public override bool Equals(object other)
        {
            return this.Equals(other as Face);
        }

        /// <inheritdoc />
        public bool Equals(Face other)
        {
            return !(other is null) && other.defaultOrdinal == this.defaultOrdinal;
        }

        /// <inheritdoc />
        public int CompareTo(Face other)
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
        public int CompareTo(Face other, bool aceHigh)
        {
            // Null should come first
            if (other is null)
            {
                return 1;
            }

            return this.GetOrdinal(aceHigh) - other.GetOrdinal(aceHigh);
        }

        private int GetOrdinal(bool aceHigh)
        {
            return aceHigh ? this.alternateOrdinal : this.defaultOrdinal;
        }
    }
}
