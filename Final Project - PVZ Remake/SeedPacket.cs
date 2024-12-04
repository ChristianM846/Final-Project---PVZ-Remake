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
    public class SeedPacket
    {
        private Texture2D _packetTexture;
        private Rectangle _location;
        private int _plant;
        private int _sunCost;
        private float _coolDown;
        private float _coolDownTimer;
        private float _timeStamp;
        private Color _color;

        public SeedPacket (Texture2D texture, int plant, int cost, float cooldown, Rectangle location)
        {
            _packetTexture = texture;
            _location = location;
            _plant = plant;
            _sunCost = cost;
            _coolDown = cooldown;
            _coolDownTimer = 0;
            _color = Color.White;
        }

        public void Update (GameTime gameTime,MouseState mouseState)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_packetTexture, _location, _color);
        }

    }
}
