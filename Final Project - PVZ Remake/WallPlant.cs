using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project___PVZ_Remake
{
    public class WallPlant : Plant
    {

        public WallPlant(Texture2D texture, Rectangle location, int health, int grid)
        {
            _plantTexture = texture;
            _plantLocation = location;
            _plantHealth = health;
            _gridSpace = grid;
        }

        public override void Update(GameTime gameTime, List<SunNode> nodes, List<Projectiles> projectiles) //Nodes and projectiles not used here
        {

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
