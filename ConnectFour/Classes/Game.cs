using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class Game: IGame
    {
        public bool isSetup { get; set; }

        public bool gameOver { get; set; }

        private char currentTurn = 'r';

        private uint boardWidth = 0;

        private uint boardHeight = 0;

        private enum victoryTypes {draw, horizontal, vertical, diagonal};

        private victoryTypes victory;

        private char[,] board;

        /// <summary>
        /// Initialise game without setting up.
        /// </summary>
        public Game()
        {
            isSetup = false;
        }

        /// <summary>
        /// Requests user input and sets up the game.
        /// </summary>
        public void setup()
        {
            Console.WriteLine("Please enter the board dimensions (number of rows, number of columns)");
            char[] delimChars = { ' ', ',', ';' };
            string[] dimensions = Console.ReadLine().Split(delimChars);

            if (dimensions.Length == 2)
            {
                if (!uint.TryParse(dimensions[0], out boardHeight) || !uint.TryParse(dimensions[1], out boardWidth))
                {
                    dimensionError();
                    return;
                }
                board = setupBoard(boardWidth, boardHeight);
                redrawBoard();
                isSetup = true;
            } else
            {
                dimensionError();
            }
        }

        /// <summary>
        /// Initialises the board and sets all its values to their default state.
        /// </summary>
        /// <param name="width">Board width</param>
        /// <param name="height">Board height</param>
        /// <returns>Returns the initialised board.</returns>
        private char[,] setupBoard(uint width, uint height)
        {
            char[,] board = new char[width, height];
            for (uint i = 0; i < width; i++)
            {
                for (uint j = 0; j < height; j++)
                {
                    board[i,j] = 'o';
                }
            }
            return board;
        }

        /// <summary>
        /// Prints an error when invalid board dimensions are given.
        /// </summary>
        private void dimensionError()
        {
            Console.WriteLine("Invalid board dimensions");
        }

        /// <summary>
        /// Prints an error when an invalid move is made.
        /// </summary>
        private void moveError()
        {
            Console.WriteLine("Invalid move");
        }

        /// <summary>
        /// Executes a single turn for the current player.
        /// </summary>
        public void executeTurn()
        {
            uint column;
            if (currentTurn == 'r')
            {
                Console.WriteLine("Red's turn: ");
            } else
            {
                Console.WriteLine("Yellow's turn: ");
            }
            string move = Console.ReadLine();
            if (!uint.TryParse(move, out column))
            {
                moveError();
                return;
            }
            column--;
            if (column >= boardWidth || board[column,boardHeight-1] != 'o')
            {
                moveError();
                return;
            }
            insertToken(currentTurn, column);
            redrawBoard();
            if (checkWinner(currentTurn)) {
                displayWinner(currentTurn);
            }
            switchPlayer(currentTurn);
        }

        /// <summary>
        /// insert the current player's token into the game board.
        /// </summary>
        /// <param name="player">The current player</param>
        /// <param name="column">The column the token is to be inserted into</param>
        private void insertToken(char player, uint column)
        {
            uint counter = 0;
            while (true)
            {
                if (board[column, counter] == 'o')
                {
                    board[column, counter] = player;
                    return;
                } else
                {
                    counter++;
                }
            }
        }

        /// <summary>
        /// Switches the current player.
        /// </summary>
        /// <param name="player">The current player</param>
        private void switchPlayer(char player)
        {
            if (player == 'r')
            {
                currentTurn = 'y';
            } else
            {
                currentTurn = 'r';
            }
        }

        /// <summary>
        /// Checks for an endgame condition.
        /// </summary>
        /// <param name="player">The current player</param>
        /// <returns>Returns true if game is won, false if otherwise.</returns>
        private bool checkWinner(char player)
        {
            for (uint j = 0; j < boardHeight; j++)
            {
                for (uint i = 0; i < boardWidth; i++)
                {
                    if (board[i, j] == player)
                    {
                        if (checkHorizonal(player, i, j) || checkVertical(player, i , j) 
                            || checkDiagonalToLeft(player, i, j) || checkDiagonalToRight(player, i, j))
                        {
                            gameOver = true;
                            return true;
                        }
                    }
                }
            }
            if (checkDraw())
            {
                gameOver = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the game has ended in a draw.
        /// </summary>
        /// <returns>Returns true if a draw has occurred, false if otherwise.</returns>
        private bool checkDraw()
        {
            for (uint i = 0; i < boardWidth; i++)
            {
                if (board[i, boardHeight - 1] == 'o') return false;
            }
            victory = victoryTypes.draw;
            return true;
        }

        /// <summary>
        /// Checks to see if the current player has won the game by connecting 4 tokens horizontally.
        /// </summary>
        /// <param name="player">the current player</param>
        /// <param name="i">Horizontal coordinate</param>
        /// <param name="j">Vertical coordinate</param>
        /// <returns>Returns true if horizontal victory has occurred.</returns>
        private bool checkHorizonal(char player, uint i, uint j)
        {
            if ((i + 3) < boardWidth && board[i + 1, j] == player
                && board[i + 2, j] == player && board[i + 3, j] == player)
            {
                victory = victoryTypes.horizontal;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the current player has won the game by connecting 4 tokens vertically.
        /// </summary>
        /// <param name="player">the current player</param>
        /// <param name="i">Horizontal coordinate</param>
        /// <param name="j">Vertical coordinate</param>
        /// <returns>Returns true if vertical victory has occurred.</returns>
        private bool checkVertical(char player, uint i, uint j)
        {
            if ((j + 3) < boardHeight && board[i, j + 1] == player
                && board[i, j + 2] == player && board[i, j + 3] == player)
            {
                victory = victoryTypes.vertical;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the current player has won the game by connecting 4 tokens on the main diagonal.
        /// </summary>
        /// <param name="player">the current player</param>
        /// <param name="i">Horizontal coordinate</param>
        /// <param name="j">Vertical coordinate</param>
        /// <returns>Returns true if main diagonal victory has occurred.</returns>
        private bool checkDiagonalToLeft(char player, uint i, uint j)
        {
            if ((i - 3) >= 0 && (j + 3) < boardHeight && board[i - 1, j + 1] == player
                && board[i - 2, j + 2] == player && board[i - 3, j + 3] == player)
            {
                victory = victoryTypes.diagonal;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the current player has won the game by connecting 4 tokens on the antidiagonal.
        /// </summary>
        /// <param name="player">the current player</param>
        /// <param name="i">Horizontal coordinate</param>
        /// <param name="j">Vertical coordinate</param>
        /// <returns>Returns true if antidiagonal victory has occurred.</returns>
        private bool checkDiagonalToRight(char player, uint i, uint j)
        {
            if ((i + 3) < boardWidth && (j + 3) < boardHeight && board[i + 1, j + 1] == player
                && board[i + 2, j + 2] == player && board[i + 3, j + 3] == player)
            {
                victory = victoryTypes.diagonal;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Prints the type of victory and the winner to the console.
        /// </summary>
        /// <param name="player">The current player</param>
        private void displayWinner(char player)
        {
            if (victory != victoryTypes.draw)
            {
                if (player == 'r')
                {
                    Console.Write("Red wins - ");
                }
                else
                {
                    Console.Write("Yellow wins - ");
                }
                switch (victory)
                {
                    case victoryTypes.horizontal:
                        Console.Write("horizontal");
                        break;
                    case victoryTypes.vertical:
                        Console.Write("vertical");
                        break;
                    case victoryTypes.diagonal:
                        Console.Write("diagonal");
                        break;
                }
                Console.WriteLine();
            } else
            {
                Console.WriteLine("Draw");
            }
        }
        
        /// <summary>
        /// Redraws the board in the console.
        /// </summary>
        private void redrawBoard()
        {
            for (uint j = boardHeight ; j-- > 0;)
            {
                for (uint i = 0; i < boardWidth; i++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

    }
}
