using TicTacToeTests;
using System;
using System.Collections.Generic;

namespace TicTacToeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Let's Start");
            RunGame();
        }

        public static void RunGame()
        {
            var game = new Game();
            var lastGamer = State.Ball;
            while (game.GameIsOver() == false)
            {
                for (int i = 1; i <= 9; i = i + 3)
                {
                    Console.Write(GetExibition(i, game)); Console.Write("|"); Console.Write($"{GetExibition(i + 1, game)}"); Console.Write("|"); Console.Write($"{GetExibition(i + 2, game)}");
                    Console.WriteLine();
                    Console.Write("-------"); Console.Write("|"); Console.Write("-------"); Console.Write("|"); Console.Write("-------");
                    Console.WriteLine();
                }

                Console.WriteLine("Write your choice...");
                var key = int.Parse(Console.ReadKey().KeyChar.ToString());
                var gamer = lastGamer == State.Cross ? State.Ball : State.Cross;
                game.FillBox(key, gamer);
                lastGamer = gamer;
                Console.WriteLine();
            }

            var winner = game.GetWinner();
            if (winner.HasValue)
                Console.WriteLine($"And the Winner is {game.GetWinner()}");
            else
            {
                Console.WriteLine($"It's a Draw! ");
                Console.WriteLine($"Deu Velha Boy! ");
            }

            static string GetExibition(int i, Game game)
            {
                var icons = new Dictionary<State, string>()
                {
                    {State.Ball,"O" },
                    {State.Cross,"X" },
                };


                var state = game.GetState(i);
                var box = state == State.Unfilled ? i.ToString() : icons[state];
                return $"   {box}   ";
            }
        }
    }
}
