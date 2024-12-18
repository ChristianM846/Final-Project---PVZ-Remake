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
        private bool _dragging;
        private MouseState prevMouseState;

        public PlantShadow(Texture2D texture, Rectangle homeLocation, int sunCost)
        {
            _shadowTexture = texture;
            _shadowHomeLocation = homeLocation;
            _shadowLocation = homeLocation;
            _sunCost = sunCost;
        }

        public void Update(MouseState mouseState, List<PlantGrid> grid)
        {
            if (_shadowLocation.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                _dragging = true;
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


            prevMouseState = mouseState;
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
    }
}
