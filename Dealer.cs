public class Dealer : Participant
{ 
    public Dealer() : base()
    {
    }

    // Metoda rozhoduje, kdy si má dealer vzít další kartu
    public bool ShouldHit()
    {
        // V BJ musí dealer do 17 lízat
        if (GetHandScore() < 17)
        {
            return true;  
        }
        else
        {
            return false; 
        }
    }

    // Metoda vypíše dealerovu ruku během hry, hráč vidí jen jednu (druhá je skrytá)
    public void PrintVisibleHand()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Dealer");
        Console.ResetColor();
        Console.Write(" má: ");

        // Pokud má dealer v ruce aspoň jednu kartu, vypíše se první karta
        if (hand.Count > 0)
        {
            Card firstCard = hand[0];

            // Pokud je karta srdce nebo káry, vypíše se červeně
            if (firstCard.Type == Card.CardType.Hearts || firstCard.Type == Card.CardType.Diamonds)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            // Vypíše se jenom první viditelná karta dealera
            Console.Write($"[ {firstCard.GetTypeSymbol()} {firstCard.GetValueString()} ] ");
            Console.ResetColor();
        }
        
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("[ Skrytá Karta ]");
        Console.ResetColor();
        
        // Skóre se počítá jen z viditelné první karty.
        int visibleScore = hand.Count > 0 ? hand[0].GetBlackjackValue() : 0;
        Console.WriteLine($" (Skóre: {visibleScore})");
    }
}