using System.Collections.Generic;
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
	public class GameStartShould
	{
		[Test]
		public void no_points_are_added_if_first_card_is_not_an_ace()
		{
			var deck = Substitute.For<Deck>();
			deck.GetRandomCard().Returns(new Card(Suit.Spades, Value.Two));

			var table = new Table();
			var tableCardDealer = new TableCardDealer(table, deck);
			tableCardDealer.PutCard();

			tableCardDealer.GetPoints().Should().Be(0);
		}
	}

	public class TableCardDealer
	{
		private readonly Table _table;
		private readonly Deck _deck;
		private int _pointsToBeAdded;

		public TableCardDealer(Table table, Deck deck)
		{
			_table = table;
			_deck = deck;
			_pointsToBeAdded = 0;
		}

		public void PutCard()
		{
			_table.AddCard(_deck.GetRandomCard());
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