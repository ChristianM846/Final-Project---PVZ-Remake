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
    public class PlantGrid
    {
        private Texture2D _highlight;
        private Rectangle _gridSquare;
        private float _transparency;
        private bool _taken;

        public PlantGrid(Texture2D texture, Rectangle location)
        {
            _highlight = texture;
            _gridSquare = location;
            _transparency = 0;
            _taken = false;
        }

        public void Update (MouseState mouseState)
        {
            if (_gridSquare.Contains(mouseState.Position))
            {
                _transparency = 0.5f;
            }
            else
            {
                _transparency = 0;
            }
        }

        public void Draw (SpriteBatch spritebatch)
        {
            spritebatch.Draw(_highlight, _gridSquare, Color.White * _transparency);
        }

        public Rectangle GridSquare
        {
            get { return _gridSquare; }
        }

        public bool Taken
        {
            get { return _taken; }
            set { _taken = value; }
        }
    }
}
