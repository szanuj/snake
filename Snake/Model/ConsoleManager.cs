using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Model.Utilities;

namespace SnakeGame.Model
{
    /// <summary>
    /// Obsługuje wyświetlanie w konsoli.
    /// </summary>
    static class ConsoleManager
    {
        /// <summary>
        /// Wielkość okna gry.
        /// </summary>
        public static Vector2Int WindowSize { get; set; }
        /// <summary>
        /// Przygotowuje okno konsoli do wyświetlania gry.
        /// </summary>
        /// <param name="size">Preferowana wielkość okna.</param>
        public static void PrepareConsole(Vector2Int size)
        {
            WindowSize = size;
            Console.SetWindowSize(size.X, size.Y);
            Console.SetBufferSize(size.X, size.Y);
            Console.CursorVisible = false;
        }
        /// <summary>
        /// Wyświetla menu główne z aktualnym rekordem.
        /// </summary>
        /// <param name="highScore"></param>
        public static void DisplayMenu(int highScore)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(WindowSize.X / 2, WindowSize.Y / 2);
            Console.Write("S N A K E\n");
            Console.SetCursorPosition(WindowSize.X / 2, WindowSize.Y / 2 + 2);
            Console.Write("Obecny rekord: {0}.", highScore);
            Console.SetCursorPosition(WindowSize.X / 2, WindowSize.Y / 2 + 3);
            Console.Write("Sterowanie: zmieniaj kierunek klawiszami strzałek.");
            Console.SetCursorPosition(WindowSize.X / 2, WindowSize.Y / 2 + 5);
            Console.Write("Wciśnij odpowiedni klawisz, aby wybrać opcję.");
            Console.SetCursorPosition(WindowSize.X / 2, WindowSize.Y / 2 + 6);
            Console.Write("1. Rozpocznij grę.");
            Console.SetCursorPosition(WindowSize.X / 2, WindowSize.Y / 2 + 7);
            Console.Write("2. Zamknij program.");
            Console.SetCursorPosition(WindowSize.X / 2, WindowSize.Y / 2 + 9);
            Console.ResetColor();
        }
        /// <summary>
        /// Wyświetla komunikat oznaczający koniec gry.
        /// </summary>
        /// <param name="score">Obecny wynik.</param>
        /// <param name="highScore">Najwyższy wynik.</param>
        public static void DisplayGameOverScreen(int score, int highScore)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(WindowSize.X / 2, WindowSize.Y / 2);
            Console.Write("Koniec gry. Twój wynik: {0}. Najwyższy wynik: {1}.", score, highScore);
            Console.SetCursorPosition(WindowSize.X / 2, WindowSize.Y / 2 + 1);
            Console.Write("Wciśnij Enter, aby wrócić do menu.");
            Console.ResetColor();
        }
        public static void ResetConsole()
        {
            Console.ResetColor();
            Console.Clear();
        }
        /// <summary>
        /// Rysuje podane elementy w oknie konsoli.
        /// </summary>
        /// <param name="userInterface">Interfejs użytkownika do narysowania.</param>
        /// <param name="level">Level do narysowania.</param>
        public static void DrawGameView(UserInterface userInterface, Level level)
        {
            Console.Clear();
            // Rysowanie interfejsu użytkownika.
            DrawUI(userInterface);
            // Rysowanie levelu.
            DrawLevel(level);
        }
        /// <summary>
        /// Rysuje podany interfejs użytkownika.
        /// </summary>
        /// <param name="userInterface">Interfejs użytkownika do narysowania.</param>
        public static void DrawUI(UserInterface userInterface)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(userInterface.Position.X, userInterface.Position.Y);
            Console.Write(userInterface.ScoreLabel + userInterface.Score);
            Console.SetCursorPosition(userInterface.Size.X / 2, userInterface.Position.Y);
            Console.Write(userInterface.HighScoreLabel + userInterface.HighScore);
            Console.SetCursorPosition(userInterface.Position.X, userInterface.Position.Y + userInterface.Size.Y - 1);
            string line = "";
            for (int i = 0; i < userInterface.Size.X; i++)
                line += '\u2588';
            Console.Write(line);
            Console.ResetColor();
        }
        /// <summary>
        /// Rysuje podany level.
        /// </summary>
        /// <param name="level">Level do narysowania.</param>
        public static void DrawLevel(Level level)
        {
            Console.SetCursorPosition(level.Position.X, level.Position.Y);
            Vector2Int currentPoint = new Vector2Int();
            for (int i = 0; i < level.Size.Y; i++)
            {
                for (int j = 0; j < level.Size.X; j++)
                {
                    currentPoint.X = j;
                    currentPoint.Y = i;
                    // Odczytanie wartości w danym punkcie levelu i napisanie znaku o właściwym dla typu obiektu gry kolorze.
                    switch (level.GetPointType(currentPoint))
                    {
                        case PointTypes.Empty:
                            break;
                        case PointTypes.Bonus:
                            currentPoint += level.Position;
                            Console.SetCursorPosition(currentPoint.X, currentPoint.Y);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("\u2588");
                            Console.ResetColor();
                            break;
                        case PointTypes.Snake:
                            currentPoint += level.Position;
                            Console.SetCursorPosition(currentPoint.X, currentPoint.Y);
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write("\u2588");
                            Console.ResetColor();
                            break;
                    }
                }
            }
        }
    }
}
