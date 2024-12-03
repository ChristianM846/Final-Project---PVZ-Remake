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
            _sunValue = 0;
        }

        public void Update(GameTime gameTime, MouseState mouseState)
        {
            _sunTimer = (float)Math.Round(gameTime.TotalGameTime.TotalSeconds - _timeStamp, 2);
            
            _sunValue = 0;

            if (_location.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released)
            {
                _location.Y = -50;
                _speed.Y = 0;
                _sunValue = 25;
                _timeStamp = (float)gameTime.TotalGameTime.TotalSeconds;

            }

            if ((_sunTimer == 10))
            {
                _floor = generator.Next(150, 480);
                _location.X = generator.Next(200, 700);
                _speed.Y = 1;
            }

            _location.Offset(_speed);

            if (_location.Bottom >= _floor)
            {
                _location.Y = _floor - 40;
                _speed.Y = 0;
            }

            _prevMouseState = mouseState;

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

        public int SunValue
        {
            get { return _sunValue; }
        }

        public Rectangle FallingSunRect
        {
            get { return _location; }
        }
    }
}
