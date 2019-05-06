using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Model.GameObjects;

namespace SnakeGame.Model.Utilities
{
    /// <summary>
    /// Konwerter zmiennych typu string na typy używane przez grę.
    /// </summary>
    static class ConvertString
    {
        /// <summary>
        /// Konwertuje tablicę zmiennych typu string na obiekt typu Snake.
        /// </summary>
        /// <param name="initialValues">Tablica wartości początkowych.</param>
        /// <returns>Obiekt typu Snake.</returns>
        public static Snake ToSnake(string[] initialValues)
        {
            Vector2Int headPosition = new Vector2Int(0, 0);
            int length = 0;
            double speed = 0;
            Directions direction = 0;
            foreach (string text in initialValues)
            {
                if (text.StartsWith("Snake"))
                {
                    string property = text.Substring(0, text.LastIndexOf(':'));
                    int value = Convert.ToInt32(text.Substring(text.LastIndexOf(' ') + 1));
                    switch (property)
                    {
                        case "SnakeHeadPositionX":
                            headPosition.X = value;
                            break;
                        case "SnakeHeadPositionY":
                            headPosition.Y = value;
                            break;
                        case "SnakeLength":
                            length = value;
                            break;
                        case "SnakeDirection":
                            direction = (Directions)value;
                            break;
                        case "SnakeSpeed":
                            speed = value;
                            break;
                    }
                }
            }
            Snake snake = new Snake(headPosition, length, speed, direction);
            return snake;
        }
        /// <summary>
        /// Konwertuje tablicę zmiennych typu string na wektor rozmiaru okna.
        /// </summary>
        /// <param name="initialValues">Tablica wartości początkowych.</param>
        /// <returns>Wektor rozmiaru okna.</returns>
        public static Vector2Int ToWindowSize(string[] initialValues)
        {
            Vector2Int size = new Vector2Int(0, 0);
            foreach (string text in initialValues)
            {
                if (text.StartsWith("Window"))
                {
                    string property = text.Substring(0, text.LastIndexOf(':'));
                    int value = Convert.ToInt32(text.Substring(text.LastIndexOf(' ') + 1));
                    switch (property)
                    {
                        case "WindowSizeX":
                            size.X = value;
                            break;
                        case "WindowSizeY":
                            size.Y = value;
                            break;
                    }
                }
            }
            return size;
        }
    }
}
