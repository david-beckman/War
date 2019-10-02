//-----------------------------------------------------------------------
// <copyright file="Game.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace War
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    /// <summary>
    ///     The representation of a game of War - a ordered set of battles. There are 2 random elements in play: the initial hands of each
    ///     player and the order of the cards returned following a battle.
    /// </summary>
    public class Game : IEnumerator<Battle>
    {
        private static readonly Random SeedGenerator = new Random();

        private readonly int seed;
        private bool disposed = false;
        private Random random;
        private Hand left;
        private Hand right;
        private Battle current;

        /// <summary>Initializes a new instance of the <see cref="Game" /> class.</summary>
        public Game()
            : this(SeedGenerator.Next())
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Game" /> class.</summary>
        /// <param name="seed">The seed to use for randomization.</param>
        public Game(int seed)
        {
            this.seed = seed;
            this.Reset();
        }

        /// <summary>Finalizes an instance of the <see cref="Game" /> class.</summary>
        ~Game()
        {
            this.Dispose(false);
        }

        /// <summary>Gets the game metadata.</summary>
        /// <returns>The game metadata.</returns>
        public GameMetadata Metadata { get; private set; }

        /// <inheritdoc />
        public Battle Current
        {
            get
            {
                this.CheckDisposed();

                if (this.current != null)
                {
                    return this.current;
                }

                throw new InvalidOperationException(this.left == null ?
                    Strings.InvalidOperation_EnumNotStarted :
                    Strings.InvalidOperation_EnumEnded);
            }
        }

        /// <inheritdoc />
        object IEnumerator.Current => this.Current;

        /// <inheritdoc />
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        public bool MoveNext()
        {
            this.CheckDisposed();

            if (this.left == null)
            {
                this.PopulateHands();
            }

            if (this.left.IsEmpty || this.right.IsEmpty)
            {
                this.current = null;
                this.Metadata.IsComplete = true;
                return false;
            }

            this.Metadata.AddBattle(this.current = this.RunNextBattle());
            return true;
        }

        /// <inheritdoc />
        public void Reset()
        {
            this.CheckDisposed();
            this.random = new Random(this.seed);
            this.left = null;
            this.right = null;
            this.Metadata = new GameMetadata();
        }

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
            return this.Metadata.ToString(provider);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        /// <param name="disposing">Indicates if dispose was called (vs the destructor).</param>
        [SuppressMessage("Usage", "CA1801:Review unused parameters", Justification = "Even though it is not used, there needs to be a signature distinction.")]
        protected virtual void Dispose(bool disposing)
        {
            this.disposed = true;
        }

        private void CheckDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
        }

        private void PopulateHands()
        {
            var deck = Suit.Suits
                .SelectMany(suit => Face.Faces.Select(face => new Card(face, suit)))
                .OrderBy(_ => this.random.Next()).ToArray();

            // Should be 26
            var halvsies = deck.Length / 2;

            this.left = new Hand(deck.Take(halvsies));
            this.right = new Hand(deck.Skip(halvsies).Take(deck.Length - halvsies));
        }

        private Battle RunNextBattle()
        {
            var l = this.left.Take();
            var r = this.right.Take();

            Battle result;

            if (l == null || r == null || l.Face != r.Face)
            {
                result = new SimpleBattle(l, r);
            }
            else
            {
                var leftCasualties = this.left.Take(3);
                var rightCasualties = this.right.Take(3);
                var next = this.RunNextBattle();
                result = new DeepBattle(l, r, leftCasualties, rightCasualties, next);
            }

            // Don't return the cards in the event of a tie.
            if (result.Winner != Winner.Tie)
            {
                Hand winningHand = result.Winner == Winner.Left ? this.left : this.right;
                winningHand.Add(result.AllCards.OrderBy(_ => this.random.Next()));
            }

            return result;
        }
    }
}
