using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Maze
{
    public static class Const
    {
        public const int Path = 0;
        public const int Wall = 1;
    }

    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }
}