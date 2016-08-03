namespace Bejewled.Model.Models.Scores
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using Bejewled.Model.Interfaces;
    using Bejewled.Model.Scores;

    [Serializable]
    public class ScoreTable
    {
        private const int MaxNumberOfKeptScores = 5;

        private readonly string fileName;
        private readonly IPreserver<List<Score>> preserver;

        private List<Score> scoreCollection;

        public ScoreTable(IPreserver<List<Score>> preserver)
        {
            this.scoreCollection = new List<Score>();
            this.preserver = preserver;

            this.fileName = @"ScoreSave.txt";
        }

        public ScoreTable(IPreserver<List<Score>> preserver, string fileName)
        {
            this.scoreCollection = new List<Score>();
            this.preserver = preserver;

            this.fileName = fileName;
        }

        public void LoadScoreTable()
        {
            try
            {
                this.scoreCollection = this.preserver.LoadData(fileName);
                this.scoreCollection.Sort();
                this.scoreCollection.Reverse();
            }
            catch (Exception)
            {
                this.scoreCollection = new List<Score>();
            }
        }

        public void SaveScoreTable()
        {
            this.preserver.SaveData(scoreCollection, fileName);
        }

        public void AddScore(Score score)
        {
            this.scoreCollection.Add(score);
            this.scoreCollection.Sort();
            this.scoreCollection.Reverse();

            if (scoreCollection.Count > 5)
            {
                this.scoreCollection = scoreCollection.GetRange(0, MaxNumberOfKeptScores);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            spriteBatch.DrawString(font, "High Scores:", new Vector2(30, 180), Color.GreenYellow);

            int y = 200;
            if (this.scoreCollection.Count != 0)
            {
                for (int i = 0; i < this.scoreCollection.Count; i++)
                {
                    Console.WriteLine();
                    spriteBatch.DrawString(font, $"{i + 1}: {this.scoreCollection[i]}", new Vector2(30, y), Color.GreenYellow);

                    y += 20;
                }
            }
            else
            {
                spriteBatch.DrawString(font, $"None available.", new Vector2(30, y), Color.GreenYellow);
            }

            spriteBatch.End();
        }

        public override string ToString()
        {
            string output = "High Scores:";

            if (this.scoreCollection.Count != 0)
            {
                for (int i = 0; i < this.scoreCollection.Count; i++)
                {
                    output += Environment.NewLine;
                    output += $"{i + 1}: {this.scoreCollection[i]}";
                }
            }
            else
            {
                output += Environment.NewLine;
                output += $"None available.";
            }

            return output;
        }
    }
}
