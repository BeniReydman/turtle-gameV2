using System;
using TurtleGame.Entities;
using TurtleGame.GameInterfaces;

namespace TurtleGame.GameLogic
{
    public class GameCommand : IGameCommand
    {
        private string _commandType;
        private Func<Entity, string[], bool> _execute;

        public GameCommand(string commandType, Func<Entity, string[], bool> execute)
        {
            _commandType = commandType;
            _execute = execute;
        }

        public bool CanExecute(string commandType)
        {
            return _commandType.Equals(commandType);
        }

        public bool Execute(Entity entity, string[] args)
        {
            return _execute(entity, args);
        }

    }
}
