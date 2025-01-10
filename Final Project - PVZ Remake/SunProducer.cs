using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project___PVZ_Remake
{
    public class SunProducer : Plant
    {
        private float _timeStamp;
        private float _sunTimer;

        public SunProducer(Texture2D texture, Rectangle location, int health, float timeStamp)
        {
            _plantTexture = texture;
            _plantLocation = location;
            _plantHealth = health;
            _timeStamp = timeStamp;
        }

        public override void Update(GameTime gameTime)
        {
            _sunTimer = (float)Math.Round(gameTime.TotalGameTime.TotalSeconds - _timeStamp, 2);

            if (_sunTimer == 24.25)
            {
                // make sun node
                _timeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_plantTexture, _plantLocation, Color.White);
        }

        public Rectangle PlantRect
        {
            get { return _plantLocation; }
            set { _plantLocation = value; }

        }
    }
}
