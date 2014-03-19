// Dharma Bellamkonda
// Game of Life


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Game
    {
        private const int Rows = 25;
        private const int Cols = 40;
        private int[,] grid;
        private int[,] nextGrid;

        public Game()
        {
            this.grid = new int[Rows, Cols];
            this.nextGrid = new int[Rows, Cols];
            SetupTestScenario();
        }

        private void SetupTestScenario()
        {
            // horiontal lines
            int[] lineRows = { 13 };
            foreach (int row in lineRows)
            {
                for (int col = 14; col <= 26; col++)
                {
                    grid[row, col] = 1;
                }
            }

            // vertical lines
            int[] lineCols = { 12, 20, 28 };
            foreach (int col in lineCols)
            {
                for (int row = 8; row <= 10; row++)
                {
                    grid[row, col] = 1;
                }
            }
        }

        public void Run()
        {
            Console.SetWindowSize(Cols, Rows);
            Console.CursorVisible = false;

            DrawFrame();
            SetStatusMessage("   Press Enter to run the simulation   ");
            Console.ReadKey();

            SetStatusMessage("   Press Enter to end the simulation   ");

            do
            {
                DrawFrame();
                UpdateGame();
            } while (!Console.KeyAvailable);
            SetStatusMessage("   Press Enter to close this window...  ");
        }

        private void SetStatusMessage(String message)
        {
            Console.SetCursorPosition(0, Rows - 1);
            Console.Write(message);
        }

        private void DrawFrame()
        {
            Console.SetCursorPosition(0, 0);
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    Console.Write(grid[row, col] == 1 ? String.Format("{0}", (char)9786) : " ");
                }
                Console.SetCursorPosition(0, row);
            }
        }

        private void UpdateGame()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    nextGrid[row, col] = NextCellValue(row, col);
                }
            }
            grid = nextGrid;
            nextGrid = new int[Rows, Cols]; //If this line is removed, the simulation does not run correctly. I have no idea why.
        }

        private int NextCellValue(int row, int col)
        {
            int n = NumberOfNeighbors(row, col);
            if (grid[row, col] == 0)
            {
                if (n != 3)
                    return 0; // No change, 
                else
                    return 1; // Cell birth
            }
            else
            {
                if (n <= 1)
                    return 0; // Die from loneliness
                else if (n >= 4)
                    return 0; // Die from overcrowding
                else
                    return 1; // No change
            }

        }

        private int NumberOfNeighbors(int row, int col)
        {
            bool cellsToNorth = (row == 0 ? false : true);
            bool cellsToSouth = (row == (Rows - 1) ? false : true);
            bool cellsToWest = (col == 0 ? false : true);
            bool cellsToEast = (col == (Cols - 1) ? false : true);
            int numberOfNeighbors = 0;

            if (cellsToNorth && cellsToWest && grid[row - 1, col - 1] == 1) { numberOfNeighbors++; }
            if (cellsToNorth &&                grid[row - 1, col    ] == 1) { numberOfNeighbors++; }
            if (cellsToNorth && cellsToEast && grid[row - 1, col + 1] == 1) { numberOfNeighbors++; }
            if (                cellsToEast && grid[row    , col + 1] == 1) { numberOfNeighbors++; }
            if (cellsToSouth && cellsToEast && grid[row + 1, col + 1] == 1) { numberOfNeighbors++; }
            if (cellsToSouth &&                grid[row + 1, col    ] == 1) { numberOfNeighbors++; }
            if (cellsToSouth && cellsToWest && grid[row + 1, col - 1] == 1) { numberOfNeighbors++; }
            if (                cellsToWest && grid[row    , col - 1] == 1) { numberOfNeighbors++; }

            return numberOfNeighbors;
        }

    }
}
