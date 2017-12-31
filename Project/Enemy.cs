using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Enemy 
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public Room Room { get; set; }

        public Enemy(string name, Room room)
        {
            Name = name;
            Health = 100;
            Room = room;
        }
    }


}