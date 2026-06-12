public abstract class Participant
{
    // Seznam karet, které má v ruce
    protected List<Card> hand;
    
    // IReadOnlyList znamená, že ostatní části programu si karty můžou prohlédnout, ale nemůžou je přímo měnit
    public IReadOnlyList<Card> Hand => hand;

    // Konstruktor vytvoří prázdnou ruku pro hráče nebo dealera
    public Participant()
    {
        hand = new List<Card>();
    }
    
    public void AddCard(Card card)
    {
        hand.Add(card);
    }
    
    public void ClearHand()
    {
        hand.Clear();
    }

    // Metoda, spočítá hodnotu celé ruky 
    public int GetHandScore()
    {
        int score = 0;    
        int aceCount = 0; 

        // Projde všechny karty v ruce
        foreach (Card card in hand)
        {
            score = score + card.GetBlackjackValue();
            
            if (card.Value == Card.CardValue.A)
            {
                aceCount++;
            }
        }
        
        // Funkce es
        while (score > 21 && aceCount > 0)
        {
            score = score - 10; 
            aceCount--;         
        }
        return score;
    }

    // Metoda, vytvoří textový zápis karet v ruce
    public string GetHandString()
    {
        string text = ""; 

        // Projde všechny karty v ruce
        foreach (Card card in hand)
        {
            text = text + $"[ {card.GetTypeSymbol()} {card.GetValueString()} ] ";
        }
        return text;
    }

    // Metoda, vypíše celou ruku do konzole
    public void PrintHand()
    {
        string handString = GetHandString();
        if (string.IsNullOrEmpty(handString)) return;

        // Text ruky se rozdělí na jednotlivé karty
        string[] cards = handString.Split(new string[] { " ]" }, StringSplitOptions.RemoveEmptyEntries);

        // Projde každou kartu zvlášť.
        foreach (var cardText in cards)
        {
            string trimmedCard = cardText.Trim();

            // Pokud by vznikl prázdný text, přeskočí se.
            if (string.IsNullOrEmpty(trimmedCard)) continue;
            
            if (trimmedCard.Contains("♥") || trimmedCard.Contains("♦"))
            {
                Console.ForegroundColor = ConsoleColor.Red; 
            }
            
            Console.Write(trimmedCard + " ] ");
            Console.ResetColor(); 
        }
    }
}