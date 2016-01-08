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
        private TableCardDealer _tableCardDealer;
        private Table _table;
        private Deck _deck;

        [SetUp]
        public void Initialize()
        {
            _deck = Substitute.For<Deck>();
            _table = new Table();
            _tableCardDealer = new TableCardDealer(_table, _deck);
        }

        [Test]
        public void add_a_point_when_no_card_is_repeated_over_the_table()
        {
            _deck.GetRandomCard().Returns(new Card(Suit.Spades, Value.King),
                new Card(Suit.Spades, Value.Queen),
                new Card(Suit.Spades, Value.Knave),
                new Card(Suit.Spades, Value.Seven));

            _tableCardDealer.PutCards();

            _tableCardDealer.GetPoints().Should().Be(1);
        }

        [Test]
        public void add_points_based_on_card_value_if_the_value_corresponds_to_its_emplacement_order_and_an_extra_one_for_no_repeated_cards()
        {
            _deck.GetRandomCard().Returns(new Card(Suit.Spades, Value.Ace),
                new Card(Suit.Spades, Value.Two),
                new Card(Suit.Spades, Value.Three),
                new Card(Suit.Spades, Value.Four));

            _tableCardDealer.PutCards();

            _tableCardDealer.GetPoints().Should().Be(11);
        }

        // TODO implement a test for Card equality based on Value only
        // in class CardShould
    }
}