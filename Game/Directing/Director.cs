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
        private int points = 0;
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
            Actor player = cast.GetFirstActor("player");
            List<Actor> minerals = cast.GetActors("minerals");

            banner.SetText($"{points}");
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            player.MoveNext(maxX, maxY);

            foreach (Actor actor in minerals)
            {
                if (player.GetPosition().Equals(actor.GetPosition()))
                {
                    Mineral mineral = (Mineral) actor;
                    int minScore = mineral.GetScore();
                    this.points += minScore;
                    banner.SetText($"{this.points}");
                }
                actor.MoveNext(maxX, maxY);
            }
            Random random = new Random();
            for (int i = 0; i < random.Next(1,5); i++)
            {Spawn(cast);}

            
        }

        private void Spawn(Cast cast) {
            int COLS = 60;
            int CELL_SIZE = 15;
            Random random = new Random();
            
            int x = random.Next(1, COLS);
            int y = 0;
            Point position = new Point(x, y);
            position = position.Scale(CELL_SIZE);

            int r = random.Next(0, 256);
            int g = random.Next(0, 256);
            int b = random.Next(0, 256);
            Color color = new Color(r, g, b);

            Mineral mineral = new Mineral();
            mineral.SetColor(color);
            mineral.SetPosition(position);
            Point speed = new Point(0,10);
            mineral.SetVelocity(speed);
            //mineral.SetMessage(message);
            cast.AddActor("minerals", mineral);

            int points = random.Next(0,2);
            if (points == 0)
            {
            points = -100;
            mineral.SetText("o");}
            else
            {
            points = 100;
            mineral.SetText("*");}

            mineral.SetScore(points);

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