using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace XNA
{
    class Bank
    {
        public int money=0;
        private int prevmoney = 0;
        private int position=469;
        private StaticSprite[] numbers;
        public Bank(ContentManager Content)
        {
            numbers=new StaticSprite[4];
            numbers[0] = new StaticSprite(Content, "numbers1");
            numbers[1] = new StaticSprite(Content, "numbers1");
            numbers[2] = new StaticSprite(Content, "numbers1");
            numbers[3] = new StaticSprite(Content, "numbers1");
            CalcPosition();
        }
        public void ClearAll()
        {
            foreach (StaticSprite num in numbers) num.Visible = false;
        }
        private void CalcPosition()
        {
            prevmoney = money;
            string mon;
            mon = money.ToString();
            foreach (StaticSprite num in numbers) num.Visible = false;
            switch (mon.Count())
            {
                case 1:
                    {
                        numbers[0].ChangeRect = new Rectangle(0, 5 * Int16.Parse(mon.First().ToString()), 8, 5);
                        numbers[0].ChangePosition(new Vector2(position-7, 51));
                        numbers[0].Visible = true;
                        numbers[1].ChangeRect = new Rectangle(0, 50, 8, 5);
                        numbers[1].ChangePosition(new Vector2(position+1, 51));
                        numbers[1].Visible = true;
                        break;
                    }
                case 2:
                    {
                        numbers[0].ChangeRect = new Rectangle(0, 5 * Int16.Parse(mon.First().ToString()), 8, 5);
                        numbers[0].ChangePosition(new Vector2(position-11, 51));
                        numbers[0].Visible = true;
                        numbers[1].ChangeRect = new Rectangle(0, 5 * Int16.Parse(mon[1].ToString()), 8, 5);
                        numbers[1].ChangePosition(new Vector2(position -3, 51));
                        numbers[1].Visible = true;
                        numbers[2].ChangeRect = new Rectangle(0, 50, 8, 5);
                        numbers[2].ChangePosition(new Vector2(position + 5, 51));
                        numbers[2].Visible = true;
                        break;
                    }
                case 3:
                    {
                        numbers[0].ChangeRect = new Rectangle(0, 5 * Int16.Parse(mon.First().ToString()), 8, 5);
                        numbers[0].ChangePosition(new Vector2(position-15, 51));
                        numbers[0].Visible = true;
                        numbers[1].ChangeRect = new Rectangle(0, 5 * Int16.Parse(mon[1].ToString()), 8, 5);
                        numbers[1].ChangePosition(new Vector2(position - 7, 51));
                        numbers[1].Visible = true;
                        numbers[2].ChangeRect = new Rectangle(0, 5 * Int16.Parse(mon[2].ToString()), 8, 5);
                        numbers[2].ChangePosition(new Vector2(position + 1, 51));
                        numbers[2].Visible = true;
                        numbers[3].ChangeRect = new Rectangle(0, 50, 8, 5);
                        numbers[3].ChangePosition(new Vector2(position + 9, 51));
                        numbers[3].Visible = true;
                        break;
                    }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (StaticSprite num in numbers) num.Draw(spriteBatch);
            if (prevmoney!=money) CalcPosition();
        }
    }

    class Money
    {
        private int money=200;
        private int prevmoney = 0;
        private Vector2 position;
        private StaticSprite[] numbers;
        public Money(ContentManager Content,Vector2 pos)
        {
            position = pos;
            numbers=new StaticSprite[4];
            numbers[0] = new StaticSprite(Content, "numbers");
            numbers[1] = new StaticSprite(Content, "numbers");
            numbers[2] = new StaticSprite(Content, "numbers");
            numbers[3] = new StaticSprite(Content, "numbers");
            CalcPosition();
        }
        public int SetMoney
        {
            set { money = value; }
        }
        public void ChangeMoney(byte mon)
        {
            money += mon;
        }
        public void ClearAll()
        {
            foreach (StaticSprite num in numbers) num.Visible = false;
        }
        private void CalcPosition()
        {
            prevmoney = money;
            string mon;
            mon = money.ToString();
            foreach (StaticSprite num in numbers) num.Visible = false;
            switch (mon.Count())
            {
                case 2:
                    {
                        numbers[0].ChangeRect = new Rectangle(0, 7 * Int16.Parse(mon.First().ToString()), 8, 7);
                        numbers[0].ChangePosition(new Vector2(position.X - 11, position.Y));
                        numbers[0].Visible = true;
                        numbers[1].ChangeRect = new Rectangle(0, 7 * Int16.Parse(mon[1].ToString()), 8, 7);
                        numbers[1].ChangePosition(new Vector2(position.X - 3, position.Y));
                        numbers[1].Visible = true;
                        numbers[2].ChangeRect = new Rectangle(0, 70, 8, 7);
                        numbers[2].ChangePosition(new Vector2(position.X + 9, position.Y-1));
                        numbers[2].Visible = true;
                        break;
                    }
                case 3:
                    {
                        if (mon.First() != '-')
                        {
                            numbers[0].ChangeRect = new Rectangle(0, 7 * Int16.Parse(mon.First().ToString()), 8, 7);
                            numbers[0].ChangePosition(new Vector2(position.X - 25, position.Y));
                        }
                        else
                        {
                            numbers[0].ChangeRect = new Rectangle(0, 79, 8, 7);
                            numbers[0].ChangePosition(new Vector2(position.X - 25, position.Y+2));
                        }
                        numbers[0].Visible = true;
                        numbers[1].ChangeRect = new Rectangle(0, 7 * Int16.Parse(mon[1].ToString()), 8, 7);
                        numbers[1].ChangePosition(new Vector2(position.X - 16, position.Y));
                        numbers[1].Visible = true;
                        numbers[2].ChangeRect = new Rectangle(0, 7 * Int16.Parse(mon[2].ToString()), 8, 7);
                        numbers[2].ChangePosition(new Vector2(position.X - 7, position.Y));
                        numbers[2].Visible = true;
                        numbers[3].ChangeRect = new Rectangle(0, 70, 8, 9);
                        numbers[3].ChangePosition(new Vector2(position.X + 4, position.Y-1));
                        numbers[3].Visible = true;
                        break;
                    }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (StaticSprite num in numbers) num.Draw(spriteBatch);
            if (prevmoney!=money) CalcPosition();
        }
    }
}
