public class Menu
{ 
    char[,] board = new char[6, 7];
    char currentPlayer = 'R';
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
            Console.WriteLine("\n Select a option: ");
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
                        Console.WriteLine("\n Wrong Input. Please select 1, 2, ,or 3 from the menu.");
                        break;
                }
            }
        }
        
    }

    void StartGame()
    {
        Console.Clear();
        Console.WriteLine("Game Starts.....");
        InitializeBoard();
        DisplayBoard();
        

        while (true)
        {
            Console.WriteLine($"\n Player {currentPlayer}'s turn. Choose a column (0-6): ");
            int column = GetPlayerMove();
            
            bool isPlaced = PlaceChecker(column);
            if (!isPlaced)
            {
                Console.WriteLine("column is full. Try again.");
                continue;
            }
            DisplayBoard();

            if (CheckWin())
            {
                Console.WriteLine($"\n Player {currentPlayer} WINS!");
                break;
            }
            currentPlayer = (currentPlayer == 'R') ? 'G' : 'R';
        }
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
    }

    void Exit()
    {
        Console.WriteLine("Goodbye.");
    }

    void InitializeBoard()
    {
        Console.WriteLine("Connect 4 Board");
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                board[row, col] = '.';
            }
        }
    }

    void DisplayBoard()
    {
        Console.Clear();
        Console.WriteLine("Connect 4 Board");
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                Console.Write(board[row, col] + " ");
            }
            Console.WriteLine();
        }
    }

    int GetPlayerMove()
    {
        int column = -1;
        while (true) 
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out column)) 
            {
                if (column < 0 ||  column < 7)
                {
                    return column;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please select a column (0-6): ");    
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please select a column (0-6): ");
            }
        }

    }

    bool PlaceChecker(int column)
    {
        for (int row = 5; row >= 0; row--)
        {
            if (board[row, column] == '.')
            {
                board[row, column] = currentPlayer;
                return true;
            }
        }
        return false;
    }

    bool CheckWin()
    {
        
        // horizontal
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (board[row, col] == currentPlayer &&
                    board[row, col + 1] == currentPlayer &&
                    board[row, col + 2] == currentPlayer &&
                    board[row, col + 3] == currentPlayer)
                {
                    return true;
                }
            }
        }
        
        //Vertical
        for (int col = 0; col < 7; col++)
        {
            for (int row = 0; row < 3; row++)
            {
                if (board[row, col] == currentPlayer &&
                    board[row + 1, col] == currentPlayer &&
                    board[row + 2, col] == currentPlayer &&
                    board[row + 3, col] == currentPlayer)
                {
                    return true;
                }
            }
        }
        
        //top-left to bottom-right diagonal
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (board[row, col] == currentPlayer &&
                    board[row + 1, col + 1] == currentPlayer &&
                    board[row + 2, col + 2] == currentPlayer &&
                    board[row + 3, col + 3] == currentPlayer)
                {
                    return true;
                }
            }
        }
        
        //top-right to bottom-left diagonal
        for (int row = 0; row < 3; row++)
        {
            for (int col = 3; col < 7; col++)
            {
                if (board[row, col] == currentPlayer &&
                    board[row + 1, col - 1] == currentPlayer &&
                    board[row + 2, col - 2] == currentPlayer &&
                    board[row + 3, col - 3] == currentPlayer)
                {
                    return true;
                }
            }
        }
        
        return false;
        


    }

    public void Start()
    {
        UserInput();
    }
}


    


class Program
{
    static void Main()
    {
        var menu = new Menu();
        menu.Start();
    }
}