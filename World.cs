using System;
using static System.Console;
namespace AssistantBarb
{
    public class World
    {
        private string[,] Grid;
        private int Rows;
        private int Cols;

        public World(string[,] grid)
        {
            Grid = grid;
            Rows = Grid.GetLength(0);
            Cols = Grid.GetLength(1);
        }

        public void Draw()
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Cols; x++)
                {
                    string element = Grid[y, x];
                    SetCursorPosition(x, y);
                    if (element == "░")
                    {
                        ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else
                    {
                        ForegroundColor = ConsoleColor.White;
                    }
                    Write(element);
                }
            }
        }

        public string GetElementAt(int x, int y)
        {
            return Grid[y, x];
        }

        public bool IsPositionWalkable(int x, int y)
        {
            if (x < 0 || y < 0 || x>= Cols || y >= Rows)
            {
                return false;
            }

            return Grid[y, x] == " " || Grid[y, x] == "░" || Grid[y,x] == "█";
        }
    }
}
