using GameOfGoose.GameObjects;
using GameOfGoose.Interfaces;
using GameOfGoose.PlayerObject;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace GameOfGoose
{
    public class GameOfGooseTests
    {
        private ITestOutputHelper output;
        private GameBoard board;
        private PlayerManager playerManager;

        public GameOfGooseTests(ITestOutputHelper output)
        {
            this.output = output;

            board = new GameBoard();
            playerManager = new PlayerManager();
            playerManager.AddPlayer(new Player("player1", playerManager));
            playerManager.AddPlayer(new Player("player2", playerManager));
            playerManager.AddPlayer(new Player("player3", playerManager));
            playerManager.AddPlayer(new Player("player4", playerManager));
        }

        [Fact]
        public void GameBoardInitialSetup()
        {
            // Arrange

            // Act

            int numberOfSquares = board.Squares.Count;

            // Assert

            Assert.Equal(64, numberOfSquares);
            output.WriteLine("If passed Board has 63 squares and start");
        }

        [Fact]
        public void GameboardGooseSquares()
        {
            // Arrange

            var gooseSquareNumbers = new List<int>() { 5, 9, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59 };

            // Act

            var gooseSquares = board.Squares.OfType<ISquares.GooseSquare>().ToList();

            // Assert

            foreach (int gooseSquareNumber in gooseSquareNumbers)
            {
                Assert.Contains(gooseSquares, square => square.Number == gooseSquareNumber);
            }
            output.WriteLine("If passed all 13 goose squares are created.");
        }

        [Fact]
        public void GameboardSpecialSquares()
        {
            // Arrange

            var SpecialSquareNumbers = new List<int>() { 6, 19, 31, 42, 52, 58, 63 };

            // Act

            var specialSquares = board.Squares.OfType<ISquares.SpecialSquare>().ToList();

            // Assert

            foreach (int specialSquareNumber in SpecialSquareNumbers)
            {
                Assert.Contains(specialSquares, square => square.Number == specialSquareNumber);
            }
            output.WriteLine("if passed all special squares are implemented");
        }

        [Fact]
        public void PlayerMovesAfterDiceRoll()
        {
            // Arrange
            var mockDice = new Mock<IDice>();
            var _playerList = playerManager.GetPlayerList();

            mockDice.SetupSequence(d => d.Roll()).Returns(4).Returns(3);
            // Act

            _playerList[0].DiceRollAndMove(board, mockDice.Object);

            // Assert

            Assert.Equal(7, _playerList[0].Position);
            output.WriteLine("If passed Player is created and can move position");
        }

        [Fact]
        public void PlayerFirstRollIsSixAndThree()
        {
            // Arrange

            var mockdice = new Mock<IDice>();
            var _playerManager = playerManager.GetPlayerList();

            mockdice.SetupSequence(d => d.Roll()).Returns(6).Returns(3);

            // Act

            _playerManager[0].DiceRollAndMove(board, mockdice.Object);

            // Assert

            Assert.Equal(53, _playerManager[0].Position);

            output.WriteLine("If player rolls 6 and 3 on first go, position is 53");
            output.WriteLine(_playerManager[0].Name);
        }

        [Fact]
        public void PlayerFirstRollIsFiveAndFour()
        {
            // Arrange

            var mockdice = new Mock<IDice>();
            var _playerList = playerManager.GetPlayerList();

            mockdice.SetupSequence(d => d.Roll()).Returns(5).Returns(4);

            // Act

            _playerList[0].DiceRollAndMove(board, mockdice.Object);

            // Assert

            Assert.Equal(26, _playerList[0].Position);

            output.WriteLine("If player rolls 5 and 4 on first go, position is 26");
        }

        [Fact]
        public void PlayerStopsOnGoosRollsDoubled()
        {
            // Arrange
            var mockDice = new Mock<IDice>();
            var _playerList = playerManager.GetPlayerList();

            mockDice.SetupSequence(d => d.Roll()).Returns(2).Returns(3);
            // Act

            _playerList[0].DiceRollAndMove(board, mockDice.Object);

            // Assert

            Assert.Equal(10, _playerList[0].Position);
            output.WriteLine("If player lands on a goose square, dice roll is added again to position.");
        }

        [Fact]
        public void PlayerLandsOnBridge()
        {
            // Arrange
            var mockDice = new Mock<IDice>();
            var _playerList = playerManager.GetPlayerList();

            mockDice.SetupSequence(d => d.Roll()).Returns(3).Returns(3);

            // Act
            _playerList[0].DiceRollAndMove(board, mockDice.Object);

            // Assert
            Assert.Equal(12, _playerList[0].Position);
            output.WriteLine("If player lands on bridge, position is set to 12");
        }

        [Fact]
        public void PlayerLandsOnInn()
        {
            // Arrange
            var _playerList = playerManager.GetPlayerList();
            _playerList[0].Position = 15;
            var mockDice = new Mock<IDice>();

            mockDice.SetupSequence(d => d.Roll()).Returns(2).Returns(2);

            // Act
            _playerList[0].DiceRollAndMove(board, mockDice.Object);

            // Assert
            Assert.True(_playerList[0].SkipNextTurn);
            output.WriteLine("If player lands on Inn, skipturn boolean is set to true");
        }

        [Fact]
        public void PlayerLandsOnMaze()
        {
            // Arrange
            var _playerList = playerManager.GetPlayerList();
            _playerList[0].Position = 40;
            var mockDice = new Mock<IDice>();

            mockDice.SetupSequence(d => d.Roll()).Returns(1).Returns(1);

            // Act
            _playerList[0].DiceRollAndMove(board, mockDice.Object);

            // Assert
            Assert.Equal(39, _playerList[0].Position);
            output.WriteLine("If player lands on maze, position is set to 39");
        }

        [Fact]
        public void PlayerLandsOnPrison()
        {
            // Arrange
            var _playerList = playerManager.GetPlayerList();
            _playerList[0].Position = 50;
            var mockDice = new Mock<IDice>();

            mockDice.SetupSequence(d => d.Roll()).Returns(1).Returns(1);

            // Act
            _playerList[0].DiceRollAndMove(board, mockDice.Object);

            // Assert
            Assert.True(_playerList[0].SkipThreeTurns);
            output.WriteLine("If player lands on prison, skipThreeTurns is set to true");
        }

        [Fact]
        public void PlayerLandsOnDeath()
        {
            // Arrange
            var _playerList = playerManager.GetPlayerList();
            _playerList[0].Position = 56;
            var mockDice = new Mock<IDice>();

            mockDice.SetupSequence(d => d.Roll()).Returns(1).Returns(1);

            // Act
            _playerList[0].DiceRollAndMove(board, mockDice.Object);

            // Assert
            Assert.Equal(1, _playerList[0].Position);
            output.WriteLine("If player lands on Death, Go back to square 1");
        }

        [Fact]
        public void PlayerLandsOnEnd()
        {
            // Arrange
            var _playerList = playerManager.GetPlayerList();
            _playerList[0].Position = 56;
            var mockDice = new Mock<IDice>();

            mockDice.SetupSequence(d => d.Roll()).Returns(4).Returns(3);

            // Act
            _playerList[0].DiceRollAndMove(board, mockDice.Object);

            // Assert
            Assert.True(_playerList[0].GameOver);
            output.WriteLine("If player lands on the end,GameOver bool is set to true");
        }

        [Fact]
        public void PlayerOvershootsEnd()
        {
            // Arrange
            var _playerList = playerManager.GetPlayerList();
            _playerList[0].Position = 60;
            var mockDice = new Mock<IDice>();

            mockDice.SetupSequence(d => d.Roll()).Returns(5).Returns(4);

            // Act
            _playerList[0].DiceRollAndMove(board, mockDice.Object);

            // Assert

            Assert.Equal(57, _playerList[0].Position);
            output.WriteLine("If Player passes the end square, the number of points left from the dice roll are counted back.");
        }

        [Fact]
        public void PlayerLandsOnWell_WellIsEmpty()
        {
            // Arrange
            var _playerList = playerManager.GetPlayerList();
            var mockDice = new Mock<IDice>();

            _playerList[0].Position = 27;
            mockDice.SetupSequence(d => d.Roll()).Returns(2).Returns(2);

            // Act
            _playerList[0].DiceRollAndMove(board, mockDice.Object);

            // Assert
            Assert.True(_playerList[0].IsWaiting);
            output.WriteLine("Player 1 will wait on the square until a second player also lands on the square. Boolean Iswaiting is set.");
        }

        [Fact]
        public void PlayerLandsOnWell_WellIsOcupied()
        {
            // Arrange
            var _playerList = playerManager.GetPlayerList();
            var mockDice = new Mock<IDice>();

            _playerList[0].Position = 31;
            _playerList[0].IsWaiting = true;
            _playerList[1].Position = 27;
            mockDice.SetupSequence(d => d.Roll()).Returns(2).Returns(2);

            // Act
            _playerList[1].DiceRollAndMove(board, mockDice.Object);

            // Assert

            Assert.False(_playerList[0].IsWaiting);
            Assert.True(_playerList[1].IsWaiting);
            output.WriteLine("Player2 lands on the well where player1 was waiting. Player 1 plays on and now player2 will wait.");
        }
    }
}