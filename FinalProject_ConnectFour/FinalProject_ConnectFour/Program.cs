using System;

public class ConnectFour
{
    private const int Rows = 6;
    private const int Columns = 7;
    private char[,] board;
    private char currentPlayer;
    private bool gameOver;

    public ConnectFour()
    {
        board = new char[Rows, Columns];
        currentPlayer = 'X';
        gameOver = false;
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                board[row, col] = ' ';
            }
        }
    }

    public void Play()
    {
        Console.WriteLine("Welcome to Connect Four!");

        while (!gameOver)
        {
            DisplayBoard();
            int column = GetValidColumn();

            if (MakeMove(column))
            {
                if (CheckWin(currentPlayer))
                {
                    DisplayBoard();
                    Console.WriteLine($"Player {currentPlayer} wins!");
                    gameOver = true;
                }
                else if (CheckDraw())
                {
                    DisplayBoard();
                    Console.WriteLine("It's a draw!");
                    gameOver = true;
                }
                else
                {
                    SwitchPlayer();
                }
            }
            else
            {
                Console.WriteLine("Invalid move. Please try again.");
            }
        }

        Console.WriteLine("Thanks for playing Connect Four!");
    }

    private void DisplayBoard()
    {
        Console.WriteLine();
        for (int row = Rows - 1; row >= 0; row--)
        {
            Console.Write("|");
            for (int col = 0; col < Columns; col++)
            {
                Console.Write($"{board[row, col]}|");
            }
            Console.WriteLine();
        }
        Console.WriteLine("---------------");
        Console.WriteLine(" 1 2 3 4 5 6 7");
        Console.WriteLine();
    }

    private int GetValidColumn()
    {
        int column;
        while (true)
        {
            Console.Write($"Player {currentPlayer}, enter a column (1-{Columns}): ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out column) && column >= 1 && column <= Columns)
            {
                return column - 1; // Subtract 1 to convert to 0-based index
            }
            Console.WriteLine("Invalid column. Please try again.");
        }
    }

    private bool MakeMove(int column)
    {
        for (int row = 0; row < Rows; row++)
        {
            if (board[row, column] == ' ')
            {
                board[row, column] = currentPlayer;
                return true;
            }
        }
        return false; // Column is full
    }

    private bool CheckWin(char player)
    {
        // Check horizontal
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col <= Columns - 4; col++)
            {
                if (board[row, col] == player &&
                    board[row, col + 1] == player &&
                    board[row, col + 2] == player &&
                    board[row, col + 3] == player)
                {
                    return true;
                }
            }
        }

        // Check vertical
        for (int row = 0; row <= Rows - 4; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                if (board[row, col] == player &&
                    board[row + 1, col] == player &&
                    board[row + 2, col] == player &&
                    board[row + 3, col] == player)
                {
                    return true;
                }
            }
        }

        // Check diagonal (top-left to bottom-right)
        for (int row = 0; row <= Rows - 4; row++)
        {
            for (int col = 0; col <= Columns - 4; col++)
            {
                if (board[row, col] == player &&
                    board[row + 1, col + 1] == player &&
                    board[row + 2, col + 2] == player &&
                    board[row + 3, col + 3] == player)
                {
                    return true;
                }
            }
        }

        // Check diagonal (bottom-left to top-right)
        for (int row = 3; row < Rows; row++)
        {
            for (int col = 0; col <= Columns - 4; col++)
            {
                if (board[row, col] == player &&
                    board[row - 1, col + 1] == player &&
                    board[row - 2, col + 2] == player &&
                    board[row - 3, col + 3] == player)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool CheckDraw()
    {
        for (int col = 0; col < Columns; col++)
        {
            if (board[Rows - 1, col] == ' ')
            {
                return false; // There is an empty cell, game is not a draw
            }
        }
        return true; // All cells are filled, game is a draw
    }

    private void SwitchPlayer()
    {
        currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
    }
}

class Program
{
    static void Main(string[] args)
    {
        ConnectFour game = new ConnectFour();
        game.Play();
    }
}
