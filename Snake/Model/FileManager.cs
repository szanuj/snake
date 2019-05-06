using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SnakeGame.Model.Utilities;

namespace SnakeGame.Model
{
    /// <summary>
    /// Obsługuje odczyt i zapis plików.
    /// </summary>
    static class FileManager
    {
        /// <summary>
        /// Odczytuje kolejne linie pliku docelowego i zapisuje je do tablicy typu string.
        /// </summary>
        /// <param name="path">Ścieżka pliku docelowego.</param>
        /// <returns>Tablica kolejnych linii znajdujących się w pliku docelowym.</returns>
        public static string[] LoadLines(string path)
        {
            string[] contents = null;
            try
            {
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine("WindowSizeX: {0}", Constants.WindowSizeX);
                        sw.WriteLine("WindowSizeY: {0}", Constants.WindowSizeY);
                        sw.WriteLine("SnakeHeadPositionX: {0}", Constants.SnakeHeadPosition.X);
                        sw.WriteLine("SnakeHeadPositionY: {0}", Constants.SnakeHeadPosition.Y);
                        sw.WriteLine("SnakeLength: {0}", Constants.SnakeLength);
                        sw.WriteLine("SnakeDirection: {0}", (int)Constants.SnakeDirection);
                        sw.WriteLine("SnakeSpeed: {0}", Constants.SnakeSpeed);
                    }
                }
                contents = File.ReadAllLines(path);
            }
            catch (Exception e) { Console.WriteLine("Nie można otworzyć pliku: {0}", e.Message); }
            return contents;
        }
        /// <summary>
        /// Jeśli istnieje docelowy plik, zwraca jego zawartość przekonwertowaną na liczbę całkowitą. W przeciwnym razie zwraca 0.
        /// </summary>
        /// <param name="path">Ścieżka pliku docelowego</param>
        /// <returns>Jeśli istnieje docelowy plik, zwraca jego zawartość przekonwertowaną na liczbę całkowitą. W przeciwnym razie zwraca 0</returns>
        public static int LoadHighScore(string path)
        {
            int highScore = 0;
            try
            {
                if (!File.Exists(path))
                    return highScore;
                highScore = Convert.ToInt32(File.ReadAllText(path));
            }
            catch (Exception e) { Console.WriteLine("Nie można otworzyć pliku: {0}", e.Message); }
            return highScore;
        }
        /// <summary>
        /// Nadpisuje plik docelowy z najwyższym wynikiem.
        /// </summary>
        /// <param name="path">Ścieżka pliku docelowego.</param>
        /// <param name="highScore">Najwyższy wynik do zapisania.</param>
        public static void SaveHighScore(string path, int highScore)
        {
            try
            {
                File.WriteAllText(path, highScore.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Nie można otworzyć pliku: {0}", e.Message);
            }
        }
    }
}
