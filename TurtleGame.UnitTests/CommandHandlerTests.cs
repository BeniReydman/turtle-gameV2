using NUnit.Framework;
using TurtleGame.Entities;
using TurtleGame.Values;

namespace TurtleGame.UnitTests.CommandHandlerTests
{
    [TestFixture]
    class CommandHandlerTests
    {
        Entity entity;
        CommandHandler CH;

        [SetUp]
        public void Setup()
        {
            // Default
            CH = new CommandHandler();
            entity = new Entity(new Values.Coords { X = 1, Y = 1 }, new Values.CardinalDirection { CurrDirection = Values.Direction.NORTH });
        }

        [Test(Description = "Have to manually change if Tabletop size changes")]
        [TestCase("PLACE 0,0,NORTH")]
        [TestCase("PLACE 0,0,EAST")]
        [TestCase("PLACE 0,0,SOUTH")]
        [TestCase("PLACE 0,0,WEST")]
        [TestCase("PLACE 4,4,NORTH")]
        [TestCase("PLACE 4,4,EAST")]
        [TestCase("PLACE 4,4,SOUTH")]
        [TestCase("PLACE 4,4,WEST")]
        public void CreateEntityOrNull_ShouldCreateEntity(string userInput)
        {
            entity = CH.GetEntityOrNull(userInput);
            Assert.NotNull(entity);
        }

        [Test(Description = "Have to manually change if Tabletop size changes")]
        [TestCase("PLACE -1,0,NORTH")]
        [TestCase("PLACE 0,-1,EAST")]
        [TestCase("PLACE -1,-1,SOUTH")]
        [TestCase("PLACE 0,0,WESTT")]
        [TestCase("PLACE 5,4,NORTH")]
        [TestCase("PLACE 4,5,EAST")]
        [TestCase("PLACE 5,5,SOUTH")]
        [TestCase("PLACE 4,4,NORTHH")]
        [TestCase("PLACE0,0,NORTH")]
        [TestCase("PLACE")]
        [TestCase("0,0,NORTH")]
        [TestCase("")]
        [TestCase(null)]
        public void CreateEntityOrNull_ShouldReturnNull(string userInput)
        {
            entity = CH.GetEntityOrNull(userInput);
            Assert.Null(entity);
        }

        [Test(Description = "Have to manually change if Tabletop size changes")]
        [TestCase("PLACE 0,0,NORTH")]
        [TestCase("PLACE 0,0,EAST")]
        [TestCase("PLACE 0,0,SOUTH")]
        [TestCase("PLACE 0,0,WEST")]
        [TestCase("PLACE 4,4,NORTH")]
        [TestCase("PLACE 4,4,EAST")]
        [TestCase("PLACE 4,4,SOUTH")]
        [TestCase("PLACE 4,4,WEST")]
        public void RunCurrCommand_PlaceCommand_ReturnTrue(string userInput)
        {
            bool result = CH.RunCurrCommand(userInput, entity);
            Assert.True(result);
        }

        [Test(Description = "Have to manually change if Tabletop size changes")]
        [TestCase("PLACE -1,0,NORTH")]
        [TestCase("PLACE 0,-1,EAST")]
        [TestCase("PLACE -1,-1,SOUTH")]
        [TestCase("PLACE 0,0,WESTT")]
        [TestCase("PLACE 5,4,NORTH")]
        [TestCase("PLACE 4,5,EAST")]
        [TestCase("PLACE 5,5,SOUTH")]
        [TestCase("PLACE 4,4,NORTHH")]
        [TestCase("PLACE0,0,NORTH")]
        [TestCase("PLACE")]
        [TestCase("0,0,NORTH")]
        public void RunCurrCommand_PlaceCommand_ReturnFalse( string userInput)
        {
            bool result = CH.RunCurrCommand(userInput, entity);
            Assert.False(result);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void RunCurrCommand_NoCommand_ReturnFalse(string userInput)
        {
            bool result = CH.RunCurrCommand(userInput, entity);
            Assert.False(result);
        }

        [Test]
        [TestCase("MOVE")]
        [TestCase("LEFT")]
        [TestCase("RIGHT")]
        [TestCase("REPORT")]
        public void RunCurrCommand_SingleCommands_ReturnTrue(string userInput)
        {
            bool result = CH.RunCurrCommand(userInput, entity);
            Assert.True(result);
        }

        [Test]
        [TestCase("PLACE 2,3,SOUTH")]
        public void RunCurrCommand_PlaceCommand_ChangesEntity(string userInput)
        {
            bool result = CH.RunCurrCommand(userInput, entity);
            Assert.True(result);
            Assert.That(entity.Coords.X, Is.EqualTo(2));
            Assert.That(entity.Coords.Y, Is.EqualTo(3));
            Assert.That(entity.CardinalDirection.CurrDirection, Is.EqualTo(Direction.SOUTH));
        }

        [Test]
        [TestCase("PLACE 5,3,SOUTH")]
        public void RunCurrCommand_BadPlaceCommand_DoesNotChangeEntity(string userInput)
        {
            bool result = CH.RunCurrCommand(userInput, entity);
            Assert.False(result);
            Assert.That(entity.Coords.X, Is.EqualTo(1));
            Assert.That(entity.Coords.Y, Is.EqualTo(1));
            Assert.That(entity.CardinalDirection.CurrDirection, Is.EqualTo(Direction.NORTH));
        }

        [Test]
        [TestCase("MOVE")]
        public void RunCurrCommand_MoveCommand_MovesTurtle(string userInput)
        {
            bool result = CH.RunCurrCommand(userInput, entity);
            Assert.True(result);
            Assert.That(entity.Coords.X, Is.EqualTo(1));
            Assert.That(entity.Coords.Y, Is.EqualTo(2));
            Assert.That(entity.CardinalDirection.CurrDirection, Is.EqualTo(Direction.NORTH));
        }

        [Test]
        [TestCase("LEFT")]
        public void RunCurrCommand_LeftCommand_TurnsTurtle(string userInput)
        {
            bool result = CH.RunCurrCommand(userInput, entity);
            Assert.True(result);
            Assert.That(entity.Coords.X, Is.EqualTo(1));
            Assert.That(entity.Coords.Y, Is.EqualTo(1));
            Assert.That(entity.CardinalDirection.CurrDirection, Is.EqualTo(Direction.WEST));
        }

        [Test]
        [TestCase("RIGHT")]
        public void RunCurrCommand_RightCommand_TurnsTurtle(string userInput)
        {
            bool result = CH.RunCurrCommand(userInput, entity);
            Assert.True(result);
            Assert.That(entity.Coords.X, Is.EqualTo(1));
            Assert.That(entity.Coords.Y, Is.EqualTo(1));
            Assert.That(entity.CardinalDirection.CurrDirection, Is.EqualTo(Direction.EAST));
        }

        [Test]
        [TestCase("REPORT")]
        public void RunCurrCommand_ReportCommand_DoesNotChangeEntity(string userInput)
        {
            bool result = CH.RunCurrCommand(userInput, entity);
            Assert.True(result);
            Assert.That(entity.Coords.X, Is.EqualTo(1));
            Assert.That(entity.Coords.Y, Is.EqualTo(1));
            Assert.That(entity.CardinalDirection.CurrDirection, Is.EqualTo(Direction.NORTH));
        }
    }
}
