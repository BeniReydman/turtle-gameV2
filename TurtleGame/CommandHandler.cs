using System;
using System.Collections.Generic;
using System.Text;
using TurtleGame.Entities;
using TurtleGame.Values;

namespace TurtleGame
{
    public class CommandHandler
    {
        private string[] commands = { "PLACE", "MOVE", "LEFT", "RIGHT", "REPORT" };

        public CommandHandler() { }

        // Not to be async, game has to wait for response
        public string ReadCommand()
        {
            return Console.ReadLine()?.ToUpper();
        }

        // Run the current command
        public bool RunCurrCommand(string command, Entity entity)
        {
            if (command == null) return false;

            switch (command)
            {
                case "MOVE": entity.Move(); break;
                case "LEFT": entity.Left(); break;
                case "RIGHT": entity.Right(); break;
                case "REPORT": Console.WriteLine("\n--- Output ---");  entity.Report(); Console.WriteLine("\n--- input ---"); break;
                default:
                    // Check if default case is a Place COMMAND
                    string[] potentialPlacement = command.Split(' ');
                    if (isFirstCommand(potentialPlacement))
                    {
                        // Try to create entity to confirm if args are correct
                        Entity newEntity = CreateEntityOrNull(potentialPlacement[1]);
                        if (newEntity == null)
                            return false;

                        // Change coordinates of the already existing entity
                        entity.Coords = newEntity.Coords;
                        entity.CardinalDirection = newEntity.CardinalDirection;
                    }
                    else
                        return false;
                    break;
            }
            return true;
        }

        // Try to create entity from user input
        public Entity GetEntityOrNull(string enentityPlacement)
        {
            string[] entityPlacement = enentityPlacement?.ToUpper().Split(' ');

            // Check if command and coords were potentially received
            if (!isFirstCommand(entityPlacement))
                return null;

            // Try to create entity
            Entity entity = CreateEntityOrNull(entityPlacement[1]);

            return entity;
        }

        // Attempt to create Entity with arguments in string form
        private Entity CreateEntityOrNull(string placement)
        {
            if (placement == null) return null;

            // entity placement
            string[] ep = placement.ToUpper().Split(',');

            // Check if command and coords were potentially received
            if (ep.Length != 3)
                return null;

            // Attempt to create entity
            try
            {
                // Attempt to parse coords
                Coords coords = new Coords { X = Int32.Parse(ep[0]), Y = Int32.Parse(ep[1]) };
                if (!isInsideTable(coords))
                    return null;

                // Attempt to parse direction and create entity
                Direction direction;
                if (isDirection(ep[2], out direction))
                    return new TurtleEntity(coords, new CardinalDirection { CurrDirection = direction });  // TurtleEntity for now, more generic later
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private bool isInsideTable(Coords coords)
        {
            bool xInside = coords.X >= 0 && coords.X < Tabletop.WIDTH;
            bool yInside = coords.Y >= 0 && coords.Y < Tabletop.HEIGHT;
            if (xInside && yInside)
                return true;
            return false;
        }

        // Check if it is PLACE command, ignore command arguments
        private bool isFirstCommand(string[] command)
        {
            if (command == null) return false;

            // Check if command and coords were potentially received
            if (command.Length != 2)
                return false;
            // Check if correct command given
            if (!command[0].Equals(commands[0]))  // creation command should always be first command
                return false;
            return true;
        }

        // Check if it string is a Direction. Parse and set if true.
        private bool isDirection(string input, out Direction direction)
        {
            direction = Direction.NORTH; // default
            if (input == null) return false;

            // Get all the Directions in the form of a string
            string[] directions = Enum.GetNames(typeof(Direction));
            for (int x = 0; x < directions.Length; x++)
            {
                if (input.Equals(directions[x]))
                {
                    direction = (Direction)x;
                    return true;
                }
            }
      
            return false;
        }
    }
}
