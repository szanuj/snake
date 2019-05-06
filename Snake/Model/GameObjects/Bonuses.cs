using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Model.Utilities;

namespace SnakeGame.Model.GameObjects
{
    /// <summary>
    /// Reprezentuje bonusy, jakie może zebrać wąż.
    /// </summary>
    class Bonuses : GameObject
    {
        /// <summary>
        /// Inicjalizuje właściwości obiektu gry.
        /// </summary>
        public Bonuses() : base(PointTypes.Bonus) { }
        /// <summary>
        /// Dodaje w podanym punkcie bonus do zebrania.
        /// </summary>
        /// <param name="position">Pozycja punktu, w którym ma się znaleźć bonus.</param>
        public void SpawnBonus(Vector2Int position)
        {
            Points.Add(position);
        }
        /// <summary>
        /// Usuwa bonus z podanej pozycji, o ile punkt należy do listy punktów obiektu gry.
        /// </summary>
        /// <param name="position">Pozycja, z której należy usunąć bonus.</param>
        public void DeleteBonus(Vector2Int position)
        {
            if (Points.Contains(position))
                Points.Remove(position);
            else
                throw new ArgumentOutOfRangeException("position", "Podana pozycja nie znajduje się na liście punktów.");
        }
    }
}
