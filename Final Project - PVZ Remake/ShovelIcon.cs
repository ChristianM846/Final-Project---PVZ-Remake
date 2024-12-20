using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project___PVZ_Remake
{
    public class ShovelIcon
    {
        private Texture2D _iconTexture;
        private Rectangle _location;
        MouseState _prevMouseState;

        public ShovelIcon(Texture2D icon, Rectangle location)
        {
            _iconTexture = icon;
            _location = location;
        }

        public void Update(MouseState mouseState, Shovel shovel)
        {
            if (_location.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released)
            {
                shovel.ShovelRectX = mouseState.Position.X - 35;
                shovel.ShovelRectY = mouseState.Position.Y - 35;
            }

            _prevMouseState = mouseState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_iconTexture, _location, Color.White);
        }

        public Rectangle IconRect
        {
            get { return _location; }
        }
    }
}
