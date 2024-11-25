using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project___PVZ_Remake
{
    public class ShovelIcon
    {
        private Texture2D _iconTexture;
        private Rectangle _location;

        public ShovelIcon(Texture2D icon, Rectangle location)
        {
            _iconTexture = icon;
            _location = location;
        }

        public void Update(MouseState mousestate)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_iconTexture, _location, Color.White);
        }

        public Rectangle iconRect
        {
            get { return _location; }
        }
    }
}
