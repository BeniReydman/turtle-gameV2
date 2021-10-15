using TurtleGame.GameLogic;

namespace TurtleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            RunGame();
        }

        public static void RunGame()
        {
            GameHandler GM = new GameHandler();
            GM.StartGame(null, null);
        }

    }
}
