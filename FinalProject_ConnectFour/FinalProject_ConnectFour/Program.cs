using System;

// Interface for a player
interface IPlayer
{
    int PlayMove();
}

// Base class for a player
abstract class Player : IPlayer
{
    protected string name;
    protected char symbol;

    protected internal string Name
    {
        get { return name; }
    }
    protected internal char Symbol
    {
        get { return symbol; }
    }

    public Player(string name, char symbol)
    {
        this.name = name;
        this.symbol = symbol;
    }

    public abstract int PlayMove();
}

// Human player 1 class inheriting from Player
class HumanPlayer1 : Player
{
    public HumanPlayer1(string name, char symbol) : base(name, symbol)
    {
    }

    public override int PlayMove()
    {
        Console.WriteLine($"[{name}], it's your turn (symbol: {symbol}). Enter the column number to drop your piece:");
        int column = Convert.ToInt32(Console.ReadLine());
        return column;
    }
}

// Human player 2 class inheriting from Player
class HumanPlayer2 : Player
{
    public HumanPlayer2(string name, char symbol) : base(name, symbol)
    {
    }

    public override int PlayMove()
    {
        Console.WriteLine($"[{name}], it's your turn (symbol: {symbol}). Enter the column number to drop your piece:");
        int column = Convert.ToInt32(Console.ReadLine());
        return column;
    }
}

// Connect Four game class
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
    }

    public void StartGame()
    {
        Console.WriteLine("===== CONNECT FOUR =====");
        Console.WriteLine("Welcome to Connect Four!");

        Player currentPlayer = player1;
        int moveCount = 0;

        while (!gameOver)
        {
            Console.WriteLine("\nCurrent Board:");
            PrintBoard();

            int column = currentPlayer.PlayMove();

            if (IsValidMove(column))
            {
                int row = DropPiece(column, currentPlayer.Symbol);
                if (IsWinningMove(row, column, currentPlayer.Symbol))
                {
                    Console.WriteLine($"\n[{currentPlayer.Name}] Wins! Congratulations!");
                    gameOver = true;
                }
                else if (IsBoardFull())
                {
                    Console.WriteLine("\nThe game is a draw.");
                    gameOver = true;
                }
                else
                {
                    currentPlayer = (currentPlayer == player1) ? player2 : player1;
                    moveCount++;
                }
            }
            else
            {
                Console.WriteLine("Invalid move. Please try again.");
            }
        }

        Console.WriteLine("\nGame Over.");
    }

    private bool IsValidMove(int column)
    {
        return column >= 1 && column <= 7 && board[0, column - 1] == '\0';
    }

    private int DropPiece(int column, char symbol)
    {
        for (int row = 5; row >= 0; row--)
        {
            if (board[row, column - 1] == '\0')
            {
                board[row, column - 1] = symbol;
                return row;
            }
        }
        return -1;
    }

    private bool IsWinningMove(int row, int column, char symbol)
    {
        int count = 0;

        // Check horizontally
        for (int c = 0; c < 7; c++)
        {
            if (board[row, c] == symbol)
                count++;
            else
                count = 0;

            if (count >= 4)
                return true;
        }

        // Check vertically
        count = 0;
        for (int r = 0; r < 6; r++)
        {
            if (board[r, column - 1] == symbol)
                count++;
            else
                count = 0;

            if (count >= 4)
                return true;
        }

        // Check diagonals (top-left to bottom-right)
        count = 0;
        int startRow = row - Math.Min(row, column - 1);
        int startColumn = column - 1 - Math.Min(row, column - 1);
        for (int r = startRow, c = startColumn; r < 6 && c < 7; r++, c++)
        {
            if (board[r, c] == symbol)
                count++;
            else
                count = 0;

            if (count >= 4)
                return true;
        }

        // Check diagonals (top-right to bottom-left)
        count = 0;
        startRow = row - Math.Min(row, 6 - column);
        startColumn = column - 1 + Math.Min(row, 6 - column);
        for (int r = startRow, c = startColumn; r < 6 && c >= 0; r++, c--)
        {
            if (board[r, c] == symbol)
                count++;
            else
                count = 0;

            if (count >= 4)
                return true;
        }

        return false;
    }

    private bool IsBoardFull()
    {
        for (int r = 0; r < 6; r++)
        {
            for (int c = 0; c < 7; c++)
            {
                if (board[r, c] == '\0')
                    return false;
            }
        }
        return true;
    }

    private void PrintBoard()
    {
        for (int r = 0; r < 6; r++)
        {
            for (int c = 0; c < 7; c++)
            {
                Console.Write(board[r, c] + " ");
            }
            Console.WriteLine();
        }
    }
}

// Main program
class Program
{
    static void Main(string[] args)
    {
        Player player1 = new HumanPlayer1("Player 1", 'X');
        Player player2 = new HumanPlayer2("Player 2", 'O');

        ConnectFour game = new ConnectFour(player1, player2);
        game.StartGame();
    }
}
