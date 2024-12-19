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
    public class PlantShadow
    {
        private Texture2D _shadowTexture;
        private Rectangle _shadowLocation;
        private Rectangle _shadowHomeLocation;
        private int _sunCost;
        private int _deductSun;
        private bool _dragging;


        public PlantShadow(Texture2D texture, Rectangle homeLocation, int sunCost)
        {
            _shadowTexture = texture;
            _shadowHomeLocation = homeLocation;
            _shadowLocation = homeLocation;
            _sunCost = sunCost;
            _deductSun = 0;
        }

        public void Update(MouseState mouseState, List<PlantGrid> grid) // add plants here later
        {
            _deductSun = 0;

            if (_shadowLocation.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                _dragging = true;
            }

            foreach (PlantGrid tile in grid)
            {
                if (_dragging && mouseState.LeftButton == ButtonState.Released && tile.GridSquare.Contains(mouseState.Position) && !tile.Taken)
                {
                    //spawn plant
                    _deductSun = _sunCost;
                    tile.Taken = true;
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
