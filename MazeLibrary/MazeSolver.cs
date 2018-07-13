using System;

namespace MazeLibrary
{
    public class MazeSolver
    {
        private int[,] maze;
        private int startX;
        private int startY;
        private int rows;
        private int colomns;
        private int exitX;
        private int exitY;
        private int numOfExits = 0; 

        public MazeSolver(int[,] mazeModel, int startX, int startY)
        {
            maze = mazeModel ?? throw new ArgumentNullException(nameof(mazeModel));
            rows = maze.GetLength(0);
            colomns = maze.GetLength(1);
            this.startX = startX;
            this.startY = startY;

            if (startY < 0 || startY >= rows)
            {
                throw new ArgumentException(nameof(startY));
            }

            if (startX < 0 || startX >= colomns)
            {
                throw new ArgumentException(nameof(startX));
            }

            if (startX != 0 && startX != colomns - 1)
            {
                if (startY != 0 && startY != rows - 1)
                {
                    throw new ArgumentException(nameof(startY));
                }
            }

            if (mazeModel[startX, startY] == -1) 
            {
                throw new ArgumentException("This coordinates dont't have a point o enter.");
            }
          
            maze[startX, startY] = 1;
            for (int j = 0; j < rows; j++)
            {
                if (maze[0, j] == 0) 
                {
                    exitX = 0;
                    exitY = j;
                    numOfExits++;
                }

                if (maze[colomns - 1, j] == 0)
                {
                    exitX = colomns - 1;
                    exitY = j;
                    numOfExits++;
                }
            }

            for (int i = 0; i < colomns; i++)
            {
                if (maze[i, 0] == 0)
                {
                    exitX = i;
                    exitY = 0;
                    numOfExits++;
                }

                if (maze[i, rows - 1] == 0)
                {
                    exitX = i;
                    exitY = rows - 1;
                    numOfExits++;
                }
            }

            if (numOfExits < 1) 
            {
                throw new ArgumentException("There is no exits in the maze. ", nameof(maze));
            }
        }

        public int[,] MazeWithPass()
        {
            return maze;
        }

        public void PassMaze()
        {
            int count = 1;
            PassMaze(count, maze, startX, startY);
        }

        private void PassMaze(int count, int[,] maze, int i, int j)
        {
            maze[i, j] = count++;

            if (i == exitX && j == exitY)
            {                
                return;
            }

            if (i != colomns - 1 && j != rows - 1)
            {
                if (maze[i + 1, j] == 0)
                {
                    PassMaze(count, maze, i + 1, j);
                    if (maze[exitX, exitY] != 0)
                    {
                        return;
                    }
                }

                if (maze[i, j + 1] == 0)
                {
                    PassMaze(count, maze, i, j + 1);
                    if (maze[exitX, exitY] != 0)
                    {
                        return;
                    }
                }
            }

            if (i != 0 && j != 0)
            {
                if (maze[i - 1, j] == 0)
                {
                    PassMaze(count, maze, i - 1, j);
                    if (maze[exitX, exitY] != 0)
                    {
                        return;
                    }
                }

                if (maze[i, j - 1] == 0)
                {
                    PassMaze(count, maze, i, j - 1);
                    if (maze[exitX, exitY] != 0)
                    {
                        return;
                    }
                }
            }

            if (maze[exitX, exitY] == 0)
            {
                maze[i, j] = 0;
                count--;
                return;
            }
        }
    }
}
