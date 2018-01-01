using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Game : IGame
    {
        public Room CurrentRoom { get; set; }
        public bool InGame { get; set; }
        public bool Replay { get; set; }
        public Player CurrentPlayer { get; set; }
        public List<Room> GameRooms { get; set; }
        public List<Enemy> Enemies { get; set; }
        public Enemy CurrentEnemy { get; set; }
        public List<Item> GameItems { get; set; }



        public void Reset()
        {
            bool inReset = true;
            while (inReset)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Play Again? Y/N");
                string userInput = Console.ReadLine().ToLower();
                if (userInput == "y")
                {
                    Replay = true;
                    InGame = false;
                    inReset = false;
                }
                else if (userInput == "n")
                {
                    InGame = false;
                    inReset = false;
                }
                else
                {
                    Console.WriteLine("I did not understand your response.");
                    continue;
                }
            }
        }

        internal void GetRoomItems(Room currentRoom)
        {
            if (currentRoom.Items.Count != 0)
            {
                Console.WriteLine("These items are in the room...");
                for (int i = 0; i < currentRoom.Items.Count; i++)
                {
                    string item = currentRoom.Items[i].Name;
                    Console.WriteLine(item);
                }
            }
        }

        public string GetUserInput()
        {
            Console.Write($"\nWhat would you like to do {CurrentPlayer.Name}?: ");
            return Console.ReadLine();

        }
        public void SetCurrentPlayer(string player)
        {
            CurrentPlayer = new Player(player);
        }

        public void CheckInventory()
        {
            Console.Clear();
            for (int i = 0; i < CurrentPlayer.Inventory.Count; i++)
            {
                Item item = CurrentPlayer.Inventory[i];
                Console.WriteLine(item.Name);
            }
        }
        public void Look()
        {
            Console.Clear();

        }

        public void Go(string direction)
        {
            // given a string direction...
            // check if the current.exts contains a key for direction
            if (CurrentRoom.Exits.ContainsKey(direction))
            {
                if (!CurrentRoom.Exits[direction].IsLocked)
                {
                    var PrevRoom = CurrentRoom;
                    Console.Clear();
                    CurrentRoom = CurrentRoom.Exits[direction];
                    if (CurrentRoom.Name == "Room 1")
                    {
                        if (!CurrentRoom.Visited)
                        {
                            CurrentRoom.Exits["w"].IsLocked = true;
                            CurrentRoom.Visited = true;
                        }
                    }
                    else if (CurrentRoom.Name == "Painting")
                    {
                        if (!CurrentRoom.Visited)
                        {
                            PaintingRiddle();
                        }
                    }
                    else if (CurrentRoom.Name == "Room 3 South")
                    {
                        BookRoom();
                    }
                    else if (CurrentRoom.Name == "Dungeon Cell" && !CurrentRoom.Visited)
                    {
                        SkeletonFight();
                        CurrentRoom.Description = "The bars that surround this cell fell cold, they are as black as coal. An exit is to your East";
                    }
                    else if (CurrentRoom.Name == "Upstairs Reading Room" && !CurrentRoom.Visited)
                    {
                        SkeletonFight();
                        CurrentRoom.Description = "The room smells of leather and mohagany. You look closely at the room and find many bookshelves of historic first edition books. There is an exit to your east.";
                    }
                    else if (CurrentRoom.Name == "Room 5E")
                    {
                        TrapDoor();
                    }
                    else if (CurrentRoom.Name == "Room 4s")
                    {
                        SecretRoom();
                    }
                    else if (CurrentRoom.Name == "Display Case")
                    {
                        if (CurrentRoom.Items.Count != 0)
                        {
                            Console.WriteLine("The door shuts behind you. You cannot exit the room.");
                            DisplayCase();
                        }
                    }
                    else if (CurrentRoom.Name == "Upstairs Master Suite")
                    {
                        Console.WriteLine("");
                        BossFight();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("This Room Appears to be locked. Use the appropriate key to unlock this door.");
                    Look();
                }
            }
        }
        public void TakeItem(string option)
        {
            for (int i = 0; i < CurrentRoom.Items.Count; i++)
            {
                Item item = CurrentRoom.Items[i];
                if (item.Name.ToLower() == option)
                {
                    if (option == "book")
                    {
                        CurrentPlayer.Inventory.Add(item);
                        Console.Clear();
                        Console.WriteLine($"{item.Name} has been added to your inventory");
                        CurrentRoom.Items.Remove(item);
                        RemoveBook(item);
                    }
                    else if (option == "rock" && CurrentRoom.Name == "Room 3 South")
                    {
                        CurrentPlayer.Inventory.Add(item);
                        Console.Clear();
                        Console.WriteLine($"{item.Name} has been added to your inventory");
                        CurrentRoom.Items.Remove(item);
                        RemoveBook(item);
                    }
                    else
                    {
                        CurrentPlayer.Inventory.Add(item);
                        Console.Clear();
                        Console.WriteLine($"{item.Name} has been added to your inventory");
                        CurrentRoom.Items.Remove(item);
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"{option} does not exist in this room.");
                }
            }
        }
        public void Help()
        {
            Console.Clear();
            Console.WriteLine($@"
Commands that can be used.
============================================================
look - displays current room.
inventory - displays the list of items you have.
go - accepts the following... w , e, s, n, 
     (up and down when using stairs or ladders).
take - adds items found in rooms to your inventory.
use - uses the items in your inventory for various actions.
q - exit the game.
help - shows commands available.
============================================================
            ");
        }

        public void Setup()
        {
            Replay = false;
            Console.WriteLine("Halt traveler. Identify yourself.");
            Console.Write("I go by: ");
            String player = Console.ReadLine();
            SetCurrentPlayer(player);

            Item rustyDagger = new Item("rustyDagger", "Battle Worn Dagger that has been rusted and dulled");
            Item rustyKnife = new Item("rustyKnife", "Small Single Edged Knife that has been rusted and dulled");
            Item greatSword = new Item("greatSword", "Two Handed Steel Sword, with gold and silver details along the handle. Provides heavy damage, but slower to attack with.");
            Item battleAxe = new Item("battleAxe", "Double sided axe. heavy damage, sligthly slower attack than traditional light axe.");
            Item healthPotion = new Item("healthPotion", "Health potion that recovers health that has been lost during battle");
            Item brassKey = new Item("brassKey", "Used for unlocking a single brass lock");
            Item decorativeKey = new Item("decorativeKey", "This key is absolutely useless.");
            Item steelKey = new Item("steelKey", "unlocks room 17");
            Item silverKey = new Item("silverKey", "unlocks many of the cells in the dungeon.");
            Item goldKey = new Item("goldKey", "Unlocks the final room of the game");
            Item book = new Item("book", "Used to unlock the secret room behind the book case in room 4");
            Item wine = new Item("wine", "Disorients player, making player go in a random direction for 3 moves");
            Item food = new Item("food", "Food can be used to distract animals, as well as provide small health regeneration");
            Item rock = new Item("rock", "Large rock can be used in place of other objects that control pressure plates");
            Item crown = new Item("crownOfFire", "Once the player equips this, the game is over.");
            List<Item> GameItems = new List<Item>();

            GameItems.Add(rustyDagger);
            GameItems.Add(rustyKnife);
            GameItems.Add(greatSword);
            GameItems.Add(battleAxe);
            GameItems.Add(healthPotion);
            GameItems.Add(brassKey);
            GameItems.Add(decorativeKey);
            GameItems.Add(silverKey);
            GameItems.Add(goldKey);
            GameItems.Add(book);
            GameItems.Add(wine);
            GameItems.Add(food);
            GameItems.Add(crown);

            Room Room0 = new Room("Room 0", "First floor Main Hallway");
            Room Room0N = new Room("Room 0 N", "The North side of the main hallway");
            Room Room1 = new Room("Room 1", "You hear the floor creek as you cross the threshold into this room.. \nSLAM!!! Click!!! \nYou Look behind you and see that the door has shut and locked behind you. \nYou try to open it with no luck. However there is no keyhole on the door. \nA large Painting appears on the wall to the east.");
            Room Painting = new Room("Painting", "The Painting grows larger as you move toward it. \nYou see some writing, it appears to be a riddle. \n\" To Exit you Must Solve this Riddle...\"");
            Room Room2 = new Room("Room 2", "Large table with food and drink to the North. \nStaircase leading to second Floor to the West");
            Room Room2N = new Room("Room 2 North", "Large table with food and drink. There is Also a large heavy rock lying on the ground.\nStaircase to the west, and room continues to the South.");
            Room Room3 = new Room("Room 3", "Room has large desk with a book sitting on it to the South");
            Room Room3S = new Room("Room 3 South", "In the middle of the large desk in front of you sits a large heavy book. \nThe book looks important.");
            Room Room4 = new Room("Room 4", "Room has Door to South with a brass padlock on it, and Bookshelf to East");
            Room Room4s = new Room("Room 4s", "Secret room off of room 4, requires book to open bookshelf");
            Room Room5 = new Room("Room 5", "Room has a large luxurious rug on the ground spanning the width of the room. \nTo the East is a large desk. \nYou can see a large stack of gold coins sitting on it.");
            Room Room5E = new Room("Room 5E", "TRAP DOOR!");
            Room StairCase = new Room("Stairs", "Stairs from First floor to Second floor");
            Room StairCase2 = new Room("Stairs", "Stairs from Fisrt floor to Dungeon");
            Room Room6 = new Room("Upstairs Hallway", "You are in a long narrow hallway. There is no source of light. \nYou stumble through this hallway until you can see light. \nThe hallway continues to your North");
            Room Room7 = new Room("Upstairs Hallway South", "There are hundreds of lit candles covering the walls of this hallway. \nThere is a room to the West. \nThe hallway continues to the North and South");
            Room Room7N = new Room("Upstairs Hallway North", "There are hundreds of lit candles covering the walls of this hallway. \nthere is a room to the West with a gold padlock on it. \nThe hallway continues to the South");
            Room Room8 = new Room("Upstairs Reading Room", "You enter the room and are instantly attacked by a Skeleton Ranger! You must fight!");
            Room Room9 = new Room("Upstairs Master Suite", "There is a large Throne sitting in the middle of the room. \nThere is a balcony to the West and the hallway to the East.");
            Room Balcony = new Room("Balcony", "You see the great Crown of Fire sitting on a large table.");
            Room Ladder = new Room("Ladder", "The ladder takes you through a secret passage into the dungeon");
            Room Room10 = new Room("Dungeon Room 10", "Secret entrance to dungeon. \nThe door to the west is locked by a silver padlock.");
            Room Room10W = new Room("Duneon Room 10 west", "Narrow hallway, exits to the west and east.");
            Room Room11 = new Room("Dungeon Hallway North", "A long hallway dimly lit by candles. \nThere are rooms to your East and your West. \nThe hallway continues to the south");
            Room Room12 = new Room("Dungeon Hallway", "A long hallway dimly lit by candles. \nThere are rooms to your East, and West. \nThe hallway continues to your North and South");
            Room Room13 = new Room("Dungeon Hallway South", "A long hallway dimly lit by candles. \nThere are rooms to your East and West. \nThe door to your west appears to have a steel lock on it. \nThe hallway continues to your North");
            Room Room14 = new Room("Dungeon Room 14", $"you fall hearing a crunch as you hit the hard cement floor. \nYou have sustained serious injury. You have {CurrentPlayer.Health - 20} health remaining. \nYou stand up only to be greeted by a large Troll!");
            Room Room15 = new Room("Dungeon Cell", "You walk into an old dungeon cell. \nYou are instantly greeted by a Skeleton Warrior!");
            Room Room16 = new Room("Dungeon Display Room", "You see a large display case with a floating gold key inside of it to the east. \nThere is an exit to your West");
            Room DisplayCase = new Room("Display Case", "The gold key grows as you approach it. \nThe case is a triangular shape, each side of it has distinct writing on it. \nThey appear to be riddles.");
            Room Room17 = new Room("Dungeon Stair Entrance", "You enter a small room that is very dimly lit. \nThere is a door to the East, however it is locked with a steel padlock");
            Room Room18 = new Room("Dungeon Torture Room", "You enter a large room filled with various torture devices. \nFragmented bones line the floor. \nEvery step you take within these walls you hear Crunch, Crack, Snap. \nThere is an exit to your East.");

            List<Room> GameRooms = new List<Room>();
            CurrentRoom = Room0;

            Room4s.IsLocked = true;
            Room5.IsLocked = true;
            Room10W.IsLocked = true;

            Room9.IsLocked = true;
            Room17.IsLocked = true;

            GameRooms.Add(Painting);
            GameRooms.Add(StairCase);
            GameRooms.Add(StairCase2);
            GameRooms.Add(DisplayCase);
            GameRooms.Add(Ladder);
            GameRooms.Add(Balcony);
            GameRooms.Add(Room0);
            GameRooms.Add(Room0N);
            GameRooms.Add(Room1);
            GameRooms.Add(Room2);
            GameRooms.Add(Room2N);
            GameRooms.Add(Room3);
            GameRooms.Add(Room3S);
            GameRooms.Add(Room4);
            GameRooms.Add(Room5);
            GameRooms.Add(Room5E);
            GameRooms.Add(Room6);
            GameRooms.Add(Room7);
            GameRooms.Add(Room7N);
            GameRooms.Add(Room8);
            GameRooms.Add(Room9);
            GameRooms.Add(Room10);
            GameRooms.Add(Room10W);
            GameRooms.Add(Room11);
            GameRooms.Add(Room12);
            GameRooms.Add(Room13);
            GameRooms.Add(Room14);
            GameRooms.Add(Room15);
            GameRooms.Add(Room16);
            GameRooms.Add(Room17);
            GameRooms.Add(Room18);

            Room15.UsableItems.Add(rustyDagger);
            Room15.UsableItems.Add(rustyKnife);
            Room15.UsableItems.Add(battleAxe);
            Room15.UsableItems.Add(greatSword);

            Room14.UsableItems.Add(rustyDagger);
            Room14.UsableItems.Add(rustyKnife);
            Room14.UsableItems.Add(battleAxe);
            Room14.UsableItems.Add(greatSword);

            Room8.UsableItems.Add(rustyDagger);
            Room8.UsableItems.Add(rustyKnife);
            Room8.UsableItems.Add(battleAxe);
            Room8.UsableItems.Add(greatSword);

            Room9.UsableItems.Add(rustyDagger);
            Room9.UsableItems.Add(rustyKnife);
            Room9.UsableItems.Add(battleAxe);
            Room9.UsableItems.Add(greatSword);
            Room9.UsableItems.Add(healthPotion);

            foreach (Room room in GameRooms)
            {
                room.UsableItems.Add(healthPotion);
                room.UsableItems.Add(food);
                room.UsableItems.Add(wine);
                room.UsableItems.Add(crown);
            }

            Room13.UsableItems.Add(steelKey);
            Room4.UsableItems.Add(brassKey);
            Room10.UsableItems.Add(silverKey);
            Room4.UsableItems.Add(book);
            Room3S.UsableItems.Add(rock);
            Room3S.UsableItems.Add(book);
            Room7N.UsableItems.Add(goldKey);

            Room0.Exits.Add("w", Room2);
            Room0.Exits.Add("e", Room1);
            Room0.Exits.Add("n", Room0N);

            Room0N.Exits.Add("w", Room3);
            Room0N.Exits.Add("e", Room4);
            Room0N.Exits.Add("s", Room0);

            Room1.Exits.Add("w", Room0);
            Room1.Exits.Add("e", Painting);

            Room1.Items.Add(brassKey);

            Painting.Exits.Add("w", Room1);

            Room2.Exits.Add("w", StairCase);
            Room2.Exits.Add("e", Room0);
            Room2.Exits.Add("n", Room2N);

            Room2N.Exits.Add("s", Room2);
            Room2N.Exits.Add("w", StairCase2);

            Room2N.Items.Add(wine);
            Room2N.Items.Add(food);
            Room2N.Items.Add(rock);

            StairCase.Exits.Add("up", Room6);
            StairCase.Exits.Add("down", Room2);

            StairCase2.Exits.Add("up", Room2N);
            StairCase2.Exits.Add("down", Room17);

            Room3.Exits.Add("e", Room0N);
            Room3.Exits.Add("s", Room3S);

            Room3S.Exits.Add("n", Room3);

            Room3S.Items.Add(book);

            Room4.Exits.Add("w", Room0N);
            Room4.Exits.Add("e", Room4s);
            Room4.Exits.Add("s", Room5);

            Room4s.Exits.Add("w", Room4);
            Room4s.Exits.Add("e", Ladder);

            Room4s.Items.Add(battleAxe);
            Room4s.Items.Add(greatSword);
            Room4s.Items.Add(healthPotion);

            Ladder.Exits.Add("down", Room10);

            Room5.Exits.Add("e", Room5E);
            Room5.Exits.Add("n", Room4);

            Room5E.Exits.Add("down", Room14);
            Room5E.Exits.Add("w", Room5);

            Room6.Exits.Add("n", Room7);
            Room6.Exits.Add("w", StairCase);

            Room7.Exits.Add("w", Room8);
            Room7.Exits.Add("n", Room7N);
            Room7.Exits.Add("s", Room6);

            Room7N.Exits.Add("s", Room7);
            Room7N.Exits.Add("w", Room9);

            Room8.Exits.Add("e", Room7);

            Room9.Exits.Add("w", Balcony);
            Room9.Exits.Add("e", Room7N);

            Room10.Exits.Add("w", Room10W);

            Room10.Items.Add(silverKey);

            Room10W.Exits.Add("e", Room10);
            Room10W.Exits.Add("w", Room11);

            Room11.Exits.Add("w", Room18);
            Room11.Exits.Add("e", Room10W);
            Room11.Exits.Add("s", Room12);

            Room12.Exits.Add("n", Room11);
            Room12.Exits.Add("w", Room15);
            Room12.Exits.Add("e", Room14);
            Room12.Exits.Add("s", Room13);

            Room13.Exits.Add("n", Room12);
            Room13.Exits.Add("w", Room17);
            Room13.Exits.Add("e", Room16);

            Room14.Exits.Add("w", Room12);

            Room15.Exits.Add("e", Room12);

            Room15.Items.Add(steelKey);

            Room16.Exits.Add("w", Room13);
            Room16.Exits.Add("e", DisplayCase);

            DisplayCase.Exits.Add("w", Room16);

            DisplayCase.Items.Add(goldKey);

            Room17.Exits.Add("w", StairCase2);
            Room17.Exits.Add("e", Room13);

            Room18.Exits.Add("e", Room11);

            Room18.Items.Add(decorativeKey);

            Balcony.Exits.Add("e", Room9);

            Balcony.Items.Add(crown);

            Enemy warrior = new Enemy("Skeleton Warrior", Room15);
            Enemy ranger = new Enemy("Skeleton Ranger", Room8);
            Enemy troll = new Enemy("Troll", Room14);
            Enemy king = new Enemy("Undead King", Room9);

            List<Enemy> Enemies = new List<Enemy>();

            Room15.Enemy = warrior;
            Room8.Enemy = ranger;
            Room14.Enemy = troll;
            Room9.Enemy = king;

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
            3: health potion
            What Say You?
            ");
                var selection = Console.ReadLine();
                if (selection == "1")
                {
                    CurrentPlayer.Inventory.Add(rustyDagger);
                    Console.Clear();
                    Console.WriteLine($"A {rustyDagger.Name} has been added to your inventory");
                    noInventory = false;
                }
                else if (selection == "2")
                {
                    CurrentPlayer.Inventory.Add(rustyKnife);
                    Console.Clear();
                    Console.WriteLine($"A {rustyKnife.Name} has been added to your inventory");
                    noInventory = false;
                }
                else if (selection == "3")
                {
                    CurrentPlayer.Inventory.Add(healthPotion);
                    Console.Clear();
                    Console.WriteLine($"A {healthPotion.Name} has been added to your inventory");
                    noInventory = false;
                }
                else
                {
                    Console.WriteLine("I did not understand your request.");
                    continue;
                }
            }
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
            else if (userInput == "inventory")
            {
                CheckInventory();
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
                else if (command == "use" && option != null)
                {
                    Item item = null;
                    for (int i = 0; i < CurrentPlayer.Inventory.Count; i++)
                    {
                        Item gItem = CurrentPlayer.Inventory[i];
                        if (option == gItem.Name.ToLower())
                        {
                            item = gItem;
                            UseItem(item);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("I did not understand your command");
                }
            }

        }

        public void SecretRoom()
        {
            while (CurrentRoom.Items.Count != 0)
            {
                CurrentRoom.Description = "As the bookshelf swing open, a large golden chest is revealed. You can exit the room to your West";
                Console.WriteLine(CurrentRoom.Description);
                GetRoomItems(CurrentRoom);
                string next = GetUserInput();
                HandleUserInput(next);
            }
            if (CurrentRoom.Items.Count == 0)
            {
                CurrentRoom.Description = "The large gold chest is empty. A ladder going down appears to the east";
                Console.WriteLine(CurrentRoom.Description);
                GetRoomItems(CurrentRoom);
                string next = GetUserInput();
                HandleUserInput(next);
            }
        }

        public void DisplayCase()
        {
            Console.WriteLine($@"
            To collect this key for your quest,
            You must prove to be the best.
            On each side there is a clue,
            Correct answers will let you through.

            You have 3 guesses for each riddle.
            ");
            int guess = 0;
            string input = "";
            bool inRiddles = true;
            bool riddle1 = false;
            bool riddle2 = false;
            bool riddle3 = false;
            while (inRiddles)
            {
                if (!riddle1)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Black;
                    if (guess < 3)
                    {
                        Console.Clear();
                        Console.WriteLine($@"
                        What word contains all of the twenty six letters?
                        ");
                        Console.WriteLine($"You have {3 - guess} guesses remaining");
                        Console.Write("What is your guess?:");
                        input = Console.ReadLine().ToLower();
                        if (input == "q")
                        {
                            GameOver();
                            return;
                        }
                        if (input == "alphabet" || input == "the alphabet")
                        {
                            Console.WriteLine("You have passed the first test of three, you have 2 riddles remaining to retrieve your prize.");
                            riddle1 = true;
                            guess = 0;
                        }
                        else if (input != "alphabet" || input != "the alphabet")
                        {
                            guess++;
                            Console.WriteLine($"{input} is not the correct answer.");
                            if (guess == 3)
                            {
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("The room slowly fills up with a toxic gass, it becomes harder and harder to breathe");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("The room has completely filled up with the toxic gas. You no longer can breathe. You have been poisoned and die.");
                        GameOver();
                    }
                }
                else if (riddle1 == true && riddle2 == false)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Black;
                    if (guess < 3)
                    {
                        Console.Clear();
                        Console.WriteLine($@"
                        I turn everything around without moving. What am I?
                        ");
                        Console.WriteLine($"You have {3 - guess} guesses remaining");
                        Console.Write("What is your guess?:");
                        input = Console.ReadLine().ToLower();
                        if (input == "q")
                        {
                            GameOver();
                            return;
                        }
                        if (input == "mirror" || input == "a mirror")
                        {
                            Console.WriteLine("You have passed the second test of three, you have 1 riddle remaining to retrieve your prize.");
                            riddle2 = true;
                            guess = 0;
                        }
                        else if (input != "mirror" || input != "a mirror")
                        {
                            guess++;
                            Console.WriteLine($"{input} is not the correct answer.");
                            if (guess == 3)
                            {
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("The room slowly fills up with a toxic gass, it becomes harder and harder to breathe");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("The room has completely filled up with the toxic gas. You no longer can breathe. You have been poisoned and die.");
                        GameOver();
                    }
                }
                else if (riddle1 == true && riddle2 == true && riddle3 == false)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    if (guess < 3)
                    {
                        Console.Clear();
                        Console.WriteLine($@"
                        What cannot talk but will always reply when spoken to?
                        ");
                        Console.WriteLine($"You have {3 - guess} guesses remaining");
                        Console.Write("What is your guess?:");
                        input = Console.ReadLine().ToLower();
                        if (input == "q")
                        {
                            GameOver();
                            return;
                        }
                        if (input == "echo" || input == "an echo")
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("You have passed all 3 tests! You may claim your prize.");
                            riddle3 = true;
                            guess = 0;
                            inRiddles = false;
                            CurrentRoom.Description = "There is a large pillar in the middle of the room.\n There is an exit to your West";
                            CurrentRoom.Exits["w"].Description = "There is an exit to your West, and an empty room to your East";
                        }
                        else if (input != "echo" || input != "an echo")
                        {
                            guess++;
                            Console.WriteLine($"{input} is not the correct answer.");
                            if (guess == 3)
                            {
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("The room slowly fills up with a toxic gass, it becomes harder and harder to breathe");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("The room has completely filled up with the toxic gas. You no longer can breathe. You have been poisoned and die.");
                        GameOver();
                    }
                }
            }
            Console.Clear();
            Console.WriteLine("The display case glass walls retract into a large stone pillar.\n The key is now able to be picked up.");
        }

        public void SkeletonFight()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            CheckInventory();
            Console.WriteLine(CurrentRoom.Description);
            Console.WriteLine($"The {CurrentRoom.Enemy.Name} takes a swing at you but misses!");
            CurrentEnemy = CurrentRoom.Enemy;
            bool inBattle = true;
            CurrentRoom.Visited = true;
            while (inBattle)
            {
                Console.WriteLine($@"
    {CurrentRoom.Enemy.Name} - {CurrentRoom.Enemy.Health} : {CurrentPlayer.Name} - {CurrentPlayer.Health}
                ");
                if (CurrentPlayer.Health > 0 && CurrentRoom.Enemy.Health > 0)
                {
                    Console.WriteLine("What would you like to do?");
                    string fight = Console.ReadLine().ToLower();
                    if (fight == "q")
                        {
                            GameOver();
                            return;
                        }
                    HandleUserInput(fight);
                    if (CurrentRoom.Enemy.Health > 0)
                    {
                        CheckInventory();
                        Console.WriteLine($"The {CurrentRoom.Enemy.Name} swung its sword at you and connected with a mighty thud. You took a substantial amount of damage!");
                        CurrentPlayer.Health -= 8;
                    }
                }
                else if (CurrentPlayer.Health > 0 && CurrentRoom.Enemy.Health <= 0)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Black;
                    CheckInventory();
                    Console.WriteLine($"With the final blow you have defeated the {CurrentRoom.Enemy.Name}");
                    inBattle = false;
                    CurrentRoom.Enemy = null;
                    continue;
                }
                else if (CurrentPlayer.Health <= 0 && CurrentRoom.Enemy.Health > 0)
                {
                    Console.Clear();
                    inBattle = false;
                    GameOver();
                }
            }
        }

        public void BossFight()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            CheckInventory();
            Console.WriteLine($"The {CurrentRoom.Enemy.Name} takes a swing at you, the sword makes contact with you!");
            CurrentPlayer.Health -= 5;
            CurrentEnemy = CurrentRoom.Enemy;
            bool inBattle = true;
            CurrentRoom.Visited = true;
            while (inBattle)
            {
                Console.WriteLine($@"
    {CurrentRoom.Enemy.Name} - {CurrentRoom.Enemy.Health} : {CurrentPlayer.Name} - {CurrentPlayer.Health}
                ");

                if (CurrentPlayer.Health > 0 && CurrentRoom.Enemy.Health > 0)
                {
                    Console.WriteLine("What would you like to do?");
                    string fight = Console.ReadLine().ToLower();
                    if (fight == "q")
                        {
                            GameOver();
                            return;
                        }
                    HandleUserInput(fight);
                    if (CurrentRoom.Enemy.Health > 0)
                    {
                        CheckInventory();
                        Console.WriteLine($"The {CurrentRoom.Enemy.Name} swung its sword at you and connected with a mighty thud. You took a substantial amount of damage!");
                        CurrentPlayer.Health -= 15;
                    }
                }
                else if (CurrentPlayer.Health > 0 && CurrentRoom.Enemy.Health <= 0)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Black;
                    CheckInventory();
                    Console.WriteLine($"With the final blow you have defeated the {CurrentRoom.Enemy.Name}");
                    inBattle = false;
                    CurrentRoom.Enemy = null;
                    continue;
                }
                else if (CurrentPlayer.Health <= 0 && CurrentRoom.Enemy.Health > 0)
                {
                    Console.Clear();
                    inBattle = false;
                    GameOver();
                }
            }
        }

        public void Intoxicated()
        {

        }

        public void BookRoom()
        {
            CurrentRoom.Description = $"In the middle of the large desk in front of you sits a large heavy {CurrentRoom.Items[0].Name}. The book looks important.";
            Console.WriteLine(CurrentRoom.Description);
            GetRoomItems(CurrentRoom);
            string next = GetUserInput();
            HandleUserInput(next);
        }

        public void RemoveBook(Item item)
        {

            CurrentRoom.Exits["n"].IsLocked = true;

            CurrentRoom.Description = $"As you lift the {item.Name} up, a large steel gate drops behind you!. You are trapped in this small room!. On the desk you notice that there is a small pressure plate where the book was sitting. The door is controlled by this plate.";
            while (CurrentRoom.Exits["n"].IsLocked)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(CurrentRoom.Description);
                string nextMove = GetUserInput();
                if (nextMove == "q")
                {
                    GameOver();
                    return;
                }
                HandleUserInput(nextMove);
                for (int i = 0; i < CurrentRoom.Items.Count; i++)
                {
                    Item cItem = CurrentRoom.Items[i];
                    if (cItem.Name == "book" || cItem.Name == "rock")
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Black;
                        CurrentRoom.Exits["n"].IsLocked = false;
                        CurrentRoom.Description = "The gate slowly lifts up. you can now move around freely. exit to the North";
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("You cannot use this command.");
                        continue;
                    }
                }
            }
        }

        public void PaintingRiddle()
        {
            int guess = 0;
            bool inRiddle = true;
            string answer = "time";
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(CurrentRoom.Description);
            while (inRiddle)
            {
                if (guess < 3)
                {
                    Console.WriteLine($@"
                    Mountains will crumble and temples will fall, 
                    And no man can survive its endless call.
                                What is it?
                                ");
                    Console.WriteLine($"You have {3 - guess} guesses remaining.");
                    string response = Console.ReadLine().ToLower();
                    if (response == "q")
                        {
                            GameOver();
                            return;
                        }
                    if (response == answer)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Clear();
                        Console.WriteLine("You have proven your wisdom. You may continue on your quest.");
                        inRiddle = false;
                        CurrentRoom.Exits["w"].Visited = true;
                        Room nextRoom = CurrentRoom.Exits["w"];
                        CurrentRoom.Exits["w"].IsLocked = false;
                        nextRoom.Exits["w"].IsLocked = false;
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
                        if (guess == 3)
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
                    Console.Clear();
                    Console.WriteLine("You have used up all of your guesses. The walls continue to collapse on you. You can no longer move. You figure out the answer to the riddle, but it is too late. You have died.");
                    GameOver();
                    inRiddle = false;
                    return;

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
            CurrentPlayer.Health -= 20;
            Console.WriteLine(CurrentRoom.Description);
            SkeletonFight();
            CurrentRoom.Description = "The room around you is as black as night, but you can see some light shining from under the door to your West.";
        }

        public void Crown()
        {
            Console.WriteLine("You place the crown upon your head. You feel as though you are being lifted out of your body to ascend. The skyline turns deep red and the ground becomes flames. Only you can control the flames. You are now the ruler of the known world!");
            GameOver();
        }

        public void GameOver()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Clear();
            Console.WriteLine($@"
                            -------------
                            | GAME OVER |
                            -------------
            ");
            Reset();
        }

        public void UseItem(Item item)
        {
            for (int i = 0; i < CurrentRoom.UsableItems.Count; i++)
            {
                Item usable = CurrentRoom.UsableItems[i];
                if (item.Name == usable.Name)
                {
                    if (item.Name == "brassKey")
                    {
                        CurrentRoom.Exits["s"].IsLocked = false;
                        CurrentPlayer.Inventory.Remove(item);
                        CurrentRoom.Description = "Room has Door to South that has been unlocked, and Bookshelf to East";
                    }
                    else if (item.Name == "silverKey")
                    {
                        CurrentRoom.Exits["w"].IsLocked = false;
                        CurrentPlayer.Inventory.Remove(item);
                        Console.Clear();
                        CurrentRoom.Description = "Secret entrance to dungeon. The room to your West has been unlocked";
                    }
                    else if (item.Name == "steelKey")
                    {
                        CurrentRoom.Exits["w"].IsLocked = false;
                        CurrentPlayer.Inventory.Remove(item);
                        Console.Clear();
                        CurrentRoom.Exits["w"].Description = "A small dimly lit room. Stairs to your west, and door to your right";
                        CurrentRoom.Description = "A long hallway dimly lit by candles. There are rooms to your East and West. The door to your west has been unlocked. The hallway continues to your North";
                    }
                    else if (item.Name == "goldKey")
                    {
                        CurrentRoom.Exits["w"].IsLocked = false;
                        CurrentPlayer.Inventory.Remove(item);
                        Console.Clear();
                        CurrentRoom.Description = "There are hundreds of lit candles covering the walls of this hallway. The room to your West has been unlocked. You can hear something through the walls, but you cannot make out what it is. The hallway continues to the South";
                    }
                    else if (item.Name == "book")
                    {
                        if (CurrentRoom.Name == "Room 3 South")
                        {
                            CurrentRoom.Exits["n"].IsLocked = false;
                            CurrentRoom.Items.Add(item);
                            CurrentPlayer.Inventory.Remove(item);
                            Console.Clear();
                        }
                        else
                        {
                            CurrentRoom.Exits["e"].IsLocked = false;
                            CurrentPlayer.Inventory.Remove(item);
                            Console.Clear();
                            CurrentRoom.Description = "There is a room to the South. The bookshelf cracks open revealing a secret room";
                        }
                    }
                    else if (item.Name == "wine")
                    {
                        Intoxicated();
                        CurrentPlayer.Inventory.Remove(item);
                    }
                    else if (item.Name == "food")
                    {
                        if (CurrentPlayer.Health < 70)
                        {
                            CurrentPlayer.Health += 15;
                            CurrentPlayer.Inventory.Remove(item);
                        }
                    }
                    else if (item.Name == "rock")
                    {
                        if (CurrentRoom.Name == "Room 3 South")
                        {
                            CurrentRoom.Exits["n"].IsLocked = false;
                            CurrentRoom.Items.Add(item);
                            CurrentPlayer.Inventory.Remove(item);
                            Console.Clear();
                        }
                    }
                    else if (item.Name == "healthPotion")
                    {
                        if (CurrentPlayer.Health < 60)
                        {
                            CurrentPlayer.Health += 40;
                            Console.Clear();
                            Console.WriteLine($"You now have {CurrentPlayer.Health} health remaining");
                            CurrentPlayer.Inventory.Remove(item);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"You currently have {CurrentPlayer.Health}. It would be pointless to use this. You have plenty of health left. Try again when your health is less than 60");
                        }
                    }
                    else if (item.Name == "crownOfFire")
                    {
                        Console.Clear();
                        Crown();
                    }
                    else if (item.Name == "rustyKnife" || item.Name == "rustyDagger")
                    {
                        if (CurrentEnemy.Name == "Skeleton Warrior" || CurrentEnemy.Name == "Skeleton Ranger")
                        {
                            if (CurrentEnemy.Health > 0)
                            {
                                CurrentEnemy.Health -= 20;
                                Console.WriteLine($"You struck the {CurrentEnemy.Name} with {item.Name}. Its health has been diminished. The {CurrentEnemy.Name} has {CurrentEnemy.Health} remaining");
                            }
                            else
                            {
                                Console.WriteLine($"With your last blow, you have slain the {CurrentEnemy.Name}.");
                            }
                        }
                        else if (CurrentEnemy.Name == "Troll")
                        {
                            if (CurrentEnemy.Health > 0)
                            {
                                CurrentEnemy.Health -= 15;
                                Console.WriteLine($"You struck the {CurrentEnemy.Name} with {item.Name}. Its health has been diminished. The {CurrentEnemy.Name} has {CurrentEnemy.Health} remaining");
                            }
                            else
                            {
                                Console.WriteLine($"With your last blow, you have slain the {CurrentEnemy.Name}.");
                            }
                        }
                        else if (CurrentEnemy.Name == "Undead King")
                        {
                            if (CurrentEnemy.Health > 0)
                            {
                                CurrentEnemy.Health -= 10;
                                Console.WriteLine($"You struck the {CurrentEnemy.Name} with {item.Name}. Its health has been diminished. The {CurrentEnemy.Name} has {CurrentEnemy.Health} remaining");
                            }
                            else
                            {
                                Console.WriteLine($"With your last blow, you have slain the {CurrentEnemy.Name}.");
                            }
                        }
                    }
                    else if (item.Name == "greatSword" || item.Name == "battleAxe")
                    {
                        if (CurrentEnemy.Name == "Skeleton Warrior" || CurrentEnemy.Name == "Skeleton Ranger")
                        {
                            if (CurrentEnemy.Health > 0)
                            {
                                CurrentEnemy.Health -= 50;
                                Console.WriteLine($"You struck the {CurrentEnemy.Name} with {item.Name}. Its health has been diminished. The {CurrentEnemy.Name} has {CurrentEnemy.Health} remaining");
                            }
                            else
                            {
                                Console.WriteLine($"With your last blow, you have slain the {CurrentEnemy.Name}.");
                            }
                        }
                        else if (CurrentEnemy.Name == "Troll")
                        {
                            if (CurrentEnemy.Health > 0)
                            {
                                CurrentEnemy.Health -= 34;
                                Console.WriteLine($"You struck the {CurrentEnemy.Name} with {item.Name}. Its health has been diminished. The {CurrentEnemy.Name} has {CurrentEnemy.Health} remaining");
                            }
                            else
                            {
                                Console.WriteLine($"With your last blow, you have slain the {CurrentEnemy.Name}.");
                            }
                        }
                        else if (CurrentEnemy.Name == "Undead King")
                        {
                            if (CurrentEnemy.Health > 0)
                            {
                                CurrentEnemy.Health -= 20;
                                Console.WriteLine($"You struck the {CurrentEnemy.Name} with {item.Name}. Its health has been diminished. The {CurrentEnemy.Name} has {CurrentEnemy.Health} remaining");
                            }
                            else
                            {
                                Console.WriteLine($"With your last blow, you have slain the {CurrentEnemy.Name}.");
                            }
                        }
                    }
                }
            }
        }
    }
}