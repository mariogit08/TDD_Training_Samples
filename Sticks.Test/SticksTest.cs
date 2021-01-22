
using System;
using System.Collections.Generic;
using System.Linq;
//using Microsoft.VisualStudio.TestPlatform;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

using NUnit.Framework;
using Sticks.Core;


namespace Sticks.Test
{
    [TestFixture]
    public class SticksTest
    {
        [Test]
        public void GameStartsWith31Sticks()
        {
            var stickGame = new StickGame();
            Assert.IsTrue(stickGame.QtdSticks == 31);
        }

        [Test]
        public void GameStartsWithPlayersListInitialized()
        {
            var stickGame = new StickGame();
            Assert.IsTrue(stickGame.Players != null);
        }

        [Test]
        public void GameStartsWithTurnsListInitialized()
        {
            var stickGame = new StickGame();
            Assert.IsTrue(stickGame.Players != null);
        }

        [Test]
        public void GameRegistersAmountOfScoreAfterAPlayerRemoveAStick()
        {
            var stickGame = new StickGame();
            AddDefaultPlayers(stickGame);
            stickGame.RemoveSticks("mario", 2);
            var playerMario = stickGame.Players.FirstOrDefault(a => a.Name == "mario");
            Assert.IsTrue(playerMario.Score == 20);
        }

        [Test]
        public void GameDoesNotAllowAddMorePlayersThanSticks()
        {
            var stickGame = new StickGame();
            const int TOTAL_STICKS = StickGame.TOTAL_STICKS;
            var stickersCount = new int[TOTAL_STICKS].ToList();
            TestDelegate code = () => { stickersCount.ForEach((i) => stickGame.AddPlayer(new Player($"player{i}"))); };
            Assert.Throws<InvalidOperationException>(code);
        }

        [Test]
        public void GameDoesNotPermitAddMoreThanOnePlayerWithSameName()
        {
            var stickGame = new StickGame();
            Assert.Throws<InvalidOperationException>(() =>
            {
                stickGame.AddPlayer(new Player("mario"));
                stickGame.AddPlayer(new Player("mario"));
            }
            );
        }



        [Test]
        public void GameMustInformTheWinnerCorrectly()
        {
            var stickGame = new StickGame();
            AddDefaultPlayers(stickGame);
            stickGame.RemoveSticks("mario", 14);
            stickGame.RemoveSticks("pc", 16);
            Assert.IsTrue(stickGame.GetWinner().Name == "pc");

            stickGame = new StickGame();
            AddDefaultPlayers(stickGame);
            stickGame.RemoveSticks("mario", 5);
            stickGame.RemoveSticks("pc", 5);
            stickGame.RemoveSticks("mario", 5);
            stickGame.RemoveSticks("pc", 5);
            stickGame.RemoveSticks("mario", 5);
            stickGame.RemoveSticks("pc", 2);
            stickGame.RemoveSticks("mario", 1);
            Assert.IsTrue(stickGame.GetWinner().Name == "mario");
        }

        [Test]
        public void GameMustInformScoreOfPlayersCorrectly()
        {
            var stickGame = new StickGame();
            AddDefaultPlayers(stickGame);
            stickGame.RemoveSticks("mario", 10);
            stickGame.RemoveSticks("pc", 10);
            stickGame.RemoveSticks("mario", 4);
            stickGame.RemoveSticks("pc", 6);
            Assert.IsTrue(stickGame.Players.First(a => a.Name == "mario").Score == 140);
            Assert.IsTrue(stickGame.Players.First(a => a.Name == "pc").Score == 160);

            stickGame = new StickGame();
            AddDefaultPlayers(stickGame);
            stickGame.RemoveSticks("mario", 5);
            stickGame.RemoveSticks("pc", 5);
            stickGame.RemoveSticks("mario", 5);
            stickGame.RemoveSticks("pc", 5);
            stickGame.RemoveSticks("mario", 5);
            stickGame.RemoveSticks("pc", 2);
            stickGame.RemoveSticks("mario", 1);
            Assert.IsTrue(stickGame.Players.First(a => a.Name == "mario").Score == 160);
            Assert.IsTrue(stickGame.Players.First(a => a.Name == "pc").Score == 120);
        }

        [Test]
        public void GameMustInformGameStatus()
        {
            var stickGame = new StickGame();
            AddDefaultPlayers(stickGame);
            Assert.IsTrue(stickGame.GetStatus() == GameStatus.NotStarted);
            stickGame.RemoveSticks("mario", 10);
            Assert.IsTrue(stickGame.GetStatus() == GameStatus.Happening);
            stickGame.RemoveSticks("pc", 10);
            stickGame.RemoveSticks("mario", 4);
            stickGame.RemoveSticks("pc", 7);
            Assert.IsTrue(stickGame.GetStatus() == GameStatus.Finished);
        }


        [Test]
        public void GameDecreaseQuantitySticksCorretlyAfterAPlayerRemoveOne()
        {
            var stickGame = new StickGame();
            AddDefaultPlayers(stickGame);
            stickGame.RemoveSticks("mario", 2);
            Assert.IsTrue(stickGame.QtdSticks == 29);
        }

        [Test]
        public void GameThrowsInvalidOperationExceptionWhenSomeoneTriesToRemoveMoreSticksThanTheTotalOfSticks()
        {
            var stickGame = new StickGame();
            AddDefaultPlayers(stickGame);
            Assert.Throws<InvalidOperationException>(() =>
            {
                stickGame.RemoveSticks("pc", 32);
            });

            stickGame = new StickGame();
            AddDefaultPlayers(stickGame);

            Assert.Throws<InvalidOperationException>(() =>
            {
                stickGame.RemoveSticks("mario", 10);
                stickGame.RemoveSticks("pc", 10);
                stickGame.RemoveSticks("mario", 6);
                stickGame.RemoveSticks("pc", 6);
            });
        }

        [Test]
        public void GameThrowsKeyNotFoundExceptionWhenWasInputedAInvalidPlayer()
        {
            var stickGame = new StickGame();
            AddDefaultPlayers(stickGame);
            Assert.Throws<KeyNotFoundException>(() =>
            {
                stickGame.RemoveSticks("incorrectPlayer", 5);
            });
        }

        [Test]
        public void PlayerIsPresentInPlayersListWhenWasAddedBefore()
        {
            var stickGame = new StickGame();
            stickGame.AddPlayer(new Player("mario"));
            stickGame.Players.First(a => a.Name == "mario");
        }

        [Test]
        public void GameControlTurnsPlayerShouldPlayOnceByGame_ThrowsInvalidOperationExceptionWhenPlayerPlaysInWrongMoment()
        {
            var stickGame = new StickGame();
            stickGame.AddPlayer(new Player("mario"));
            stickGame.AddPlayer(new Player("pc"));
            stickGame.AddPlayer(new Player("maria"));
            stickGame.AddPlayer(new Player("anacleto"));
            stickGame.AddPlayer(new Player("malu"));

            TestDelegate assert = () => stickGame.RemoveSticks("anacleto", 1);
            Assert.Throws<InvalidOperationException>(assert);
        }

        private static void AddDefaultPlayers(StickGame stickGame)
        {
            stickGame.AddPlayer(new Player("mario"));
            stickGame.AddPlayer(new Player("pc"));
        }
    }





}
