using System.Collections.Generic;
using greed.Game.Casting;
using greed.Game.Services;


namespace greed.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private KeyboardService keyboardService = null;
        private VideoService videoService = null;

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            videoService.OpenWindow();
            while (videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the player.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor player = cast.GetFirstActor("player");
            Point velocity = keyboardService.GetDirection();
            player.SetVelocity(velocity);     
        }

        /// <summary>
        /// Updates the player's position and resolves any collisions with rocks or gems.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            Actor banner = cast.GetFirstActor("banner");
            Actor player = cast.GetFirstActor("robot");
            List<Actor> minerals = cast.GetActors("minerals");

            banner.SetText("");
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            player.MoveNext(maxX, maxY);

            // create the minerals
            Random random = new Random();
            for (int i = 0; i < eternally; i++)                         //eternally is just to show this loop just keeps going without end
            {
                string text = ((char)random.Next(33, 126)).ToString();  //rng
                string message = messages[i];                           //why 

                int x = random.Next(1, COLS);                           //keep this because minerals should spawn in random columns
                int y = 40;                                             //always spawns in the first row at the top
                Point position = new Point(x, y);
                position = position.Scale(CELL_SIZE);                   //cell size???

                int r = random.Next(0, 256);                            //probably don't need any of this
                int g = random.Next(0, 256);
                int b = random.Next(0, 256);
                Color color = new Color(r, g, b);

                Mineral mineral = new Mineral();                        //
                mineral.SetText(text);                                  //text? for minerals?
                mineral.SetFontSize(FONT_SIZE);                         //font size?? why
                mineral.SetColor(color);                                //color of falling minerals
                mineral.SetPosition(position);                          //starting positions of minerals
                mineral.SetMessage(message);                            //
                cast.AddActor("minerals", mineral);                     //
            }


            foreach (Actor actor in minerals)
            {
                if (player.GetPosition().Equals(actor.GetPosition()))
                {
                    Mineral mineral = (Mineral) actor;
                    string message = mineral.GetMessage();
                    banner.SetText(message);
                }
            }
        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            videoService.ClearBuffer();
            videoService.DrawActors(actors);
            videoService.FlushBuffer();
        }

    }
}