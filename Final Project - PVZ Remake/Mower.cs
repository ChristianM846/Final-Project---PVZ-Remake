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
        private float _mowerSpeed;
        private float _damage;

        public Mower(Texture2D texture, Rectangle location)
        {
            _mowerTexture = texture;
            _location = location;
            _mowerSpeed = 0;
            _damage = 0;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_mowerTexture, _location, Color.White);
        }

    }
}
