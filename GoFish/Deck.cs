using System;
using System.Collections.ObjectModel;

namespace GoFish
{
    public class Deck : List<Card>
    {
        private static Random random = Player.Random;
        public Deck()
        {
            Reset();
        }
        public void Reset()
        {
            Clear();
            foreach (Suits suit in Enum.GetValues(typeof(Suits)))
            {
                foreach(Values value in Enum.GetValues(typeof(Values)))
                {
                    Add(new Card(value, suit));
                }
            }

        }

        public Card Deal(int index)
        {
            Card cardToDeal = base[index];
            RemoveAt(index);
            return cardToDeal;
        }

        public Deck Shuffle()
        {
            List<Card> anotherDeckCards = new List<Card>(this);
            Clear();
            while (anotherDeckCards.Count > 0)
            {
                Card pickedCard = anotherDeckCards.ElementAt(random.Next(anotherDeckCards.Count));
                anotherDeckCards.Remove(pickedCard);
                Add(pickedCard);
            }
            return this;
        }

        public void Sort()
        {
            List<Card> sortedCards = new List<Card>(this);
            sortedCards.Sort(new CardComparerByValue());
            Clear();
            foreach(Card card in sortedCards)
            {
                Add(card);
            }
        }

    }
}
