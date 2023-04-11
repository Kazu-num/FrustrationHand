using System;
using System.Collections.Generic;

namespace Maze
{
    /// <summary>
    /// modified  code from https://algoful.com/Archive/Algorithm/MazeDig
    /// </summary>
    public class MazeCreateor_Dig
    {
        public int[,] Maze { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        private List<Cell> StartCells;

        public MazeCreateor_Dig(int width, int height)
        {
            // 5ñ¢ñûÇÃÉTÉCÉYÇ‚ãÙêîÇ≈ÇÕê∂ê¨Ç≈Ç´Ç»Ç¢
            if (width < 5 || height < 5) throw new ArgumentOutOfRangeException();
            if (width % 2 == 0) width++;
            if (height % 2 == 0) height++;

            // ñ¿òHèÓïÒÇèâä˙âª
            this.Width = width;
            this.Height = height;
            Maze = new int[width, height];
            StartCells = new List<Cell>();
        }

        public int[,] CreateMaze()
        {
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    if (x == 0 || y == 0 || x == this.Width - 1 || y == this.Height - 1)
                    {
                        Maze[x, y] = Const.Path;
                    }
                    else
                    {
                        Maze[x, y] = Const.Wall;
                    }
                }
            }

            Dig(1, 1);

            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    if (x == 0 || y == 0 || x == this.Width - 1 || y == this.Height - 1)
                    {
                        Maze[x, y] = Const.Wall;
                    }
                }
            }
            return Maze;
        }

        private void Dig(int x, int y)
        {
            Random rnd = new Random();
            while (true)
            {
                var directions = new List<Direction>();
                if (this.Maze[x, y - 1] == Const.Wall && this.Maze[x, y - 2] == Const.Wall)
                    directions.Add(Direction.Up);
                if (this.Maze[x + 1, y] == Const.Wall && this.Maze[x + 2, y] == Const.Wall)
                    directions.Add(Direction.Right);
                if (this.Maze[x, y + 1] == Const.Wall && this.Maze[x, y + 2] == Const.Wall)
                    directions.Add(Direction.Down);
                if (this.Maze[x - 1, y] == Const.Wall && this.Maze[x - 2, y] == Const.Wall)
                    directions.Add(Direction.Left);

                if (directions.Count == 0) break;

                SetPath(x, y);
                var dirIndex = rnd.Next(directions.Count);
                switch (directions[dirIndex])
                {
                    case Direction.Up:
                        SetPath(x, --y);
                        SetPath(x, --y);
                        break;
                    case Direction.Right:
                        SetPath(++x, y);
                        SetPath(++x, y);
                        break;
                    case Direction.Down:
                        SetPath(x, ++y);
                        SetPath(x, ++y);
                        break;
                    case Direction.Left:
                        SetPath(--x, y);
                        SetPath(--x, y);
                        break;
                }
            }

            var cell = GetStartCell();
            if (cell != null)
            {
                Dig(cell.X, cell.Y);
            }
        }

        private void SetPath(int x, int y)
        {
            this.Maze[x, y] = Const.Path;
            if (x % 2 == 1 && y % 2 == 1)
            {
                StartCells.Add(new Cell() { X = x, Y = y });
            }
        }

        private Cell GetStartCell()
        {
            if (StartCells.Count == 0) return null;

            var rnd = new Random();
            var index = rnd.Next(StartCells.Count);
            var cell = StartCells[index];
            StartCells.RemoveAt(index);

            return cell;
        }
    }
}
