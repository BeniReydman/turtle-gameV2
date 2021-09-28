using System;
using System.Collections.Generic;
using System.Text;
using TurtleGame.Values;

namespace TurtleGame.Entities
{
    public class Entity
    {
        public Coords Coords;
        public CardinalDirection CardinalDirection;

        public Entity(Coords coords, CardinalDirection cardinalDirection)
        {
            this.Coords = coords;
            this.CardinalDirection = cardinalDirection;  
        }

        // Note, checking if coords are inside table is done inside CommandHandler
        public void Move()
        {
            switch (CardinalDirection.CurrDirection) // If movement will go off table, ignore
            {
                case Direction.NORTH: if(Coords.Y < Tabletop.HEIGHT - 1) Coords.Y++; break;
                case Direction.EAST: if(Coords.X < Tabletop.WIDTH - 1) Coords.X++; break;
                case Direction.SOUTH: if(Coords.Y > 0) Coords.Y--; break;
                case Direction.WEST: if(Coords.X > 0) Coords.X--; break;
            }

        }

        // Turn the entity 90 degrees to its left
        public void Left()
        {
            CardinalDirection--;
        }

        // Turn the entity 90 degrees to its right
        public void Right()
        {
            CardinalDirection++;
        }

        public void Report()
        {
            Console.WriteLine(this.ToString());
        }

        public override string ToString()
        {
            return $"{Coords.X},{Coords.Y},{CardinalDirection}";
        }
    }
}
