using Sticks.Core;
using System;

namespace Sticks.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello! Stick Game has started!");
            Console.WriteLine("\nThe 1rst step is create the players");
            var cont = 1;
            var playerName = "";
            var stickGame = new StickGame();
            CreatePlayersStep(cont, stickGame);
            Console.Clear();
            Console.WriteLine("\n\nLet's go to the game!");

            while (stickGame.GetStatus() != GameStatus.Finished)
            {
                foreach (var player in stickGame.Players)
                {
                    Console.WriteLine($"\n[Player {player.Name}] - How many sticks?");
                    int.TryParse(Console.ReadLine(), out int quantity);
                    stickGame.RemoveSticks(player.Name, quantity);
                    GiveFeedBack(quantity);
                    Console.WriteLine($"{stickGame.QtdSticks} sticks remaining!");

                    if (stickGame.GetStatus() == GameStatus.Finished)
                        break;
                }
            }
            Console.WriteLine($"And the winner was {stickGame.GetWinner().Name} with {stickGame.GetWinner().Score} points!");
            Console.WriteLine($"Really nice! Bye!");

        }

        private static void GiveFeedBack(int quantity)
        {
            switch (quantity)
            {
                case 0:
                    Console.WriteLine($"Ok, try to be more careful!");
                    break;
                case 1:
                    Console.WriteLine($"Ok, its was good, lets improve in the next!");
                    break;
                case 2:
                    Console.WriteLine($"Hum.. Its was cool!");
                    break;
                default:
                    Console.WriteLine($"GREAT! You are going very well!");
                    break;
            }
        }

        private static void CreatePlayersStep(int cont, StickGame stickGame)
        {
            while (true)
            {
                Console.WriteLine($"\nPlayer {cont}");
                Console.Write($"Name:");
                var userInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(userInput.Trim()))
                {
                    stickGame.AddPlayer(new Player(userInput));
                    cont++;
                }
                else
                {
                    Console.WriteLine($"Name cannot be empty or white space!");
                    continue;
                }

                while (userInput != "y" && userInput != "n")
                {
                    Console.WriteLine($"\nDo you want add one more player? (y to yes - n to no)");
                    userInput = Console.ReadLine();
                    if (userInput == "y")
                        continue;
                    if (userInput == "n")
                        return;
                }
            }
        }
    }
}
