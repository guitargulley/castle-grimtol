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

        }
        public void Start()
        {
            Console.WriteLine($@"
            Welcome {CurrentPlayer} to the castle of .
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
        }
        public void Go(string userInput)
        {

        }
        public void TakeItem(string userInput)
        {

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
            String Player = Console.ReadLine();

            Player CurrentPlayer = new Player(Player);

            Console.WriteLine($@"
            Welcome {CurrentPlayer} to the castle of .
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

            Item rustyDagger = new Item("RustyDagger", "Battle Worn Dagger that has been rusted and dulled");
            Item rustyKnife = new Item("RustyKnife", "Small Single Edged Knife that has been rusted and dulled");
            Item woodenShield = new Item("WoodenShield", "Battle Worn Wooded Shield that has been splintered and cracked");
            Item greatSword = new Item("GreatSword", "Two Handed Steel Sword, with gold and silver details along the handle. Provides heavy damage, but slower to attack with.");
            Item battleAxe = new Item("BattleAxe", "Double sided axe. heavy damage, sligthly slower attack than traditional light axe.");
            Item heavyArmor = new Item("HeavyArmor", "Heavy duty steel armor, reduces health loss from during attacks.");
            Item lightArmor = new Item("ligthArmor", "Light armor, designed for stealth and speed, reduces damage from incoming attacks");
            Item healthPoultice = new Item("HealthPoultice", "Health potion that recovers health that has been lost during battle");
            Item brassKey = new Item("BrassKey", "Used for unlocking a single brass lock");
            Item decorativeKey = new Item("DecorativeKey", "This key is absolutely useless.");
            Item silverKey = new Item("SilverKey", "unlocks many of the cells in the dungeon.");
            Item goldKey = new Item("GoldKey", "Unlocks the final room");
            Item book = new Item("Book", "Used to unlock the secret room behind the book case in room 4");
            Item wine = new Item("Wine", "Disorients player, making player go in a random direction for 3 moves");
            Item food = new Item("Food", "Food can be used to distract animals, as well as provide small health regeneration");
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
            Room Room1 = new Room("Room 1", "First Floor Room with Painting and brass key");
            Room Room2 = new Room("Room 2", "First floor, Large table with food and drink, staircase leading to second Floor");
            Room Room3 = new Room("Room 3", "First Floor, Room has large desk with a secret compartment");
            Room Room4 = new Room("Room 4", "First Floor, Room has Door to South, and Bookshelf to East");
            Room Room4s = new Room("Room 4s", "Secret room off of room 4, requires book to open bookshelf");
            Room Room5 = new Room("Room 5", "First Floor, Needs brass key to open");
            Room StairCase = new Room("Stairs", "Stairs from first floor to second floor");
            Room DungeonRoom1 = new Room("Dungeon Room1", "Entrance to dungeon");
            Room DungeonRoom2 = new Room("Dungeon Room2", "Accessed by falling through trap door, taking damage");
            List<Room> GameRooms = new List<Room>();

            Room4s.IsLocked = true;
            Room5.IsLocked = true;
            DungeonRoom1.IsLocked = true;

            GameRooms.Add(Room0);
            GameRooms.Add(Room1);
            GameRooms.Add(Room2);
            GameRooms.Add(Room3);
            GameRooms.Add(Room4);
            GameRooms.Add(Room5);
            GameRooms.Add(StairCase);
            GameRooms.Add(DungeonRoom1);

            Room0.Exits.Add("W", Room2);
            Room0.Exits.Add("E", Room1);
            Room0.Exits.Add("S", Room0);
            Room0.Exits.Add("N", Room0);

            Room1.Exits.Add("W", Room0);
            Room1.Exits.Add("E", Room1);
            Room1.Exits.Add("S", Room1);
            Room1.Exits.Add("N", Room1);

            Room2.Exits.Add("W", StairCase);
            Room2.Exits.Add("E", Room0);
            Room2.Exits.Add("S", Room2);
            Room2.Exits.Add("N", Room2);

            Room3.Exits.Add("W", Room3);
            Room3.Exits.Add("E", Room0);
            Room3.Exits.Add("S", Room3);
            Room3.Exits.Add("N", Room5);

            Room4.Exits.Add("W", Room2);
            Room4.Exits.Add("E", Room4s);
            Room4.Exits.Add("S", Room4);
            Room4.Exits.Add("N", Room0);

            Room4s.Exits.Add("W", Room4);
            Room4s.Exits.Add("E", DungeonRoom1);
            Room4s.Exits.Add("S", Room4s);
            Room4s.Exits.Add("N", Room4s);

            Room5.Exits.Add("W", Room5);
            Room5.Exits.Add("E", DungeonRoom2);
            Room5.Exits.Add("S", DungeonRoom2);
            Room5.Exits.Add("N", Room4);
        }

        public void UseItem(string itemName)
        {

        }

    }
}