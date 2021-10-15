using TurtleGame.Entities;
using TurtleGame.GameLogic;

namespace TurtleGame.GameInterfaces
{
    interface ICommandHandler
    {
        // Add command that can be ran by the game
        public void AddGameCommand(string commandType, GameCommand command);

        // Read command from user input in console
        public string ReadUserCommand();

        // Attempts to run user command, returns True if successful
        public bool ExecuteUserCommand(Command command, Entity entity);
    }
}
