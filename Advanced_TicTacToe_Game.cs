using System;

namespace Tic_Tac_Toe
{
    class MainLoop
    {
        static TicTacToe tT;
        public static void Main(string[] args)
        {
            tT = new TicTacToe();
            tT.startGameLoop();
        }
    };

    public class TicTacToe
    {
        UserInterface uI;
        
        public TicTacToe()
        {
            uI = new UserInterface();
        }
        bool isCellEmpty(int x, int y)
        {
            if(uI.grid[x, y] == '-')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        UserInterface.PLAYER checkWinner()
        {
            if ((uI.grid[0, 0] == 'O' && uI.grid[1, 1] == 'O' && uI.grid[2, 2] == 'O') || (uI.grid[0, 2] == 'O' && uI.grid[1, 1] == 'O' && uI.grid[2, 0] == 'O') || 
                (uI.grid[0, 0] == 'O' && uI.grid[0, 1] == 'O' && uI.grid[0, 2] == 'O') || (uI.grid[1, 0] == 'O' && uI.grid[1, 1] == 'O' && uI.grid[1, 2] == 'O') ||
                (uI.grid[0, 0] == 'O' && uI.grid[1, 0] == 'O' && uI.grid[2, 0] == 'O') || (uI.grid[0, 1] == 'O' && uI.grid[1, 1] == 'O' && uI.grid[2, 1] == 'O') ||
                (uI.grid[2, 0] == 'O' && uI.grid[2, 1] == 'O' && uI.grid[2, 2] == 'O') || (uI.grid[0, 2] == 'O' && uI.grid[1, 2] == 'O' && uI.grid[2, 2] == 'O'))
            {
                return UserInterface.PLAYER.player_2;
            }
            else if((uI.grid[0, 0] == 'X' && uI.grid[1, 1] == 'X' && uI.grid[2, 2] == 'X') || (uI.grid[0, 2] == 'X' && uI.grid[1, 1] == 'X' && uI.grid[2, 0] == 'X') ||
                (uI.grid[0, 0] == 'X' && uI.grid[0, 1] == 'X' && uI.grid[0, 2] == 'X') || (uI.grid[1, 0] == 'X' && uI.grid[1, 1] == 'X' && uI.grid[1, 2] == 'X') ||
                (uI.grid[0, 0] == 'X' && uI.grid[1, 0] == 'X' && uI.grid[2, 0] == 'X') || (uI.grid[0, 1] == 'X' && uI.grid[1, 1] == 'X' && uI.grid[2, 1] == 'X') ||
                (uI.grid[2, 0] == 'X' && uI.grid[2, 1] == 'X' && uI.grid[2, 2] == 'X') || (uI.grid[0, 2] == 'X' && uI.grid[1, 2] == 'X' && uI.grid[2, 2] == 'X'))
            {
                return UserInterface.PLAYER.player_1;
            }
            return UserInterface.PLAYER.none;
        }

        public void updateTurn()
        {
            if(uI.currentTurn == UserInterface.PLAYER.player_1)
            {
                uI.currentTurn = UserInterface.PLAYER.player_2;
            }
            else
            {
                uI.currentTurn = UserInterface.PLAYER.player_1;
            }
        }

        public void setGridValue(int x, int y, UserInterface.PLAYER pCurrentTurn)
        {
            if(pCurrentTurn == UserInterface.PLAYER.player_1)
            {
                uI.grid[x, y] = 'X';
            }
            else
            {
                uI.grid[x, y] = 'O';
            }
            updateTurn();
        }

        public bool isGridFull()
        {
            if ((uI.grid[0, 0] == 'X' || uI.grid[0, 0] == 'O') && (uI.grid[0, 1] == 'X' || uI.grid[0, 1] == 'O') && 
             (uI.grid[0, 2] == 'X' || uI.grid[0, 2] == 'O') && (uI.grid[1, 0] == 'X' || uI.grid[1, 0] == 'O') && 
             (uI.grid[1, 1] == 'X' || uI.grid[1, 1] == 'O') && (uI.grid[1, 2] == 'X' || uI.grid[1, 2] == 'O') && 
             (uI.grid[2, 0] == 'X' || uI.grid[2, 0] == 'O') && (uI.grid[2, 1] == 'X' || uI.grid[2, 1] == 'O') &&
             (uI.grid[2, 2] == 'X' || uI.grid[2, 2] == 'O'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void startGameLoop()
        {
            char playAgain;
            do
            {
                uI.message = " ";
                if (uI.currentTurn == UserInterface.PLAYER.player_2)
                {
                    uI.currentTurn = UserInterface.PLAYER.player_1;
                }
                uI.clearGrid();
                uI.updateUIForTurn();

                while (isGridFull() == false)
                {
                    if (isCellEmpty(uI.rowIndex, uI.columnIndex) == false)
                    {
                        uI.message = "Cell is already filled.";
                        updateTurn();
                        updateTurn();

                    }
                    else
                    {
                        uI.message = " ";
                        setGridValue(uI.rowIndex, uI.columnIndex, uI.currentTurn);
                    }
                    

                    UserInterface.PLAYER winnerPlayer = checkWinner();
                    if (winnerPlayer != UserInterface.PLAYER.none)
                    {
                        Console.Clear();
                        uI.displayGrid();
                        uI.message = "Wins" + winnerPlayer;
                        uI.updateUIForEnd(winnerPlayer);
                        break;
                    }
                    else if (isGridFull() == true && (checkWinner() == UserInterface.PLAYER.none))
                    {
                        Console.Clear();
                        uI.displayGrid();
                        Console.WriteLine("It is a DRAW!");
                        break;
                    }
                    uI.updateUIForTurn();
                    
                }
                Console.WriteLine("To play Agian enter 'y' otherwise enter 'n'");
                playAgain = char.Parse(Console.ReadLine());
            }
            while (playAgain == 'y');
            Console.ReadKey();



        }

        
    };

    public class UserInterface
    {
        public char[,] grid;
        public enum PLAYER { 
            player_1,
            player_2,
            none
        }
        public PLAYER currentTurn = PLAYER.player_1;
        public int rowIndex, columnIndex;

        public string message;
        public UserInterface()
        {
            grid = new char[3, 3];
        }

        public void playerInputRC()
        {
            char tryAgain;
            do
            {
                tryAgain = 'n';
                Console.WriteLine("Enter Row and Column Index");
                rowIndex = int.Parse(Console.ReadLine());

                //Console.WriteLine("Enter Column Index");
                columnIndex = int.Parse(Console.ReadLine());
                if((rowIndex < 0 || rowIndex > 2) || (columnIndex < 0 || columnIndex > 2))
                {
                    Console.WriteLine();
                    Console.WriteLine("Alert:");
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine("Column or Row Entered does not Exist in Index, Try Again");
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine();
                    tryAgain = 'y';
                }
            }
            while (tryAgain == 'y');
        }
        public void displayGrid()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(grid[i, j] + "\t");
                    if (j==2)
                    {
                        Console.WriteLine("");
                    }
                    
                }
            }
        }

        public void clearGrid()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j <3; j++)
                {
                    grid[i, j] = '-';
                }
            }
        }

        public void updateUIForTurn()
        {
            Console.Clear();
            Console.WriteLine("Tic Tac Toe Game");
            displayGrid();
            if(message != string.Empty)
            {
                Console.WriteLine(message);
            }

            Console.WriteLine("Turn::" + currentTurn);
            playerInputRC();

        }

        public void updateUIForEnd(PLAYER pwinner)
        {
            if(pwinner == PLAYER.player_1)
            {
                Console.WriteLine("PLAYER 1 WINS");

            }
            else
            {
                Console.WriteLine("PLAYER 2 WINS");
            }
        }
    };

}



