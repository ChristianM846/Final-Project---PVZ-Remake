using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project___PVZ_Remake
{
    public class Zombie
    {
        private Texture2D _zombieTexture;
        //private Texture2D _browncoatTexture;
        //private Texture2D _coneheadTexture;
        //private Texture2D _bucketheadTexture;
        //private Texture2D _flagZombieTexture;
        private Rectangle _location;
        private float _speed;
        private float _health;
        private int _pointValue;
        private int _zombieType;

        public Zombie(Texture2D browncoatTexture, Texture2D coneheadTexture, Texture2D bucketheadTexture, Texture2D flagZombieTexture, Rectangle location, int zombieType)
        {
            //_browncoatTexture = browncoatTexture;
            //_coneheadTexture = coneheadTexture;
            //_bucketheadTexture = bucketheadTexture;
            //_flagZombieTexture = flagZombieTexture;
            _location = location;
            _zombieType = zombieType;

            if (_zombieType == 1)
            {
                _zombieTexture = browncoatTexture;
                _health = 181;
                _speed = 0.25f;
                _pointValue = 1;
            }
            else if (_zombieType == 2)
            {
                _zombieTexture = coneheadTexture;
                _health = 551;
                _speed = 0.25f;
                _pointValue = 2;
            }
            else if (_zombieType == 3)
            {
                _zombieTexture = bucketheadTexture;
                _health = 1281;
                _speed = 0.25f;
                _pointValue = 4;
            }
            else if (_zombieType == 4)
            {
                _zombieTexture = flagZombieTexture;
                _health = 181;
                _speed = 0.33f;
                _pointValue = 0;
            }


        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_zombieTexture, _location, Color.White);
        }


    }
}
