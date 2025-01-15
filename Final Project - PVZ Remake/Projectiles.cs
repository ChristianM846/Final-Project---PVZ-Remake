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
        private bool _hit;

        public Projectiles(Texture2D texture, Rectangle location, int type)
        {
            _projectileTexture = texture;
            _location = location;
            _type = type;
            _hit = false;

            if (_type == 1)
            {
                _dmgValue = 20;
                _speedX = new Vector2 (4, 0);
                _slow = false;
            }
            // add more for other projectiles later
        }

        public void Update (GameTime gametime, List<Zombie> zombies, Rectangle window)
        {
            _location.Offset(_speedX);

            if (!_location.Intersects(window))
            {
                _hit = true;
            }

            for (int z = 0; z < zombies.Count; z++)
            {
                if (_location.Intersects(zombies[z].ZombieRect))
                {
                    _hit = true;
                    zombies[z].Health -= _dmgValue;
                }
            }
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

        public bool Hit
        {
            get { return _hit; }
        }
    }
}
