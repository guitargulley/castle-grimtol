using System;
using CastleGrimtol.Project;

namespace CastleGrimtol
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            bool Playing = true;
            while (Playing)
            {
                Game game = new Game();
                Console.Clear();
                game.Setup();
                game.InGame = true;
                while (game.InGame)
                {
                    if (!game.InGame)
                    {
                        game.HandleUserInput("q");

                    }
                    Console.WriteLine($@"
 {game.CurrentRoom.Description}
                    ");
                    game.GetRoomItems(game.CurrentRoom);
                    var userInput = game.GetUserInput().ToLower();

                    game.HandleUserInput(userInput);
                    //TO QUIT THE GAME
                    if (userInput == "q")
                    {
                        game.InGame = false;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Clear();
                        continue;
                    }
                }
                if (game.Replay)
                {
                    Playing = true;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    Playing = false;
                }
            }
        }
    }
}
