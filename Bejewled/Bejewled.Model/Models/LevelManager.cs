namespace Bejewled.Model.Models
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using Bejewled.Model.Models.Scores;

    public class LevelManager
    {
        private ScoreManager scoreManager;
        private RoundTimer timer;

        private int levelScoreIncreasing;
        private int timeAddedOnScoreReach;

        private int initialLevel;
        private int initialScoreGoal;

        private int currentLevel;
        private int currentScoreGoal;

        public LevelManager(ScoreManager scoreManager, RoundTimer timer, int levelScoreIncreasing, int timeAddedOnScoreReach)
        {
            this.scoreManager = scoreManager;
            this.timer = timer;

            this.levelScoreIncreasing = levelScoreIncreasing;
            this.timeAddedOnScoreReach = timeAddedOnScoreReach;

            this.initialLevel = 1;
            this.initialScoreGoal = this.levelScoreIncreasing;

            this.currentLevel = this.initialLevel;
            this.currentScoreGoal = this.initialScoreGoal;
        }

        public void Update(GameTime gameTime)
        {
            int currentScore = this.scoreManager.CurrentGameScore.PlayerScore;

            if (currentScore >= this.currentScoreGoal)
            {
                this.currentLevel++;
                this.currentScoreGoal += this.levelScoreIncreasing;

                this.timer.AddAdditionalTime(timeAddedOnScoreReach);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            spriteBatch.DrawString(
                font,
                $"Level {this.currentLevel}",
                new Vector2(30, 120),
                Color.GreenYellow);

            spriteBatch.DrawString(
                font,
                $"Target {this.currentScoreGoal}",
                new Vector2(30, 150),
                Color.GreenYellow);

            spriteBatch.End();
        }

        public void Reset()
        {
            this.currentLevel = this.initialLevel;
            this.currentScoreGoal = this.initialScoreGoal;
        }
    }
}
