using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Player : IPlayer
    {
        public int Score { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }

        public List<Item> Inventory { get; set; }


        public Player(string name)
        {
            Name = name;
            Inventory = new List<Item>();
            Score = 0;
            Health = 100;
        }
    }


}