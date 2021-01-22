using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeTests
{
    [TestFixture]
    public class TicTacToeTests
    {

        [Test]
        public void BoxFilledAfterGamerChooseIt()
        {
            var game = new Game();
            game.FillBox(1, State.Cross);
            var boxIsFilledCorrectly = game.GetState(1) == State.Cross;
            Assert.IsTrue(boxIsFilledCorrectly);
        }

        [Test]
        public void AllBoxesAreInitializedAsUnfilledStartingGame()
        {
            var game = new Game();
            for (int i = 0; i < 9; i++)
            {
                var statusIsUnfilled = game.GetState(i) == State.Unfilled;
                Assert.IsTrue(statusIsUnfilled);
            }
        }

        [Test]
        public void ThrowExceptionWhenIndexIsOutOfRangeOfGameMatrix()
        {
            var game = new Game();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                game.FillBox(0, State.Cross);
            });

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                game.FillBox(10, State.Cross);
            });

        }

        [Test]
        public void ThrowAnExceptionWhenSameGamerFillTwice()
        {
            var game = new Game();
            Assert.Throws<InvalidOperationException>(() =>
            {
                game.FillBox(1, State.Cross);
                game.FillBox(5, State.Cross);
            });
        }

        [Test]
        public void ThrowAnExceptionWhenGamerTryToFillFilledBox()
        {
            var game = new Game();
            Assert.Throws<InvalidOperationException>(() =>
            {
                game.FillBox(1, State.Cross);
                game.FillBox(1, State.Ball);
            });
        }

        [Test]
        public void GamerCrossWinWhenTheMovesFormALine()
        {
            var game = new Game();
            game.FillBox(1, State.Cross);
            game.FillBox(3, State.Ball);
            game.FillBox(4, State.Cross);
            game.FillBox(5, State.Ball);
            game.FillBox(7, State.Cross);
            Assert.IsTrue(game.GameIsOver());
            Assert.IsTrue(game.GetWinner() == State.Cross);
        }

        [Test]
        public void GamerBallWinWhenTheMovesFormADiagonal()
        {
            var game = new Game();
            game.FillBox(1, State.Ball);
            game.FillBox(3, State.Cross);
            game.FillBox(5, State.Ball);
            game.FillBox(4, State.Cross);
            game.FillBox(9, State.Ball);
            Assert.IsTrue(game.GameIsOver());
            Assert.IsTrue(game.GetWinner() == State.Ball);
        }

        [Test]
        public void GameEndsInADrawWhenTheAllMovesDoesNotCompleteALine()
        {
            var game = new Game();
            game.FillBox(1, State.Cross);
            game.FillBox(8, State.Ball);
            game.FillBox(5, State.Cross);
            game.FillBox(3, State.Ball);
            game.FillBox(2, State.Cross);
            game.FillBox(4, State.Ball);
            game.FillBox(6, State.Cross);
            game.FillBox(9, State.Ball);
            game.FillBox(7, State.Cross);
            Assert.IsTrue(game.GameIsOver());
            Assert.IsTrue(game.GetWinner() == null);
        }
    }

    public class Game
    {
        List<State> turns = new List<State>();
        State[] matrix = new State[10];

        public Game()
        {
        }

        public void FillBox(int index, State state)
        {
            if (index < 1 || index > 9)
                throw new ArgumentOutOfRangeException();

            if (turns.Count() > 0 && turns.Last() == state)
                throw new InvalidOperationException();

            if (matrix[index] != State.Unfilled)
                throw new InvalidOperationException();

            matrix[index] = state;
            turns.Add(state);
        }

        public State GetState(int index)
        {
            return matrix[index];
        }

        public bool GameIsOver()
        {
            var allBoxFilled = true;
            for (int i = 1; i <= 9; i++)
            {
                var statusIsUnfilled = GetState(i) == State.Unfilled;
                if (statusIsUnfilled)
                {
                    allBoxFilled = false;
                    break;
                }
            }

            var anyoneWonTheGame = CheckIfGamerWonTheGame(State.Ball) || CheckIfGamerWonTheGame(State.Cross);
            if (allBoxFilled || anyoneWonTheGame)
                return true;
            else
                return false;
        }

        private bool CheckIfGamerWonTheGame(State state)
        {
            var line1Completed = matrix[1] == state && matrix[2] == state && matrix[3] == state;
            var line2Completed = matrix[1] == state && matrix[4] == state && matrix[7] == state;
            var line3Completed = matrix[1] == state && matrix[5] == state && matrix[9] == state;

            var line4Completed = matrix[7] == state && matrix[8] == state && matrix[9] == state;
            var line5Completed = matrix[7] == state && matrix[5] == state && matrix[3] == state;

            var line6Completed = matrix[2] == state && matrix[5] == state && matrix[8] == state;
            var line7Completed = matrix[3] == state && matrix[5] == state && matrix[7] == state;
            var line8Completed = matrix[4] == state && matrix[5] == state && matrix[6] == state;

            var line9Completed = matrix[9] == state && matrix[6] == state && matrix[3] == state;
            var line10Completed = matrix[8] == state && matrix[5] == state && matrix[2] == state;

            if (line1Completed || line2Completed || line3Completed || line4Completed || line5Completed || line6Completed || line7Completed || line8Completed || line9Completed || line10Completed)
                return true;
            else
                return false;
        }

        public State? GetWinner()
        {
            if (CheckIfGamerWonTheGame(State.Cross))
                return State.Cross;
            if (CheckIfGamerWonTheGame(State.Ball))
                return State.Ball;

            return null;
        }
    }

    public enum State
    {
        Unfilled,
        Ball,
        Cross,
    }
}
