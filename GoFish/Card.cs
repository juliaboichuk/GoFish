namespace GoFish
{
    public class Card
    {
        public Suits Suit { get; private set; }
        public Values Value { get; private set; }

        public Card(Values value, Suits suit)
        {
            Suit = suit;
            Value = value;
        }

        public override string ToString() => $"{Value} of {Suit}";
    }
}
