using System.Collections.Generic;
using TurtleGame.Entities;

namespace TurtleGame.GameObjects
{
    struct Default
    {
        public const int LENGTH = 5;
        public const int WIDTH = 5;
    }

    public class Table
    {
        public int Length { get; set; }
        public int Width { get; set; }
        public ICollection<Entity> Entities { get; }

        public Table(int? length, int? width)
        {
            Length = length ?? Default.LENGTH;
            Width = width ?? Default.WIDTH;
            Entities = new List<Entity>();
        }

        public void AddEntity(Entity entity)
        {
            Entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            Entities.Remove(entity);
        }
    }
}
