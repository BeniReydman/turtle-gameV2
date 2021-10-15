using System;
using TurtleGame.Entities;
using TurtleGame.GameInterfaces;
using TurtleGame.GameObjects;
using System.Linq;

namespace TurtleGame.GameLogic
{
    enum GameState
    {
        Initializing,
        ReadingCommand,
        RunningCommand,
        Finished
    }

    class GameHandler : IGameHandler
    {
        private CommandHandler _commandHandler;
        private GameState _gameState;
        private Parser _parser;
        private Table _table;
        private Command _currCommand;

        public GameHandler()
        {
            _commandHandler = new CommandHandler();
            _gameState = GameState.Initializing;
            _parser = new Parser();
        }

        public void StartGame(int? tableLength, int? tableWidth)
        {
            // Create table with set size or default
            _table = new Table(tableLength, tableWidth);
            _table.AddEntity(new Entity(_table));  // Add generic entity for user to set

            Console.WriteLine("--- input ---");

            // Loop through game
            for (;;)
            {
                switch (_gameState)
                {
                    case GameState.Initializing: Initialize(); break;
                    case GameState.ReadingCommand: ReadCommand(); break;
                    case GameState.RunningCommand: RunCommand(); break;
                    case GameState.Finished: Console.WriteLine("Thanks for playing."); return;
                }
            }
        }

        // Initialize the game, wait for user to give PLACE command
        private void Initialize()
        {
            Command command = GetCommandOrNull();

            // Check if valid command was given
            if (command == null)
            {
                Console.WriteLine("Initial command correct usage is: <PLACE X,Y,F>");
                return;
            } else
            if (!command.CommandType.Equals("PLACE"))
            {
                Console.WriteLine("Error, incorrect command given. Initial command correct usage is: <PLACE X,Y,F>");
                return;
            }

            var result = _commandHandler.ExecuteUserCommand(command, _table.Entities.FirstOrDefault());
            if (!result)
            {
                Console.WriteLine(" Initial command correct usage is: <PLACE X,Y,F>");
                return;
            }

            _gameState = GameState.ReadingCommand;
        }

        private void ReadCommand()
        {
            Command command = GetCommandOrNull();

            if (command == null)
            {
                Console.WriteLine($"Correct usage is: <{GetCurrCommandTypes()}>");
                return;
            }

            // Update state to run command
            _currCommand = command;
            _gameState = GameState.RunningCommand;
        }

        private void RunCommand()
        {
            var result = _commandHandler.ExecuteUserCommand(_currCommand, _table.Entities.FirstOrDefault());
            if(result)
            {
                if (_currCommand.CommandType.Equals("EXIT"))
                { 
                    _gameState = GameState.Finished;
                    return;
                }
                _gameState = GameState.ReadingCommand;
            } else
            {
                Console.WriteLine($" Correct usage is: <{GetCurrCommandTypes()}>");
                _gameState = GameState.ReadingCommand;
            }
        }

        // Helper function to get command from user
        private Command GetCommandOrNull()
        {
            // Get command
            string userCommand = _commandHandler.ReadUserCommand();
            Command command = _parser.ParseUserCommandOrNull(userCommand);

            // Partial error, extended in called function
            if (command == null)
            {
                Console.Write("Error, no command was given.");
            }

            return command;
        }

        // Helper function to get curr commands available
        private string GetCurrCommandTypes()
        {
            var commands = _commandHandler.GameCommands.Select(key => key.Key).ToList();
            return String.Join(", ", commands);
    }

    }
}
