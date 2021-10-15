using NUnit.Framework;
using TurtleGame.Entities;
using TurtleGame.GameLogic;
using TurtleGame.GameObjects;
using TurtleGame.Values;

namespace TurtleGame.UnitTests.CommandHandlerTests
{
    [TestFixture]
    class CommandHandlerTests
    {
        Entity entity;
        CommandHandler CH;
        Parser parser;

        [SetUp]
        public void Setup()
        {
            // Default
            CH = new CommandHandler();
            entity = new Entity(new Table(null, null));
            parser = new Parser();
        }

        [TestCase("PLACE 0,0,NORTH")]
        [TestCase("PLACE 0,0,EAST")]
        [TestCase("PLACE 0,0,SOUTH")]
        [TestCase("PLACE 0,0,WEST")]
        [TestCase("PLACE 4,4,NORTH")]
        [TestCase("PLACE 4,4,EAST")]
        [TestCase("PLACE 4,4,SOUTH")]
        [TestCase("PLACE 4,4,WEST")]
        [TestCase("MOVE")]
        [TestCase("LEFT")]
        [TestCase("RIGHT")]
        [TestCase("REPORT")]
        [TestCase("EXIT")]
        public void ExecuteUserCommand__ValidInput_ShouldReturnTrue(string userInput)
        {
            Command command = parser.ParseUserCommandOrNull(userInput);
            bool result = CH.ExecuteUserCommand(command, entity);
            Assert.True(result);
        }

        [TestCase("PLACE -1,0,NORTH")]
        [TestCase("PLACE 0,-1,EAST")]
        [TestCase("PLACE -1,-1,SOUTH")]
        [TestCase("PLACE 0,0,WESTT")]
        [TestCase("PLACE 5,4,NORTH")]
        [TestCase("PLACE 4,5,SOUTH")]
        [TestCase("PLACE 5,5,WEST")]
        [TestCase("PLACE 4,4,NORTHH")]
        [TestCase("PLACE0,0,NORTH")]
        [TestCase("PLACE")]
        [TestCase("0,0,NORTH")]
        [TestCase("")]
        [TestCase(null)]
        public void ExecuteUserCommand_InvalidInput_ShouldReturnFalse(string userInput)
        {
            Command command = parser.ParseUserCommandOrNull(userInput);
            bool result = CH.ExecuteUserCommand(command, entity);
            Assert.False(result);
        }

        [Test]
        [TestCase("PLACE 2,3,SOUTH")]
        public void ExecuteUserCommand_ValidPlaceCommand_ChangesEntity(string userInput)
        {
            Command command = parser.ParseUserCommandOrNull(userInput);
            bool result = CH.ExecuteUserCommand(command, entity);
            Assert.True(result);
            Assert.That(entity.Coords.X, Is.EqualTo(2));
            Assert.That(entity.Coords.Y, Is.EqualTo(3));
            Assert.That(entity.CardinalDirection.CurrDirection, Is.EqualTo(Direction.SOUTH));
        }

        [Test]
        [TestCase("PLACE 5,3,SOUTH")]
        public void ExecuteUserCommand_InvalidPlaceCommand_DoesNotChangeEntity(string userInput)
        {
            Command command = parser.ParseUserCommandOrNull(userInput);
            bool result = CH.ExecuteUserCommand(command, entity);
            Assert.False(result);
            Assert.That(entity.Coords.X, Is.EqualTo(0));
            Assert.That(entity.Coords.Y, Is.EqualTo(0));
            Assert.That(entity.CardinalDirection.CurrDirection, Is.EqualTo(Direction.NORTH));
        }

        [Test]
        [TestCase("MOVE")]
        public void ExecuteUserCommand_MoveCommand_MovesTurtle(string userInput)
        {
            Command command = parser.ParseUserCommandOrNull(userInput);
            bool result = CH.ExecuteUserCommand(command, entity);
            Assert.True(result);
            Assert.That(entity.Coords.X, Is.EqualTo(0));
            Assert.That(entity.Coords.Y, Is.EqualTo(1));
            Assert.That(entity.CardinalDirection.CurrDirection, Is.EqualTo(Direction.NORTH));
        }

        [Test]
        [TestCase("LEFT")]
        public void ExecuteUserCommand_LeftCommand_TurnsTurtle(string userInput)
        {
            Command command = parser.ParseUserCommandOrNull(userInput);
            bool result = CH.ExecuteUserCommand(command, entity);
            Assert.True(result);
            Assert.That(entity.Coords.X, Is.EqualTo(0));
            Assert.That(entity.Coords.Y, Is.EqualTo(0));
            Assert.That(entity.CardinalDirection.CurrDirection, Is.EqualTo(Direction.WEST));
        }

        [Test]
        [TestCase("RIGHT")]
        public void ExecuteUserCommand_RightCommand_TurnsTurtle(string userInput)
        {
            Command command = parser.ParseUserCommandOrNull(userInput);
            bool result = CH.ExecuteUserCommand(command, entity);
            Assert.True(result);
            Assert.That(entity.Coords.X, Is.EqualTo(0));
            Assert.That(entity.Coords.Y, Is.EqualTo(0));
            Assert.That(entity.CardinalDirection.CurrDirection, Is.EqualTo(Direction.EAST));
        }

        [Test]
        [TestCase("REPORT")]
        public void ExecuteUserCommand_ReportCommand_DoesNotChangeEntity(string userInput)
        {
            Command command = parser.ParseUserCommandOrNull(userInput);
            bool result = CH.ExecuteUserCommand(command, entity);
            Assert.True(result);
            Assert.That(entity.Coords.X, Is.EqualTo(0));
            Assert.That(entity.Coords.Y, Is.EqualTo(0));
            Assert.That(entity.CardinalDirection.CurrDirection, Is.EqualTo(Direction.NORTH));
        }
    }
}
