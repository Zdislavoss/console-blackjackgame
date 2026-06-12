public class Deck
{
    private List<Card> cards;
    
    // Generátor náhodných čísel, pro míchání karet
    private Random random;
    
    public Deck()
    {
        cards = new List<Card>(); 
        random = new Random();     
        
        ResetDeck(); // Na začátku hry naplní balíček 52 kartami
    }

    // Metoda, která resetuje a generuje novou sadu karet pro novou hru
    public void ResetDeck()
    {
        cards.Clear();
        
        
        //SRDCE 
        cards.Add(new Card(Card.CardType.Hearts, Card.CardValue.Dva));
        cards.Add(new Card(Card.CardType.Hearts, Card.CardValue.Tri));
        cards.Add(new Card(Card.CardType.Hearts, Card.CardValue.Ctyri));
        cards.Add(new Card(Card.CardType.Hearts, Card.CardValue.Pet));
        cards.Add(new Card(Card.CardType.Hearts, Card.CardValue.Sest));
        cards.Add(new Card(Card.CardType.Hearts, Card.CardValue.Sedm));
        cards.Add(new Card(Card.CardType.Hearts, Card.CardValue.Osm));
        cards.Add(new Card(Card.CardType.Hearts, Card.CardValue.Devet));
        cards.Add(new Card(Card.CardType.Hearts, Card.CardValue.Deset));
        cards.Add(new Card(Card.CardType.Hearts, Card.CardValue.J));
        cards.Add(new Card(Card.CardType.Hearts, Card.CardValue.Q));
        cards.Add(new Card(Card.CardType.Hearts, Card.CardValue.K));
        cards.Add(new Card(Card.CardType.Hearts, Card.CardValue.A));

        //KÁRY
        cards.Add(new Card(Card.CardType.Diamonds, Card.CardValue.Dva));
        cards.Add(new Card(Card.CardType.Diamonds, Card.CardValue.Tri));
        cards.Add(new Card(Card.CardType.Diamonds, Card.CardValue.Ctyri));
        cards.Add(new Card(Card.CardType.Diamonds, Card.CardValue.Pet));
        cards.Add(new Card(Card.CardType.Diamonds, Card.CardValue.Sest));
        cards.Add(new Card(Card.CardType.Diamonds, Card.CardValue.Sedm));
        cards.Add(new Card(Card.CardType.Diamonds, Card.CardValue.Osm));
        cards.Add(new Card(Card.CardType.Diamonds, Card.CardValue.Devet));
        cards.Add(new Card(Card.CardType.Diamonds, Card.CardValue.Deset));
        cards.Add(new Card(Card.CardType.Diamonds, Card.CardValue.J));
        cards.Add(new Card(Card.CardType.Diamonds, Card.CardValue.Q));
        cards.Add(new Card(Card.CardType.Diamonds, Card.CardValue.K));
        cards.Add(new Card(Card.CardType.Diamonds, Card.CardValue.A));

        //PIKY
        cards.Add(new Card(Card.CardType.Spades, Card.CardValue.Dva));
        cards.Add(new Card(Card.CardType.Spades, Card.CardValue.Tri));
        cards.Add(new Card(Card.CardType.Spades, Card.CardValue.Ctyri));
        cards.Add(new Card(Card.CardType.Spades, Card.CardValue.Pet));
        cards.Add(new Card(Card.CardType.Spades, Card.CardValue.Sest));
        cards.Add(new Card(Card.CardType.Spades, Card.CardValue.Sedm));
        cards.Add(new Card(Card.CardType.Spades, Card.CardValue.Osm));
        cards.Add(new Card(Card.CardType.Spades, Card.CardValue.Devet));
        cards.Add(new Card(Card.CardType.Spades, Card.CardValue.Deset));
        cards.Add(new Card(Card.CardType.Spades, Card.CardValue.J));
        cards.Add(new Card(Card.CardType.Spades, Card.CardValue.Q));
        cards.Add(new Card(Card.CardType.Spades, Card.CardValue.K));
        cards.Add(new Card(Card.CardType.Spades, Card.CardValue.A));

        //KŘÍŽE
        cards.Add(new Card(Card.CardType.Clubs, Card.CardValue.Dva));
        cards.Add(new Card(Card.CardType.Clubs, Card.CardValue.Tri));
        cards.Add(new Card(Card.CardType.Clubs, Card.CardValue.Ctyri));
        cards.Add(new Card(Card.CardType.Clubs, Card.CardValue.Pet));
        cards.Add(new Card(Card.CardType.Clubs, Card.CardValue.Sest));
        cards.Add(new Card(Card.CardType.Clubs, Card.CardValue.Sedm));
        cards.Add(new Card(Card.CardType.Clubs, Card.CardValue.Osm));
        cards.Add(new Card(Card.CardType.Clubs, Card.CardValue.Devet));
        cards.Add(new Card(Card.CardType.Clubs, Card.CardValue.Deset));
        cards.Add(new Card(Card.CardType.Clubs, Card.CardValue.J));
        cards.Add(new Card(Card.CardType.Clubs, Card.CardValue.Q));
        cards.Add(new Card(Card.CardType.Clubs, Card.CardValue.K));
        cards.Add(new Card(Card.CardType.Clubs, Card.CardValue.A));
    }

    // Metoda, která z balíčku vytáhne náhodnou kartu
    public Card DrawCard()
    {
        if (cards.Count == 0)
        {
            ResetDeck(); 
        }

        // Vybere náhodný index od 0 do zbyvajicího poctu karet
        int index = random.Next(cards.Count);
        Card drawnCard = cards[index]; 
        cards.RemoveAt(index);          
        return drawnCard;               
    }
}