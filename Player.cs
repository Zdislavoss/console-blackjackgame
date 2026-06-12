public class Player : Participant
{ 
    public int Chips { get; private set; }
    public int CurrentBet { get; set; } 
 
    public Player(int startingChips) : base()  
    {
        Chips = startingChips;
        CurrentBet = 0;
    }

    // Metoda pro vsazení žetonů do hry
    public bool PlaceBet(int amount)
    {
        // Kontrola, že sázka není záporná nebo není víc než má
        if (amount <= 0 || amount > Chips)
        {
            return false;
        }

        CurrentBet = amount;
        Chips = Chips - amount; 
        return true;
    }
    
    // Metoda pro odstranění karty z ruky
    public Card RemoveCard(int index)
    {
        // Kontrola, zda zadaný index spadá do reálného rozsahu karet v ruce 
        if (index >= 0 && index < hand.Count)
        {
            Card card = hand[index]; // Uloží kartu z dané pozice do pomocné proměnné
            hand.RemoveAt(index);    
            return card;             // Vrátí smazanou kartu pro GameManager
        }
        return null;
    }

    // Metoda, výhra
    public void Win(double multiplier = 1.0)
    {
        int wonAmount = (int)(CurrentBet * multiplier);
        Chips = Chips + CurrentBet + wonAmount;
        CurrentBet = 0; 
    }

    // Metoda, remíza
    public void Draw()
    {
        Chips = Chips + CurrentBet; 
        CurrentBet = 0;
    }

    // Metoda, prohra
    public void Lose()
    {
        CurrentBet = 0; 
    }
}