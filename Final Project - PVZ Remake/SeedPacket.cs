using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Final_Project___PVZ_Remake
{
    public class SeedPacket
    {
        private Texture2D _packetTexture;
        private Texture2D _associatedPlantTexture;
        private Rectangle _location;
        private int _plant;
        private int _sunCost;
        private float _coolDown;
        private float _coolDownTimer;
        private bool _cooling;
        private bool _locked;
        private float _timeStamp;
        MouseState _prevMouseState;
        private Color _color;

        public SeedPacket (Texture2D texture, Texture2D plantTexture, int plant, int cost, float cooldown, Rectangle location, bool locked)
        {
            _packetTexture = texture;
            _associatedPlantTexture = plantTexture;
            _location = location;
            _plant = plant;
            _sunCost = cost;
            _coolDown = cooldown;
            _locked = locked;
            _coolDownTimer = 0;          
            _timeStamp = -30;
            _color = Color.White;
        }

        public void Update (GameTime gameTime, MouseState mouseState, int sun, List<PlantShadow> shadows)
        {
            if (_locked)
            {
                _color = Color.Gray;
            }
            else
            {
                _coolDownTimer = (float)Math.Round(gameTime.TotalGameTime.TotalSeconds - _timeStamp, 2);

                if (_coolDownTimer >= _coolDown)
                {
                    _color = Color.White;
                    _cooling = false;
                }
                else
                {
                    _color = Color.Gray;
                    _cooling = true;
                }


                if (_location.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released && !_cooling && sun >= _sunCost)
                {
                    shadows[_plant].ShadowRectX = mouseState.Position.X - 25;
                    shadows[_plant].ShadowRectY = mouseState.Position.Y - 25;
                }



                _prevMouseState = mouseState;

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_packetTexture, _location, _color);
        }

        public float TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }

    }
}
