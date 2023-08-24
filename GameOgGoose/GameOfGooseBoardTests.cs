using GameOfGoose.Factory;
using GameOfGoose.GameObjects;
using GameOfGoose.GameObjects.Squares;
using GameOfGoose.Interfaces;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace GameOfGoose
{
    public class GameOfGooseBoardTests
    {
        private ITestOutputHelper output;
        private GameBoard board;
        private List<IPlayer> playerList;
        private Dice dice;
        private Processor processor;
        private Mock<ILogger> logger = new Mock<ILogger>();

        public GameOfGooseBoardTests(ITestOutputHelper output)
        {
            this.output = output;

            board = new GameBoard();
            playerList = new List<IPlayer>
            {
                new Player("player1"),
                new Player("player2")
            };
            dice = new Dice();
            processor = new Processor(logger.Object);
        }

        [Fact]
        public void GameBoardInitialSetup()
        {
            // Arrange

            // Act

            int numberOfSquares = board.Squares.Count;

            // Assert

            Assert.Equal(64, numberOfSquares);
            output.WriteLine("If passed Board has 63 squares and start (or 0)");
        }

        [Fact]
        public void GameBoardGooseSquaresCreated()
        {
            // arrange

            var goosesquarenumbers = new List<int>() { 5, 9, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59 };

            // act

            var goosesquares = board.Squares.OfType<GooseSquare>().ToList();

            // assert

            foreach (int goosesquarenumber in goosesquarenumbers)
            {
                Assert.Contains(goosesquares, square => square.Number == goosesquarenumber);
            }
            output.WriteLine("if passed all 13 goose squares are created.");
        }

        [Fact]
        public void GameBoardBridgeSquareCreated()
        {
            // Arrange

            int bridgeSquareNumber = 6;

            // Act

            var BridgeSquare = board.Squares.OfType<BridgeSquare>().ToList();

            // Assert

            Assert.Contains(BridgeSquare, square => square.Number == bridgeSquareNumber);
            output.WriteLine("Bridge is created on index number 6 in the list.");
        }

        [Fact]
        public void GameBoardInnSquareCreated()
        {
            // Arrange

            int InnSquareNumber = 19;

            // Act

            var InnSquare = board.Squares.OfType<InnSquare>().ToList();

            // Assert

            Assert.Contains(InnSquare, square => square.Number == InnSquareNumber);
            output.WriteLine("Inn is created on index number 19 in the list");
        }

        [Fact]
        public void GameBoardWellSquareCreated()
        {
            // Arrange

            int WellSquareNumber = 31;

            // Act

            var WellSquare = board.Squares.OfType<WellSquare>().ToList();

            // Assert

            Assert.Contains(WellSquare, square => square.Number == WellSquareNumber);
            output.WriteLine("Well is ceeated on index number 31 in the list");
        }
        [Fact]
        public void GameBoardMazeSquareCreated()
        {
            // Arrange

            int MazeNumber = 42;

            // Act

            var MazeSquare = board.Squares.OfType<MazeSquare>().ToList();

            // Assert

            Assert.Contains(MazeSquare, square => square.Number == MazeNumber);
            output.WriteLine("Maze is created on index 42 in the list");
        }

        [Fact]
        public void GameBoardPrisonSquareCreated()
        {
            // Arrange

            int PrisonNumber = 52;

            // Act

            var PrisonSquare = board.Squares.OfType<PrisonSquare>().ToList();

            // Assert

            Assert.Contains(PrisonSquare, square => square.Number == PrisonNumber);
            output.WriteLine("Prison is created on index 52 in the list");
        }

        [Fact]
        public void GameBoardDeathSquareCreated()
        {
            // Arrange

            int DeathNumber = 58;

            // Act

            var DeathSquare = board.Squares.OfType<DeathSquare>().ToList();

            // Assert

            Assert.Contains(DeathSquare, square => square.Number == DeathNumber);
            output.WriteLine("Death is created on index 58 in the list");
        }

        [Fact]
        public void GameBoardEndSquareCreated()
        {
            // Arrange

            int EndNumber = 63;

            // Act

            var EndSquare = board.Squares.OfType<EndSquare>().ToList();

            // Assert

            Assert.Contains(EndSquare, square => square.Number == EndNumber);
            output.WriteLine("End is created on index 63 in the list");
        }

        [Fact]
        public void PiecesCanMove()
        {
            // Arrange
            
            var player = playerList.First();
            player.Position = 0;
            int diceRoll = dice.Roll();

            // Act

            player.Position += diceRoll;

            // Assert

            Assert.Equal(diceRoll, player.Position);
        }

        [Fact]
        public void PlayerFirstRollIsSixAndThree()
        {
            // Arrange

            var player = playerList.First();
           

            // Act

            processor.TurnLogic(6,3,1,player);


            // Assert

            Assert.Equal(53, player.Position);
            output.WriteLine("Player lands on Square 53, if first thhrow was 6 and 3");
        }
        [Fact]
        public void PlayerFirstRollIsFiveAndFour()
        {
            // Arrange

            var player = playerList.First();


            // Act

           processor.TurnLogic(5, 4, 1, player);


            // Assert

            Assert.Equal(26, player.Position);
            output.WriteLine("Player lands on Square 26, if first thhrow was 5 and 4");
        }
        [Fact]
        public void PlayerLandsOnBridge()
        {
            // Arrange
           
            var player = playerList.First();
            player.Position = 6;
            var Square = board.GetSquare(player.Position);

            // Act
            Square.Action(player);
            
            // Assert
            Assert.Equal(12, player.Position);
            output.WriteLine("If player lands on bridge, position is set to 12");
        }
        [Fact]
        public void PlayerLandsOnInn()
        {
            // Arrange

            var player = playerList.First();
            player.Position = 19;
            var Square = board.GetSquare(player.Position);

            //Act
            Square.Action(player);

            Assert.True(player.SkipNextTurn);
            output.WriteLine("player bool skipturn is set to true");
        }
        [Fact]
        public void PlayerLandsOnMaze() 
        {
            // Arrange 
            var player = playerList.First();
            player.Position = 42;
            var Square = board.GetSquare(player.Position);

            // Act
            Square.Action(player);
            
            // Assert

            Assert.Equal(39,player.Position);
            output.WriteLine("Player lands on maze and goes to 39.");
        }
        [Fact]
        public void PlayerLandsOnPrison()
        {
            // Arrange 
            var player = playerList.First();
            player.Position = 52;
            var Square = board.GetSquare(player.Position);

            // Act
            Square.Action(player);

            // Assert

            Assert.True(player.SkipThreeTurns);
            output.WriteLine("Player bool skipThreeTurns is set to true");
        }
        [Fact]
        public void PlayerLandsOnDeath()
        {
            // Arrange 
            var player = playerList.First();
            player.Position = 58;
            var Square = board.GetSquare(player.Position);

            // Act
            Square.Action(player);

            // Assert

            Assert.Equal(0, player.Position);
            output.WriteLine("Player lands on Death and goes to start (0).");
        }
        [Fact]
        public void PlayerLandsOnEnd()
        {
            // Arrange 
            var player = playerList.First();
            player.Position = 63;
            var Square = board.GetSquare(player.Position);

            // Act
            Square.Action(player);

            // Assert

            Assert.True(player.GameOver);
            output.WriteLine("Player lands on end and game ends.");
            output.WriteLine((player.GameOver).ToString());
        }
        [Fact]
        public void PlayerLandsOnWell_NoOtherPlayers()
        {
            // Arrange 
            var player = playerList.First();
            player.Position = 31;
            var Square = board.GetSquare(player.Position);

            // Act
            Square.Action(player);

            // Assert

            Assert.True(player.IsWaiting);
            output.WriteLine("Player lands on well with no other players and starts waiting");
        }

        [Fact]
        public void PlayerLandsOnWell_OtherPlayers()
        {
            // Arrange 
            var player = playerList.First();
            var player2 = playerList[1];
            player.Position = 31;
            var Square = board.GetSquare(player.Position);
            player2.Position = 31;
            var Square2 = board.GetSquare(player.Position);

            // Act
            Square.Action(player);
            Square2.Action(player2);
            // Assert

            Assert.False(player.IsWaiting);
            Assert.True(player2.IsWaiting);
            output.WriteLine("Player lands on well with no other players and starts waiting");
        }
        [Fact]
        public void IfPlayerOvershootsEndGetsSendBack()
        {
            // Arrange
            var player = playerList.First();
            player.Position = 62;
            int diceThrow = 4;
            // Act

            player.Move(board,diceThrow);

            // Assert

            Assert.Equal(60, player.Position);

        }

        [Fact]
        public void GooseSquaresAddTheDiceThrow() 
        {
            // Arrange 

            var player = playerList.First();
            player.Position = 51;
            var Square = board.GetSquare(player.Position);

            // Act

            player.Move(board, 8);

            // Assert
            
            Assert.Equal(51,player.Position);
        }

        [Fact]
        public void PlayerSkipsNextTurnWhenOnInn()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var player = playerList.First();
            var processor = new Processor(mockLogger.Object);
            processor.Board = board;
            processor.players.Add(player);
            processor.players[0].Position = 19;
            processor.players[0].SkipNextTurn = true;
            int turn = 1;
            
            // Act

            processor.TurnLogic(3, 4, 1, player);
            processor.TurnLogic(3, 4, 2, player);

            // Assert

            Assert.Equal(19, processor.players[0].Position); 
        }
    }
}