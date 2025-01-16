using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Final_Project___PVZ_Remake
{
    public class PlantShadow
    {
        private Texture2D _shadowTexture;
        private Rectangle _shadowLocation;
        private Rectangle _shadowHomeLocation;
        private SoundEffect _plantingTheme;
        private int _sunCost;
        private int _deductSun;
        private int _plant;
        private bool _dragging;
        private Texture2D _sunNodeTexture;
        private Texture2D _peaTexture;

        public PlantShadow(Texture2D texture, int plant, Rectangle homeLocation, SoundEffect planting, int sunCost, Texture2D nodeTexture, Texture2D peaTexture)
        {
            _shadowTexture = texture;
            _shadowHomeLocation = homeLocation;
            _plant = plant;
            _shadowLocation = homeLocation;
            _plantingTheme = planting;
            _sunCost = sunCost;
            _deductSun = 0;
            _sunNodeTexture = nodeTexture;
            _peaTexture = peaTexture;
        }

        public void Update(MouseState mouseState, GameTime gameTime, List<PlantGrid> grid, List<SeedPacket> seeds, List<Plant> plants)
        {
            _deductSun = 0;

            if (_shadowLocation.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                _dragging = true;
            }

            for (int g = 0; g < grid.Count; g++)
            {
                if (_dragging && mouseState.LeftButton == ButtonState.Released && grid[g].GridSquare.Contains(mouseState.Position) && !grid[g].Taken)
                {
                    if (_plant == 0)
                    {
                        plants.Add(new SunProducer(_shadowTexture, grid[g].GridSquare, 300, g, (float)gameTime.TotalGameTime.TotalSeconds, _sunNodeTexture));
                    }
                    else if (_plant == 1)
                    {
                        plants.Add(new ShooterPlant(_shadowTexture, grid[g].GridSquare, 300, 1, g, (float)gameTime.TotalGameTime.TotalSeconds, _peaTexture));
                    }
                    else if (_plant == 2)
                    {
                        plants.Add(new WallPlant(_shadowTexture, grid[g].GridSquare, 4000, g));
                    }

                    _deductSun = _sunCost;
                    _plantingTheme.Play();
                    seeds[_plant].TimeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                    grid[g].Taken = true;
                }
            }

            if (mouseState.LeftButton == ButtonState.Released)
            {
                _dragging = false;
            }

            if (_dragging)
            {
                _shadowLocation.X = mouseState.X - 25;
                _shadowLocation.Y = mouseState.Y - 25;
            }

            if (_dragging == false)
            {
                _shadowLocation = _shadowHomeLocation;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_shadowTexture, _shadowLocation, Color.White * 0.5f);
        }

        public int ShadowRectX
        {
            get { return _shadowLocation.X; }
            set { _shadowLocation.X = value; }
        }

        public int ShadowRectY
        {
            get { return _shadowLocation.Y; }
            set { _shadowLocation.Y = value; }
        }

        public int DeductSun
        {
            get { return _deductSun; }
        }
    }
}
