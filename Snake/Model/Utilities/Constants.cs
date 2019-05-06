using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Model.GameObjects;

namespace SnakeGame.Model.Utilities
{
    /// <summary>
    /// Przechowuje bezpieczne stałe pozwalające uruchomić program w przypadku, gdy brakuje dynamicznych wartości.
    /// </summary>
    static class Constants
    {
        public static readonly string iniPath = "Game.ini";
        public static readonly string highScorePath = "HighScore.txt";
        public static readonly int WindowSizeX = 160;
        public static readonly int WindowSizeY = 40;
        public static readonly Vector2Int UserInterfacePosition = new Vector2Int(0, 0);
        public static readonly int UserInterfaceSizeY = 2;
        public static readonly Vector2Int LevelPosition = new Vector2Int(0, UserInterfacePosition.Y + UserInterfaceSizeY);
        public static readonly Vector2Int LevelSize = new Vector2Int(WindowSizeX, WindowSizeY - UserInterfaceSizeY);
        public static readonly Vector2Int SnakeHeadPosition = new Vector2Int(LevelSize.X / 2, LevelSize.Y / 2);
        public static readonly int SnakeLength = 10;
        public static readonly int SnakeSpeed = 30;
        public static readonly Directions SnakeDirection = Directions.Right;
        public static readonly int Score = 0;
        public static readonly int HighScore = 0;
    }
}
