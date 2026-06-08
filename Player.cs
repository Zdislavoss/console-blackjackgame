public class Player : Participant
{ 
    public int Chips { get; private set; }
    public int CurrentBet { get; private set; }
    
    public Player(int startingChips) : base()  // Prvně načte Participanta
    {
        Chips = startingChips;
        CurrentBet = 0; 
    }

    // Metoda pro vsazení žetonů
    public bool PlaceBet(int amount)
    {
        if (amount <= 0 || amount > Chips)
        {
            return false; // Sázka je neplatná - nemůže vsadit 0 nebo nic
        }

        CurrentBet = amount;     
        Chips = Chips - amount;  
        return true;             
    }

    // Metoda, výherní kolo
    public void Win()
    {
        
        Chips = Chips + (CurrentBet * 2);
        CurrentBet = 0; // Kolo skončilo, vyčistíme sázku pro další hru
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