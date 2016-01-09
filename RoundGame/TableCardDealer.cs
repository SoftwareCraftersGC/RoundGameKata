using System;
using System.Collections.Generic;
using System.Linq;

namespace RoundGame
{
    public class TableCardDealer
    {
        private const int MaxCardsOnTable = 4;
        private readonly Table _table;
        private readonly Deck _deck;
        private int _pointsToBeAdded;

        public TableCardDealer(Table table, Deck deck)
        {
            _table = table;
            _deck = deck;
            _pointsToBeAdded = 0;
        }

        public void PutCards()
        {
            var values = CardValues();
            for (var i = 0; i < MaxCardsOnTable; i++)
            {
                _table.AddCard(_deck.GetRandomCard());
                if (_table.Cards[i].Value == values[i]) _pointsToBeAdded += (int) values[i];
            }

            if (NoCardsAreRepeated()) _pointsToBeAdded += 1;
            else ReplaceRepeatedCards();
        }

        private void ReplaceRepeatedCards()
        {
            while (CardsAreRepeated())
            {
                for (var cardPos = 0; cardPos < _table.Cards.Count; cardPos++)
                {
                    var card = GetCardAt(cardPos);
                    ReplaceRepeated(card, cardPos);
                }
            }
        }

        private Card GetCardAt(int cardPos)
        {
            return _table.Cards[cardPos];
        }

        private void ReplaceRepeated(Card card, int cardPos)
        {
            for (var i = cardPos +1; i < _table.Cards.Count; i++)
            {
                if (card.Equals(_table.Cards[i])) ReplaceRepeatedCardAt(i);
            }
        }

        private void ReplaceRepeatedCardAt(int i)
        {
            _table.Cards[i] = _deck.GetRandomCard();
        }

        private static List<Value> CardValues()
        {
            return Enum.GetValues(typeof (Value)).Cast<Value>().ToList();
        }

        private bool CardsAreRepeated()
        {
            return _table.Cards.GroupBy(card => card)
                               .Any(group => group.Count() > 1);
        }

        private bool NoCardsAreRepeated()
        {
            return !CardsAreRepeated();
        }


        public int GetPoints()
        {
            return _pointsToBeAdded;
        }
    }
}