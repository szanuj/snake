using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model.Utilities
{
    /// <summary>
    /// Reprezentuje wektor w przestrzeni dwuwymiarowej liczb całkowitych.
    /// </summary>
    struct Vector2Int
    {
        /// <summary>
        /// Współrzędna X wektora.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Współrzędna Y wektora.
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Inicjalizuje właściwości wyznaczające wektor.
        /// </summary>
        /// <param name="X">Współrzędna X wektora.</param>
        /// <param name="Y">Współrzędna Y wektora.</param>
        public Vector2Int(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        /// <summary>
        /// Sprawdza równość dwóch wektorów.
        /// </summary>
        /// <param name="a">Pierwszy wektor.</param>
        /// <param name="b">Drugi wektor.</param>
        /// <returns>Wartość logiczna reprezentująca równość podanych wektorów.</returns>
        public static bool operator ==(Vector2Int a, Vector2Int b)
        {
            if (a.X == b.X && a.Y == b.Y)
                return true;
            return false;
        }
        /// <summary>
        /// Sprawdza nierówność dwóch wektorów.
        /// </summary>
        /// <param name="a">Pierwszy wektor.</param>
        /// <param name="b">Drugi wektor.</param>
        /// <returns>Wartość logiczna reprezentująca nierówność podanych wektorów.</returns>
        public static bool operator !=(Vector2Int a, Vector2Int b)
        {
            if (a.X == b.X && a.Y == b.Y)
                return false;
            return true;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// Dodaje dwa wektory.
        /// </summary>
        /// <param name="a">Pierwszy wektor.</param>
        /// <param name="b">Drugi wektor.</param>
        /// <returns>Wektor będący sumą podanych wektorów.</returns>
        public static Vector2Int operator +(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.X + b.X, a.Y + b.Y);
        }
        /// <summary>
        /// Odejmuje dwa wektory.
        /// </summary>
        /// <param name="a">Wektor, od którego zostanie odjęty drugi wektor.</param>
        /// <param name="b">Wektor odejmowany.</param>
        /// <returns>Wektor będący różnicą podanych wektorów.</returns>
        public static Vector2Int operator -(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.X - b.X, a.Y - b.Y);
        }
    }
}
