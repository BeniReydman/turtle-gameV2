using NUnit.Framework;
using TurtleGame.Entities;
using TurtleGame.Values;

namespace TurtleGame.UnitTests.EntityTests
{
    [TestFixture]
    public class TestEntity
    {
        Entity entity;

        [SetUp]
        public void Setup()
        {
            // Default
            entity = new Entity(new Values.Coords { X = 1, Y = 1 }, new Values.CardinalDirection { CurrDirection = Values.Direction.NORTH });
        }

        [Test]
        [TestCase(Direction.NORTH, ExpectedResult = Direction.EAST)]
        [TestCase(Direction.EAST, ExpectedResult = Direction.SOUTH)]
        [TestCase(Direction.SOUTH, ExpectedResult = Direction.WEST)]
        [TestCase(Direction.WEST, ExpectedResult = Direction.NORTH)]
        public Direction CanTurnRight_Turns90Degrees(Direction startDirection)
        {
            entity.CardinalDirection.CurrDirection = startDirection;
            entity.Right();
            return entity.CardinalDirection.CurrDirection;
        }

        [Test]
        [TestCase(Direction.NORTH, ExpectedResult = Direction.WEST)]
        [TestCase(Direction.EAST, ExpectedResult = Direction.NORTH)]
        [TestCase(Direction.SOUTH, ExpectedResult = Direction.EAST)]
        [TestCase(Direction.WEST, ExpectedResult = Direction.SOUTH)]
        public Direction CanTurnLeft_Turns90Degrees(Direction startDirection)
        {
            entity.CardinalDirection.CurrDirection = startDirection;
            entity.Left();
            return entity.CardinalDirection.CurrDirection;
        }

        [Test]
        public void CanMoveForward_FacingNorth()
        {
            entity.CardinalDirection.CurrDirection = Direction.NORTH;
            entity.Move();
            Assert.That(entity.Coords.Y, Is.EqualTo(2));
        }

        [Test]
        public void CanMoveForward_FacingEast()
        {
            entity.CardinalDirection.CurrDirection = Direction.EAST;
            entity.Move();
            Assert.That(entity.Coords.X, Is.EqualTo(2));
        }

        [Test]
        public void CanMoveForward_FacingSouth()
        {
            entity.CardinalDirection.CurrDirection = Direction.SOUTH;
            entity.Move();
            Assert.That(entity.Coords.Y, Is.EqualTo(0));
        }

        [Test]
        public void CanMoveForward_FacingWest()
        {
            entity.CardinalDirection.CurrDirection = Direction.WEST;

            entity.Move();
            Assert.That(entity.Coords.X, Is.EqualTo(0));
        }

        [Test]
        public void CannotMoveForward_FacingNorth_IfAtEdgeOfTable()
        {
            entity.CardinalDirection.CurrDirection = Direction.NORTH;
            entity.Coords.Y = Tabletop.HEIGHT - 1;
            entity.Move();
            Assert.That(entity.Coords.Y, Is.EqualTo(Tabletop.HEIGHT - 1));
        }

        [Test]
        public void CannotMoveForward_FacingEast_IfAtEdgeOfTable()
        {
            entity.CardinalDirection.CurrDirection = Direction.EAST;
            entity.Coords.X = Tabletop.WIDTH - 1;
            entity.Move();
            Assert.That(entity.Coords.X, Is.EqualTo(Tabletop.WIDTH - 1));
        }

        [Test]
        public void CannotMoveForward_FacingSouth_IfAtEdgeOfTable()
        {
            entity.CardinalDirection.CurrDirection = Direction.SOUTH;
            entity.Coords.Y = 0;
            entity.Move();
            Assert.That(entity.Coords.Y, Is.EqualTo(0));
        }

        [Test]
        public void CannotMoveForward_FacingWest_IfAtEdgeOfTable()
        {
            entity.CardinalDirection.CurrDirection = Direction.WEST;
            entity.Coords.X = 0;
            entity.Move();
            Assert.That(entity.Coords.X, Is.EqualTo(0));
        }
    }
}
