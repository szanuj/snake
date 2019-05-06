using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Model.Utilities;

namespace SnakeGame.Model.GameObjects
{
    /// <summary>
    /// Reprezentuje węża.
    /// </summary>
    class Snake : GameObject
    {
        /// <summary>
        /// Zwrot węża wyrażony wektorem jednostkowym.
        /// </summary>
        private Vector2Int directionVector;
        /// <summary>
        /// Zwrot węża wyrażony polem typu wyliczeniowego Directions.
        /// </summary>
        private Directions direction;
        /// <summary>
        /// Zwrot, w jakim obecnie porusza się wąż.
        /// </summary>
        public Directions Direction
        {
            get { return direction; }
            set { direction = value; directionVector = ConvertDirection.ToVector2Int(value); }
        }
        /// <summary>
        /// Obecna długość węża.
        /// </summary>
        public int Length { get { return Points.Count; } }
        /// <summary>
        /// Szybkość węża wyrażona w liczbie ruchów na sekundę. Maksymalna wartość jest równa liczbie klatek na sekundę.
        /// </summary>
        public double Speed { get; set; }
        /// <summary>
        /// Pomaga w kontroli szybkości węża.
        /// </summary>
        public Clock speedTimer;
        /// <summary>
        /// Przechowuje zaktualizowany, ale niekoniecznie obecny zwrot.
        /// </summary>
        public Directions UpdatedDirection { get; set; }
        /// <summary>
        /// Określa, czy należy podczas następnego ruchu zaktualizować zwrot.
        /// </summary>
        public bool DirectionUpdateWaiting { get; set; } = false;
        /// <summary>
        /// Aktualizuje zwrot, jeśli to potrzebne.
        /// </summary>
        public void UpdateDirection()
        {
            if (DirectionUpdateWaiting)
            {
                Direction = UpdatedDirection;
                DirectionUpdateWaiting = false;
            }
        }
        /// <summary>
        /// Inicjalizuje instancję węża oraz ustawia początkowe wartości pól i właściwości.
        /// </summary>
        /// <param name="headPosition">Początkowe koordynaty X i Y głowy węża.</param>
        /// <param name="length">Początkowa ilość punktów składających się na ciało węża.</param>
        /// <param name="speed">Początkowa szybkość węża wyrażona liczbą ruchów na sekundę.</param>
        /// <param name="direction">Początkowy zwrot węża.</param>
        public Snake(Vector2Int headPosition, int length, double speed, Directions direction) : base(PointTypes.Snake)
        {
            Points.Add(headPosition);
            Direction = direction;
            Speed = speed;
            speedTimer = new Clock();
            // Generowanie ciała węża.
            for (int i = 1; i < length; i++) { Points.Add(Points[i - 1] - directionVector); }
        }
        /// <summary>
        /// Przesuwa węża w aktualnym kierunku.
        /// </summary>
        /// <param name="grow">Określa, czy wąż ma urosnąć o 1 punkt.</param>
        public void Move(bool grow = false)
        {
            if (Length == 1)
                Points[0] += directionVector;
            if (Length > 1)
            {
                int lastIndex = Length - 1;
                if (grow)
                    Points.Add(Points[lastIndex]);
                // Zaczynając od końca ogona, a kończąc przed głową każdy kolejny punkt węża zmieniany jest na następny. Głowa przesuwana jest o wektor aktualnego zwrotu.
                for (int i = lastIndex; i > 0; i--) { Points[i] = Points[i - 1]; }
                Points[0] += directionVector;
            }
        }
    }
}
