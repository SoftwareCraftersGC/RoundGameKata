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
            else
            {
                while (CardsAreRepeated()) ReplaceRepeatedCards();
            }
        }

        private void ReplaceRepeatedCards()
        {
            for (var j = 0; j < _table.Cards.Count; j++)
            {
                var card = _table.Cards[j];
                var repeated = 0;
                for (var i = 0; i < _table.Cards.Count; i++)
                {
                    if (card.Equals(_table.Cards[i])) repeated += repeated == 1 ? 2 : 1;
                    if (repeated > 1) _table.Cards[i] = _deck.GetRandomCard();
                }

            }
        }

        private static List<Value> CardValues()
        {
            return Enum.GetValues(typeof (Value)).Cast<Value>().ToList();
        }

        private bool CardsAreRepeated()
        {
            foreach (var currentCard in _table.Cards)
            {
                var repeated = 0;
                repeated += _table.Cards.Count(t => currentCard.Equals(t));
                if (repeated > 1) return true;
            }
            return false;
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