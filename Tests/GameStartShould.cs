using NUnit.Framework;

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

	}
}