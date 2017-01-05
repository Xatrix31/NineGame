using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace XNA
{
    class Player
    {
        private Texture2D player;
        private Texture2D[] gettincard;
        private Texture2D thinkingPlayer;
        private Card[] cards;
        private Money money;
        private Vector2 startposition;
        private Texture2D sleeppingPlayer;
        private ContentManager content;
        public bool isthinking = false;
        public bool issleeping = false;
        private bool isgettingcard = false;
        private float timePerFrame;
        private Vector2 position;
        private byte numberofhiddencard;
        private float totalElapsed;
        private byte numberOfFrame = 0;
        private bool isr = true;
        private Rectangle rect;
        public void ChangeMoney(byte m)
        {
            money.ChangeMoney(m);
        }
        public void TakeCard(byte number,byte color,byte dignity)
        {
            cards[number].colorcard=color;
            cards[number].dignity = dignity;
        }
        public void MakeDecision(byte card)
        {
            isgettingcard = true;
            if (((card >= 1) && (card <= 3)) || (card >= 7) && (card <= 9))
            {
                gettincard[0] = content.Load<Texture2D>("right");
                if ((card == 2)|| (card==8)) gettincard[1] = content.Load<Texture2D>("getr"); else gettincard[1] = content.Load<Texture2D>("right");
                gettincard[2] = content.Load<Texture2D>("get"+card.ToString());
                gettincard[3] = content.Load<Texture2D>("lookright");
                if (isr)
                {
                    gettincard[4] = content.Load<Texture2D>("getrl");
                }
                else gettincard[4] = content.Load<Texture2D>("getrr");
            } else
            if (((card >= 4) && (card <= 6)) || (card >= 10) && (card <= 12))
            {
                gettincard[0] = content.Load<Texture2D>("left");
                if ((card == 5) || (card == 11)) gettincard[1] = content.Load<Texture2D>("getl"); else gettincard[1] = content.Load<Texture2D>("left");
                gettincard[2] = content.Load<Texture2D>("get" + card.ToString());
                gettincard[3] = content.Load<Texture2D>("lookleft");
                if (isr)
                {
                    gettincard[4] = content.Load<Texture2D>("getll");
                }
                else gettincard[4] = content.Load<Texture2D>("getrr");
            }
            numberofhiddencard = card;
        }
        public void SetCard(byte number,byte color, byte dignity)
        {
            cards[number].Setcard(color, dignity);
        }
        private void ChangeFrameForGetting(float elpT)
        {
            totalElapsed += elpT;
            if (totalElapsed > timePerFrame)
            {
                if (numberOfFrame >= 4)
                {
                    isgettingcard = false;
                }
                else numberOfFrame++;
                if (numberOfFrame == 3) cards[numberofhiddencard - 1].Visible = false;
                rect = new Rectangle(0,0, gettincard[numberOfFrame].Width, gettincard[numberOfFrame].Height);
                totalElapsed = 0;
            }
        }
        private void ChangeFrameForThink(float elpT, byte frames)
        {
            totalElapsed += elpT;
            if (totalElapsed > timePerFrame)
            {
                if (numberOfFrame >= frames-1)
                {
                    isthinking = false;
                    numberOfFrame = 0;
                    rect = new Rectangle(0, 0, gettincard[0].Width, gettincard[0].Height);
                    isgettingcard = true;
                }
                else numberOfFrame++;
                rect = new Rectangle(0, (int)thinkingPlayer.Height / frames * numberOfFrame, thinkingPlayer.Width, thinkingPlayer.Height / frames);
                totalElapsed = 0;
            }
        }
        public Player(ContentManager Content, bool isright)
        {
            content = Content;
            isr = isright;
            if (isright) position = new Vector2(9, 100); else position = new Vector2(520, 100);
            money = new Money(Content, new Vector2((int)position.X + 60,188));
            money.SetMoney = 200;
            gettincard =new Texture2D[5];
            timePerFrame = (float)1 / 8;
            thinkingPlayer = Content.Load<Texture2D>("thinking");
            sleeppingPlayer = Content.Load<Texture2D>("sleep");
            player = Content.Load<Texture2D>("player");
            startposition = new Vector2(position.X + 5, position.Y + 45);
            cards = new Card[12];
            for (byte i = 0; i < 6; i++)
            {
                cards[i] = new Card(Content, "closedcard", startposition);
                cards[i].Visible = true;
                startposition.X += 17;
            }
            startposition.X -= 102;
            startposition.Y += 18;
            for (byte i = 6; i < 12; i++)
            {
                cards[i] = new Card(Content, "closedcard", startposition);
                cards[i].Visible = true;
                startposition.X += 17;
            }

            //cards = new ClosedCards(Content,new Vector2(position.X+5,position.Y+45));
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            if (isthinking)
            {
                spriteBatch.Draw(thinkingPlayer, new Vector2(position.X+6,position.Y+1), rect, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                ChangeFrameForThink((float)gametime.ElapsedGameTime.TotalSeconds,15);
            } else
                if (isgettingcard)
                {
                    spriteBatch.Draw(gettincard[numberOfFrame], position, rect, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                    ChangeFrameForGetting((float)gametime.ElapsedGameTime.TotalSeconds);
                }
                else spriteBatch.Draw(player, position,Color.White);
            foreach (Card tex in cards) tex.Draw(spriteBatch);
            money.Draw(spriteBatch);
        }
    }

    class Human
    {
        public byte EspectedColor;
        public byte EspectedDignity;
        private Card[] cards;
        private Money money;
        private Vector2 tempposition;
        public Human(ContentManager content,Vector2 position)
        {
            money = new Money(content, position);
            money.SetMoney = 200;
            cards = new Card[12];
            tempposition = position;
        }
        public void TakeCard(byte number,Card card)
        {
            cards[number] = card;
            cards[number].Visible = true;

        }
        public void SortCard()
        {
            byte temp = 0, count = 0;
            Card card;
            for (byte a=1;a<5;a++)
            {
                for (byte b=0;b<12;b++)
                    if (cards[b].colorcard==a)
                    {
                        card = cards[temp];
                        cards[temp] = cards[b];
                        cards[b] = card;
                        temp++;
                        count++;
                    }
                for (byte b = (byte)(temp - count); b < temp; b++)
                {
                    for (byte c = (byte)(temp - count); c < temp; c++)
                    {
                        if (cards[b].dignity < cards[c].dignity)
                        {
                            card = cards[b];
                            cards[b] = cards[c];
                            cards[c] = card;

                        }
                    }
                }
                count = 0;
            }
            for (byte b=0;b<12;b++)
            {
                if ((b > 0) && (cards[b].colorcard != cards[b - 1].colorcard)) tempposition.X += 80;
                cards[b].ChangePosition(new Vector2(tempposition.X - 262, tempposition.Y + 17));
                tempposition.X += 16;
            }
        }
        public Card CheckPosition(int x, int y)
        {
            for (sbyte i = 11; i >=0; i--)
            {
                if ((cards[i].Visible) && (x > cards[i].GetPosition.X) && (x < cards[i].GetPosition.X + 83) && (y > cards[i].GetPosition.Y) && (y < cards[i].GetPosition.Y + 89))
                {
                    return cards[i];
                }
            }
            return null;
        }
        public void ChangeMoney(byte m)
        {
            money.ChangeMoney(m);
        }
        public void SelectCard(byte color, byte dignity)
        {
            foreach (Card tex in cards)
            {
                if ((tex.colorcard == color) && (tex.dignity == dignity)) tex.Visible = false;
            }
        }
        public void Draw(SpriteBatch spritebatch)
        {
            money.Draw(spritebatch);
            foreach (Card tex in cards) tex.Draw(spritebatch);
        }
    }

    class Boss
    {
        private Texture2D boss;
        private Rectangle rect;
        private Rectangle rect1;
        private Texture2D g;
        private Texture2D givel;
        private Texture2D lookd;
        private Texture2D giver;
        private Texture2D gived;
        public bool give = false;
        public bool giveleft = false;
        public bool lookdown = false;
        public bool giveright = false;
        public bool givedown = false;
        private float timePerFrame;
        private float totalElapsed;
        private byte numberOfFrame = 0;
        public Boss(ContentManager Content)
        {
            boss = Content.Load<Texture2D>("boss");
            rect = new Rectangle(0, 0, boss.Width, boss.Height);
            g = Content.Load<Texture2D>("bossgive");
            givel = Content.Load<Texture2D>("bossgiveleft");
            giver = Content.Load<Texture2D>("bossgiveright");
            gived = Content.Load<Texture2D>("bossgivedown");
            lookd = Content.Load<Texture2D>("bosslookdown");
            timePerFrame = (float)1 / 6;
        }
        private void ChangeFrame(float elpT, byte frames, Texture2D texture)
        {
            totalElapsed += elpT;
            if (totalElapsed > timePerFrame)
            {
                if (numberOfFrame >= frames - 1)
                {
                    lookdown = false;
                    timePerFrame = (float)1 / 6;
                    give = false;
                    giveleft = false;
                    giveright = false;
                    givedown = false;
                    numberOfFrame = 0;
                }
                else numberOfFrame++;
                rect1 = new Rectangle((int)texture.Width / frames * numberOfFrame, 0, texture.Width / frames, texture.Height);
                totalElapsed = 0;
            }
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            if (give)
            {
                spriteBatch.Draw(g, new Vector2(278, 20), rect1, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                ChangeFrame((float)gametime.ElapsedGameTime.TotalSeconds, 4, g);
            }
            else
                if (giveleft)
                {
                    spriteBatch.Draw(givel, new Vector2(278, 3), rect1, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                    ChangeFrame((float)gametime.ElapsedGameTime.TotalSeconds, 6, givel);
                }
            else
                if (giveright)
                {
                    spriteBatch.Draw(giver, new Vector2(273, 20), rect1, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                    ChangeFrame((float)gametime.ElapsedGameTime.TotalSeconds, 6, giver);
                }
            else
                if (givedown)
                {
                    spriteBatch.Draw(gived, new Vector2(278, 20), rect1, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                    ChangeFrame((float)gametime.ElapsedGameTime.TotalSeconds, 5, gived);
                }
            else if (lookdown)
                {
                    timePerFrame = 1;
                    spriteBatch.Draw(boss, new Vector2(290, 20), rect, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                    spriteBatch.Draw(lookd, new Vector2(280, 20), rect1, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                    ChangeFrame((float)gametime.ElapsedGameTime.TotalSeconds, 2, lookd);
                }
            else                    
                spriteBatch.Draw(boss, new Vector2(290, 20), rect, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
        }
    }
}
