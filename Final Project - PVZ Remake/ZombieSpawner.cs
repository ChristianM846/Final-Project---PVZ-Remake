using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project___PVZ_Remake
{
    public class ZombieSpawner
    {
        private Texture2D _browncoatTexture;
        private Texture2D _coneheadTexture;
        private Texture2D _bucketheadTexture;
        private Texture2D _flagZombieTexture;
        private Random _generator = new Random();
        private int _wave;
        private int _pointCounter;
        private int _wavepoints;
        private int _winPointGoal;
        private int _spawnLane;
        private int _spawnHeight;
        private int _spawnChoice;
        private int _zombiesSpawned;
        private List<int> _spawnList;

        public ZombieSpawner(List<int> spawnList, Texture2D browncoatTexture, Texture2D coneheadTexture, Texture2D bucketheadTexture, Texture2D flagZombieTexture)
        {
            _browncoatTexture = browncoatTexture;
            _coneheadTexture = coneheadTexture;
            _bucketheadTexture = bucketheadTexture;
            _flagZombieTexture = flagZombieTexture;
            _spawnList = spawnList;
            _wave = 0;
            _pointCounter = 0;
            _wavepoints = 0;

            foreach (int value in _spawnList)
            {
                _winPointGoal += value;
            }

        }

        public void Update(List<Zombie> zombies)
        {
            if (_pointCounter == _wavepoints)
            {
                _wave++;
                _wavepoints = _spawnList[_wave];
                _pointCounter = _wavepoints;
                _zombiesSpawned = 0;

                if (_wave == 10 || _wave == 20)
                {
                    //spawn flag zombie
                }


                while (_pointCounter > 0)
                {
                    _spawnLane = _generator.Next(1,6);

                    if (_spawnLane == 1)
                    {
                        _spawnHeight = 75;
                    }
                    else if (_spawnLane == 2)
                    {
                        _spawnHeight = 160;
                    }
                    else if (_spawnLane == 3)
                    {
                        _spawnHeight = 250;
                    }
                    else if (_spawnLane == 4)
                    {
                        _spawnHeight = 330;
                    }
                    else if (_spawnLane == 5)
                    {
                        _spawnHeight = 410;
                    }

                    _spawnChoice =  _generator.Next(1,11);

                    if (_spawnChoice >= 1 && _spawnChoice <= 5 && _pointCounter >= 1)
                    {
                        zombies.Add(new Zombie(_browncoatTexture, new Rectangle(800  + (_zombiesSpawned * 10), _spawnHeight, 50, 80) , 1));
                        _zombiesSpawned++;
                        _pointCounter -= 1;
                    }
                    //else if (_spawnChoice >= 6 && _spawnChoice <= 8 && _pointCounter >= 2)
                    //{
                    //    //spawn conehead
                    //    _zombiesSpawned++;
                    //    _pointCounter -= 2;
                    //}
                    //else if (_spawnChoice >= 9 && _spawnChoice <= 10 && _pointCounter >= 4)
                    //{
                    //    //spawn buckethead
                    //    _zombiesSpawned++;
                    //    _pointCounter -= 4;
                    //}


                }



            }
        }

        public int PointCount
        {
            get { return _pointCounter; }
            set { _pointCounter = value; }
        }

        public int Wave
        {
            get { return _wave; }
        }
    }
}
