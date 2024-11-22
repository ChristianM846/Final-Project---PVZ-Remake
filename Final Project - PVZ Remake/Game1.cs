﻿using Microsoft.Xna.Framework;
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

        Texture2D titleScreen;
        Texture2D frontYard;
        Texture2D plantRoster;
        Texture2D gridHighlight;

        Rectangle window;
        Rectangle plantRosterRect;

        Vector2 sunBankLocation;

        SoundEffect introTheme;
        SoundEffectInstance introThemeInstance;

        SpriteFont titleFont;
        SpriteFont sunFont;

        List<PlantGrid> grid;




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

            // Iniialize Class Lists
            grid = new List<PlantGrid>();

            base.Initialize();

            // Make Lists in order of appearance above

            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    grid.Add(new PlantGrid(gridHighlight, new Rectangle(202 + (x * 64), 105 + (y * 84), 50, 50)));
                }
            }


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //TitleScreen
            titleScreen = Content.Load<Texture2D>("Images/TitleScreen");
            titleFont = Content.Load<SpriteFont>("Fonts/TitleFont2");

            //Game
                //Base Things That Will Always Be There
            frontYard = Content.Load<Texture2D>("Images/FrontYard");
            plantRoster = Content.Load<Texture2D>("Images/PlantRoster");
            sunFont = Content.Load<SpriteFont>("Fonts/SunFontSP");
                //Things That Will Only Be On Screen Somethimes
            gridHighlight = Content.Load<Texture2D>("Images/rectangle");

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
                _spriteBatch.Draw(titleScreen, window, Color.White);
                _spriteBatch.DrawString(titleFont, "A Jank Remake", new Vector2(100, 100), Color.Black);
            }
            else if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(frontYard, window, Color.White);
                _spriteBatch.Draw(plantRoster, plantRosterRect, Color.White);
                _spriteBatch.DrawString(sunFont, $"{sun}", sunBankLocation, Color.Black);
                foreach (PlantGrid square in grid)
                {
                    square.Draw(_spriteBatch);
                }
            }



            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
