using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model.Utilities
{
    /// <summary>
    /// Zegar wskazujący ile czasu upłynęło od rozpoczęcia mierzenia.
    /// </summary>
    class Clock
    {
        /// <summary>
        /// Przechowuje datę rozpoczęcia mierzenia upływu czasu.
        /// </summary>
        private DateTime timeStarted = DateTime.Now;
        /// <summary>
        /// Ilość czasu, jaki upłynął od rozpoczęcia mierzenia.
        /// </summary>
        public TimeSpan TimeElapsed { get { return DateTime.Now - timeStarted; } }
        /// <summary>
        /// Rozpoczyna mierzenie czasu.
        /// </summary>
        public Clock() { Reset(); }
        public void Reset() { timeStarted = DateTime.Now; }
        /// <summary>
        /// Zmniejsza czas, jaki upłynął, o podany okres.
        /// </summary>
        /// <param name="time"></param>
        public void SubtractTimeElapsed(TimeSpan time)
        {
            if (time <= TimeElapsed)
                timeStarted += time;
        }
    }
}
