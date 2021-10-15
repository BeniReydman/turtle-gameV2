namespace TurtleGame.GameInterfaces
{
    interface IGameHandler
    {
        // Starts Game until user decides to exit
        public void StartGame(int? tableWidth, int? tableLength);
    }
}
