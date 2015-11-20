namespace RoundGame
{
	public class Card
	{
		public Card(Suit suit, Value value)
		{
			Suit = suit;
			Value = value;
		}

		public Suit Suit { get; }
		public Value Value { get; }
	}
}