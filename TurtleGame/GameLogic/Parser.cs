using System;
using System.Linq;
using TurtleGame.Values;

namespace TurtleGame.GameLogic
{
    public class Parser
    {
        public Command ParseUserCommandOrNull(string userCommand)
        {
            // Check if null or empty string was given
            if (userCommand == null || userCommand.Length == 0)
                return null;

            // Split will guarentee length of at least 1 if not null or empty
            string[] components = userCommand.Split(' ');
            if (components.Length == 1)
                return new Command(components[0], null);  // Return command with no args
            else
                return new Command(components[0], components.Skip(1).ToArray()); // Return command with all args
        }

        // Returns CardinalDirection given a char
        public CardinalDirection ParseCardinalDirectionOrNull(string c)
        {
            switch (c)
            {
                case "NORTH": return new CardinalDirection { CurrDirection = Direction.NORTH };
                case "EAST": return new CardinalDirection { CurrDirection = Direction.EAST };
                case "SOUTH": return new CardinalDirection { CurrDirection = Direction.SOUTH };
                case "WEST": return new CardinalDirection { CurrDirection = Direction.WEST };
                default: return null;
            }
        }

        // Returns PLACE command values
        public Tuple<int, int, CardinalDirection> ParsePlaceArgsOrNull(string args)
        {
            try
            {
                // Attempt to parse values
                string[] items = args.Split(',');

                if (items.Length > 3) // Edge case: X,Y,N,Cheese
                    return null;

                int x = int.Parse(items[0]);
                int y = int.Parse(items[1]);
                string direction = items[2];
                CardinalDirection CD = ParseCardinalDirectionOrNull(direction);
                if (CD == null)
                    return null;  // invalid args

                // Return parsed values
                return Tuple.Create(x, y, CD);
            } catch (Exception e)
            {
                return null;
            }
        }
    }
}
