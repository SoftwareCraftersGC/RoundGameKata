using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using RoundGame;


namespace Tests
{
    [TestFixture]
    public class TableCardDealerShould
    {
        private TableCardDealer TableCardDealer { get; set; }
        private Table Table { get; set; }
        private Deck Deck { get; set; }

        [SetUp]
        public void Initialize()
        {
            Deck = Substitute.For<Deck>();
            Table = new Table();
            TableCardDealer = new TableCardDealer(Table, Deck);
        }

        [Test]
        public void add_a_point_when_no_card_is_repeated_over_the_table()
        {
            GivenADeckWithNoRepeatedCards();
            TableCardDealer.PutCards();
            TableCardDealer.GetPoints().Should().Be(1);
        }

        [Test]
        public void add_points_based_on_card_value_if_the_value_corresponds_to_its_emplacement_order_and_an_extra_one_for_no_repeated_cards()
        {
            GivenADeckWithSucesiveCards();
            TableCardDealer.PutCards();
            TableCardDealer.GetPoints().Should().Be(11);
        }

        [Test]
        public void replace_any_repeated_cards_even_if_they_are_repeated_again_when_getting_a_new_one_from_the_deck()
        {
            GivenADeckWithAsMuchRepetitionsAsPossible();
            TableCardDealer.PutCards();
            TableCardDealer.GetPoints().Should().Be(0);
            Table.Cards.ShouldBeEquivalentTo(ExpectedTable());
        }

        private void GivenADeckWithNoRepeatedCards()
        {
            Deck.GetRandomCard().Returns(new Card(Suit.Spades, Value.King),
										 new Card(Suit.Spades, Value.Queen),
										 new Card(Suit.Spades, Value.Knave),
										 new Card(Suit.Spades, Value.Seven));
        }

        private void GivenADeckWithSucesiveCards()
        {
            Deck.GetRandomCard().Returns(new Card(Suit.Spades, Value.Ace),
										 new Card(Suit.Spades, Value.Two),
										 new Card(Suit.Spades, Value.Three),
										 new Card(Suit.Spades, Value.Four));
        }

        private void GivenADeckWithAsMuchRepetitionsAsPossible()
        {
            Deck.GetRandomCard().Returns(new Card(Suit.Diamonds, Value.King),
										 new Card(Suit.Clubs, Value.King),
										 new Card(Suit.Hearts, Value.King),
										 new Card(Suit.Spades, Value.King),
										 new Card(Suit.Clubs, Value.Five),
										 new Card(Suit.Spades, Value.Five),
										 new Card(Suit.Spades, Value.Five),
										 new Card(Suit.Clubs, Value.Four),
										 new Card(Suit.Spades, Value.Four),
										 new Card(Suit.Spades, Value.Ace));
        }

        private static List<Card> ExpectedTable()
        {
            return new List<Card>
            {
                new Card(Suit.Diamonds, Value.King),
                new Card(Suit.Clubs, Value.Five),
                new Card(Suit.Clubs, Value.Four),
                new Card(Suit.Spades, Value.Ace)
            };
        }
    }
}