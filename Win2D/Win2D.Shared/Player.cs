using Microsoft.Graphics.Canvas;
using System.Collections.Generic;
using System.Collections;
using Windows.Foundation;

namespace Win2D
{
    class Player : IList<Card>
    {
        private int cash;
        private List<Card> cards = new List<Card>();
        public CanvasBitmap MoneyTexture { get; private set; }

        public List<Card> Cards
        {
            get
            {
                return cards;
            }
            set
            {
                cards = value;
            }
        }

        public Card CheckCard(Point position)
        {
            foreach(Card CurrentCard in cards)
            {
                if ((position.X > CurrentCard.Position.X) && (position.X < CurrentCard.Position.X + 83 * Parameters.Scale) && (position.Y > CurrentCard.Position.Y) && (position.Y < CurrentCard.Position.Y + 89 * Parameters.Scale)) return CurrentCard;
            }
            return null;
        }

        public int Cash
        {
            get { return cash; }
            set
            {
                cash = value;
                //нарисовать картинку
            }
        }

        public Card this[int index]
        {
            get
            {
                return cards[index];
            }

            set
            {
                cards[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return cards.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return IsReadOnly;
            }
        }

        public void Add(Card item)
        {
            cards.Add(item);
        }

        public void Clear()
        {
            cards.Clear();
        }

        public bool Contains(Card item)
        {
            return cards.Contains(item);
        }

        public void CopyTo(Card[] array, int arrayIndex)
        {
            cards.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return cards.GetEnumerator();
        }

        public int IndexOf(Card item)
        {
            return IndexOf(item);
        }

        public void Insert(int index, Card item)
        {
            cards.Insert(index, item);
        }

        public bool Remove(Card item)
        {
            return Remove(item);
        }

        public void RemoveAt(int index)
        {
            cards.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Draw()
        {

        }
    }
}