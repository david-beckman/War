//-----------------------------------------------------------------------
// <copyright file="RandomExtensions.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace War.Tests
{
    using System;
    using System.Collections.Generic;

    public static class RandomExtensions
    {
        public static T Next<T>(this Random source, IReadOnlyList<T> list)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (list == null || list.Count == 0)
            {
                return default(T);
            }

            var index = source.Next(list.Count);
            return list[index];
        }
    }
}
