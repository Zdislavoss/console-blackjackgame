public class GameManager
{
    private Deck deck;
    private Player player;
    private Dealer dealer;
    
    public GameManager()
    {
        deck = new Deck();
        player = new Player(1000); 
        dealer = new Dealer();
    }

    // Metoda, hlavní spuštění
    public void StartGame()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.Clear();
        Console.WriteLine("=================================");
        Console.WriteLine("    VÍTEJTE VE HŘE BLACKJACK!    ");
        Console.WriteLine("=================================");
        Console.WriteLine();
        
        while (player.Chips > 0)
        {
            PlayRound();
            
            // Otázka po každém kole, jestli chce pokracovat
            Console.WriteLine($"Máš: {player.Chips} žetonů.");
            Console.Write("Chceš hrát další kolo? (ano/ne): ");
            string continueChoice = Console.ReadLine().ToLower();
            
            if (continueChoice != "ano")
            {
                break;
            }
        }

        Console.WriteLine("Hra skončila. Děkujeme za hru!");
    }

    // Metoda, celé kolo
    private void PlayRound()
    {
        Console.Clear();
        Console.WriteLine($"=== NOVÉ KOLO (Tvoje žetony: {player.Chips}) ===");
        Console.WriteLine();

        // bet
        bool validBet = false;
        while (!validBet)
        {
            Console.Write("Zadej svou sázku: ");
            string input = Console.ReadLine();
            
            if (int.TryParse(input, out int betAmount))
            {
                // kontrola
                if (player.PlaceBet(betAmount))
                {
                    validBet = true;
                }
                else
                {
                    Console.WriteLine("Neplatná částka! Nemůžeš vsadit víc než máš, nebo neplatné číslo.");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Prosím, zadej platné číslo.");
                Console.WriteLine();
            }
        }
        
        player.ClearHand();
        dealer.ClearHand();
        
        player.AddCard(deck.DrawCard());
        dealer.AddCard(deck.DrawCard());
        player.AddCard(deck.DrawCard());
        dealer.AddCard(deck.DrawCard());

        // Hit nebo stand
        while (player.GetHandScore() < 21)
        {
            Console.WriteLine($"Dealer má: [ {dealer.GetHandString().Split(' ')[1]} {dealer.GetHandString().Split(' ')[2]} ] [ Skrytá Karta ]"); // Jen jedna karta
            Console.WriteLine();
            Console.WriteLine($"Tvoje karty: {player.GetHandString()}(Skóre: {player.GetHandScore()})");
            Console.WriteLine();
            Console.Write("Chceš další kartu (hit) nebo stát (stand)? ");
            string action = Console.ReadLine().ToLower();

            if (action == "hit")
            {
                player.AddCard(deck.DrawCard());
            }
            else if (action == "stand")
            {
                break;
            }
        }

        // Vyhodnocení 21 - Ano/Ne
        if (player.GetHandScore() > 21)
        {
            Console.WriteLine($"Tvoje karty: {player.GetHandString()}(Skóre: {player.GetHandScore()})");
            Console.WriteLine("Přetáhl jsi! Prohráváš toto kolo.");
            player.Lose();
            return; 
        }

        // Tah krupiera
        Console.WriteLine("--- Hraje dealer ---");
        while (dealer.ShouldHit())
        {
            dealer.AddCard(deck.DrawCard());
        }

        // Zobrazení karet
        Console.WriteLine($"Karty dealera: {dealer.GetHandString()}(Skóre: {dealer.GetHandScore()})");
        Console.WriteLine($"Tvoje karty:  {player.GetHandString()}(Skóre: {player.GetHandScore()})");

        // Vyhodnocení výsledků
        int playerScore = player.GetHandScore();
        int dealerScore = dealer.GetHandScore();

        if (dealerScore > 21)
        {
            Console.WriteLine("Dealer přetáhl! Vyhráváš!");
            player.Win();
        }
        else if (playerScore > dealerScore)
        {
            Console.WriteLine("Máš víc bodů než dealer! Vyhráváš!");
            player.Win();
        }
        else if (playerScore < dealerScore)
        {
            Console.WriteLine("Dealer má víc bodů. Prohráváš.");
            player.Lose();
        }
        else
        {
            Console.WriteLine("Remíza (Push). Žetony se ti vrací.");
            player.Draw();
        }
    }
}