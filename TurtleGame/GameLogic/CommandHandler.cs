using System;
using System.Collections.Generic;
using TurtleGame.Entities;
using TurtleGame.GameInterfaces;
using System.Linq;

namespace TurtleGame.GameLogic
{
    public class CommandHandler : ICommandHandler
    {
        public Dictionary<string, GameCommand> GameCommands { get; }
        private Parser _parser;

        public CommandHandler() 
        {
            GameCommands = new Dictionary<string, GameCommand>();
            SeedCommands();
            _parser = new Parser();
        }

        public void AddGameCommand(string commandType, GameCommand command)
        {
            GameCommands.Add(commandType, command);
        }

        // Not to be async, game has to wait for response
        public string ReadUserCommand()
        {
            return Console.ReadLine()?.ToUpper();
        }

        // Attempt to execute UserCommand
        public bool ExecuteUserCommand(Command userCommand, Entity entity)
        {
            bool result = false;

            if (userCommand == null)
                return result;

            // Check if CommandType exists, if so, grab first matching GameCommand
            GameCommand gameCommand = GameCommands.Where(x => x.Key.Equals(userCommand.CommandType)).Select(x => x.Value).FirstOrDefault();
            if (gameCommand == null)
            {
                Console.Write("Error, command does not exist.");
                return result;
            }

            // Attempt to execute
            if (gameCommand.CanExecute(userCommand.CommandType))
                result = gameCommand.Execute(entity, userCommand.Args);

            if(result == false)
                Console.Write("Error, invalid arguments.");

            return result;
        }

        private void SeedCommands()
        {
            PlaceCommand();
            MoveCommand();
            LeftCommand();
            RightCommand();
            ReportCommand();
            ExitCommand();
        }

        // Entity will go forward 1 based on cardinal direction
        private void PlaceCommand()
        {
            // Create Command
            string commandType = "PLACE";
            Func<Entity, string[], bool> execute = (entity, args) => {
                if (entity == null || args == null)
                    return false;  // No args

                if (args.Length != 1)
                    return false;  // Too many args

                if (!UpdateEntity(entity, args[0]))
                    return false;  // Invalid args

                return true;  // Updated
            };

            AddGameCommand(commandType, new GameCommand(commandType, execute));
        }

        private bool UpdateEntity(Entity entity, string args)
        {
            // Atempt to parse arguments
            var result = _parser.ParsePlaceArgsOrNull(args);
            if (result == null)
                return false;

            // Check values
            if (result.Item1 < 0 || result.Item1 >= entity.Table.Length ||
                result.Item2 < 0 || result.Item2 >= entity.Table.Width)
            {
                Console.WriteLine("Not within table dimensions.");
                return false;
            }

            // Update Entity
            entity.Coords.X = result.Item1;
            entity.Coords.Y = result.Item2;
            entity.CardinalDirection = result.Item3;
            return true;
        }

        // Entity will go forward 1 based on cardinal direction
        private void MoveCommand()
        {
            // Create Command
            string commandType = "MOVE";
            Func<Entity, string[], bool> execute = (entity, args) => {
                if (entity == null || args != null)
                    return false;  // Invalid args

                entity.Move();
                return true;
            };

            AddGameCommand(commandType, new GameCommand(commandType, execute));
        }

        // Entity will turn left 90 degrees based on cardinal direction
        private void LeftCommand()
        {
            // Create Command
            string commandType = "LEFT";
            Func<Entity, string[], bool> execute = (entity, args) => {
                if (entity == null || args != null)
                    return false;  // Invalid args

                entity.Left();
                return true;
            };

            AddGameCommand(commandType, new GameCommand(commandType, execute));
        }

        // Entity will turn right 90 degrees based on cardinal direction
        private void RightCommand()
        {
            // Create Command
            string commandType = "RIGHT";
            Func<Entity, string[], bool> execute = (entity, args) => {
                if (entity == null || args != null)
                    return false;  // Invalid args

                entity.Right();
                return true;
            };

            AddGameCommand(commandType, new GameCommand(commandType, execute));
        }

        // Entity will report its position in console
        private void ReportCommand()
        {
            // Create Command
            string commandType = "REPORT";
            Func<Entity, string[], bool> execute = (entity, args) => {
                if (entity == null || args != null)
                    return false;  // Invalid args

                Console.WriteLine("\n--- output ---");
                entity.Report();
                Console.WriteLine("\n--- input ---");

                return true;
            };

            AddGameCommand(commandType, new GameCommand(commandType, execute));
        }

        // Returns true if exit command is given
        private void ExitCommand()
        {
            // Create Command
            string commandType = "EXIT";
            Func<Entity, string[], bool> execute = (entity, args) => {
                if (entity == null || args == null)
                    return true;

                return false;  // Invalid args
            };

            AddGameCommand(commandType, new GameCommand(commandType, execute));
        }
    }
}
