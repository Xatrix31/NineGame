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
    class Sprite
    {
        private Vector2 Position;
        private Texture2D Picture;
        private byte numberOfFrame = 0;
        private float timePerFrame;
        private float totalElapsed;
        private byte frames;
        private Rectangle sprRec;
        private void ChangeFrame(float elpT)
        {
                totalElapsed += elpT;
                if (totalElapsed > timePerFrame)
                {
                    if (numberOfFrame >= frames - 1)
                        numberOfFrame = 0;
                    else numberOfFrame++;
                    sprRec = new Rectangle((int)Picture.Width / frames * numberOfFrame, 0, Picture.Width / frames, Picture.Height);
                    totalElapsed = 0;
            }
        }
        public Sprite(ContentManager content, string textureName, Vector2 position, byte FramesPerSec, byte NumberOfFrame,byte Frames)
        {
            timePerFrame = (float)1 / FramesPerSec;
            numberOfFrame = NumberOfFrame;
            Picture = content.Load<Texture2D>(textureName);
            Position = position;
            sprRec = new Rectangle(0, 0, Picture.Width / Frames, Picture.Height);
            frames = Frames;

        }
        public void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            spriteBatch.Draw(Picture, Position, sprRec, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            ChangeFrame((float)gametime.ElapsedGameTime.TotalSeconds);
        }
    }
    class StaticSprite
    {
        public Vector2 Position; //координаты от верхнего левого угла
        private Texture2D Picture; //картинка, графически представляющая объект
        public bool Visible=false;
        private Rectangle sprRec;
        private ContentManager Content;
        public StaticSprite(ContentManager content, string picture)
        {
            Content = content;
            Picture = content.Load<Texture2D>(picture);
            sprRec = new Rectangle(0, 0, Picture.Width, Picture.Height);
        }
        public StaticSprite(ContentManager content)
        {
            Content = content;
        }
        public StaticSprite(ContentManager content, string textureName, Vector2 position)
        {
            Content = content;
            Picture = content.Load<Texture2D>(textureName);
            Position = position;
            sprRec = new Rectangle(0, 0, Picture.Width, Picture.Height);

        }
        public StaticSprite(ContentManager content, string textureName, Vector2 position, int x,int y)
        {
            Content = content;
            Picture = content.Load<Texture2D>(textureName);
            Position = position;
            sprRec = new Rectangle(x, y, Picture.Width/4, Picture.Height/9);

        }
        public Rectangle ChangeRect
        {
            set { sprRec = value; }
        }
        public void ChangePosition(Vector2 position)
        {
            Position = position;
        }
        public void ChangeTexture(string texture)
        {
            Picture = Content.Load<Texture2D>(texture);
            sprRec = new Rectangle(0, 0, Picture.Width, Picture.Height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Visible) spriteBatch.Draw(Picture, Position, sprRec, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
        }
    }
}