using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace XNA
{
    class Card
    {
        private StaticSprite card;
        public byte colorcard;
        public byte dignity;
        public bool Visible
        {
            get { return card.Visible; }
            set { card.Visible = value; }
        }
        public void Setcard(byte color, byte dign)
        {
            colorcard = color;
            dignity = dign;
            card.ChangeTexture(Convert.ToString(color) + '_' + Convert.ToString(dignity));
        }

        public Vector2 GetPosition
        {
            get { return card.Position; }
        }
        public void ChangePosition(Vector2 position)
        {
            card.ChangePosition(position);
        }
        public Card(ContentManager Content, string tex, Vector2 position)
        {
            card = new StaticSprite(Content,tex,position);
        }
        public Card(ContentManager Content)
        {
            card = new StaticSprite(Content);
        }
        public void Draw(SpriteBatch spritebatch)
        {
            card.Draw(spritebatch);
        }
    }
}
