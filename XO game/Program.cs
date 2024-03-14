/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XO_game
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}*/

using System;

namespace XO_game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XoGame game = new XoGame();
            game.PlayGame();
        }
    }

    class XoGame
    {
        private char[,] board;

        public XoGame()
        {
            board = new char[3, 3];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = '-';
                }
            }
        }

        public void PrintBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public bool MakeMove(int row, int col, char player)
        {
            if (row < 0 || row >= 3 || col < 0 || col >= 3 || board[row, col] != '-')
            {
                Console.WriteLine("Invalid move! The cell is already taken. Try again.");
                return false;
            }
            board[row, col] = player;
            return true;
        }

        public bool CheckWin(char player)
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player)
                {
                    return true; // Check rows
                }
                if (board[0, i] == player && board[1, i] == player && board[2, i] == player)
                {
                    return true; // Check columns
                }
            }
            if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
            {
                return true; // Check diagonal
            }
            if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
            {
                return true; // Check reverse diagonal
            }
            return false;
        }

        public bool IsBoardFull()
        {
            foreach (char cell in board)
            {
                if (cell == '-')
                {
                    return false;
                }
            }
            return true;
        }

        public void ComputerMove()
        {
            // Implement computer's move here
            // Check if there's a winning move for computer
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == '-')
                    {
                        board[i, j] = 'O';
                        if (CheckWin('O'))
                            return;
                        board[i, j] = '-';
                    }
                }
            }

            // Check if there's a winning move for player, and block it
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == '-')
                    {
                        board[i, j] = 'X';
                        if (CheckWin('X'))
                        {
                            board[i, j] = 'O';
                            return;
                        }
                        board[i, j] = '-';
                    }
                }
            }

            // If no winning move found, make a random move
            Random random = new Random();
            int row, col;
            do
            {
                row = random.Next(0, 3);
                col = random.Next(0, 3);
            } while (board[row, col] != '-');

            board[row, col] = 'O';
        }

        public bool PlayGame()
        {
            Console.WriteLine("Welcome to the X O game!");
            Console.WriteLine("You are playing against the computer. You are X, and the computer is O.");
            Console.WriteLine("To make a move, enter the row (0-2) and column (0-2) separated by space.");

            char currentPlayer = 'X';

            while (true)
            {
                Console.WriteLine("\nCurrent board:");
                PrintBoard();

                int row, col;

                if (currentPlayer == 'X')
                {
                    Console.WriteLine("\nYour turn (X):");
                    string[] input;
                    do
                    {
                        Console.WriteLine("Enter row and column (0-2) separated by space:");
                        input = Console.ReadLine().Split(' ');
                    } while (input.Length != 2 || !int.TryParse(input[0], out row) || !int.TryParse(input[1], out col) || row < 0 || row >= 3 || col < 0 || col >= 3 || !MakeMove(row, col, currentPlayer));
                }
                else
                {
                    Console.WriteLine("\nComputer's turn (O):");
                    ComputerMove();
                }

                if (CheckWin(currentPlayer))
                {
                    Console.WriteLine("\nCongratulations! " + currentPlayer + " wins!");
                    if (!PlayAgain())
                        return true;
                    InitializeBoard();
                }
                else if (IsBoardFull())
                {
                    Console.WriteLine("\nThe game is a draw!");
                    if (!PlayAgain())
                        return true;
                    InitializeBoard();
                }
                else if (currentPlayer == 'X')
                {
                    Console.WriteLine("\nNo winner in this game.");
                }

                currentPlayer = (currentPlayer == 'X') ? 'O' : 'X'; // Switch players
            }
        }

        public bool PlayAgain()
        {
            Console.WriteLine("Do you want to play again? (Y/N)");
            string response = Console.ReadLine().ToUpper();
            return (response == "Y");
        }
    }
}



