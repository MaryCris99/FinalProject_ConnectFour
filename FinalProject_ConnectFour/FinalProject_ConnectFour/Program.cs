using System;

interface IPlayer //Interface for a player
{
    int PlayMove();
}

abstract class Player : IPlayer
{
    protected char symbol;

    public Player(char symbol)
    {
        this.symbol = symbol;
    }
    public abstract int PlayMove();
}

class HumanPlayer : Player
{
    public HumanPlayer(string v, char symbol): base(symbol)
    {

    }

    public override int PlayMove()
   
    {
        int column;
        int Columns = 7;

        while (true)
        {
            Console.Write($"Player {symbol}, enter a column (1-{Columns}): ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out column) && column >= 1 && column <= Columns)
            {
                return column - 1; // Subtract 1 to convert to 0-based index
            }
            Console.WriteLine("Invalid column. Please try again.");
        }

    }
}

//Connect Four Game Class
class ConnectFour
{
    private char[,] board;
    private Player player1;
    private Player player2;
    private bool gameOver;

    public ConnectFour(Player player1, Player player2)
    {
        board = new char[6, 7];
        this.player1 = player1;
        this.player2 = player2;
        gameOver = false;
        //InitializeBoard();
    }

    /*private void InitializeBoard()
    {
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                board[row, col] = ' ';
            }
        }
    }*/

    public void PlayGame()
    {
        Console.WriteLine("Welcome to Connect Four!");
        Player currentPlayer = player1;
        
       

        while (!gameOver)
        {
            int column = currentPlayer.PlayMove();

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
        for (int row = 6 - 1; row >= 0; row--)
        {
            Console.Write("|");
            for (int col = 0; col < 7; col++)
            {
                Console.Write($"{board[row, col]}|");
            }
            Console.WriteLine();
        }
        Console.WriteLine("---------------");
        Console.WriteLine(" 1 2 3 4 5 6 7");
        Console.WriteLine();
    }

    /*private int GetValidColumn()
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
    }*/

    private bool MakeMove(int column)
    {
        for (int row = 0; row < 6; row++)
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
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col <= 7 - 4; col++)
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
        for (int row = 0; row <= 6 - 4; row++)
        {
            for (int col = 0; col < 7; col++)
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
        for (int row = 0; row <= 6 - 4; row++)
        {
            for (int col = 0; col <= 7 - 4; col++)
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
        for (int row = 3; row < 6; row++)
        {
            for (int col = 0; col <= 7 - 4; col++)
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
        for (int col = 0; col < 7; col++)
        {
            if (board[6 - 1, col] == ' ')
            {
                return false; // There is an empty cell, game is not a draw
            }
        }
        return true; // All cells are filled, game is a draw
    }

    private void SwitchPlayer()
    {
        player1 = v == 'X' ? 'O' : 'X';
    }
}

class Program
{
    static void Main(string[] args)
    {
        Player player1 = new HumanPlayer("Player 1", 'X');
        ConnectFour game = new ConnectFour(player1);
        game.PlayGame();
    }
}
