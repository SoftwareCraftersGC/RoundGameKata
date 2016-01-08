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
            var values = cardValues();
            for (int i = 0; i < MaxCardsOnTable; i++)
            {
                _table.AddCard(_deck.GetRandomCard());
                if (_table.Cards[i].Value == values[i]) _pointsToBeAdded += (int) values[i];
            }

            if (NoCardsAreRepeated()) _pointsToBeAdded += 1;
        }

        private static List<Value> cardValues()
        {
            return Enum.GetValues(typeof (Value)).Cast<Value>().ToList();
        }

        private bool NoCardsAreRepeated()
        {
            var previousCard = _table.Cards[0];

            for (var i = 1; i < _table.Cards.Count; i++)
            {
                var currentCard = _table.Cards[i];
                if (previousCard.Equals(currentCard)) return false;
                previousCard = currentCard;
            }

            return true;
        }


        public int GetPoints()
        {
            return _pointsToBeAdded;
        }
    }
}