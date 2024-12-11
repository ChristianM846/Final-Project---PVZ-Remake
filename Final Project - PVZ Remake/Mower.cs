using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project___PVZ_Remake
{
    public class Mower
    {
        private Texture2D _mowerTexture;
        private Rectangle _location;
        private Vector2 _mowerSpeed;

        public Mower(Texture2D texture, Rectangle location)
        {
            _mowerTexture = texture;
            _location = location;
            _mowerSpeed = Vector2.Zero;
        }

        public void Update(List<Zombie> zombies)
        {
            _location.Offset(_mowerSpeed);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_mowerTexture, _location, Color.White);
        }

        public Rectangle MowerRect
        {
            get { return _location; }
            set { _location = value; }
        }

        public Vector2 MowerSpeed
        {
            get { return _mowerSpeed; }
            set { _mowerSpeed = value; }
        }
    }
}
