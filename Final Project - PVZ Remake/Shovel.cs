using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project___PVZ_Remake
{
    public class Shovel
    {
        private Texture2D _shovelTexture;
        private Rectangle _shovelLocation;
        private Rectangle _shovelHomeLocation;
        private bool _dragging;

        public Shovel(Texture2D texture, Rectangle location)
        {
            _shovelTexture = texture;
            _shovelLocation = location;
            _shovelHomeLocation = location;
        }

        public void Update(MouseState mouseState, List<PlantGrid> grid, List<Plant> plants)
        {
            if (_shovelLocation.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                _dragging = true;
            }

            foreach (PlantGrid tile in grid)
            {
                if (_dragging && mouseState.LeftButton == ButtonState.Released && tile.GridSquare.Contains(mouseState.Position) && tile.Taken)
                {
                    tile.Taken = false;
                }
            }

            foreach (Plant plant in plants)
            {
                if (_dragging && mouseState.LeftButton == ButtonState.Released && plant.PlantLocation.Contains(mouseState.Position))
                {
                    plant.PlantHealth = 0;
                }
            }


            if (mouseState.LeftButton == ButtonState.Released)
            {
                _dragging = false;
            }

            if (_dragging)
            {
                _shovelLocation.X = mouseState.X - 35;
                _shovelLocation.Y = mouseState.Y - 35;
            }

            if (_dragging == false)
            {
                _shovelLocation = _shovelHomeLocation;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_shovelTexture, _shovelLocation, Color.White);
        }

        public int ShovelRectX
        {
            get { return _shovelLocation.X; }
            set { _shovelLocation.X = value; }
        }

        public int ShovelRectY
        {
            get { return _shovelLocation.Y; }
            set { _shovelLocation.Y = value; }
        }

    }
}
