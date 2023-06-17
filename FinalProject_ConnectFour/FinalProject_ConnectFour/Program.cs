using System;

public class ConnectFour
{
    
    private char currentPlayer;
    private bool gameOver;
    private const int Rows = 6;
    private const int Columns = 7;
    private char [,] board;

    public ConnectFour()
    {
        currentPlayer = 'X'; 
        gameOver= false;
        board = new char[Rows, Columns]; // Adding board for Rows and Columns
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
    
    private void Display()
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

    private int GetColumn()
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
        //if else statement, 
    }

    private bool CheckWin(char player)
    {
        // Check horizontal
        

        // Check vertical
       
        

        // Check diagonal 
        
        

       // return false;
    }

    private bool CheckDraw()
    {
        //check draw
    }

    private void SwitchPlayer()
    {
        //switch player
    }
}

class Program
{
    static void Main(string[] args)
    {
        //Play();
    }
}
