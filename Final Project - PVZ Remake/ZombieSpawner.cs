using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project___PVZ_Remake
{
    public class ZombieSpawner
    {
        private Random _generator = new Random();
        private int _wave;
        private int _pointCounter;
        private int _points;
        private int _winPointGoal;
        private int _spawnLane;
        private int _spawnHeight;
        private List<int> _spawnList;

        public ZombieSpawner(List<int> spawnList)
        {
            _spawnList = spawnList;
            _wave = 0;

            foreach (int value in _spawnList)
            {
                _winPointGoal += value;
            }

        }

        public void Update(List<Zombie> zombies)
        {

        }

    }
}
