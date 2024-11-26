using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Final_Project___PVZ_Remake
{
    // Christian Moyes
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        int sun;

        MouseState mouseState, prevMouseState;
        KeyboardState keyboardState;

        //Title Screen Stuff
        Texture2D titleScreenTexture;
        Rectangle window;

        //Game Stuff (Always visible)
        Texture2D frontYardTexture;
        Texture2D plantRosterTexture;
        Texture2D shovelIconTexture;
        Texture2D mowerTexture;
        Texture2D sunflowerSeedTexture;
        Texture2D peashooterSeedTexture;
        Texture2D wallnutSeedTexture;
        Texture2D potatoMineSeedTexture;
        Texture2D cherryBombSeedTexture;
        Texture2D snowPeaSeedTexture;
        Texture2D repeaterSeedTexture;

        Rectangle plantRosterRect;

        Vector2 sunBankLocation;

        //Game Stuff (Sometimes Visible)
        Texture2D gridHighlightTexture;




        //Sounds and Fonts
        SoundEffect introTheme;
        SoundEffectInstance introThemeInstance;

        SpriteFont titleFont;
        SpriteFont sunFont;

        // Class Objects
        List<PlantGrid> grid;
        List<Mower> mowers;
        List<SeedPacket> seeds;


        ShovelIcon shovelIcon;
        SeedPacket sunflowerSeed;
        SeedPacket peashooterSeed;
        SeedPacket wallnutSeed;
        SeedPacket potatoMineSeed;
        SeedPacket cherryBombSeed;
        SeedPacket snowPeaSeed;
        SeedPacket repeaterSeed;



        Screen screen;

        enum Screen
        {
            Title,
            Intro,
            Game,
            Between,
            GameOver,
            Thanks
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 520;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            screen = Screen.Title;
            window = new Rectangle(0, 0, 800, 520);
            sun = 50;

            plantRosterRect = new Rectangle(200, 2, 450, 70);

            // Initialize Class Lists
            grid = new List<PlantGrid>();
            mowers = new List<Mower>();
            seeds = new List<SeedPacket>();

            //Initialize Class object Rectangles


            base.Initialize();

            // Make Lists in order of appearance above

            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    grid.Add(new PlantGrid(gridHighlightTexture, new Rectangle(202 + (x * 64), 105 + (y * 83), 50, 50)));
                }
            }

            for (int i = 0; i < 5; i++)
            {
                mowers.Add(new Mower(mowerTexture, new Rectangle(150, 105 + (i * 83), 40, 40)));
            }

            seeds.Add(sunflowerSeed = new SeedPacket(sunflowerSeedTexture, 0, 50, 7.5f, new Rectangle(260, 10, 35, 50)));
            seeds.Add(peashooterSeed = new SeedPacket(peashooterSeedTexture, 1, 100, 7.5f, new Rectangle(298, 10, 35, 50)));
            seeds.Add(wallnutSeed = new SeedPacket(wallnutSeedTexture, 2, 50, 30f, new Rectangle(336, 10, 35, 50)));
            seeds.Add(potatoMineSeed = new SeedPacket(potatoMineSeedTexture, 3, 25, 30f, new Rectangle(374, 10, 35, 50)));
            seeds.Add(cherryBombSeed = new SeedPacket(cherryBombSeedTexture, 4, 150, 50f, new Rectangle(412, 10, 35, 50)));
            seeds.Add(snowPeaSeed = new SeedPacket(snowPeaSeedTexture, 5, 175, 7.5f, new Rectangle(450, 10, 35, 50)));
            seeds.Add(repeaterSeed = new SeedPacket(repeaterSeedTexture, 6, 200, 7.5f, new Rectangle(488, 10, 35, 50)));




            // Make Other Class Objects here, in order of appearance

            shovelIcon = new ShovelIcon(shovelIconTexture, new Rectangle (651, 2, 70, 70));





        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //TitleScreen
            titleScreenTexture = Content.Load<Texture2D>("Images/TitleScreen");
            titleFont = Content.Load<SpriteFont>("Fonts/TitleFont2");

            //Game
            //Base Things That Will Always Be There
            frontYardTexture = Content.Load<Texture2D>("Images/FrontYard");
            plantRosterTexture = Content.Load<Texture2D>("Images/PlantRoster");
            shovelIconTexture = Content.Load<Texture2D>("Images/shovelIcon");
            mowerTexture = Content.Load<Texture2D>("Images/LawnMower");
            sunflowerSeedTexture = Content.Load<Texture2D>("Images/SunflowerSeedPacket");
            peashooterSeedTexture = Content.Load<Texture2D>("Images/PeaShooterSeedPacket");
            wallnutSeedTexture = Content.Load<Texture2D>("Images/WallnutSeedPacket");
            potatoMineSeedTexture = Content.Load<Texture2D>("Images/PotatoMineSeedPacket");
            cherryBombSeedTexture = Content.Load<Texture2D>("Images/CherryBombSeedPacket");
            snowPeaSeedTexture = Content.Load<Texture2D>("Images/SnowPeaSeedPacket");
            repeaterSeedTexture = Content.Load<Texture2D>("Images/RepeaterSeedPacket");
            sunFont = Content.Load<SpriteFont>("Fonts/SunFontSP");


            //Things That Will Only Be On Screen Somethimes
            gridHighlightTexture = Content.Load<Texture2D>("Images/rectangle");

            //SoundEffects
            introTheme = Content.Load<SoundEffect>("Sounds/IntroTheme");
            introThemeInstance = introTheme.CreateInstance();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();

            if (screen == Screen.Title)
            {
                if (introThemeInstance.State == SoundState.Stopped)
                {
                    introThemeInstance.Play();
                }

                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && window.Contains(mouseState.Position))
                {
                    screen = Screen.Intro;
                }
            }
            else if (screen == Screen.Intro)
            {
                for (int i = 0; i < grid.Count; i++)
                {
                    grid[i].Update(mouseState);
                }
            }


            if (sun == 0)
            {
                sunBankLocation = new Vector2(224, 53);
            }
            else if (sun > 0 && sun < 100)
            {
                sunBankLocation = new Vector2(221, 53);
            }
            else if (sun >= 100 && sun < 200)
            {
                sunBankLocation = new Vector2(218, 53);
            }
            else if (sun >= 200 && sun < 1000)
            {
                sunBankLocation = new Vector2(216, 53);
            }
            else if (sun >= 1000 && sun < 2000)
            {
                sunBankLocation = new Vector2(213, 53);
            }
            else
            {
                sunBankLocation = new Vector2(210, 53);
            }



            prevMouseState = Mouse.GetState();
            base.Update(gameTime);
        }


        public void UpdateLevel(GameTime gameTime, MouseState mouseState)
        {

        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            if (screen == Screen.Title)
            {
                _spriteBatch.Draw(titleScreenTexture, window, Color.White);
                _spriteBatch.DrawString(titleFont, "A Jank Remake", new Vector2(100, 100), Color.Black);
            }
            else if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(frontYardTexture, window, Color.White);
                _spriteBatch.Draw(plantRosterTexture, plantRosterRect, Color.White);
                _spriteBatch.DrawString(sunFont, $"{sun}", sunBankLocation, Color.Black);

                foreach (PlantGrid square in grid)
                {
                    square.Draw(_spriteBatch);
                }

                foreach (Mower mower in mowers)
                {
                    mower.Draw(_spriteBatch);
                }
                
                foreach (SeedPacket seed in seeds)
                {
                    seed.Draw(_spriteBatch);
                }

                shovelIcon.Draw(_spriteBatch);
            }



            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
