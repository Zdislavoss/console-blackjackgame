public abstract class Participant
{
    // Seznam karet, které drží hráč nebo krupiér v ruce
    protected List<Card> hand;
    public Participant()
    {
        hand = new List<Card>();
    }

    // Metoda pro přidání karty do ruky - líznutí
    public void AddCard(Card card)
    {
        hand.Add(card);
    }

    // Metoda pro vymazání všech karet z ruky 
    public void ClearHand()
    {
        hand.Clear();
    }

    // Metoda která spočítá celkové body karet v ruce a automaticky vyřeší Esa
    public int GetHandScore()
    {
        int score = 0;
        int aceCount = 0;

        // sečtení základní hodnoty všech karet v ruce
        foreach (Card card in hand)
        {
            score = score + card.GetBlackjackValue();
            
            if (card.Value == CardValue.Ace)
            {
                aceCount++;
            }
        }
        
        // Logika esa (nad 21 = -10)
        while (score > 21 && aceCount > 0)
        {
            score = score - 10;
            aceCount--; 
        }
            
        return score;
    }

    // Textový Výstup
    public string GetHandString()
    {
        string text = "";
        
        foreach (Card card in hand)
        {
            text = text + "[ " + card.Type + " " + card.Value + " ] ";
        }
        
        return text;
    }
}