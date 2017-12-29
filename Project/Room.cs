using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Room : IRoom
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsLocked { get; set; }
        public bool Visited { get; set; }
        public List<Item> Items { get; set; }

        public Dictionary<string, Room> Exits { get; set; }

        public void UseItem(Item item)
        {
            throw new System.NotImplementedException();
        }

        public Room(string name, string description)
        {
            Name = name;
            Description = description;
            IsLocked = false;
            Visited = false;
            Items = new List<Item>();
            Exits = new Dictionary<string, Room>();
        }
    }
}