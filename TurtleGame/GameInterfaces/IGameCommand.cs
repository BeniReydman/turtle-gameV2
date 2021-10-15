using TurtleGame.Entities;

namespace TurtleGame.GameInterfaces
{
    interface IGameCommand
    {
        // Checks if command is executable by command
        public bool CanExecute(string commandType);

        // Attempts to execute command on given entity
        public bool Execute(Entity entity, string[] args);
    }
}
