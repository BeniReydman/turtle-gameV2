using System;
using TurtleGame.GameObjects;
using TurtleGame.Values;

namespace TurtleGame.Entities
{
    public class Entity
    {
        public Coords Coords { get; set; }
        public CardinalDirection CardinalDirection { get; set; }
        public Table Table { get; private set; }

        public Entity(Table table)
        {
            Table = table;
            Coords = new Coords { X = 0, Y = 0 };
            CardinalDirection = new CardinalDirection { CurrDirection = Direction.NORTH };
        }

        public Entity(Table table, Coords coords, CardinalDirection cardinalDirection)
        {
            Table = table;
            Coords = coords;
            CardinalDirection = cardinalDirection;  
        }

        // Note, checking if coords are inside table is done inside CommandHandler
        public void Move()
        {
            switch (CardinalDirection.CurrDirection) // If movement will go off table, ignore
            {
                case Direction.NORTH: if(Coords.Y < Table.Width - 1) Coords.Y++; break;
                case Direction.EAST: if(Coords.X < Table.Length - 1) Coords.X++; break;
                case Direction.SOUTH: if(Coords.Y > 0) Coords.Y--; break;
                case Direction.WEST: if(Coords.X > 0) Coords.X--; break;
            }

        }

        // Turn the entity 90 degrees to its left
        public virtual void Left()
        {
            CardinalDirection--;
        }

        // Turn the entity 90 degrees to its right
        public virtual void Right()
        {
            CardinalDirection++;
        }

        public virtual void Report()
        {
            Console.WriteLine(this.ToString());
        }

        public override string ToString()
        {
            return $"{Coords.X},{Coords.Y},{CardinalDirection}";
        }
    }
}
