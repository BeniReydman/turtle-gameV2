using System;
using System.Collections.Generic;
using System.Text;
using TurtleGame.Values;

namespace TurtleGame.Entities
{
    // Used for potential expansion in the future
    public class TurtleEntity : Entity
    {
        public TurtleEntity(Coords coords, CardinalDirection cardinalDirection)
            : base(coords, cardinalDirection)
        {
        }
    }
}
