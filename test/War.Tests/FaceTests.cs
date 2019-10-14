//-----------------------------------------------------------------------
// <copyright file="FaceTests.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace War.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Xunit;

    public class FaceTests
    {
        private readonly Random random;

        private readonly Face defaultFace;

        public FaceTests()
        {
            this.random = new Random();
            this.defaultFace = Next(this.random);
        }

        public static Face Next(Random random)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            return random.Next(Face.Faces);
        }

        public static IEnumerable<object[]> ListAllFaces()
        {
            return (new Face[] { null }).Union(Face.Faces).Select(face => new Face[] { face });
        }

        [Theory]
        [MemberData(nameof(ListAllFaces))]
        public void ToStringShouldBeName(Face face)
        {
            Assert.Equal(face?.Name, face?.ToString());
        }

        [Fact]
        public void GetHashCodeShouldReturnADifferentValueForEachFace()
        {
            Face.Faces.Select(face => new { Face = face, Hash = face.GetHashCode() })
                .GroupBy(item => item.Hash)
                .All(groupItem =>
                {
                    Assert.Single(groupItem);
                    return true;
                });
        }

        [Fact]
        public void EqualsShouldBeFalseForNonFaces()
        {
            Assert.False(this.defaultFace.Equals(this.defaultFace.Name));
        }

        [Fact]
        public void EqualsShouldBeTrueForTheSameFace()
        {
            Assert.True(this.defaultFace.Equals((object)this.defaultFace));
            Assert.True(Face.Equals(this.defaultFace, this.defaultFace));
        }

        [Fact]
        public void CompareToShouldPutAceLowByDefault()
        {
            Assert.True(Face.Ace.CompareTo(this.defaultFace) <= 0);
            Assert.True(Face.CompareTo(Face.Ace, this.defaultFace) <= 0);
        }

        [Fact]
        public void CompareToShouldPutAceHighWhenSpecified()
        {
            Assert.True(Face.Ace.CompareTo(this.defaultFace, true) >= 0);
            Assert.True(Face.CompareTo(Face.Ace, this.defaultFace, true) >= 0);
        }

        [Fact]
        public void CompareToShouldPutNullFirst()
        {
            Assert.True(this.defaultFace.CompareTo(null) > 0);
            Assert.True(Face.CompareTo(this.defaultFace, null) > 0);
            Assert.True(Face.CompareTo(null, this.defaultFace) < 0);
        }

        [Fact]
        public void CompareToShoulMatchNulls()
        {
            Assert.Equal(0, Face.CompareTo(null, null));
        }

        [Theory]
        [MemberData(nameof(ListAllFaces))]
        public void LessThanShouldMatchCompareTo(Face face)
        {
            Assert.Equal(Face.CompareTo(this.defaultFace, face) < 0, this.defaultFace < face);
            Assert.Equal(Face.CompareTo(face, this.defaultFace) < 0, face < this.defaultFace);
        }

        [Theory]
        [MemberData(nameof(ListAllFaces))]
        public void LessThanOrEqualToShouldMatchCompareTo(Face face)
        {
            Assert.Equal(Face.CompareTo(this.defaultFace, face) <= 0, this.defaultFace <= face);
            Assert.Equal(Face.CompareTo(face, this.defaultFace) <= 0, face <= this.defaultFace);
        }

        [Theory]
        [MemberData(nameof(ListAllFaces))]
        public void EqualToShouldMatchCompareToAndEquals(Face face)
        {
            Assert.Equal(Face.CompareTo(this.defaultFace, face) == 0, this.defaultFace == face);
            Assert.Equal(Face.CompareTo(face, this.defaultFace) == 0, face == this.defaultFace);
            Assert.Equal(Face.Equals(face, this.defaultFace), face == this.defaultFace);
        }

        [Theory]
        [MemberData(nameof(ListAllFaces))]
        public void NotEqualToShouldMatchCompareToAndEquals(Face face)
        {
            Assert.Equal(Face.CompareTo(this.defaultFace, face) != 0, this.defaultFace != face);
            Assert.Equal(Face.CompareTo(face, this.defaultFace) != 0, face != this.defaultFace);
            Assert.Equal(!Face.Equals(face, this.defaultFace), face != this.defaultFace);
        }

        [Theory]
        [MemberData(nameof(ListAllFaces))]
        public void GreaterThanShouldMatchCompareTo(Face face)
        {
            Assert.Equal(Face.CompareTo(this.defaultFace, face) > 0, this.defaultFace > face);
            Assert.Equal(Face.CompareTo(face, this.defaultFace) > 0, face > this.defaultFace);
        }

        [Theory]
        [MemberData(nameof(ListAllFaces))]
        public void GreaterThanOrEqualToShouldMatchCompareTo(Face face)
        {
            Assert.Equal(Face.CompareTo(this.defaultFace, face) >= 0, this.defaultFace >= face);
            Assert.Equal(Face.CompareTo(face, this.defaultFace) >= 0, face >= this.defaultFace);
        }
    }
}
