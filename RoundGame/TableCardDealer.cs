using System;
using System.Collections.Generic;
using System.Linq;

namespace RoundGame
{
    public class TableCardDealer
    {
        private const int MaxCardsOnTable = 4;
		private Table Table { get; set; }
        private Deck Deck { get; }

		public TableCardDealer(Deck deck)
		{
			Deck = deck;
		}

		public Table ArrangeTable()
        {
			Table = new Table();
            PlaceCards();
			if (NoCardsAreRepeated()) Table.Points += 1;
            else ReplaceRepeatedCards();
			return Table;
        }

	    private void PlaceCards()
	    {
		    var values = CardValues();
		    for (var i = 0; i < MaxCardsOnTable; i++)
		    {
			    Table.Put(Deck.GetRandomCard());
			    if (Table.Cards[i].Value == values[i]) Table.Points += (int) values[i];
		    }
	    }

	    private void ReplaceRepeatedCards()
        {
            while (CardsAreRepeated())
            {
                for (var cardPos = 0; cardPos < Table.Cards.Count; cardPos++)
                {
                    var card = GetCardAt(cardPos);
                    ReplaceRepeated(card, cardPos);
                }
            }
        }

        private Card GetCardAt(int cardPos)
        {
            return Table.Cards[cardPos];
        }

        private void ReplaceRepeated(Card card, int cardPos)
        {
            for (var i = cardPos +1; i < Table.Cards.Count; i++)
                if (card.Equals(Table.Cards[i])) ReplaceRepeatedCardAt(i);
        }

        private void ReplaceRepeatedCardAt(int i)
        {
            Table.Cards[i] = Deck.GetRandomCard();
        }

        private static List<Value> CardValues()
        {
            return Enum.GetValues(typeof (Value)).Cast<Value>().ToList();
        }

        private bool CardsAreRepeated()
        {
            return Table.Cards.GroupBy(card => card)
                               .Any(group => group.Count() > 1);
        }

        private bool NoCardsAreRepeated()
        {
            return !CardsAreRepeated();
        }

        public int GetPoints()
        {
            return Table.Points;
        }
    }
}