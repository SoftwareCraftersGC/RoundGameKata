using System.Collections.Generic;

namespace RoundGame
{
    public class Table
    {
        public List<Card> Cards { get; }
		public int Points { get; set; }

        public Table()
        {
            Cards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }
    }
}