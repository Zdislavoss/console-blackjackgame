public class Deck
{
    private List<Card> cards;
    private Random random;

    
    public Deck()
    {
        cards = new List<Card>();
        random = new Random();
        
        // Na začátku hry se naplní balíček
        InitializeDeck();
    }

    // Metoda se zavolá, když se zapne hra, nebo dojdou karty
    private void InitializeDeck()
    {
        cards.Clear(); 

        //Vytváření karet do balíčku
        // Srdce
        cards.Add(new Card(CardType.Hearts, CardValue.Two));
        cards.Add(new Card(CardType.Hearts, CardValue.Three));
        cards.Add(new Card(CardType.Hearts, CardValue.Four));
        cards.Add(new Card(CardType.Hearts, CardValue.Five));
        cards.Add(new Card(CardType.Hearts, CardValue.Six));
        cards.Add(new Card(CardType.Hearts, CardValue.Seven));
        cards.Add(new Card(CardType.Hearts, CardValue.Eight));
        cards.Add(new Card(CardType.Hearts, CardValue.Nine));
        cards.Add(new Card(CardType.Hearts, CardValue.Ten));
        cards.Add(new Card(CardType.Hearts, CardValue.Jack));
        cards.Add(new Card(CardType.Hearts, CardValue.Queen));
        cards.Add(new Card(CardType.Hearts, CardValue.King));
        cards.Add(new Card(CardType.Hearts, CardValue.Ace));

        // Káry
        cards.Add(new Card(CardType.Diamonds, CardValue.Two));
        cards.Add(new Card(CardType.Diamonds, CardValue.Three));
        cards.Add(new Card(CardType.Diamonds, CardValue.Four));
        cards.Add(new Card(CardType.Diamonds, CardValue.Five));
        cards.Add(new Card(CardType.Diamonds, CardValue.Six));
        cards.Add(new Card(CardType.Diamonds, CardValue.Seven));
        cards.Add(new Card(CardType.Diamonds, CardValue.Eight));
        cards.Add(new Card(CardType.Diamonds, CardValue.Nine));
        cards.Add(new Card(CardType.Diamonds, CardValue.Ten));
        cards.Add(new Card(CardType.Diamonds, CardValue.Jack));
        cards.Add(new Card(CardType.Diamonds, CardValue.Queen));
        cards.Add(new Card(CardType.Diamonds, CardValue.King));
        cards.Add(new Card(CardType.Diamonds, CardValue.Ace));

        // Piky
        cards.Add(new Card(CardType.Spades, CardValue.Two));
        cards.Add(new Card(CardType.Spades, CardValue.Three));
        cards.Add(new Card(CardType.Spades, CardValue.Four));
        cards.Add(new Card(CardType.Spades, CardValue.Five));
        cards.Add(new Card(CardType.Spades, CardValue.Six));
        cards.Add(new Card(CardType.Spades, CardValue.Seven));
        cards.Add(new Card(CardType.Spades, CardValue.Eight));
        cards.Add(new Card(CardType.Spades, CardValue.Nine));
        cards.Add(new Card(CardType.Spades, CardValue.Ten));
        cards.Add(new Card(CardType.Spades, CardValue.Jack));
        cards.Add(new Card(CardType.Spades, CardValue.Queen));
        cards.Add(new Card(CardType.Spades, CardValue.King));
        cards.Add(new Card(CardType.Spades, CardValue.Ace));

        // Kříže
        cards.Add(new Card(CardType.Clubs, CardValue.Two));
        cards.Add(new Card(CardType.Clubs, CardValue.Three));
        cards.Add(new Card(CardType.Clubs, CardValue.Four));
        cards.Add(new Card(CardType.Clubs, CardValue.Five));
        cards.Add(new Card(CardType.Clubs, CardValue.Six));
        cards.Add(new Card(CardType.Clubs, CardValue.Seven));
        cards.Add(new Card(CardType.Clubs, CardValue.Eight));
        cards.Add(new Card(CardType.Clubs, CardValue.Nine));
        cards.Add(new Card(CardType.Clubs, CardValue.Ten));
        cards.Add(new Card(CardType.Clubs, CardValue.Jack));
        cards.Add(new Card(CardType.Clubs, CardValue.Queen));
        cards.Add(new Card(CardType.Clubs, CardValue.King));
        cards.Add(new Card(CardType.Clubs, CardValue.Ace));
    }

 
    public Card DrawCard()
    {
        // Jestli v balíčku nic nezbylo, znovu ho naplníme
        if (cards.Count == 0)
        {
            InitializeDeck();
        }

        // Vybere se náhodné číslo od 0 do počtu karet v balíčku co zbývá
        int randomIndex = random.Next(0, cards.Count);

        // Vezme se náhodná karta
        Card drawnCard = cards[randomIndex];

        // Smaže se z balíčku, aby si ji nevzal někdo dvakrát
        cards.RemoveAt(randomIndex);

        // Vrátíme ji hráči nebo krupiérovi
        return drawnCard;
    }
}