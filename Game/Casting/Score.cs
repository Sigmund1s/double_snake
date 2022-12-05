using System;


namespace Unit05.Game.Casting
{
    /// <summary>
    /// <para>A tasty item that snakes like to eat.</para>
    /// <para>
    /// The responsibility of Food is to select a random position and points that it's worth.
    /// </para>
    /// </summary>
    public class Score : Actor
    {
        private int points = 0;
        private int points2 = 0;

        /// <summary>
        /// Constructs a new instance of an Food.
        /// </summary>
        public Score()
        {
            AddPoints(0,0);
        }

        /// <summary>
        /// Adds the given points to the score.
        /// </summary>
        /// <param name="points">The points to add.</param>
        public void AddPoints(int points, int points2)
        {
            this.points += points;
            this.points2 += points2;
            SetText($"Score: {this.points}                                                                                                                                           Score2: {this.points2}");
        }
    }
}