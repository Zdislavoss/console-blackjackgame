// Dva enumy - barva a typ (pevný seznam)
public enum CardType { Hearts, Diamonds, Spades, Clubs }
public enum CardValue { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

public class Card
{
    // Vlastnosti
    public CardType Type { get; private set; }
    public CardValue Value { get; private set; }

    // Konstruktor - přiřadí barvu a hodnotu
    public Card(CardType type, CardValue value)
    {
        Type = type;
        Value = value;
    }

    // Metoda, vrací hodnotu karty podle BJ
    public int GetBlackjackValue()
    {
        // Eso = 11
        if (Value == CardValue.Ace)
        {
            return 11;
        }
        
        // Obrazek nebo 10 = 10
        if (Value == CardValue.Ten || Value == CardValue.Jack || Value == CardValue.Queen || Value == CardValue.King)
        {
            return 10;
        }
        
        // Zbytek - bežné karty
        if (Value == CardValue.Nine) return 9;
        if (Value == CardValue.Eight) return 8;
        if (Value == CardValue.Seven) return 7;
        if (Value == CardValue.Six) return 6;
        if (Value == CardValue.Five) return 5;
        if (Value == CardValue.Four) return 4;
        if (Value == CardValue.Three) return 3;
        if (Value == CardValue.Two) return 2;
        
        return 1;
    }

    // Převedení enumu na symbol (grafiku)
    public string GetTypeSymbol()
    {
        if (Type == CardType.Hearts) return "♥";
        if (Type == CardType.Diamonds) return "♦";
        if (Type == CardType.Spades) return "♠";
        if (Type == CardType.Clubs) return "♣";
        
        return "X";
    }

    // Vyhodnocení barvy - červená/černá
    public bool IsRed()
    {
        if (Type == CardType.Hearts || Type == CardType.Diamonds)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}