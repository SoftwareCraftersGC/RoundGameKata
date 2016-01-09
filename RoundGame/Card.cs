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

		protected bool Equals(Card other)
		{
			return Value == other.Value;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Card) obj);
		}

		public override int GetHashCode()
		{
			return (int) Value;
		}
	}
}