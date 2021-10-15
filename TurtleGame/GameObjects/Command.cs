namespace TurtleGame.GameLogic
{
    public class Command
    {
        public string CommandType { get; set; }
        public string[] Args { get; set; }

        public Command(string CT, string[] args)
        {
            CommandType = CT;
            Args = args;
        }
    }
}
