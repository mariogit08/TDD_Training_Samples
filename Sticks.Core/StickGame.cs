using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sticks.Core
{
    public class StickGame
    {
        private const int POINTS_BY_STICK = 10;
        public const int TOTAL_STICKS = 31;
        public int QtdSticks { get; private set; } = TOTAL_STICKS;
        public List<Player> Players { get; internal set; } = new List<Player>();
        public List<string> Turns { get; internal set; } = new List<string>();

        public List<string> TurnsOrder { get; set; } = new List<string>();


        public StickGame()
        {
        }

        public GameStatus GetStatus()
        {
            var gameStatus = default(GameStatus);

            if (QtdSticks == TOTAL_STICKS)
                gameStatus = GameStatus.NotStarted;
            else if (QtdSticks != TOTAL_STICKS && QtdSticks > 0)
                gameStatus = GameStatus.Happening;
            else if (QtdSticks == 0)
                gameStatus = GameStatus.Finished;

            return gameStatus;
        }

        public void AddPlayer(Player player)
        {
            if (Players.Count() + 1 > QtdSticks)
                throw new InvalidOperationException("You cannot add more players than sticks count!");

            if (Players.Any(a => a.Name == player.Name))
                throw new InvalidOperationException("You cannot add two playes with the same name!");

            Players.Add(player);
            TurnsOrder.Add(player.Name);
        }

        public void RemoveSticks(string playerName, int qtd)
        {
            if (qtd > QtdSticks)
                throw new InvalidOperationException("You can not remove sticks anymore!");

            var player = Players.FirstOrDefault(a => a.Name == playerName);
            if (player == null)
                throw new KeyNotFoundException("Player not found!");

            var playerIndex = TurnsOrder.IndexOf(player.Name);
            var nextPlayerIndex = GetNextPlayerIndex();

            if (playerIndex != nextPlayerIndex)
                throw new InvalidOperationException();



            QtdSticks = QtdSticks - qtd;
            Turns.Add(player.Name);

            player.Score += POINTS_BY_STICK * qtd;
        }

        private int GetNextPlayerIndex()
        {
            var lastPlayer = Turns.LastOrDefault();
            if (lastPlayer == null)
                return 0;

            return Turns.IndexOf(lastPlayer) + 1 > TurnsOrder.Count() - 1 ? 0 : Turns.IndexOf(lastPlayer) + 1;
        }

        public Player GetWinner()
        {
            return Players.First(a => a.Score == Players.Max(a => a.Score));
        }
    }
}
