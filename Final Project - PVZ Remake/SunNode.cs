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
    public class SunNode
    {
        private Texture2D _sunTexture;
        private Rectangle _location;
        private int _sunValue;
        private bool _collected;
        MouseState _prevMouseState;

        public SunNode (Texture2D sunTexture, Rectangle location)
        {
            _sunTexture = sunTexture;
            _location = location;
            _sunValue = 0;
            _collected = false;
        }

        public void Update (MouseState mouseState)
        {
            _sunValue = 0;

            if (_location.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released)
            {
                _sunValue = 25;
                _collected = true;
            }

            _prevMouseState = mouseState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sunTexture, _location, Color.White);
        }

        public int NodeValue
        {
            get { return _sunValue; }
        }

        public bool Collected
        {
            get { return _collected; }
        }

        public Rectangle SunNodeRect
        {
            get { return _location; }
            set { _location = value; }
        }


    }
}
