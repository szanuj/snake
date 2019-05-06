using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Model.Utilities;
using SnakeGame.Model.GameObjects;

namespace SnakeGame.Model
{
    /// <summary>
    /// Reprezentuje level - poziom w grze. Przechowuje informacje o położeniu i typie obiektów gry.
    /// </summary>
    class Level
    {
        /// <summary>
        /// Pole służące do generowania liczb pseudolosowych.
        /// </summary>
        private Random random;
        /// <summary>
        /// Przechowuje informacje o położeniu i typie obiektów gry.
        /// </summary>
        private int[,] level;
        /// <summary>
        /// Pozycja levelu w konsoli.
        /// </summary>
        public Vector2Int Position { get; }
        /// <summary>
        /// Rozmiar levelu w konsoli i jednocześnie wymiary levelu.
        /// </summary>
        public Vector2Int Size { get; }

        /// <summary>
        /// Inicjalizuje instancję levelu, przypisuje mu szerokość i wysokość oraz wypełnia wartościami początkowymi.
        /// </summary>
        /// <param name="width">Szerokość levelu w punktach.</param>
        /// <param name="height">Wysokość levelu w punktach.</param>
        public Level(Vector2Int position, Vector2Int size)
        {
            random = new Random();
            Position = position;
            Size = size;
            level = new int[Size.X, Size.Y];
            Clear();
        }
        /// <summary>
        /// Czyści level.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < Size.X; i++)
                for (int j = 0; j < Size.Y; j++)
                    level[i, j] = 0;
        }
        /// <summary>
        /// Czyści level i zapisuje obecne położenie obiektów gry.
        /// </summary>
        /// <param name="gameObjects">Wyliczenie obiektów gry do zapisania w levelu.</param>
        public void UpdateLevel(IEnumerable<GameObject> gameObjects)
        {
            Clear();
            foreach (GameObject gameObject in gameObjects)
            {
                foreach (Vector2Int point in gameObject.Points)
                {
                    if (point.X < Size.X && point.Y < Size.Y && point.X >= 0 && point.Y >= 0)
                        level[point.X, point.Y] = (int)gameObject.PointType;
                    else
                        throw new ArgumentOutOfRangeException("point", "Co najmniej jeden z punktów należących do obiektów gry nie należy do levelu. (Prawdopodobnie za duża początkowa długość węża?)");
                }
            }
        }
        /// <summary>
        /// Zwraca typ punktu określonego w parametrze.
        /// </summary>
        /// <param name="point">Punkt do sprawdzenia.</param>
        public PointTypes GetPointType(Vector2Int point)
        {
            if (point.X < Size.X && point.Y < Size.Y && point.X >= 0 && point.Y >= 0)
            {
                if (Enum.IsDefined(typeof(PointTypes), level[point.X, point.Y]))
                    return (PointTypes)level[point.X, point.Y];
                else
                    throw new Exception("Co najmniej jeden z punktów w levelu nie reprezentuje żadnego typu obiektu gry.");
            }
            else
                throw new ArgumentOutOfRangeException("point", "Podano punkt, który nie należy do levelu.");
        }
        /// <summary>
        /// Zwraca losowy pusty punkt levelu.
        /// </summary>
        public Vector2Int PickAnEmptyPoint()
        {
            Vector2Int point = new Vector2Int();
            bool done = false;
            while (!done)
            {
                point.X = random.Next(0, Size.X);
                point.Y = random.Next(0, Size.Y);
                if (GetPointType(point) == PointTypes.Empty)
                    done = true;
            }
            return point;
        }
        /// <summary>
        /// Zwraca pierwszy obiekt gry z listy, który zawiera podany punkt. Zwraca wartość null jeśli żaden obiekt gry z listy nie zawiera takiego punktu.
        /// </summary>
        /// <param name="point">Punkt do sprawdzenia.</param>
        /// <param name="gameObjects">Lista obiektów gry do sprawdzenia.</param>
        /// <returns>Pierwszy obiekt gry z listy, który zawiera podany punkt. Wartość null jeśli żaden obiekt gry z listy nie zawiera takiego punktu.</returns>
        public GameObject WhatObjectIsHere(Vector2Int point, IEnumerable<GameObject> gameObjects)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.Points.Contains(point))
                    return gameObject;
            }
            return null;
        }
    }
}
