using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Model.Utilities;

namespace SnakeGame.Model.GameObjects
{
    /// <summary>
    /// Reprezentuje obiekt gry mogący znaleźć się w levelu.
    /// </summary>
    class GameObject
    {
        /// <summary>
        /// Lista punktów, które reprezentują widzialną część obiektu gry.
        /// </summary>
        public List<Vector2Int> Points { get; protected set; }
        /// <summary>
        /// Właściwość typu wyliczeniowego określająca typ punktów, które reprezentują instancję obiektu gry w levelu.
        /// </summary>
        public PointTypes PointType { get; protected set; }
        /// <summary>
        /// Inicjalizuje właściwości obiektu gry.
        /// </summary>
        /// <param name="pointType">Typ punktów reprezentujący instancję obiektu gry w levelu.</param>
        /// <param name="points">Lista punktów, które reprezentują widzialną część obiektu gry.</param>
        public GameObject(PointTypes pointType = PointTypes.Empty, List<Vector2Int> points = null)
        {
            PointType = pointType;
            if (points == null)
                Points = new List<Vector2Int>();
            else
                Points = points;
        }
    }
}
