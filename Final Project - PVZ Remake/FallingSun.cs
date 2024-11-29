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
    public class FallingSun
    {
        private Random generator = new Random();
        private Texture2D _sunTexture;
        private Rectangle _location;
        private Vector2 _speed;
        private int _floor;
        private int _sunValue;
        private float _timeStamp;
        private float _sunTimer;
        MouseState _prevMouseState;

        public FallingSun (Texture2D texture, Rectangle location)
        {
            generator = new Random();
            _sunTexture = texture;
            _location = location;
            _speed = Vector2.Zero;
            _floor = 500;
            _sunValue = 25;
        }

        public int Update(GameTime gameTime, MouseState mouseState)
        {
            if (_location.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released)
            {
                _location.Y = -50;
                return 25;
            }
            _prevMouseState = mouseState;
            return 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sunTexture, _location, Color.White);
        }

        public float TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }
    }
}
