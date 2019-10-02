//-----------------------------------------------------------------------
// <copyright file="GameMetadata.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace War
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>The metadata of the game.</summary>
    public class GameMetadata
    {
        private Winner previousWinner;
        private long deepBattleDepthTotal;

        /// <summary>Gets the number of battles in the game.</summary>
        /// <returns>The number of battles in the game.</returns>
        public long Battles { get; private set; }

        /// <summary>Gets the number of battles that the left player won in the game.</summary>
        /// <returns>The number of battles the left player won in the game.</returns>
        public long BattlesLeftWon { get; private set; }

        /// <summary>Gets the number of battles that the right player won in the game.</summary>
        /// <returns>The number of battles the right player won in the game.</returns>
        public long BattlesRightWon { get; private set; }

        /// <summary>Gets the number of deep battles in the game.</summary>
        /// <returns>The number of deep battles in the game.</returns>
        public long DeepBattles { get; private set; }

        /// <summary>Gets the number of deep battles that the left player won in the game.</summary>
        /// <returns>The number of deep battles the left player won in the game.</returns>
        public long DeepBattlesLeftWon { get; private set; }

        /// <summary>Gets the number of battles that the right player won in the game.</summary>
        /// <returns>The number of battles the right player won in the game.</returns>
        public long DeepBattlesRightWon { get; private set; }

        /// <summary>Gets the average depth of deep battles in the game.</summary>
        /// <returns>The average depth of deep battles or <value>0</value> if there have not been any deep battles.</returns>
        public double AverageDeepBattleDepth => this.DeepBattles == 0 ? 0 : this.deepBattleDepthTotal * 1.0d / this.DeepBattles;

        /// <summary>Gets a value indicating whether the game has completed.</summary>
        /// <returns><value>true</value> if the game has completed; otherwise, <value>false</value>.</returns>
        public bool IsComplete { get; internal set; }

        /// <summary>Gets the winner of the game.</summary>
        /// <returns>The winner of the game or <value>null</value> if not <see cref="IsComplete" />.</returns>
        public Winner? FinalWinner => this.IsComplete ? this.previousWinner : (Winner?)null;

        /// <inheritdoc />
        public override string ToString()
        {
            return this.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        public string ToString(IFormatProvider provider)
        {
            var builder = new StringBuilder();

            if (this.IsComplete)
            {
                builder.Append("Winner: ");
                builder.Append(this.previousWinner);
                builder.AppendLine();
            }

            builder.AppendFormat(provider, "Battles: {0} ({1} vs {2})", this.Battles, this.BattlesLeftWon, this.BattlesRightWon);
            builder.AppendLine();

            builder.AppendFormat(provider, "Deep Battles: {0} ({1} vs {2})", this.DeepBattles, this.DeepBattlesLeftWon, this.DeepBattlesRightWon);
            builder.AppendLine();

            builder.AppendFormat(provider, "Average Deep Battle Depth: {0}", this.AverageDeepBattleDepth);

            return builder.ToString();
        }

        /// <summary>Adds the information from the passed <paramref name="battle" /> into this instance.</summary>
        /// <param name="battle">The battle whose information should be added.</param>
        internal void AddBattle(Battle battle)
        {
            if (battle == null)
            {
                return;
            }

            this.Battles++;
            if (battle.Winner == Winner.Left)
            {
                this.BattlesLeftWon++;
            }
            else if (battle.Winner == Winner.Right)
            {
                this.BattlesRightWon++;
            }

            var deepBattle = battle as DeepBattle;
            if (deepBattle != null)
            {
                this.DeepBattles++;
                this.deepBattleDepthTotal += deepBattle.Depth;
                if (battle.Winner == Winner.Left)
                {
                    this.DeepBattlesLeftWon++;
                }
                else if (battle.Winner == Winner.Right)
                {
                    this.DeepBattlesRightWon++;
                }
            }

            this.previousWinner = battle.Winner;
        }
    }
}
