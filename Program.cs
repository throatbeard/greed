using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using greed.Game.Casting;
using greed.Game.Directing;
using greed.Game.Services;


namespace greed
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        private static int FRAME_RATE = 12;
        private static int MAX_X = 900;
        private static int MAX_Y = 600;
        private static int CELL_SIZE = 15;
        private static int FONT_SIZE = 15;
        private static int COLS = 60;
        private static int ROWS = 40;
        private static string CAPTION = "Greed";
        private static Color WHITE = new Color(255, 255, 255);
        private static int DEFAULT_MINERALS = 40;


        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {
            // create the cast
            Cast cast = new Cast();

            // create the banner
            Mineral banner = new Mineral();
            banner.SetScore(0);
            banner.SetText("");
            banner.SetFontSize(FONT_SIZE);
            banner.SetColor(WHITE);
            banner.SetPosition(new Point(CELL_SIZE, 0));
            cast.AddActor("banner", banner);

            // create the robot
            Actor player = new Actor();
            player.SetText("#");
            player.SetFontSize(FONT_SIZE);
            player.SetColor(WHITE);
            player.SetPosition(new Point(MAX_X / 2, 15));
            cast.AddActor("player", player);

            // create the minerals
            Random random = new Random();
            for (int i = 0; i < random.Next(3,5); i++)
            {
                int COLS = 60;
                int CELL_SIZE = 15;
            
                int x = random.Next(1, COLS);
                int y = 40;
                Point position = new Point(x, y);
                position = position.Scale(CELL_SIZE);

                int r = random.Next(0, 256);
                int g = random.Next(0, 256);
                int b = random.Next(0, 256);
                Color color = new Color(r, g, b);

                Mineral mineral = new Mineral();
                mineral.SetColor(color);
                mineral.SetPosition(position);
                //mineral.SetMessage(message);
                cast.AddActor("minerals", mineral);

                int score = random.Next(0,1);
                if (score == 0)
                {score = -100;
                mineral.SetText("o");}
                else
                {score = 100;
                mineral.SetText("*");}

                mineral.SetScore(score);
            }

            // start the game
            KeyboardService keyboardService = new KeyboardService(CELL_SIZE);
            VideoService videoService 
                = new VideoService(CAPTION, MAX_X, MAX_Y, CELL_SIZE, FRAME_RATE, false);
            Director director = new Director(keyboardService, videoService);
            director.StartGame(cast);

            // test comment
        }
    }
}