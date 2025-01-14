using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project___PVZ_Remake
{
    public abstract class Plant
    {
        protected Texture2D _plantTexture;
        protected Rectangle _plantLocation;
        protected  int _plantHealth;

        public Texture2D PlantTexture
        {
            get { return _plantTexture; }
            set { _plantTexture = value; }
        }

        public Rectangle PlantLocation
        {
            get { return _plantLocation; }
            set { _plantLocation = value; }
        }

        public int PlantHealth
        {
            get { return _plantHealth; }
            set { _plantHealth = value; }
        }

        public abstract void Update(GameTime gameTime, List<SunNode> nodes, List<Projectiles> projectiles);

        public abstract void Draw(SpriteBatch spriteBatch);


    }
}
