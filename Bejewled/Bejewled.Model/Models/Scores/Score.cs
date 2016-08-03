namespace Bejewled.Model.Scores
{
    using System;
    using Bejewled.Model.Interfaces;

    [Serializable]
    public class Score : IScore, IComparable<Score>
    {
        public Score()
        {
            this.PlayerScore = 0;
        }

        public Score(int score)
        {
            this.PlayerScore = score;
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

        public int CompareTo(Score other)
        {
            return this.PlayerScore.CompareTo(other.PlayerScore);
        }

        public override string ToString()
        {
            return this.PlayerScore.ToString();
        }
    }
}