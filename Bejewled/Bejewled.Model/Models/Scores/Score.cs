namespace Bejewled.Model.Scores
{
    using System;
    using Bejewled.Model.Interfaces;

    [Serializable]
    public class Score : IScore
    {
        public Score()
        {
            this.PlayerScore = 0;
        }

        public int PlayerScore { get; private set; }

        public void IncreaseScore()
        {
            this.PlayerScore += 10;
        }

        public void Reset()
        {
            this.PlayerScore = 0;
        }
    }
}