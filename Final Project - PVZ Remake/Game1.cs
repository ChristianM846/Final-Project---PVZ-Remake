using Microsoft.Xna.Framework;
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
        Texture2D shovelTexture;

        Rectangle trashSpot;


        //plant textures
        Texture2D sunflowerTexture;
        Texture2D peashooterTexture;
        Texture2D wallnutTexture;
        Texture2D potatoMineTexture;
        Texture2D potatoMineArmingTexture;
        Texture2D cherryBombTexture;
        Texture2D snowPeaTexture;
        Texture2D repeaterTexture;


        Texture2D browncoatTexture;
        Texture2D coneheadTexture;
        Texture2D bucketheadTexture;
        Texture2D flagZombieTexture;

        // Hidden Game Things
        Rectangle fallingSunRect;
        Rectangle shadowHome;
        Rectangle shovelHome;

        //Sounds and Fonts
        SoundEffect introTheme;
        SoundEffectInstance introThemeInstance;
        SoundEffect grasswalkTheme;
        SoundEffectInstance grasswalkThemeInstance;
        SoundEffect loonboonTheme;
        SoundEffectInstance loonboonThemeInstance;
        SoundEffect sunPickup;
        SoundEffect gameOverTheme;
        SoundEffect plantingTheme;
        SoundEffect eatingTheme;
        SoundEffectInstance eatingThemeInstance;


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
        List<SunNode> sunNodes;
        List<SeedPacket> seeds;
        List<PlantShadow> shadows;
        List<Plant> plants;
        List<Zombie> zombies;


        ShovelIcon shovelIcon;
        Shovel shovel;
        //Seed Packets
        SeedPacket sunflowerSeed;
        SeedPacket peashooterSeed;
        SeedPacket wallnutSeed;
        SeedPacket potatoMineSeed;
        SeedPacket cherryBombSeed;
        SeedPacket snowPeaSeed;
        SeedPacket repeaterSeed;
        //shadows here
        PlantShadow sunflowerShadow;
        PlantShadow peashooterShadow;
        PlantShadow wallnutShadow;
        PlantShadow potatoMineShadow;
        PlantShadow cherryBombShadow;
        PlantShadow snowPeaShadow;
        PlantShadow repeaterShadow;
        //Other
        FallingSun fallingSun;
        ZombieSpawner level1Spawner;


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
            trashSpot = new Rectangle(1000, 1000, 100, 100);
            sun = 50;
            time = 0;
            levelTime = 0;

            plantRosterRect = new Rectangle(200, 2, 450, 70);

            // Non-Class lists
            level1 = new List<int>() { 0, 1, 1, 1, 2, 2, 3, 3, 3, 4, 10, 5, 5, 5, 6, 6, 7, 7, 7, 8, 20, 0 };
            level2 = new List<int>() { 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 12, 6, 6, 7, 7, 8, 8, 9, 9, 10, 24, 0 };
            level3 = new List<int>() { 0, 1, 1, 2, 2, 3, 3, 4, 5, 6, 14, 7, 8, 8, 9, 10, 10, 11, 12, 12, 28, 0 };

            // Initialize Class Lists
            grid = new List<PlantGrid>();
            mowers = new List<Mower>();
            sunNodes = new List<SunNode>();
            seeds = new List<SeedPacket>();
            shadows = new List<PlantShadow>();
            plants = new List<Plant>();
            zombies = new List<Zombie>();

            //Initialize Class object Rectangles
            fallingSunRect = new Rectangle(300, -100, 40, 40);
            shadowHome = new Rectangle(100, -200, 50, 50);
            shovelHome = new Rectangle(200, -200, 70, 70);

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

            seeds.Add(sunflowerSeed = new SeedPacket(sunflowerSeedTexture, sunflowerTexture, 0, 50, 7.5f, new Rectangle(260, 10, 35, 50), false));
            seeds.Add(peashooterSeed = new SeedPacket(peashooterSeedTexture, peashooterTexture, 1, 100, 7.5f, new Rectangle(298, 10, 35, 50), false));
            seeds.Add(wallnutSeed = new SeedPacket(wallnutSeedTexture, wallnutTexture, 2, 50, 30f, new Rectangle(336, 10, 35, 50), false));
            seeds.Add(potatoMineSeed = new SeedPacket(potatoMineSeedTexture, potatoMineTexture, 3, 25, 30f, new Rectangle(374, 10, 35, 50), true));
            seeds.Add(cherryBombSeed = new SeedPacket(cherryBombSeedTexture, cherryBombTexture, 4, 150, 50f, new Rectangle(412, 10, 35, 50), true));
            seeds.Add(snowPeaSeed = new SeedPacket(snowPeaSeedTexture, snowPeaTexture, 5, 175, 7.5f, new Rectangle(450, 10, 35, 50), true));
            seeds.Add(repeaterSeed = new SeedPacket(repeaterSeedTexture, repeaterTexture, 6, 200, 7.5f, new Rectangle(488, 10, 35, 50), true));

            shadows.Add(sunflowerShadow = new PlantShadow(sunflowerTexture, 0, shadowHome, plantingTheme, 50, sunTexture));
            shadows.Add(peashooterShadow = new PlantShadow(peashooterTexture, 1, shadowHome, plantingTheme, 100, sunTexture));
            shadows.Add(wallnutShadow = new PlantShadow(wallnutTexture, 2, shadowHome, plantingTheme, 50, sunTexture));
            shadows.Add(potatoMineShadow = new PlantShadow(potatoMineTexture, 3, shadowHome, plantingTheme, 25, sunTexture));
            shadows.Add(cherryBombShadow = new PlantShadow(cherryBombTexture, 4, shadowHome, plantingTheme, 150, sunTexture));
            shadows.Add(snowPeaShadow = new PlantShadow(snowPeaTexture, 5, shadowHome, plantingTheme, 175, sunTexture));
            shadows.Add(repeaterShadow = new PlantShadow(repeaterTexture, 6, shadowHome, plantingTheme, 200, sunTexture));


            // Make Other Class Objects here
            shovelIcon = new ShovelIcon(shovelIconTexture, new Rectangle(651, 2, 70, 70));
            shovel = new Shovel(shovelTexture, shovelHome);
            fallingSun = new FallingSun(sunTexture, fallingSunRect);
            level1Spawner = new ZombieSpawner(level1, browncoatTexture, coneheadTexture, bucketheadTexture, flagZombieTexture);

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
            shovelTexture = Content.Load<Texture2D>("Images/Shovel");

            //plant textures
            sunflowerTexture = Content.Load<Texture2D>("Images/Sunflower");
            peashooterTexture = Content.Load<Texture2D>("Images/Peashooter");
            wallnutTexture = Content.Load<Texture2D>("Images/Wallnut");
            potatoMineTexture = Content.Load<Texture2D>("Images/PotatoMineArmed");
            potatoMineArmingTexture = Content.Load<Texture2D>("Images/PotatoMineArming");
            cherryBombTexture = Content.Load<Texture2D>("Images/CherryBomb");
            snowPeaTexture = Content.Load<Texture2D>("Images/SnowPea");
            repeaterTexture = Content.Load<Texture2D>("Images/Repeater");

            //zombie Textures
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
            gameOverTheme = Content.Load<SoundEffect>("Sounds/LoseMusic");
            plantingTheme = Content.Load<SoundEffect>("Sounds/PlantingEffect");
            eatingTheme = Content.Load<SoundEffect>("Sounds/EatingEffect");
            eatingThemeInstance = eatingTheme.CreateInstance();
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

                    for (int i = 0; i < sunNodes.Count; i++)
                    {
                        sunNodes[i].Update(mouseState);
                        sun += sunNodes[i].NodeValue;

                        if (sunNodes[i].Collected)
                        {
                            sunNodes[i].SunNodeRect = trashSpot;
                            sunNodes.RemoveAt(i);
                            i--;
                        }

                    }

                    for (int m = 0; m < mowers.Count; m++)
                    {
                        mowers[m].Update(zombies);

                        if (mowers[m].MowerRect.Left == window.Right)
                        {
                            mowers[m].MowerSpeed = Vector2.Zero;
                            mowers[m].MowerRect = trashSpot;
                        }
                    }

                    foreach (SeedPacket seed in seeds)
                    {
                        seed.Update(gameTime, mouseState, sun, shadows);
                    }

                    foreach (PlantShadow shadow in shadows)
                    {
                        shadow.Update(mouseState, gameTime, grid, seeds, plants);
                        sun -= shadow.DeductSun;
                    }

                    shovelIcon.Update(mouseState, shovel);
                    shovel.Update(mouseState, grid, plants);


                    for (int p = 0; p < plants.Count; p++)
                    {
                        plants[p].Update(gameTime, sunNodes);

                        if (plants[p].PlantHealth <= 0)
                        {
                            plants[p].PlantLocation = trashSpot;
                            plants.RemoveAt(p);
                            p--;
                        }

                    }

                    if (levelTime >= 20)
                    {
                        for (int z = 0; z < zombies.Count; z++)
                        {
                            zombies[z].Update(gameTime, mowers, plants);

                            for (int p = 0; p < plants.Count; p++)
                            {
                                if (!zombies[z].ZombieRect.Intersects(plants[p].PlantLocation) && eatingThemeInstance.State == SoundState.Playing)
                                {
                                    eatingThemeInstance.Stop();
                                }

                                if (zombies[z].ZombieRect.Intersects(plants[p].PlantLocation))
                                {
                                    eatingThemeInstance.Play();
                                }
                            }

                            if (zombies[z].Health <= 0)
                            {
                                level1Spawner.PointCount += zombies[z].Points;
                                zombies[z].ZombieRect = trashSpot;
                                zombies.RemoveAt(z);
                                z--;
                            }

                            

                        }
                    }
                    

                    level1Spawner.Update(zombies);

                    if (level1Spawner.Wave == 21)
                    {
                        screen = Screen.Thanks;
                    }

                    foreach (Zombie zombie in zombies)
                    {
                        if (zombie.ZombieRect.X == 150)
                        {
                            screen = Screen.GameOver;
                            grasswalkThemeInstance.Stop();
                            loonboonThemeInstance.Stop();
                            gameOverTheme.Play();
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

                foreach (Plant plant in plants)
                {
                    plant.Draw(_spriteBatch);
                }

                foreach (Zombie zombie in zombies)
                {
                    zombie.Draw(_spriteBatch);
                }

                foreach (Mower mower in mowers)
                {
                    mower.Draw(_spriteBatch);
                }

                foreach (SeedPacket seed in seeds)
                {
                    seed.Draw(_spriteBatch);
                }

                foreach (PlantShadow shadow in shadows)
                {
                    shadow.Draw(_spriteBatch);
                }

                shovelIcon.Draw(_spriteBatch);
                _spriteBatch.DrawString(sunFont, $"{sun}", sunBankLocation, Color.Black);

                foreach (SunNode node in sunNodes)
                {
                    node.Draw(_spriteBatch);
                }

                shovel.Draw(_spriteBatch);


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


            }



            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
