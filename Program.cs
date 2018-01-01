using System;
using CastleGrimtol.Project;

namespace CastleGrimtol
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
                        continue;
                    }
                }
                if (game.Replay)
                {
                    Playing = true;
                    Console.Clear();
                }
                else
                {
                    Playing = false;
                }
            }
        }
    }
}
