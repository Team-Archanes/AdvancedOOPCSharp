using Bejewled.Model.Models;
using Bejewled.Model.Models.Scores;

namespace Bejewled.Model.Interfaces
{
    using System;

    using Bejewled.Model.EventArgs;

    public interface IView
    {
        int[,] Tiles { get; set; }

        ScoreManager GameScoreManager { get; set; }

        RoundTimer GameTimer { get; set; }

        LevelManager GameLevelManager { get; set; }

        event EventHandler OnLoad;

        event EventHandler<TileEventArgs> OnSecondTileClicked;

        event EventHandler OnExplosionFinished;

        event EventHandler<TileEventArgs> OnFirstTileClicked;

        event EventHandler OnHintClicked;

        void DisplayGameEndMessage();

        void DrawScore();

        void DrawGameBoard();
    }
}