﻿using System;

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
        Console.WriteLine("**********Welcome to Connect Four!************");

      while (!gameOver)
          DisplayBoard();
        
       ---Is statement here

            else
            {
                Console.WriteLine("Invalid move! Please try again.");
            }
        }

        Console.WriteLine("Thanks for playing Connect Four!");
    }

    private void Display()
    {
        
    }

    private int GetColumn()
    {
        
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
