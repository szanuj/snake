using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Model.Utilities;

namespace SnakeGame.Model
{
    /// <summary>
    /// Reprezentuje interfejs użytkownika widziany w grze.
    /// </summary>
    class UserInterface
    {
        /// <summary>
        /// Pozycja interfejsu użytkownika.
        /// </summary>
        public Vector2Int Position { get; }
        /// <summary>
        /// Wielkość interfejsu użytkownika.
        /// </summary>
        public Vector2Int Size { get; }
        /// <summary>
        /// Etykieta obecnego wyniku.
        /// </summary>
        public string ScoreLabel { get; set; } = "Punkty: ";
        /// <summary>
        /// Etykieta najwyższego wyniku.
        /// </summary>
        public string HighScoreLabel { get; set; } = "Rekord: ";
        /// <summary>
        /// Obecny wynik.
        /// </summary>
        private int score;
        /// <summary>
        /// Obecny wynik.
        /// </summary>
        public int Score
        {
            get { return score; }
            set
            {
                // Jeśli obecny wynik przekracza najwyższy, najwyższy wynik ustawiany jest na obecny.
                score = value;
                if (score > HighScore)
                    HighScore = score;
            }
        }
        /// <summary>
        /// Najwyższy wynik.
        /// </summary>
        public int HighScore { get; set; }
        /// <summary>
        /// Inicjalizuje właściwości.
        /// </summary>
        /// <param name="position">Pozycja interfejsu użytkownika w oknie.</param>
        /// <param name="size">Wielkość interfejsu użytkownika.</param>
        /// <param name="highScore">Najwyższy wynik.</param>
        /// <param name="score">Obecny wynik.</param>
        public UserInterface(Vector2Int position, Vector2Int size, int highScore, int score)
        {
            Position = position;
            Size = size;
            HighScore = highScore;
            Score = score;
        }
        /// <summary>
        /// Aktualizuje wyniki.
        /// </summary>
        /// <param name="score">Obecny wynik.</param>
        public void UpdateScores(int score) { Score = score; }
    }
}
