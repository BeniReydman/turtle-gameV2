using NUnit.Framework;
using System;
using TurtleGame.Entities;
using TurtleGame.GameLogic;
using TurtleGame.GameObjects;
using TurtleGame.Values;

namespace TurtleGame.UnitTests
{
    [TestFixture]
    class ParserTests
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

        [Test]
        [TestCase("PLACE 0,0,NORTH")]
        [TestCase("MOVE")]
        public void ParseUserCommandOrNull_ValidInput_AssertTrue(string userCommand)
        {
            Command command = parser.ParseUserCommandOrNull(userCommand);
            Assert.NotNull(command);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ParseUserCommandOrNull_InvalidInput_AssertTrue(string userCommand)
        {
            Command command = parser.ParseUserCommandOrNull(userCommand);
            Assert.Null(command);
        }

        [Test]
        [TestCase("0,0,NORTH", ExpectedResult = 0)]
        [TestCase("4,2,WEST", ExpectedResult = 4)]
        [TestCase("21,63,SOUTH", ExpectedResult = 21)]
        [TestCase("-7,-235,EAST", ExpectedResult = -7)]
        public int ParsePlaceArgsOrNull_ValidInput_FirstItem(string parseArgs)
        {
            return parser.ParsePlaceArgsOrNull(parseArgs).Item1;
        }

        [Test]
        [TestCase("0,0,NORTH", ExpectedResult = 0)]
        [TestCase("4,2,WEST", ExpectedResult = 2)]
        [TestCase("21,63,SOUTH", ExpectedResult = 63)]
        [TestCase("-7,-235,EAST", ExpectedResult = -235)]
        public int ParsePlaceArgsOrNull_ValidInput_SecondItem(string parseArgs)
        {
            return parser.ParsePlaceArgsOrNull(parseArgs).Item2;
        }

        [Test]
        [TestCase("0,0,NORTH", ExpectedResult = Direction.NORTH)]
        [TestCase("4,2,WEST", ExpectedResult = Direction.WEST)]
        [TestCase("21,63,SOUTH", ExpectedResult = Direction.SOUTH)]
        [TestCase("-7,-235,EAST", ExpectedResult = Direction.EAST)]
        public Direction ParsePlaceArgsOrNull_ValidInput_ThirdItem(string parseArgs)
        {
            return parser.ParsePlaceArgsOrNull(parseArgs).Item3.CurrDirection;
        }

        [Test]
        [TestCase("0,NORTH")]
        [TestCase("4,2,WEST,5")]
        [TestCase("21,63,S")]
        [TestCase("-7,-235,EASTT")]
        [TestCase("")]
        [TestCase(null)]
        public void ParsePlaceArgsOrNull_InvalidInput_AssertTrue(string parseArgs)
        {
            var tuple = parser.ParsePlaceArgsOrNull(parseArgs);
            Assert.Null(tuple);
        }
    }
}
