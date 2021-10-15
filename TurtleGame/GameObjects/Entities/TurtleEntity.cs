using TurtleGame.GameObjects;
using TurtleGame.Values;

namespace TurtleGame.Entities
{
    // Used for potential expansion in the future
    public class TurtleEntity : Entity
    {
        public TurtleEntity(Table table, Coords coords, CardinalDirection cardinalDirection)
            : base(table, coords, cardinalDirection)
        {
        }
    }
}
