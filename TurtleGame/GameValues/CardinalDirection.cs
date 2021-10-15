namespace TurtleGame.Values
{
    public enum Direction
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }

    public struct Directions
    {
        public const int SIZE = 4;
    }

    // Value Proxy to be used for the TurtleGame ( Overengineered, I was having fun )
    public class CardinalDirection
    {
        public Direction CurrDirection { get; set; }

        // Add fancy % operation
        public static CardinalDirection operator %(CardinalDirection direction, int amount)
        {
            direction.CurrDirection = (Direction)((int)direction.CurrDirection % amount);
            return direction;
        }

        // Get Next Direction value
        public static CardinalDirection operator ++(CardinalDirection direction)
        {
            direction.CurrDirection++;
            return direction % Directions.SIZE;
        }

        // Get Previous Direction value
        public static CardinalDirection operator --(CardinalDirection direction)
        {
            direction.CurrDirection += Directions.SIZE - 1;
            return direction % Directions.SIZE;
        }

        public override string ToString()
        {
            return CurrDirection.ToString();
        }
    }
}
