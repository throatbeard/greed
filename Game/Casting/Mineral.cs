namespace greed.Game.Casting
{
        /// <summary>
        /// <para>An item that falls from the top of the screen.</para>
        /// <para>
        /// The responsibility of an Artifact is to add or subtract score when the player collides with it.
        /// </para>
        /// </summary>
    public class Mineral : Actor
    {
        /// <summary>
        /// Constructs a new instance of Meteor.
        /// </summary>
        private int score = 0;
        public Mineral()
        {
        }
       

        /// <summary>
        /// Sets the actor's score to the given value.
        /// </summary>
        /// <param name="score"> the given score.</param>

        public void SetScore(int score)
        {
            this.score = score;
        }

        public int GetScore()
        {
         return score;
        }

    }
}