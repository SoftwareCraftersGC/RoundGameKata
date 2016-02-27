using FluentAssertions;
using NUnit.Framework;
using RoundGame;


/* TODO
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
		public void have_0_points_for_a_non_special_hand()
		{
			var hand = new Hand(new Card(Suit.Clubs, Value.Ace),
								new Card(Suit.Spades, Value.Three),
								new Card(Suit.Hearts, Value.Five));
			hand.Points.Should().Be(0);
		}

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

		[Test]
		public void have_3_points_for_a_caracol()
		{
			var hand = new Hand(new Card(Suit.Clubs, Value.Five),
								new Card(Suit.Spades, Value.Six),
								new Card(Suit.Hearts, Value.Seven));
			hand.Points.Should().Be(3);
		}
	}

	public class Hand
	{
		private Card First { get; }
		private Card Second { get; }
		private Card Third { get; }
		public int Points => CalculatePoints();

		private int CalculatePoints()
		{
			if (First.Equals(Second) && Second.Equals(Third))
				return 2;

			if (First.Value + 1 == Second.Value && Second.Value + 1 == Third.Value)
				return 3;

			if (First.Equals(Second) || First.Equals(Third) || Second.Equals(Third))
				return 1;

			return 0;
		}

		public Hand(Card first, Card second, Card third)
		{
			First = first;
			Second = second;
			Third = third;
		}
	}
}
