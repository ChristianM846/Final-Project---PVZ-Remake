﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Final_Project___PVZ_Remake
{
    // Christian Moyes
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Random generator = new Random();

        int sun;
        int song;
        float time;
        float levelTime;

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
        Texture2D sunTexture;

        Rectangle trashSpot;

        //plant textures

        Texture2D browncoatTexture;
        Texture2D coneheadTexture;
        Texture2D bucketheadTexture;
        Texture2D flagZombieTexture;

        Rectangle fallingSunRect;

        //Sounds and Fonts
        SoundEffect introTheme;
        SoundEffectInstance introThemeInstance;
        SoundEffect grasswalkTheme;
        SoundEffectInstance grasswalkThemeInstance;
        SoundEffect loonboonTheme;
        SoundEffectInstance loonboonThemeInstance;
        SoundEffect sunPickup;
        SoundEffectInstance sunPickupInstance;

        SpriteFont titleFont;
        SpriteFont introFont;
        SpriteFont sunFont;

        // Lists (Not class objects)
        List<int> level1;
        List<int> level2;
        List<int> level3;

        // Class Objects
        List<PlantGrid> grid;
        List<Mower> mowers;
        List<SeedPacket> seeds;
        List<Zombie> zombies;


        ShovelIcon shovelIcon;
        SeedPacket sunflowerSeed;
        SeedPacket peashooterSeed;
        SeedPacket wallnutSeed;
        SeedPacket potatoMineSeed;
        SeedPacket cherryBombSeed;
        SeedPacket snowPeaSeed;
        SeedPacket repeaterSeed;
        //shadows here
        FallingSun fallingSun;
        Zombie testZombie;



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
            generator = new Random();
            screen = Screen.Title;
            window = new Rectangle(0, 0, 800, 520);
            trashSpot = new Rectangle(100, 1000, 100, 100);
            sun = 50;
            time = 0;
            levelTime = 0;

            plantRosterRect = new Rectangle(200, 2, 450, 70);

            // Non-Class lists
            level1 = new List<int>() {1, 1, 1, 2, 2, 3, 3, 3, 4, 10, 5, 5, 5, 6, 6, 7, 7, 7, 8, 20 };
            level2 = new List<int>() {1, 1, 2, 2, 3, 3, 4, 4, 5, 12, 6, 6, 7, 7, 8, 8, 9, 9, 10, 24 };
            level3 = new List<int>() {1, 1, 2, 2, 3, 3, 4, 5, 6, 14, 7, 8, 8, 9, 10, 10, 11, 12, 12, 28 };

            // Initialize Class Lists
            grid = new List<PlantGrid>();
            mowers = new List<Mower>();
            seeds = new List<SeedPacket>();
            zombies = new List<Zombie>();

            //Initialize Class object Rectangles
            fallingSunRect = new Rectangle(300, -100, 40, 40);


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

            zombies.Add(testZombie = new Zombie(browncoatTexture, coneheadTexture, bucketheadTexture, flagZombieTexture, new Rectangle(600, 410, 50, 80), 4));


            // Make Other Class Objects here
            shovelIcon = new ShovelIcon(shovelIconTexture, new Rectangle(651, 2, 70, 70));
            fallingSun = new FallingSun(sunTexture, fallingSunRect);
            

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Title Screen
            titleScreenTexture = Content.Load<Texture2D>("Images/TitleScreen");
            titleFont = Content.Load<SpriteFont>("Fonts/TitleFont2");

            //Intro Screen
            introFont = Content.Load<SpriteFont>("Fonts/IntroFont");

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
            sunTexture = Content.Load<Texture2D>("Images/Sun");
            //plant textures

            browncoatTexture = Content.Load<Texture2D>("Images/Zombie");
            coneheadTexture = Content.Load<Texture2D>("Images/ConeHeadZombie");
            bucketheadTexture = Content.Load<Texture2D>("Images/BucketheadZombie");
            flagZombieTexture = Content.Load<Texture2D>("Images/FlagZombie");

            //SoundEffects
            introTheme = Content.Load<SoundEffect>("Sounds/IntroTheme");
            introThemeInstance = introTheme.CreateInstance();
            grasswalkTheme = Content.Load<SoundEffect>("Sounds/Grasswalk");
            grasswalkThemeInstance = grasswalkTheme.CreateInstance();
            loonboonTheme = Content.Load<SoundEffect>("Sounds/Loonboon");
            loonboonThemeInstance = loonboonTheme.CreateInstance();
            sunPickup = Content.Load<SoundEffect>("Sounds/SunPickup");
            sunPickupInstance = sunPickup.CreateInstance();
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
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    introThemeInstance.Stop();
                    screen = Screen.Game;
                    time = (float)gameTime.TotalGameTime.TotalSeconds;
                    fallingSun.TimeStamp = time;
                    song = generator.Next(1, 3);
                    fallingSun.TimeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                }
            }
            else if (screen == Screen.Game)
            {
                levelTime = (float)gameTime.TotalGameTime.TotalSeconds - time;
                if (levelTime > 3)
                {
                    if (grasswalkThemeInstance.State == SoundState.Stopped && loonboonThemeInstance.State == SoundState.Stopped)
                    {
                        if (song == 1)
                        {
                            grasswalkThemeInstance.Play();
                            song = 2;
                        }
                        else if (song == 2)
                        {
                            loonboonThemeInstance.Play();
                            song = 1;
                        }
                    }

                    for (int i = 0; i < grid.Count; i++)
                    {
                        grid[i].Update(mouseState);
                    }

                    if (fallingSun.FallingSunRect.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    {
                        sunPickup.Play();
                    }

                    fallingSun.Update(gameTime, mouseState);
                    sun += fallingSun.SunValue;

                    for (int m = 0; m < mowers.Count; m++)
                    {
                        mowers[m].Update(zombies);

                        if (mowers[m].MowerRect.Left == window.Right)
                        {
                            mowers[m].MowerSpeed = Vector2.Zero;
                            mowers[m].MowerRect = trashSpot;
                        }
                    }

                    for (int z = 0; z < zombies.Count; z++)
                    {
                        zombies[z].Update(gameTime, mowers);

                        if (zombies[z].Health <= 0)
                        {
                            zombies[z].ZombieRect = trashSpot;
                            zombies.RemoveAt(z);
                            z--;
                        }

                    }


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
                _spriteBatch.DrawString(titleFont, "Quick Tutorial", new Vector2(300, 250), Color.Black);
                _spriteBatch.DrawString(titleFont, "Press Enter to", new Vector2(310, 280), Color.Black);
                _spriteBatch.DrawString(titleFont, "continue", new Vector2(370, 310), Color.Black);
                _spriteBatch.DrawString(introFont, "Spend your sun to plant plants to defend your house", new Vector2(200, 90), Color.Black);
                _spriteBatch.DrawString(introFont, "Sunflowers produce sun, Wallnuts tank damage,", new Vector2(200, 110), Color.Black);
                _spriteBatch.DrawString(introFont, "and all the other deal damage", new Vector2(200, 130), Color.Black);
                _spriteBatch.DrawString(introFont, "Zombies will come from the right side of the lawn", new Vector2(250, 450), Color.Black);
                _spriteBatch.DrawString(introFont, "Defeat them!", new Vector2(410, 470), Color.Black);
                _spriteBatch.DrawString(introFont, "Use the", new Vector2(725, 10), Color.Black);
                _spriteBatch.DrawString(introFont, "Shovel", new Vector2(725, 30), Color.Black);
                _spriteBatch.DrawString(introFont, "to dig", new Vector2(725, 50), Color.Black);
                _spriteBatch.DrawString(introFont, "plants up", new Vector2(715, 70), Color.Black);
                _spriteBatch.DrawString(introFont, "This is your house", new Vector2(10, 170), Color.Black);
                _spriteBatch.DrawString(introFont, "If the zombies", new Vector2(10, 190), Color.Black);
                _spriteBatch.DrawString(introFont, "get here, you lose", new Vector2(10, 210), Color.Black);
                _spriteBatch.DrawString(introFont, "The Mowers will", new Vector2(10, 400), Color.Black);
                _spriteBatch.DrawString(introFont, "defeat all zombies", new Vector2(10, 420), Color.Black);
                _spriteBatch.DrawString(introFont, "in a lane", new Vector2(10, 440), Color.Black);
                _spriteBatch.DrawString(introFont, "but only once", new Vector2(10, 460), Color.Black);

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
            else if (screen == Screen.Game)
            {
                _spriteBatch.Draw(frontYardTexture, window, Color.White);
                fallingSun.Draw(_spriteBatch);
                _spriteBatch.Draw(plantRosterTexture, plantRosterRect, Color.White);

                foreach (PlantGrid square in grid)
                {
                    square.Draw(_spriteBatch);
                }

                //plants

                //zombies

                foreach (Mower mower in mowers)
                {
                    mower.Draw(_spriteBatch);
                }            

                foreach (SeedPacket seed in seeds)
                {
                    seed.Draw(_spriteBatch);
                }

                shovelIcon.Draw(_spriteBatch);
                _spriteBatch.DrawString(sunFont, $"{sun}", sunBankLocation, Color.Black);

                if (levelTime < 1)
                {
                    _spriteBatch.DrawString(titleFont, "Ready", new Vector2(400, 250), Color.Red);
                }
                else if (levelTime > 1 && levelTime < 2)
                {
                    _spriteBatch.DrawString(titleFont, "Set", new Vector2(425, 250), Color.Red);
                }
                else if (levelTime > 2 && levelTime < 3)
                {
                    _spriteBatch.DrawString(titleFont, "PLANT!", new Vector2(400, 250), Color.Red);
                }

                testZombie.Draw(_spriteBatch);
            }



            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
