using System;
using CastleGrimtol.Project;

namespace CastleGrimtol
{
    public class Program
    {
        public static void Main(string[] args)
        {


            Game game = new Game();
            game.Setup();
            bool playing = true;
        

            while (playing)
            {
                Console.WriteLine(game.CurrentRoom.Description);
                game.GetRoomItems(game.CurrentRoom);
                // Console.WriteLine("This is the room at: " ,game.GameRooms[1].Name);
                var userInput = game.GetUserInput().ToLower();
                
                game.HandleUserInput(userInput);
                //TO QUIT THE GAME
                 if (userInput == "q")
                {
                    playing = false;
                    continue;
                }
            }

        }
    }
}
