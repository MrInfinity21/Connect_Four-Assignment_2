using System;

public class Menu
{
    void DisplayMenu()
    {
        Console.WriteLine("Welcome to Connect 4");
        Console.WriteLine("1. Play Game");
        Console.WriteLine("2. View Rules");
        Console.WriteLine("3. Exit Game");
    }

    void UserInput()
    {
        bool isRunning = true;
        while (isRunning)
        {
            DisplayMenu();
            Console.WriteLine("\nSelect an option: ");
            string choice = Console.ReadLine();
            if (int.TryParse(choice, out int option))
            {
                switch (option)
                {
                    case 1:
                        StartGame();
                        break;
                    case 2:
                        ViewRules();
                        break;
                    case 3:
                        Exit();
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("\nWrong Input. Please select 1, 2, or 3 from the menu.");
                        break;
                }
            }
        }
    }

    void StartGame()
    {
        Console.Clear();
        // Create a new instance of ConnectFourGame and start the game
        var game = new ConnectFourGame();
        game.PlayGame();
    }

    void ViewRules()
    {
        Console.Clear();
        Console.WriteLine("--- Rules for Connect 4 ---");
        Console.WriteLine("1. Game starts with a 6x7 grid with two colors Red and Green.");
        Console.WriteLine("2. Players take turns to drop their checkers in the grid");
        Console.WriteLine("3. The first player to get four checkers in a row, column, or diagonal wins the game");
        Console.WriteLine("4. If all the spaces of the grid without a winner, then the game ends in a STALEMATE.");
        Console.WriteLine("Press any key to return to the Menu");
        Console.ReadKey();
    }

    void Exit()
    {
        Console.WriteLine("Goodbye.");
    }

    public void Start()
    {
        UserInput();
    }
}

public class ConnectFourGame
{
    private string[,] board = new string[6, 7];  // 6 rows and 7 columns
    private string currentPlayer;

    public ConnectFourGame()
    {
        // Initialize the game board with empty spaces
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                board[row, col] = ".";
            }
        }
        currentPlayer = "X";  // Player "X" starts first
    }

    public void PlayGame()
    {
        bool gameWon = false;
        while (!gameWon)
        {
            Console.Clear();
            DisplayBoard();  // Show the current board
            Console.WriteLine($"Player {currentPlayer}, it's your turn.");
            int column = GetPlayerMove();  // Get the column for the current player's move

            if (MakeMove(column))
            {
                gameWon = CheckForWin();
                if (gameWon)
                {
                    Console.Clear();
                    DisplayBoard();
                    Console.WriteLine($"Player {currentPlayer} wins!");
                    break;
                }

                // Switch to the next player
                currentPlayer = (currentPlayer == "X") ? "O" : "X";
            }
            else
            {
                Console.WriteLine("Column is full! Try again.");
            }
        }

        Console.WriteLine("Press any key to return to the menu.");
        Console.ReadKey();
    }

    private void DisplayBoard()
    {
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                Console.Write(board[row, col] + " ");
            }
            Console.WriteLine();
        }
    }

    private int GetPlayerMove()
    {
        int column;
        bool validInput = false;

        while (!validInput)
        {
            Console.WriteLine("Enter a column (0-6): ");
            validInput = int.TryParse(Console.ReadLine(), out column) && column >= 0 && column < 7;
            if (!validInput)
            {
                Console.WriteLine("Invalid column. Please try again.");
            }
        }

        return column;
    }

    private bool MakeMove(int column)
    {
        // Find the lowest empty space in the selected column
        for (int row = 5; row >= 0; row--)
        {
            if (board[row, column] == ".")
            {
                board[row, column] = currentPlayer;
                return true;
            }
        }
        return false;  // The column is full
    }

    private bool CheckForWin()
    {
        // Check for horizontal, vertical, and diagonal wins
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                if (board[row, col] != "." && (
                    CheckDirection(row, col, 1, 0) || // Horizontal
                    CheckDirection(row, col, 0, 1) || // Vertical
                    CheckDirection(row, col, 1, 1) || // Diagonal (down-right)
                    CheckDirection(row, col, 1, -1)))  // Diagonal (up-right)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool CheckDirection(int row, int col, int deltaRow, int deltaCol)
    {
        string player = board[row, col];
        int count = 0;

        for (int i = 0; i < 4; i++)
        {
            int r = row + i * deltaRow;
            int c = col + i * deltaCol;

            if (r >= 0 && r < 6 && c >= 0 && c < 7 && board[r, c] == player)
            {
                count++;
            }
            else
            {
                break;
            }
        }

        return count == 4;  // 4 consecutive markers in a row, column, or diagonal
    }
}

class Program
{
    static void Main()
    {
        var menu = new Menu();
        menu.Start();  // Start the menu
    }
}
