using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace XNA
{
    class Main
    {
        private Texture2D main, wrong, bells;
        private Sprite[] fires;
        private float timePerFrame;
        private float totalElapsed;
        private byte numberOfFrame = 0;
        public bool wrongclick = false;
        public bool bell = false;
        private Rectangle rect;
        private Bank bank;
        private Boss boss;
        private Human human;
        Board board;
        private Player[] player;
        public Main(ContentManager Content)
        {
            fires = new Sprite[6];
            main = Content.Load<Texture2D>("main");
            fires[0] = new Sprite(Content, "fire", new Vector2(149, 12), 5, 0, 4);
            fires[1] = new Sprite(Content, "fire", new Vector2(164, 6), 5, 1, 4);
            fires[2] = new Sprite(Content, "fire", new Vector2(179, 12), 5, 2, 4);
            fires[3] = new Sprite(Content, "fire", new Vector2(451, 12), 5, 0, 4);
            fires[4] = new Sprite(Content, "fire", new Vector2(466, 6), 5, 2, 4);
            fires[5] = new Sprite(Content, "fire", new Vector2(481, 12), 5, 4, 4);
            bank = new Bank(Content);
            boss = new Boss(Content);
            player = new Player[2];
            player[0] = new Player(Content,true);
            player[1] = new Player(Content, false);
            timePerFrame = (float)1 / 6;
            board = new Board(Content);
            wrong = Content.Load<Texture2D>("wrong");
            bells = Content.Load<Texture2D>("bells");
            human = new Human(Content, new Vector2(327,245));
            board.Visible = true;
        }
        public void TakeCard(byte play,byte number, Card card)
        {
            player[play].TakeCard(number,card.colorcard,card.dignity);
        }
        public void SortCards()
        {
            human.SortCard();
        }
        public void TakeHumanCard(byte number,Card card)
        {
            human.TakeCard(number,card);
        }
        public void CheckPosition(int x, int y)
        {
            Card card = human.CheckPosition(x, y);
            if (card != null)
                if (card.dignity<9)
                {
                    if (board.SelectCard(card.colorcard, (byte)(card.dignity + 1)).Visible)
                    {
                        board.SetVisibleCard(card.colorcard, card.dignity);
                        human.SelectCard(card.colorcard, card.dignity);
                    }
                }
                else if (card.dignity>9)
                {
                    if (board.SelectCard(card.colorcard, (byte)(card.dignity - 1)).Visible)
                    {
                        board.SetVisibleCard(card.colorcard, card.dignity);
                        human.SelectCard(card.colorcard, card.dignity);
                    }
                } else
                {
                    board.SetVisibleCard(card.colorcard, card.dignity);
                    human.SelectCard(card.colorcard, card.dignity);
                }
        }
        public void ChangeBank(byte a)
        {
            bank.money += a;
        }
        public void MakeDecision(byte play, byte card)
        {
            player[play].MakeDecision(card);
        }
        private void ChangeFrame(float elpT, byte frames, Texture2D texture)
        {
            totalElapsed += elpT;
            if (totalElapsed > timePerFrame)
            {
                if (numberOfFrame >= frames-1)
                {
                    wrongclick = false;
                    bell = false;
                    numberOfFrame = 0;
                }
                else numberOfFrame++;
                rect = new Rectangle(0, (int)texture.Height / frames * numberOfFrame, texture.Width, texture.Height / frames);
                totalElapsed = 0;
            }
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            spriteBatch.Draw(main, new Vector2(0,0),new Rectangle(0,0,main.Width,main.Height), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            boss.Draw(spriteBatch,gametime);
            for (byte i=0;i<6;i++)
            {
                fires[i].Draw(spriteBatch,gametime);
            }
            bank.Draw(spriteBatch);
            if (wrongclick)
            {
                spriteBatch.Draw(wrong, new Vector2(280, 20),rect, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                ChangeFrame((float)gametime.ElapsedGameTime.TotalSeconds,7,wrong);
            }
            if (bell)
            {
                spriteBatch.Draw(bells, new Vector2(280, 20), rect, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                ChangeFrame((float)gametime.ElapsedGameTime.TotalSeconds, 4, bells);
            }
            player[0].Draw(spriteBatch, gametime);
            player[1].Draw(spriteBatch, gametime);
            board.Draw(spriteBatch);
            human.Draw(spriteBatch);
            //spriteBatch.Draw(changeable, new Vector2(284, 170), changeableRect, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
        }
    }

    class BeforeGame
    {
        private Texture2D main;
        private Texture2D changeable;
        private ContentManager Content;
        private float totalElapsed;
        private byte numberOfFrame = 0;
        private float timePerFrame;
        public bool end = false;
        private byte pause = 0;
        private byte framecount = 0;
        private Rectangle changeableRect;
        public BeforeGame(ContentManager content)
        {
            Content = content;
            main = content.Load<Texture2D>("beforegame");
            changeable = content.Load<Texture2D>("beforegame1");
            changeableRect = new Rectangle(0, 0, changeable.Width, changeable.Height/11);
            timePerFrame = (float)1 / 6;
        }
        private void ChangeFrame(float elpT)
        {
            totalElapsed += elpT;
            if (totalElapsed > timePerFrame)
            {
                framecount++;
                if (numberOfFrame > 9)
                {
                    numberOfFrame = 0;
                    changeable = Content.Load<Texture2D>("beforegame2");
                }
                else numberOfFrame++;
                changeableRect = new Rectangle(0, (int)changeable.Height / 11 * numberOfFrame, changeable.Width, changeable.Height/11);
                totalElapsed = 0;
            }
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            spriteBatch.Draw(main, new Vector2(280, 153), Color.White);
            spriteBatch.Draw(changeable, new Vector2(284, 170), changeableRect, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            if (framecount<21) ChangeFrame((float)gametime.ElapsedGameTime.TotalSeconds); else
            {
                pause++;
                if (pause == 50) end = true;
            }
        }
    }

    class Hello
    {
        private Texture2D picture;
        public bool end = false;
        private Rectangle pictureRect;
        private float totalElapsed;
        private bool framedirection=true;
        private float timePerFrame;
        private byte pause = 0;
        private byte[] framespersec= new byte[7] {1,8,8,8,8,2,2};
        private byte numberOfFrame = 0;
        private byte framecount = 0;
        public Hello(ContentManager content)
            {
                picture = content.Load<Texture2D>("picture");
                timePerFrame = (float)1 / framespersec[numberOfFrame];
                pictureRect = new Rectangle(0, 0, picture.Width / 7, picture.Height);
            }
        private void ChangeFrame(float elpT)
        {
            totalElapsed += elpT;
            if (totalElapsed > timePerFrame)
            {
                framecount++;
                if (numberOfFrame > 5) framedirection = false;
                if (framedirection) numberOfFrame++; else numberOfFrame--;
                pictureRect = new Rectangle((int)picture.Width / 7 * numberOfFrame, 0, picture.Width / 7, picture.Height);
                totalElapsed = 0;
                timePerFrame = (float)1 / framespersec[numberOfFrame];
            }
        }
        public void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            spritebatch.Draw(picture, new Vector2(203, 98), pictureRect, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            if (framecount < 12) ChangeFrame((float)gameTime.ElapsedGameTime.TotalSeconds); else
            {
                pause++;
                if (pause == 50) end = true;
            }
        }
    }
        class Intro
        {
            private Texture2D mainimage;
            public bool end = false;
            private Texture2D pink,grey,green,red;
            private Rectangle pinkRec, greyRec, greenRec, redRec;
            private float timePerFrame;
            private ContentManager Content;
            private byte framesperec = 15;
            private byte pause=0;
            private bool chance = true;
            private float totalElapsed;
            private byte frames = 4;
            private byte numberOfFrame = 0;
            private Song snd;
            public Intro(ContentManager content)
            {
                Content = content;
                mainimage = content.Load<Texture2D>("title");
                pink = content.Load<Texture2D>("pink");
                grey = content.Load<Texture2D>("grey");
                green = content.Load<Texture2D>("green");
                red = content.Load<Texture2D>("red");
                snd= content.Load<Song>("intro");
                timePerFrame = (float)1 / framesperec;
                pinkRec = new Rectangle(0, 0, pink.Width / 4, pink.Height);
                greyRec = new Rectangle(0, 0, grey.Width / 4, grey.Height);
                greenRec = new Rectangle(0, 0, green.Width / 4, green.Height);
                redRec = new Rectangle(0, 0, red.Width / 4, red.Height);
            }
            private void ChangeFrame(float elpT)
            {
                    totalElapsed += elpT;
                    if (totalElapsed > timePerFrame)
                    {
                        if (numberOfFrame > frames-2) numberOfFrame = 0;else numberOfFrame++;
                        pinkRec = new Rectangle((int)pink.Width / frames * numberOfFrame, 0, pink.Width / frames, pink.Height);
                        greyRec = new Rectangle((int)grey.Width / frames * numberOfFrame, 0, grey.Width / frames, grey.Height);
                        greenRec = new Rectangle((int)green.Width / frames * numberOfFrame, 0, green.Width / frames, green.Height);
                        redRec = new Rectangle((int)red.Width / frames * numberOfFrame, 0, red.Width / frames, red.Height);
                        totalElapsed = 0;
                        if (numberOfFrame == 3)
                        {
                            MediaPlayer.Play(snd);
                            ChangePicts();
                        }
                }
            }
            private void ChangeFrame1(float elpT)
            {
                if (numberOfFrame > 0)
                {
                    totalElapsed += elpT;
                    if (totalElapsed > timePerFrame)
                    {
                        numberOfFrame--;
                        pinkRec = new Rectangle((int)pink.Width / frames * numberOfFrame, 0, pink.Width / frames, pink.Height);
                        greyRec = new Rectangle((int)grey.Width / frames * numberOfFrame, 0, grey.Width / frames, grey.Height);
                        greenRec = new Rectangle((int)green.Width / frames * numberOfFrame, 0, green.Width / frames, green.Height);
                        redRec = new Rectangle((int)red.Width / frames * numberOfFrame, 0, red.Width / frames, red.Height);
                        totalElapsed = 0;
                    }
                }
                else 
                {
                    pause++;
                    if (pause == 50) end = true;
                }
            }
            private void ChangePicts()
            {
                framesperec = 4;
                timePerFrame = (float)1 / framesperec;
                pink = Content.Load<Texture2D>("pink1");
                grey = Content.Load<Texture2D>("grey1");
                green = Content.Load<Texture2D>("green1");
                red = Content.Load<Texture2D>("red1");
                frames = 2;
                pinkRec = new Rectangle(0, 0, pink.Width / 2, pink.Height);
                greyRec = new Rectangle(0, 0, grey.Width / 2, grey.Height);
                greenRec = new Rectangle(0, 0, green.Width / 2, green.Height);
                redRec = new Rectangle(0, 0, red.Width / 2, red.Height);
            }
            private void ChangePictsBack()
            {
                framesperec = 15;
                timePerFrame = (float)1 / framesperec;
                pink = Content.Load<Texture2D>("pink");
                grey = Content.Load<Texture2D>("grey");
                green = Content.Load<Texture2D>("green");
                red = Content.Load<Texture2D>("red");
                frames = 4;
                pinkRec = new Rectangle(0, 0, pink.Width / 4, pink.Height);
                greyRec = new Rectangle(0, 0, grey.Width / 4, grey.Height);
                greenRec = new Rectangle(0, 0, green.Width / 4, green.Height);
                redRec = new Rectangle(0, 0, red.Width / 4, red.Height);
                numberOfFrame = 4;
            }
            public void Draw(SpriteBatch spritebatch, GameTime gameTime)
            {
                spritebatch.Draw(mainimage,new Vector2(0,0),Color.White);
                spritebatch.Draw(pink, new Vector2(20,170), pinkRec, Color.White, 0,Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                spritebatch.Draw(grey, new Vector2(90, 240), greyRec, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                spritebatch.Draw(red, new Vector2(530, 170), redRec, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                spritebatch.Draw(green, new Vector2(445, 240), greenRec, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                if ((MediaPlayer.State == MediaState.Stopped) && (frames == 2))
                {
                    chance = false;
                    ChangePictsBack();
                }
                if (chance) ChangeFrame((float)gameTime.ElapsedGameTime.TotalSeconds); else ChangeFrame1((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }
}
