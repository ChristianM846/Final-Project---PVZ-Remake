﻿using Microsoft.Xna.Framework;
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
        private int _moveCounter;
        private int _moveCountSpeed;
        private int _health;
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
            _moveCounter = 0;

            if (_zombieType == 1)
            {
                _zombieTexture = browncoatTexture;
                _health = 181;
                _moveCountSpeed = 4;
                _pointValue = 1;
            }
            else if (_zombieType == 2)
            {
                _zombieTexture = coneheadTexture;
                _health = 551;
                _moveCountSpeed = 4;
                _pointValue = 2;
            }
            else if (_zombieType == 3)
            {
                _zombieTexture = bucketheadTexture;
                _health = 1281;
                _moveCountSpeed = 4;
                _pointValue = 4;
            }
            else if (_zombieType == 4)
            {
                _zombieTexture = flagZombieTexture;
                _health = 181;
                _moveCountSpeed = 3;
                _pointValue = 0;
            }


        }

        public void Update(GameTime gameTime, List<Mower> mowers)
        {
            _moveCounter += 1;

            if (_moveCounter == _moveCountSpeed)
            {
                _location.Offset(-1, 0);
                _moveCounter = 0;
            }

            for (int m = 0; m < mowers.Count; m++)
            { 
                if (_location.Intersects(mowers[m].MowerRect))
                {
                    _health = 0;
                    mowers[m].MowerSpeed = new Vector2(2, 0);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_zombieTexture, _location, Color.White);
        }

        public int Health 
        {
            get { return _health; }
        }

        public int Points
        {
            get { return _pointValue; }
        }

        public Rectangle ZombieRect
        {
            get { return _location; }
            set { _location = value; }
        }
    }
}
