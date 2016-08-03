using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bejewled.Tests
{
    using System.Linq;

    using Bejewled.Model;
    using Bejewled.Model.Enums;
    using Bejewled.Model.Interfaces;

    using Moq;

    [TestClass]
    public class GameBoardTests
    {
        private Mock<ITileGenerator> mock;

        private IGameBoard gameBoard;

        [TestInitialize]
        public void SetUp()
        {
            this.mock = new Mock<ITileGenerator>();
            this.gameBoard = new GameBoard(this.mock.Object);
            this.SetGameboard();
        }

        [TestMethod]
        public void InitializeGameBoard_CreateGameboard_ShouldPass()
        {
            int[,] expectedArray = new int[,]
                                       {
                                           { 3, 0, 4, 5, 6, 0, 5, 2 }, { 2, 1, 5, 4, 5, 1, 4, 0 },
                                           { 6, 2, 1, 3, 0, 4, 5, 4 }, { 2, 4, 2, 2, 4, 2, 1, 5 },
                                           { 2, 5, 3, 1, 1, 3, 3, 2 }, { 6, 0, 2, 5, 3, 5, 6, 6 },
                                           { 6, 2, 0, 5, 4, 1, 3, 5 }, { 0, 1, 0, 0, 2, 4, 3, 1 }
                                       };

            var actualArray = this.gameBoard.InitializeGameBoard();

            CollectionAssert.AreEqual(expectedArray, actualArray);
        }

        [TestMethod]
        public void FirstTileClicked_ValueBelowSeven_ShouldChangeTile()
        {
            this.gameBoard.InitializeGameBoard();
            var tileModificator = 8;
            var firstTile = new Tile(TileType.Yellow, new TilePosition() { X = 0, Y = 0 });

            this.gameBoard.FirstTileClicked(firstTile);
            var newGameBoard = this.gameBoard.GenerateNumericGameBoard();

            Assert.AreEqual((int)firstTile.TileType + tileModificator, newGameBoard[0, 0]);
        }

        [TestMethod]
        public void FirstTileClicked_ValueAboveSeven_ShouldntChangeTile()
        {
            this.gameBoard.InitializeGameBoard();
            var tileModificator = 8;
            var firstTile = new Tile(TileType.Yellow, new TilePosition() { X = 0, Y = 0 });
            this.gameBoard.FirstTileClicked(firstTile);

            this.gameBoard.FirstTileClicked(firstTile);
            var newGameBoard = this.gameBoard.GenerateNumericGameBoard();
            Assert.AreEqual((int)firstTile.TileType + tileModificator, newGameBoard[0, 0]);
        }

        [TestMethod]
        public void GetHint_PossibleCombinations_ShouldPass()
        {
            this.gameBoard.InitializeGameBoard();
            var possiblePositions = new TilePosition[]
                                        {
                                            new TilePosition() { X = 7, Y = 0 }, new TilePosition() { X = 5, Y = 1 },
                                            new TilePosition() { X = 3, Y = 0 }, new TilePosition() { X = 1, Y = 0 },
                                            new TilePosition() { X = 3, Y = 5 }, new TilePosition() { X = 1, Y = 6 },
                                            new TilePosition() { X = 0, Y = 3 }
                                        };

            var tile = this.gameBoard.GetHint();
            Assert.IsTrue(possiblePositions.Any(t => t.X == tile.Position.X && t.Y == tile.Position.Y));
        }

        [TestMethod]
        public void NormalizeFocusedTile_FocusedTile_ShouldNormalize()
        {
            this.gameBoard.InitializeGameBoard();
            var firstTile = new Tile(TileType.Yellow, new TilePosition() { X = 0, Y = 0 });
            this.gameBoard.FirstTileClicked(firstTile);
            var clickedTile = new Tile(TileType.YellowClicked, new TilePosition() { X = 0, Y = 0 });

            this.gameBoard.NormalizeFocusedTile(clickedTile);
            var changedGameboard = this.gameBoard.GenerateNumericGameBoard();

            Assert.AreEqual((int)firstTile.TileType, changedGameboard[0, 0]);
        }

        private void SetGameboard()
        {
            this.mock.Setup(n => n.CreateRandomTile(0, 0))
                .Returns(new Tile(TileType.Yellow, new TilePosition() { X = 0, Y = 0 }));
            this.mock.Setup(n => n.CreateRandomTile(0, 1)).Returns(new Tile(TileType.Red, new TilePosition() { X = 0, Y = 1 }));
            this.mock.Setup(n => n.CreateRandomTile(0, 2))
                .Returns(new Tile(TileType.Purple, new TilePosition() { X = 0, Y = 2 }));
            this.mock.Setup(n => n.CreateRandomTile(0, 3))
                .Returns(new Tile(TileType.White, new TilePosition() { X = 0, Y = 3 }));
            this.mock.Setup(n => n.CreateRandomTile(0, 4))
                .Returns(new Tile(TileType.RainBow, new TilePosition() { X = 0, Y = 4 }));
            this.mock.Setup(n => n.CreateRandomTile(0, 5)).Returns(new Tile(TileType.Red, new TilePosition() { X = 0, Y = 5 }));
            this.mock.Setup(n => n.CreateRandomTile(0, 6))
                .Returns(new Tile(TileType.White, new TilePosition() { X = 0, Y = 6 }));
            this.mock.Setup(n => n.CreateRandomTile(0, 7)).Returns(new Tile(TileType.Blue, new TilePosition() { X = 0, Y = 7 }));

            this.mock.Setup(n => n.CreateRandomTile(1, 0)).Returns(new Tile(TileType.Blue, new TilePosition() { X = 1, Y = 0 }));
            this.mock.Setup(n => n.CreateRandomTile(1, 1))
                .Returns(new Tile(TileType.Green, new TilePosition() { X = 1, Y = 1 }));
            this.mock.Setup(n => n.CreateRandomTile(1, 2))
                .Returns(new Tile(TileType.White, new TilePosition() { X = 1, Y = 2 }));
            this.mock.Setup(n => n.CreateRandomTile(1, 3))
                .Returns(new Tile(TileType.Purple, new TilePosition() { X = 1, Y = 3 }));
            this.mock.Setup(n => n.CreateRandomTile(1, 4))
                .Returns(new Tile(TileType.White, new TilePosition() { X = 1, Y = 4 }));
            this.mock.Setup(n => n.CreateRandomTile(1, 5))
                .Returns(new Tile(TileType.Green, new TilePosition() { X = 1, Y = 5 }));
            this.mock.Setup(n => n.CreateRandomTile(1, 6))
                .Returns(new Tile(TileType.Purple, new TilePosition() { X = 1, Y = 6 }));
            this.mock.Setup(n => n.CreateRandomTile(1, 7)).Returns(new Tile(TileType.Red, new TilePosition() { X = 1, Y = 7 }));

            this.mock.Setup(n => n.CreateRandomTile(2, 0))
                .Returns(new Tile(TileType.RainBow, new TilePosition() { X = 2, Y = 0 }));
            this.mock.Setup(n => n.CreateRandomTile(2, 1)).Returns(new Tile(TileType.Blue, new TilePosition() { X = 2, Y = 1 }));
            this.mock.Setup(n => n.CreateRandomTile(2, 2))
                .Returns(new Tile(TileType.Green, new TilePosition() { X = 2, Y = 2 }));
            this.mock.Setup(n => n.CreateRandomTile(2, 3))
                .Returns(new Tile(TileType.Yellow, new TilePosition() { X = 2, Y = 3 }));
            this.mock.Setup(n => n.CreateRandomTile(2, 4)).Returns(new Tile(TileType.Red, new TilePosition() { X = 2, Y = 4 }));
            this.mock.Setup(n => n.CreateRandomTile(2, 5))
                .Returns(new Tile(TileType.Purple, new TilePosition() { X = 2, Y = 5 }));
            this.mock.Setup(n => n.CreateRandomTile(2, 6))
                .Returns(new Tile(TileType.White, new TilePosition() { X = 2, Y = 6 }));
            this.mock.Setup(n => n.CreateRandomTile(2, 7))
                .Returns(new Tile(TileType.Purple, new TilePosition() { X = 2, Y = 7 }));

            this.mock.Setup(n => n.CreateRandomTile(3, 0)).Returns(new Tile(TileType.Blue, new TilePosition() { X = 3, Y = 0 }));
            this.mock.Setup(n => n.CreateRandomTile(3, 1))
                .Returns(new Tile(TileType.Purple, new TilePosition() { X = 3, Y = 1 }));
            this.mock.Setup(n => n.CreateRandomTile(3, 2)).Returns(new Tile(TileType.Blue, new TilePosition() { X = 3, Y = 2 }));
            this.mock.Setup(n => n.CreateRandomTile(3, 3)).Returns(new Tile(TileType.Blue, new TilePosition() { X = 3, Y = 3 }));
            this.mock.Setup(n => n.CreateRandomTile(3, 4))
                .Returns(new Tile(TileType.Purple, new TilePosition() { X = 3, Y = 4 }));
            this.mock.Setup(n => n.CreateRandomTile(3, 5)).Returns(new Tile(TileType.Blue, new TilePosition() { X = 3, Y = 5 }));
            this.mock.Setup(n => n.CreateRandomTile(3, 6))
                .Returns(new Tile(TileType.Green, new TilePosition() { X = 3, Y = 6 }));
            this.mock.Setup(n => n.CreateRandomTile(3, 7))
                .Returns(new Tile(TileType.White, new TilePosition() { X = 3, Y = 7 }));

            this.mock.Setup(n => n.CreateRandomTile(4, 0)).Returns(new Tile(TileType.Blue, new TilePosition() { X = 4, Y = 0 }));
            this.mock.Setup(n => n.CreateRandomTile(4, 1))
                .Returns(new Tile(TileType.White, new TilePosition() { X = 4, Y = 1 }));
            this.mock.Setup(n => n.CreateRandomTile(4, 2))
                .Returns(new Tile(TileType.Yellow, new TilePosition() { X = 4, Y = 2 }));
            this.mock.Setup(n => n.CreateRandomTile(4, 3))
                .Returns(new Tile(TileType.Green, new TilePosition() { X = 4, Y = 3 }));
            this.mock.Setup(n => n.CreateRandomTile(4, 4))
                .Returns(new Tile(TileType.Green, new TilePosition() { X = 4, Y = 4 }));
            this.mock.Setup(n => n.CreateRandomTile(4, 5))
                .Returns(new Tile(TileType.Yellow, new TilePosition() { X = 4, Y = 5 }));
            this.mock.Setup(n => n.CreateRandomTile(4, 6))
                .Returns(new Tile(TileType.Yellow, new TilePosition() { X = 4, Y = 6 }));
            this.mock.Setup(n => n.CreateRandomTile(4, 7)).Returns(new Tile(TileType.Blue, new TilePosition() { X = 4, Y = 7 }));

            this.mock.Setup(n => n.CreateRandomTile(5, 0))
                .Returns(new Tile(TileType.RainBow, new TilePosition() { X = 5, Y = 0 }));
            this.mock.Setup(n => n.CreateRandomTile(5, 1)).Returns(new Tile(TileType.Red, new TilePosition() { X = 5, Y = 1 }));
            this.mock.Setup(n => n.CreateRandomTile(5, 2)).Returns(new Tile(TileType.Blue, new TilePosition() { X = 5, Y = 2 }));
            this.mock.Setup(n => n.CreateRandomTile(5, 3))
                .Returns(new Tile(TileType.White, new TilePosition() { X = 5, Y = 3 }));
            this.mock.Setup(n => n.CreateRandomTile(5, 4))
                .Returns(new Tile(TileType.Yellow, new TilePosition() { X = 5, Y = 4 }));
            this.mock.Setup(n => n.CreateRandomTile(5, 5))
                .Returns(new Tile(TileType.White, new TilePosition() { X = 5, Y = 5 }));
            this.mock.Setup(n => n.CreateRandomTile(5, 6))
                .Returns(new Tile(TileType.RainBow, new TilePosition() { X = 5, Y = 6 }));
            this.mock.Setup(n => n.CreateRandomTile(5, 7))
                .Returns(new Tile(TileType.RainBow, new TilePosition() { X = 5, Y = 7 }));

            this.mock.Setup(n => n.CreateRandomTile(6, 0))
                .Returns(new Tile(TileType.RainBow, new TilePosition() { X = 6, Y = 0 }));
            this.mock.Setup(n => n.CreateRandomTile(6, 1)).Returns(new Tile(TileType.Blue, new TilePosition() { X = 6, Y = 1 }));
            this.mock.Setup(n => n.CreateRandomTile(6, 2)).Returns(new Tile(TileType.Red, new TilePosition() { X = 6, Y = 2 }));
            this.mock.Setup(n => n.CreateRandomTile(6, 3))
                .Returns(new Tile(TileType.White, new TilePosition() { X = 6, Y = 3 }));
            this.mock.Setup(n => n.CreateRandomTile(6, 4))
                .Returns(new Tile(TileType.Purple, new TilePosition() { X = 6, Y = 4 }));
            this.mock.Setup(n => n.CreateRandomTile(6, 5))
                .Returns(new Tile(TileType.Green, new TilePosition() { X = 6, Y = 5 }));
            this.mock.Setup(n => n.CreateRandomTile(6, 6))
                .Returns(new Tile(TileType.Yellow, new TilePosition() { X = 6, Y = 6 }));
            this.mock.Setup(n => n.CreateRandomTile(6, 7))
                .Returns(new Tile(TileType.White, new TilePosition() { X = 6, Y = 7 }));

            this.mock.Setup(n => n.CreateRandomTile(7, 0)).Returns(new Tile(TileType.Red, new TilePosition() { X = 7, Y = 0 }));
            this.mock.Setup(n => n.CreateRandomTile(7, 1))
                .Returns(new Tile(TileType.Green, new TilePosition() { X = 7, Y = 1 }));
            this.mock.Setup(n => n.CreateRandomTile(7, 2)).Returns(new Tile(TileType.Red, new TilePosition() { X = 7, Y = 2 }));
            this.mock.Setup(n => n.CreateRandomTile(7, 3)).Returns(new Tile(TileType.Red, new TilePosition() { X = 7, Y = 3 }));
            this.mock.Setup(n => n.CreateRandomTile(7, 4)).Returns(new Tile(TileType.Blue, new TilePosition() { X = 7, Y = 4 }));
            this.mock.Setup(n => n.CreateRandomTile(7, 5))
                .Returns(new Tile(TileType.Purple, new TilePosition() { X = 7, Y = 5 }));
            this.mock.Setup(n => n.CreateRandomTile(7, 6))
                .Returns(new Tile(TileType.Yellow, new TilePosition() { X = 7, Y = 6 }));
            this.mock.Setup(n => n.CreateRandomTile(7, 7))
                .Returns(new Tile(TileType.Green, new TilePosition() { X = 7, Y = 7 }));
        }
    }
}
