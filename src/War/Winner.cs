//-----------------------------------------------------------------------
// <copyright file="Winner.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace War
{
    /// <summary>The representation of the winner - be it the battle or the game.</summary>
    public enum Winner
    {
        /// <summary>The players tied.</summary>
        Tie,

        /// <summary>The left player won.</summary>
        Left,

        /// <summary>The right player won.</summary>
        Right,
    }
}
