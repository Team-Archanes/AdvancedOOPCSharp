namespace Bejewled.Model.Models.Scores
{
    using System;
    using System.Collections.Generic;

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
                Console.WriteLine("Failed to load score table!");
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

        public void PrintScores()
        {
            if (this.scoreCollection.Count != 0)
            {
                for (int i = 0; i < this.scoreCollection.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {this.scoreCollection[i]}");
                }
            }
            else
            {
                Console.WriteLine("There are no scores available.");
            }
        }
    }
}
