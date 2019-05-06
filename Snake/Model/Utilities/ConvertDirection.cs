using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model.Utilities
{
    /// <summary>
    /// Konwerter zmiennych typu wyliczeniowego Directions na wektor zwrotu.
    /// </summary>
    static class ConvertDirection
    {
        /// <summary>
        /// Konwertuje zmienną typu wyliczeniowego Directions na jednostkowy wektor zwrotu.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static Vector2Int ToVector2Int(Directions direction)
        {
            Vector2Int conversionResult = new Vector2Int();
            switch (direction)
            {
                case Directions.Up:
                    conversionResult.X = 0;
                    conversionResult.Y = -1;
                    break;
                case Directions.Down:
                    conversionResult.X = 0;
                    conversionResult.Y = 1;
                    break;
                case Directions.Left:
                    conversionResult.X = -1;
                    conversionResult.Y = 0;
                    break;
                case Directions.Right:
                    conversionResult.X = 1;
                    conversionResult.Y = 0;
                    break;
            }
            return conversionResult;
        }
    }
}
