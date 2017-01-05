using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;

namespace Win2D
{
    class MainTable
    {
        public int bank { get; set; }
        public Boss MyBoss { get; set; }
        public UIPlayer player1 { get; set; }
        public UIPlayer player2 { get; set; }
        private CanvasBitmap Table;
        private List<Candle> candles = new List<Candle>();
        private float timepersecond;
        private float totalElapsed;
        private float elpT;
        private Player player = new Player();
        private double abc;
        private Point pointercoord;
        private Card[,] boardcards = new Card[4, 9];

        public MainTable(CanvasBitmap Candle, CanvasBitmap table)
        {
            Table = table;
            candles.Add(new Candle(3, new Point(149, 12)) { Texture = Candle });
            candles.Add(new Candle(2, new Point(164, 6)) { Texture = Candle });
            candles.Add(new Candle(1, new Point(179, 12)) { Texture = Candle });
            candles.Add(new Candle(0, new Point(451, 12)) { Texture = Candle });
            candles.Add(new Candle(3, new Point(466, 6)) { Texture = Candle });
            candles.Add(new Candle(2, new Point(481, 12)) { Texture = Candle });
            timepersecond = (float)1 / 5;
            DealCards();
        }

        private bool CheckCard(Card card1)
        {
            if (boardcards[2, 3] != null)
            {
                if (card1.Number < 3)
                {
                    if (boardcards[card1.ColorCard, card1.Number + 1] != null)
                    {
                        return true;
                    }
                }
                else if (card1.Number > 3)
                {
                    if (boardcards[card1.ColorCard, card1.Number - 1] != null)
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else if ((card1.ColorCard == 2) && (card1.Number == 3))
            {
                return true;
            }
            return false;
        }

        public Point PointerCoordinates
        {
            get
            {
                return pointercoord;
            }
            set
            {
                pointercoord = value;
                foreach (Card card1 in player)
                {
                    int i = player.Where(x => x.ColorCard == card1.ColorCard && x.Number > card1.Number).Count();
                    if ((pointercoord.X > card1.Position.X) && (pointercoord.X < card1.Position.X + ((i != 0) ? 23 : 84) * Parameters.Scale) && (pointercoord.Y > card1.Position.Y) && (pointercoord.X < card1.Position.X + 89 * Parameters.Scale))
                    {
                        if (CheckCard(card1))
                        {
                            player.Cards.Remove(card1);
                            boardcards[card1.ColorCard, card1.Number] = new Card() { ColorCard = card1.ColorCard, Number = card1.Number, Position = new Point((144 + (39.2 * card1.Number)) * Parameters.Scale, (70.6 + (38.2 * card1.ColorCard)) * Parameters.Scale) };
                        }
                        
                        break;
                    }
                }
            }
        }

        public void DealCards()
        {
            abc = Parameters.centerScreen.Y - (((25 * Parameters.Scale * (12 - 4)) + (83 * Parameters.Scale * (4)) + (15 * Parameters.Scale * (4 - 1))) / 2);
            Point pos = new Point(abc, 250 * Parameters.Scale);
            for (int i = 3; i < 6; i++)
            {
                player.Add(new Card() { ColorCard = 0, Number = i, Position = pos });
                pos.X += 25 * Parameters.Scale;
            }
            pos.X = pos.X + 75 * Parameters.Scale;
            for (int i = 1; i < 4; i++)
            {
                player.Add(new Card() { ColorCard = 1, Number = i, Position = pos });
                pos.X += 25 * Parameters.Scale;
            }
            pos.X = pos.X + 75 * Parameters.Scale;
            for (int i = 3; i < 6; i++)
            {
                player.Add(new Card() { ColorCard = 2, Number = i, Position = pos });
                pos.X += 25 * Parameters.Scale;
            }
            pos.X = pos.X + 75 * Parameters.Scale;
            for (int i = 2; i < 5; i++)
            {
                player.Add(new Card() { ColorCard = 3, Number = i, Position = pos });
                pos.X += 25 * Parameters.Scale;
            }
            //   int countcolors = player.Select(x => x.ColorCard).Distinct().Count();
            //  abc = 15 * (player.Count - countcolors) + countcolors * 80;
        }

        public void ChangeCardsPosition()
        {
            for (byte i = 0; i < player.Count; i++)
            {

            }
        }

        public void Draw(CanvasDrawingSession session, TimeSpan time)
        {
            session.DrawImage(Table, new Rect(Parameters.position.X, Parameters.position.Y, Table.Size.Width * Parameters.Scale, Table.Size.Height * Parameters.Scale), new Rect(0, 0, Table.Size.Width, Table.Size.Height), 1f, CanvasImageInterpolation.Anisotropic);

            elpT = (float)time.TotalSeconds;
            totalElapsed += elpT;
            foreach (var item in candles)
            {
                session.DrawImage(item.Texture, new Rect(Parameters.position.X + item.position.X * Parameters.Scale, Parameters.position.Y + item.position.Y * Parameters.Scale, item.Texture.Size.Width / 4 * Parameters.Scale, item.Texture.Size.Height * Parameters.Scale), new Rect(item.Texture.Size.Width / 4 * item.Frame, 0, item.Texture.Size.Width / 4, item.Texture.Size.Height), 1f, CanvasImageInterpolation.Anisotropic);
            }

            if (totalElapsed > timepersecond)
            {
                foreach (var item in candles)
                {
                    item.ChangeFrame();
                }
                totalElapsed = 0;
            }
            foreach(var i in player)
            {
                session.DrawImage(Textures.CardsTexture, new Rect(Parameters.position.X + i.Position.X, Parameters.position.Y + i.Position.Y, Textures.CardsTexture.Size.Width / 9 * Parameters.Scale, Textures.CardsTexture.Size.Height / 4 * Parameters.Scale), new Rect(Textures.CardsTexture.Size.Width / 9 * i.Number, Textures.CardsTexture.Size.Height / 4 * i.ColorCard, Textures.CardsTexture.Size.Width / 9, Textures.CardsTexture.Size.Height / 4), 1f, CanvasImageInterpolation.Anisotropic);
            }
            foreach (var i in boardcards)
            {
                if (i != null)
                    session.DrawImage(Textures.BoardCardsTexture, new Rect(Parameters.position.X + i.Position.X, Parameters.position.Y + i.Position.Y, Textures.BoardCardsTexture.Size.Width / 4 * Parameters.Scale, Textures.BoardCardsTexture.Size.Height / 9 * Parameters.Scale), new Rect(Textures.BoardCardsTexture.Size.Width / 4 * i.ColorCard, Textures.BoardCardsTexture.Size.Height / 9 * i.Number, Textures.BoardCardsTexture.Size.Width / 4, Textures.BoardCardsTexture.Size.Height / 9), 1f, CanvasImageInterpolation.Anisotropic);
            }
        }
    }
}