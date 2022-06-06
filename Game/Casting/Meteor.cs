namespace greed.Game.Casting
{
        /// <summary>
        /// <para>An item that falls from the top of the screen.</para>
        /// <para>
        /// The responsibility of an Artifact is to add or subtract score when the player collides with it.
        /// </para>
        /// </summary>
    public class Meteor : Actor
    {
        /// <summary>
        /// Constructs a new instance of Meteor.
        /// </summary>
        string message;
        public Meteor()
        {
        }
       

        
        /// <summary>
        /// Gets the artifact's message.
        /// </summary>
        /// <returns>The message as a string.</returns>
        public string GetMessage()
        {
            return message;
        }
        

        
        /// <summary>
        /// Sets the artifact's message to the given value.
        /// </summary>
        /// <param name="message">The given message.</param>
        public string SetMessage(string message)
        {
            this.message = message;
            return message;
        }
    
    }
}