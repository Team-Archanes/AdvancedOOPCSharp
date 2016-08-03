namespace Bejewled.Model.Models
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class RoundTimer
    {
        private int initialTime;

        public int InitialTime
        {
            get
            {
                return this.initialTime;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"{nameof(InitialTime)} cannot be negative");
                }

                this.initialTime = value;
            }
        }
        public int TimeLeft { get; private set; }
        private double GameTimeSeconds { get; set; }

        public RoundTimer(int initialTime, GameTime gameTime)
        {
            this.InitialTime = initialTime;
            this.TimeLeft = this.InitialTime;
            this.GameTimeSeconds = gameTime.TotalGameTime.TotalSeconds;
        }

        public void Update(GameTime newGameTime)
        {
            if (Math.Abs(newGameTime.TotalGameTime.TotalSeconds - this.GameTimeSeconds) >= 1)
            {
                this.TimeLeft--;

                if (this.TimeLeft < 0)
                {
                    // TODO: End game
                    Reset();
                }

                this.GameTimeSeconds = newGameTime.TotalGameTime.TotalSeconds;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            spriteBatch.DrawString(font, "Time Left: " + this.TimeLeft, new Vector2(30, 160), Color.GreenYellow);

            spriteBatch.End();
        }

        public void Reset()
        {
            this.TimeLeft = this.InitialTime;
        }

        public void Reset(int newInitialTIme)
        {
            this.InitialTime = newInitialTIme;
            this.Reset();
        }
    }
}
