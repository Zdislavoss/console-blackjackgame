public class Card
{
    // Enum pro typy karet
    public enum CardType { Hearts, Diamonds, Spades, Clubs }
    
    // Enum pro karty 2-A
    public enum CardValue { Dva = 2, Tri, Ctyri, Pet, Sest, Sedm, Osm, Devet, Deset, J, Q, K, A }
    
    public CardType Type { get; private set; }
    
    public CardValue Value { get; private set; }
    
    public Card(CardType type, CardValue value)
    {
        Type = type;   
        Value = value; 
    }

    // Metoda, která vrací bodovou hodnotu karty 
    public int GetBlackjackValue()
    {
        if (Value == CardValue.A)
        {
            return 11; 
        }
        
        // Pokud je to obrázek nebo 10, vrátí 10
        if (Value == CardValue.Deset || Value == CardValue.J || Value == CardValue.Q || Value == CardValue.K)
        {
            return 10; 
        }
        
        // Pro ostatní karty 2-9
        return (int)Value;
    }

    // Metoda, který vrací symboly pro konzoli
    public string GetTypeSymbol()
    {
        if (Type == CardType.Hearts) return "♥";   
        if (Type == CardType.Diamonds) return "♦"; 
        if (Type == CardType.Spades) return "♠";   
        if (Type == CardType.Clubs) return "♣";    
        
        return "X"; 
    }

    // Metoda, která převádí hodnotu karty na text
    public string GetValueString()
    {
        if (Value == CardValue.J) return "J"; 
        if (Value == CardValue.Q) return "Q"; 
        if (Value == CardValue.K) return "K"; 
        if (Value == CardValue.A) return "A"; 
        
        // Ostatní čísla na string
        return ((int)Value).ToString();
    }
    
    // Metoda pro barevné vykreslení karet
    public void PrintCard()
    {
        ConsoleColor originalColor = Console.ForegroundColor;
        // Pokud je karta červená (srdce nebo káry)
        if (Type == CardType.Hearts || Type == CardType.Diamonds)
        {
            Console.ForegroundColor = ConsoleColor.Red; 
        }

        // Vypíše kartu do konzole 
        Console.Write("[ " + GetTypeSymbol() + " " + GetValueString() + " ] ");
        Console.ForegroundColor = originalColor;
    }
}