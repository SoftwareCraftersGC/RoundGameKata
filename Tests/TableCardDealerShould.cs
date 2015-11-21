using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using RoundGame;


namespace Tests
{

	/* TODO
	Se ponen 4 cartas sobre el tablero. Se quita la ultima carta repetida 
	hasta que no coincida ninguna sobre el tablero.
	 */

	/* Possible Tests
	Put first card =! 1 => card is put and no points are added
	Put first card == 1 => card is put and a point is added

	Put second card != 2 => card is put and no points are added
	Put second card == 2 => card is put and 2 points are added

	...[repeat]

	Put fourth card && there is a repeated card, last card of the repeated ones is replaced with another one.
	Put fourth card && there is more than a repeated card, replace the last ones repeated and replace with others.

	If no cards are repeated, a point is given.
	*/

	[TestFixture]
	public class TableCardDealerShould
	{
		[Test]
		public void add_no_points_if_first_card_is_not_an_ace()
		{
			var deck = Substitute.For<Deck>();
			deck.GetRandomCard().Returns(new Card(Suit.Spades, Value.King));

			var table = new Table();
			var tableCardDealer = new TableCardDealer(table, deck);
			tableCardDealer.PutCards();

			tableCardDealer.GetPoints().Should().Be(0);
		}

		[Test]
		public void add_one_point_when_an_ace_is_the_first_card_on_the_table()
		{
			var deck = Substitute.For<Deck>();
			deck.GetRandomCard().Returns(new Card(Suit.Spades, Value.Ace));

			var table = new Table();
			var tableCardDealer = new TableCardDealer(table, deck);
			tableCardDealer.PutCards();

			tableCardDealer.GetPoints().Should().Be(1);
		}

		[Test]
		public void add_two_points_if_second_card_value_is_two()
		{
			var deck = Substitute.For<Deck>();
			deck.GetRandomCard().Returns(new Card(Suit.Spades, Value.King), new Card(Suit.Spades, Value.Two));

			var table = new Table();
			var tableCardDealer = new TableCardDealer(table, deck);
			tableCardDealer.PutCards();

			tableCardDealer.GetPoints().Should().Be(2);
		}
	}

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
			var values = Enum.GetValues(typeof (Value)).Cast<Value>().ToList();
			for (int i = 0; i < MaxCardsOnTable; i++)
			{
				_table.AddCard(_deck.GetRandomCard());
				if (_table.Cards[i].Value == values[i]) _pointsToBeAdded += (int) values[i];
			}
		}


		public int GetPoints()
		{
			return _pointsToBeAdded;
		}
	}

	public class Table
	{
		public List<Card> Cards { get; }

		public Table()
		{
			Cards = new List<Card>();
		}

		public void AddCard(Card card)
		{
			Cards.Add(card);
		}
	}

	public class Deck
	{
		public virtual Card GetRandomCard()
		{
			return null;
		}
	}
}