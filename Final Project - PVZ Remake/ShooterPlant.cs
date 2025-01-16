using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project___PVZ_Remake
{
    public class ShooterPlant : Plant
    {
        private int _shooterType;
        private float _timeStamp;
        private float _shootTimer;
        private Texture2D _peaTexture;

        public ShooterPlant(Texture2D texture, Rectangle location, int health, int type, int grid, float timeStamp, Texture2D peaTexture)
        {
            _plantTexture = texture;
            _plantLocation = location;
            _plantHealth = health;
            _shooterType = type;
            _gridSpace = grid;
            _timeStamp = timeStamp;
            _peaTexture = peaTexture;
        }

        public override void Update(GameTime gameTime, List<SunNode> nodes, List<Projectiles> projectiles) //Nodes not used here
        {
            _shootTimer = (float)Math.Round(gameTime.TotalGameTime.TotalSeconds - _timeStamp, 2);

            if (_shooterType == 1)
            {
                if (_shootTimer == 1.42f)
                {
                    projectiles.Add(new Projectiles(_peaTexture, new Rectangle(_plantLocation.X + 50, _plantLocation.Y + 10, 15, 15), _shooterType));
                    _timeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_plantTexture, _plantLocation, Color.White);
        }

        public float TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }

        public Rectangle PlantRect
        {
            get { return _plantLocation; }
            set { _plantLocation = value; }
        }
    }
}
