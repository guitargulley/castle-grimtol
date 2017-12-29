using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Game : IGame
    {
        public Room CurrentRoom { get; set; }
        public Player CurrentPlayer { get; set; }
        public List<Room> GameRooms { get; set; }

        public void Reset()
        {
            Console.WriteLine("Reset everything?");
            Console.ReadLine();
        }

        internal void GetRoomItems(Room currentRoom)
        {
            if(currentRoom.Items.Count != 0)
            {
                Console.WriteLine("These items are in the room...");
            for(int i = 0; i< currentRoom.Items.Count; i++)
            {
                string item = currentRoom.Items[i].Name;
                Console.WriteLine(item);
            }
            }
        }

        public string GetUserInput()
        {
            Console.Write($"What would you like to do {CurrentPlayer.Name}?: ");
            return Console.ReadLine();

        }
        public void SetCurrentPlayer(string player)
        {
            CurrentPlayer = new Player(player);
        }
        internal void HandleUserInput(string userInput)
        {


            if (userInput == "look")
            {
                Look();
            }
            else if (userInput == "help")
            {
                Help();
            }
            //figure out what to do with input
            // LOOK, GO, USE, HELP, ETC..
            else if (userInput.Contains(" "))
            {
                var choice = userInput.Split(' ');
                var command = choice[0];
                var option = choice[1];
                if (command == "go" && option != null)
                {
                    Go(option);
                }
                else if (command == "take" && option != null)
                {
                    TakeItem(option);
                }
                else if (command == "drop" && option != null)
                {
                    DropItem(option);
                }
                else if (command == "use" && option != null)
                {
                    UseItem(option);
                }
                else
                {
                    Console.WriteLine("I did not understand your command");
                }
            }

        }

        public void Look()
        {

        }
        public void Start()
        {

        }
        public void Go(string direction)
        {
            // given a string direction...
            // check if the current.exts contains a key for direction
            if (CurrentRoom.Exits.ContainsKey(direction))
            {
                if (!CurrentRoom.Exits[direction].IsLocked)
                {
                    CurrentRoom = CurrentRoom.Exits[direction];
                    if (CurrentRoom.Name == "Painting")
                    {
                        if (!CurrentRoom.Visited)
                        {
                            PaintingRiddle();
                        }
                       
                    }
                    else if (CurrentRoom.Name == "Room 3S")
                    {
                        RemoveBook();
                    }
                    else if (CurrentRoom.Name == "Room 5E")
                    {
                        TrapDoor();
                    }
                    else if (CurrentRoom.Name == "Room 4E")
                    {
                        SecretRoom();
                    }


                }
                else
                {
                    Console.WriteLine("This Room Appears to be locked. Use the appropriate key to unlock this door.");
                    Look();
                }
                //what if locked
            }
        }
        public void TakeItem(string option)
        {
            for(int i = 0; i<CurrentRoom.Items.Count; i++)
            {
                Item item = CurrentRoom.Items[i];
                if(item.Name.ToLower() == option){
                    CurrentPlayer.Inventory.Add(item);
                    Console.WriteLine($"{item.Name} has been added to your inventory");
                    CurrentRoom.Items.Remove(item);
                }
                else
                {
                    Console.WriteLine($"{option} does not exist in this room.");
                }
            }
             for(int j = 0; j<CurrentPlayer.Inventory.Count; j++)
                {
                    Item playerItem = CurrentPlayer.Inventory[j];
                    Console.WriteLine(playerItem.Name);
                }
        }
        public void DropItem(string userInput)
        {

        }
        public void Help()
        {

        }

        public void Setup()
        {
            Console.WriteLine("Halt traveler. Identify yourself.");
            Console.Write("I go by: ");
            String player = Console.ReadLine();
            SetCurrentPlayer(player);


            Item rustyDagger = new Item("rustyDagger", "Battle Worn Dagger that has been rusted and dulled");
            Item rustyKnife = new Item("rustyKnife", "Small Single Edged Knife that has been rusted and dulled");
            Item woodenShield = new Item("woodenShield", "Battle Worn Wooded Shield that has been splintered and cracked");
            Item greatSword = new Item("greatSword", "Two Handed Steel Sword, with gold and silver details along the handle. Provides heavy damage, but slower to attack with.");
            Item battleAxe = new Item("battleAxe", "Double sided axe. heavy damage, sligthly slower attack than traditional light axe.");
            Item heavyArmor = new Item("heavyArmor", "Heavy duty steel armor, reduces health loss from during attacks.");
            Item lightArmor = new Item("ligthArmor", "Light armor, designed for stealth and speed, reduces damage from incoming attacks");
            Item healthPoultice = new Item("healthPoultice", "Health potion that recovers health that has been lost during battle");
            Item brassKey = new Item("brassKey", "Used for unlocking a single brass lock");
            Item decorativeKey = new Item("decorativeKey", "This key is absolutely useless.");
            Item silverKey = new Item("silverKey", "unlocks many of the cells in the dungeon.");
            Item goldKey = new Item("goldKey", "Unlocks the final room");
            Item book = new Item("book", "Used to unlock the secret room behind the book case in room 4");
            Item wine = new Item("wine", "Disorients player, making player go in a random direction for 3 moves");
            Item food = new Item("food", "Food can be used to distract animals, as well as provide small health regeneration");
            Item rock = new Item("rock", "Large rock can be used in place of other objects that control pressure plates");
            Item crownOfFire = new Item("Crown of Fire", "Once the player equips this, the game is over.");
            List<Item> GameItems = new List<Item>();

            GameItems.Add(rustyDagger);
            GameItems.Add(rustyKnife);
            GameItems.Add(woodenShield);
            GameItems.Add(greatSword);
            GameItems.Add(battleAxe);
            GameItems.Add(heavyArmor);
            GameItems.Add(lightArmor);
            GameItems.Add(healthPoultice);
            GameItems.Add(brassKey);
            GameItems.Add(decorativeKey);
            GameItems.Add(silverKey);
            GameItems.Add(goldKey);
            GameItems.Add(book);
            GameItems.Add(wine);
            GameItems.Add(food);
            GameItems.Add(crownOfFire);

            Room Room0 = new Room("Room 0", "First floor Main Hallway");
            Room Room0N = new Room("Room 0 N", "The North side of the main hallway");
            Room Room1 = new Room("Room 1", "You hear the floor creek as you cross the threshold into this room.. SLAM!!! Click!!! You Look behind you and see that the door has shut and locked behind you. You try to open it with no luck.There is no keyhole on the door However. A large Painting appears on the wall to the east.");
            Room Painting = new Room("Painting", "The Painting grows larger as you move toward it. You see some writing, it appears to be a riddle. \" To Exit you Must Solve this Riddle...\"");
            Room Room2 = new Room("Room 2", "First floor, Large table with food and drink to the North, staircase leading to second Floor to the West");
            Room Room2N = new Room("Room 2 North", "First floor, Large table with food and drink. There is Also a large heavy rock lying on the ground.");
            Room Room3 = new Room("Room 3", "First Floor, Room has large desk with a book sitting on it to the South");
            Room Room3S = new Room("Room 3 South", "In the middle of the large desk in front of you sits a large heavy book. The book looks important.");
            Room Room4 = new Room("Room 4", "First Floor, Room has Door to South, and Bookshelf to East");
            Room Room4s = new Room("Room 4s", "Secret room off of room 4, requires book to open bookshelf");
            Room Room5 = new Room("Room 5", "First Floor, room has a large luxurious rug on the ground spanning the width of the room. to the East is a large desk, you can see a large stack of gold coins sitting on it.");
            Room Room5E = new Room("Room 5E", "TRAP DOOR!");
            Room StairCase = new Room("Stairs", "Stairs from first floor to second floor");
            Room Room10 = new Room("Dungeon Room 10", "Secret entrance to dungeon");
            Room Room14 = new Room("Dungeon Room 14", "Accessed by falling through trap door, taking damage");
            List<Room> GameRooms = new List<Room>();
            CurrentRoom = Room0;

            Room4s.IsLocked = true;
            Room5.IsLocked = true;
            Room10.IsLocked = true;

            GameRooms.Add(Room0);
            GameRooms.Add(Room0N);
            GameRooms.Add(Room1);
            GameRooms.Add(Painting);
            GameRooms.Add(Room2);
            GameRooms.Add(Room2N);
            GameRooms.Add(Room3);
            GameRooms.Add(Room3S);
            GameRooms.Add(Room4);
            GameRooms.Add(Room5);
            GameRooms.Add(Room5E);
            GameRooms.Add(StairCase);
            GameRooms.Add(Room10);
            GameRooms.Add(Room14);

            Room0.Exits.Add("w", Room2);
            Room0.Exits.Add("e", Room1);
            Room0.Exits.Add("s", Room0);
            Room0.Exits.Add("n", Room0N);

            Room0N.Exits.Add("w", Room3);
            Room0N.Exits.Add("e", Room4);
            Room0N.Exits.Add("s", Room0);
            Room0N.Exits.Add("n", Room0N);

            Room1.Exits.Add("w", Room0);
            Room1.Exits.Add("e", Painting);
            Room1.Exits.Add("s", Room1);
            Room1.Exits.Add("n", Room1);

            Room1.Items.Add(brassKey);
            

            Painting.Exits.Add("w", Room1);
            Painting.Exits.Add("e", Painting);
            Painting.Exits.Add("s", Painting);
            Painting.Exits.Add("n", Painting);

            Room2.Exits.Add("w", StairCase);
            Room2.Exits.Add("e", Room0);
            Room2.Exits.Add("s", Room2);
            Room2.Exits.Add("n", Room2N);

            Room2N.Exits.Add("w", Room2N);
            Room2N.Exits.Add("e", Room2N);
            Room2N.Exits.Add("s", Room2);
            Room2N.Exits.Add("n", Room2N);

            Room2N.Items.Add(wine);
            Room2N.Items.Add(food);
            Room2N.Items.Add(rock);

            Room3.Exits.Add("w", Room3);
            Room3.Exits.Add("e", Room0);
            Room3.Exits.Add("s", Room3);
            Room3.Exits.Add("n", Room3);

            Room3S.Exits.Add("w", Room3S);
            Room3S.Exits.Add("e", Room3S);
            Room3S.Exits.Add("s", Room3S);
            Room3S.Exits.Add("n", Room3);

            Room4.Exits.Add("w", Room2);
            Room4.Exits.Add("e", Room4s);
            Room4.Exits.Add("s", Room5);
            Room4.Exits.Add("n", Room0);

            Room4s.Exits.Add("w", Room4);
            Room4s.Exits.Add("e", Room10);
            Room4s.Exits.Add("s", Room4s);
            Room4s.Exits.Add("n", Room4s);

            Room5.Exits.Add("w", Room5);
            Room5.Exits.Add("e", Room5E);
            Room5.Exits.Add("s", Room5);
            Room5.Exits.Add("n", Room4);

            Room5E.Exits.Add("down", Room14);

            bool noInventory = true;
            while (noInventory)
            {

                Console.WriteLine($@"
            Welcome {CurrentPlayer.Name} to the castle of .
            I assume you are here to claim the Crown of Fire. 
            I must warn you. Many have come, and none have survived. 
            Caution will be your ally in this quest.
            To start you off, I shall supply you with one item. 
            You may choose:
            1: rusty dagger,
            2: rusty knife,
            3: wooden shield
            What Say You?
            ");
                var selection = Console.ReadLine();
                if (selection == "1")
                {
                    CurrentPlayer.Inventory.Add(rustyDagger);
                    Console.WriteLine($"A {rustyDagger.Name} has been added to your inventory");
                    noInventory = false;
                }
                else if (selection == "2")
                {
                    CurrentPlayer.Inventory.Add(rustyKnife);
                    Console.WriteLine($"A {rustyKnife.Name} has been added to your inventory");
                    noInventory = false;
                }
                else if (selection == "3")
                {
                    CurrentPlayer.Inventory.Add(woodenShield);
                    Console.WriteLine($"A {woodenShield.Name} has been added to your inventory");
                    noInventory = false;
                }
                else
                {
                    Console.WriteLine("I did not understand your request.");
                    continue;
                }
            }
            Start();
        }
        public void SecretRoom()
        {

        }
        public void DisplayCase()
        {

        }
        public void SkeletonFight()
        {

        }
        public void BossFight()
        {

        }
        public void Intoxicated()
        {

        }
        public void RemoveBook()
        {

        }
        public void PaintingRiddle()
        {
            int guess = 0;
            bool inRiddle = true;
            string answer = "time";
            Console.Clear();
            Console.WriteLine(CurrentRoom.Description);
            while (inRiddle)
            {
                if (guess < 3)
                {
                    Console.WriteLine("Mountains will crumble and temples will fall, and no man can survive its endless call. What is it?");
                    Console.WriteLine($"You have {3 - guess} guesses remaining.");
                    string response = Console.ReadLine().ToLower();
                    if (response == answer)
                    {
                        Console.WriteLine("You have proven your wisdom. You may continue on your quest.");
                        inRiddle = false;
                        CurrentRoom.Exits["w"].IsLocked = false;
                        CurrentRoom.Exits["w"].Description = "You are in a large room. To the East is an empty picture frame, to the West is the main hallway.";
                        CurrentRoom.Visited = true;
                        Console.WriteLine("The door to your west has unlocked and swung back open.");
                        CurrentRoom.Description = "On the wall there is now an empty picture frame. To the West is a door leading to the main hallway.";
                        continue;
                    }
                    else
                    {
                        guess++;
                        Console.Clear();
                        Console.WriteLine("Your answer was incorrect.");
                        if(guess == 3)
                        {
                        continue;
                        }
                        else
                        {
                        Console.WriteLine("The walls start to slowly collapse around you. You begin to feel trapped. You fear that you will not get out of this.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("You have used up all of your guesses. The walls continue to collapse on you. You can no longer move. You figure out the answer to the riddle, but it is too late. You have died.");
                    GameOver();
                    inRiddle = false;
                }
            }
        }
        public void TrapDoor()
        {
            CurrentRoom = CurrentRoom.Exits["down"];
            PitFall();
        }
        public void PitFall()
        {

        }
        public void GameOver()
        {
            Console.WriteLine("GAME OVER");
            Reset();
        }
        public void UseItem(string itemName)
        {

        }

    }
}