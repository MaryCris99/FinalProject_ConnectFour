using System;

// Interface for a player
interface IPlayer
{
    int PlayMove();// This method is declared as an abstract method, meaning it does not have an implementation in the base class. 
}

//Note: The subclasses derived from  "Player" will need to override and create its own implementation.
// Base class for a player
abstract class Player : IPlayer // define an abstract base class "Player" that implements "IPlayer" interface.
{
    // "name" and "symbol" are protected instance variables that store the player's name and symbol.
    // The "protected" access modifier allows these variables to be accessed by derived classes.
    protected string name;
    protected char symbol;

    protected internal string Name // is a protected internal property that provides read only access to the "name"
    {
        get { return name; }
    }
    protected internal char Symbol // is a protected internal property that provides read only access to the "symbol"
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
    //calling the base class constructor using the base (name,symbol) to initialize the name and symbol of the player.
    public HumanPlayer1(string name, char symbol) : base(name, symbol)
    {
    }

    public override int PlayMove()  //provide its own implementations for the PlayMove() method.
    {
        Console.WriteLine($"[{name}], it's your turn ({symbol}). Enter a column (1-7):");
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
        Console.WriteLine($"[{name}], it's your turn ({symbol}). Enter a column (1-7):");
        int column = Convert.ToInt32(Console.ReadLine());
        return column;
    }
}

// Connect Four game class
class ConnectFour
{
    private char[,] board; // is a character array representing the board game.
    private Player player1; // instances of the "Player" class and representing the player of the game
    private Player player2;
    private bool gameOver; //boolean variable indicating wheter the game has ended or not.

    public ConnectFour(Player player1, Player player2)
    {
        board = new char[6, 7];
        this.player1 = player1;
        this.player2 = player2;
        gameOver = false;
      
    }

    public void StartGame() // This method represents and indicates the control flows of the Connect Four Game.
    {
        Console.WriteLine("*****Welcome to Connect Four Game!*****");

        Player currentPlayer = player1;
        int moveCount = 0;

        while (!gameOver)
        {
            Console.WriteLine("\nCurrent Board:");
            DisplayBoard();

            int column = currentPlayer.PlayMove(); // the current player is prompted to make a move by calling the "PlayMove()" method of the current player

            if (MakeMove(column)) // it calls the column number that has been chosen by the player, if it is valid, the method proceeds
            {
                int row = DropPiece(column, currentPlayer.Symbol); // this method is called to drop the player's symbol in the specified column.
                if (IsWinningMove(row, column, currentPlayer.Symbol)) // this method is called to check if the player has won the game
                {
                    DisplayBoard();
                    Console.WriteLine($"\n[{currentPlayer.Name}] Wins! Congratulations!");// message is displayed if the player won the game
                    gameOver = true; // true if the player has won the game. 
                }
                else if (IsBoardFull()) // this method id used to check if the game board is full and there is no winner
                {
                    DisplayBoard();
                    Console.WriteLine("\nThe game is a draw.");
                    gameOver = true;
                }
                else
                {// If neither of the player wins nor the board is true, the turn switches to the next player and the "moveCount" is incremented.
                    currentPlayer = (currentPlayer == player1) ? player2 : player1;
                    moveCount++;
                }
            }
            else
            {
                Console.WriteLine("Invalid move. Please try again."); // this message will be displayed 
            }
        }

        Console.WriteLine("\nGame Over.");
    }

    private bool MakeMove(int column) //This method checks if the specified column is within the valid range from 1-7 and if the topmost cell in that column is empty ('\0')
    {
        return column >= 1 && column <= 7 && board[0, column - 1] == '\0';
    }

    private int DropPiece(int column, char symbol) // this method drops a symbol in the specified column by finding the lowest available row in that column.
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

    private bool IsWinningMove(int row, int column, char symbol)// this method checks if the most recent move made by a player results in a winning
    {
        int count = 0;

        // Check horizontally for a win
        for (int c = 0; c < 7; c++)
        {
            if (board[row, c] == symbol)
                count++;
            else
                count = 0;

            if (count >= 4)
                return true;
        }

        // Check vertically for a win
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

        // Check diagonals (top-left to bottom-right) for a win
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

        // Check diagonals (top-right to bottom-left) for a win
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

    private bool IsBoardFull() // this method checks if the game board is completely filled with player symbols. 
    {
        for (int r = 0; r < 6; r++)
        {
            for (int c = 0; c < 7; c++)
            {
                if (board[r, c] == '\0')
                    return false; // There is an empty cell, game is not a draw
            }
        }
        return true; // All cells are filled, game is a draw
    }

    private void DisplayBoard() // display the current state of the game board. The board is displayed as a grid of rows and columns separated by vertical lines ("|")
    {
        Console.WriteLine();
        for (int row = 0; row <6; row++)
        {
            Console.Write("| ");
            for (int col = 0; col < 7; col++)
            {
                Console.Write($"{board[row, col]}| ");
            }
            Console.WriteLine();
        }
        Console.WriteLine("----------------------");
        Console.WriteLine("  1  2  3  4  5  6  7 ");
        Console.WriteLine();

    }

}

// Main program
class Program
{
    // Entry point for the program.
    static void Main(string[] args)
    {
        Player player1 = new HumanPlayer1("Player 1", 'X');
        Player player2 = new HumanPlayer2("Player 2", 'O');

        ConnectFour game = new ConnectFour(player1, player2);
        game.StartGame(); // game is started by calling the "StartGame()" method
    }
}
