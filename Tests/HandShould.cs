using FluentAssertions;
using NUnit.Framework;
using RoundGame;


/* TODO
	- Ronda hand should be 1 point
	- Parranda hand should be 2 points
	- Caracol hand should be 3 points
	- Caracolillo hand should be 4 points
	- Royal Ronda hand should be 2 points
	- Royal Parranda hand should be 3 points
	- Royal Caracol hand should be 4 points
	- Royal Caracolillo hand should be 5 points
*/

namespace Tests
{
	[TestFixture]
	public class HandShould
	{
		[Test]
		public void have_1_point_for_a_ronda()
		{
			var first = new Card(Suit.Clubs, Value.Ace);
			var second = new Card(Suit.Clubs, Value.Ace);
			var third = new Card(Suit.Spades, Value.King);
			var hand = new Hand(first, second, third);
			hand.Points.Should().Be(1);
		}

		[Test]
		public void have_2_points_for_a_parranda()
		{
			var hand = new Hand(new Card(Suit.Clubs, Value.Five),
								new Card(Suit.Spades, Value.Five),
								new Card(Suit.Hearts, Value.Five));
			hand.Points.Should().Be(2);
		}
	}

	public class Hand
	{
		private Card First { get; }
		private Card Second { get; }
		private Card Third { get; }
		public int Points => First.Equals(Second) && Second.Equals(Third) ? 2 : 1;

		public Hand(Card first, Card second, Card third)
		{
			First = first;
			Second = second;
			Third = third;
		}
	}
}
