using System;
using TurtleGame.Entities;

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
            // Variables
            var CH = new CommandHandler();

            // Ask for input
            Console.WriteLine("--- input ---");

            // Attempt to create TurtleEntity
            TurtleEntity entity = (TurtleEntity) GetEntity(CH);

            // Read commands until user wants to exit
            string currCommand = CH.ReadCommand();
            while (!currCommand.Equals("EXIT"))
            {
                // Attempt to run command that was read
                if(!CH.RunCurrCommand(currCommand, entity))
                    Console.WriteLine("Error, correct usage is: <PLACE, MOVE, LEFT, RIGHT, REPORT, EXIT>");
                currCommand = CH.ReadCommand();
            }

            Console.WriteLine("Thanks for playing.");
        }

        // Return entity only if user inputs correct PLACE command
        private static Entity GetEntity(CommandHandler CH)
        {
            Entity entity = (TurtleEntity)CH.GetEntityOrNull(CH.ReadCommand());
            while (entity == null)
            {
                Console.WriteLine("Error, initial command correct usage is: <PLACE X,Y,F>");
                entity = CH.GetEntityOrNull(CH.ReadCommand());
            }
            return entity;
        }
    }
}
