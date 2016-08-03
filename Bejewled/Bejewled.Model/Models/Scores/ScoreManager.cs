namespace Bejewled.Model.Models.Scores
{
    using Bejewled.Model.Scores;

    public class ScoreManager
    {
        public ScoreManager(Score currentGameScore, ScoreTable highScorreTable)
        {
            this.CurrentGameScore = currentGameScore;
            this.HighScorreTable = highScorreTable;
        }

        public Score CurrentGameScore { get; set; }
        public ScoreTable HighScorreTable { get; set; }

        public void UpdateHighScoreTableWithCurrentScore()
        {
            this.HighScorreTable.AddScore(this.CurrentGameScore);
            this.HighScorreTable.SaveScoreTable();
        }
    }
}
