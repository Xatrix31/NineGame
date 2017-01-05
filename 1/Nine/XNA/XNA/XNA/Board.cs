using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace XNA
{
    class Board
    {
        private StaticSprite[,] cards;
        private Vector2 startposition;
        public bool Visible = false;
        public Board(ContentManager content)
        {
            startposition = new Vector2(144,71);
            cards=new StaticSprite[4,9];
            for (byte x = 0; x < 4; x++)
            {
                for (byte y = 0; y < 9; y++)
                {
                    cards[x, y] = new StaticSprite(content, "board", startposition, (int)149 / 4 * x, (int)325 / 9 * y);
                    startposition.X += 39;
                }
                startposition.X -= 351;
                startposition.Y += 38;
            }
        }
        public void SetVisibleCard(byte color,byte dignity)
        {
            cards[color - 1, dignity - 6].Visible = true;
        }
        public StaticSprite SelectCard(byte color, byte dignity)
        {
            return cards[color - 1, dignity - 6];
        }
        public void ClearBoard()
        {
            foreach (StaticSprite card in cards) card.Visible = false;
        }
        public void Draw(SpriteBatch spritebatch)
        {
            if(Visible) foreach (StaticSprite card in cards) card.Draw(spritebatch);
        }
    }
}
