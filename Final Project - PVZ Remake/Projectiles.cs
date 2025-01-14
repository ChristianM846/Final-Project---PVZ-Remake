using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project___PVZ_Remake
{
    public class Projectiles
    {
        private Texture2D _projectileTexture;
        private Rectangle _location;
        private Vector2 _speedX;
        private int _type;
        private int _dmgValue;
        private bool _slow;

        public Projectiles(Texture2D texture, Rectangle location, int type)
        {
            _projectileTexture = texture;
            _location = location;
            _type = type;

            if (_type == 1)
            {
                _dmgValue = 20;
                _speedX = new Vector2 (4, 0);
                _slow = false;
            }
            // add more for other projectiles later
        }

        public void Update (GameTime gametime)
        {
            _location.Offset(_speedX);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_projectileTexture, _location, Color.White);
        }

        public Rectangle ProjectileLocation
        {
            get { return _location; }
            set { _location = value; }
        }

        public int Damage
        {
            get { return _dmgValue; }
        }

        public Vector2 Speed
        {
            get { return _speedX; }
            set { _speedX = value; }
        }

        public bool Slowing
        {
            get { return _slow; }
        }
    }
}
