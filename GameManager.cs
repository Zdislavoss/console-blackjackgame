public class GameManager
{
    private Deck deck;
    private Player player;
    private Dealer dealer;
    
    private int totalGames = 0;
    private int playerWins = 0;
    private int dealerWins = 0;
    private int draws = 0;
    private int playerBlackjacks = 0;
    
    public GameManager()
    {
        deck = new Deck();
        player = new Player(1000);
        dealer = new Dealer();
    }

    // Hlavní metoda, která spustí celou hru
    // Zobrazuje úvodní obrazovku a potom spouští jednotlivá kola
    public void StartGame()
    {
        // Nastaví konzoli na UTF-8 pro zobrazení symbolů karet
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Clear();
        Console.WriteLine("=================================");
        Console.WriteLine("    VÍTEJTE VE HŘE BLACKJACK!    ");
        Console.WriteLine("=================================");
        Console.ResetColor();
        
        // Hlavní cyklus. Hra běží, dokud má hráč nějaké žetony
        while (player.Chips > 0)
        {
            PlayRound();
            
            if (player.Chips <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Prohrál jsi všechny žetony!");
                Console.ResetColor();
                break;
            }
            
            string choice;
            
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Máš: {player.Chips} žetonů.");
                Console.ResetColor();

                Console.Write("Chceš hrát další kolo? (ano/ne): ");
                choice = Console.ReadLine()?.ToLower().Trim();
                
                if (choice == "ano" || choice == "ne")
                {
                    Console.Clear();
                    break;
                }
                
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Chyba! Napiš přesně 'ano' nebo 'ne'.");
                Console.ResetColor();
            }
            
            if (choice == "ne")
            {
                break;
            }
        }
        
        PrintFinalStats();
    }

    // Metoda, která odehraje jedno celé kolo Blackjacku
    private void PlayRound()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine();
        Console.WriteLine($"=== NOVÉ KOLO (Tvoje žetony: {player.Chips}) ===");
        Console.ResetColor();
        Console.WriteLine();
        
        player.ClearHand();
        dealer.ClearHand();

        // Hráč zadá sázku
        int bet = GetBet();
        
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Vsadil jsi: {bet} žetonů.");
        Console.ResetColor();
        Console.WriteLine();

        // Počáteční rozdání karet
        player.AddCard(deck.DrawCard());
        dealer.AddCard(deck.DrawCard());
        player.AddCard(deck.DrawCard());
        dealer.AddCard(deck.DrawCard());

        // Proměnné pro split
        bool splitActive = false;
        int splitBet = 0;
        Player secondHand = new Player(0);

        // Kontrola, jestli má hráč hned po rozdání Blackjack.
        bool hasMainBlackjack = player.Hand.Count == 2 && player.GetHandScore() == 21;
        bool isFirstTurn = true;

        // Smyčka tahu hráče která beží, dokud má hráč méně než 21 bodů a nemá hned blackjack
        while (player.GetHandScore() < 21 && !hasMainBlackjack)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            
            Console.WriteLine(splitActive ? "=== HLAVNÍ RUKA ===" : "=== TVŮJ TAH ===");
            Console.ResetColor();
            Console.WriteLine();

            // Vypíše dealerovu viditelnou kartu a skrytou kartu
            dealer.PrintVisibleHand();

            // Vypíše karty hráče a jeho aktuální skóre
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(splitActive ? "Hlavní ruka" : "Player");
            Console.ResetColor();
            Console.Write(" má: ");
            player.PrintHand();
            Console.WriteLine($"(Skóre: {player.GetHandScore()})");

            // Pokud už byl proveden split tak vypíše se i druhá ruka
            if (splitActive)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Druhá ruka");
                Console.ResetColor();
                Console.Write(" má: ");
                secondHand.PrintHand();
                Console.WriteLine($"(Skóre: {secondHand.GetHandScore()})");
            }
            
            string options = "'hit' nebo 'stand'";
            int score = player.GetHandScore();

            // Double jen když splňuje podmínky
            bool canDouble = isFirstTurn && (score == 9 || score == 10 || score == 11) && player.Chips >= player.CurrentBet;

            // Split je povolen pouze na prvním tahu když má hráč dvě stejné karty, a má více peněz než je jeho sázka
            bool canSplit = isFirstTurn &&
                            player.Hand.Count == 2 &&
                            player.Hand[0].GetValueString() == player.Hand[1].GetValueString() &&
                            player.Chips >= player.CurrentBet;
            
            if (canDouble) options += " nebo 'double'";
            if (canSplit) options += " nebo 'split'";
            
            Console.Write($"Co chceš udělat? ({options}): ");
            string choice = Console.ReadLine()?.ToLower().Trim();
            
            if (choice == "hit")
            {
                player.AddCard(deck.DrawCard());
                isFirstTurn = false;
                Console.Clear();
            }
            else if (choice == "stand")
            {
                break;
            }
            
            else if (choice == "double" && canDouble)
            {
                player.PlaceBet(player.CurrentBet);
                player.CurrentBet *= 2;
                player.AddCard(deck.DrawCard());
                break;
            }

            else if (choice == "split" && canSplit)
            {
                splitActive = true;
                splitBet = player.CurrentBet;

                // Odečte se další stejná sázka za druhou ruku.
                player.PlaceBet(splitBet);

                // Druhá karta se přesune z hlavní ruky do druhé ruky.
                Card splitCard = player.RemoveCard(1);
                secondHand.AddCard(splitCard);
                
                player.AddCard(deck.DrawCard());
                secondHand.AddCard(deck.DrawCard());

                isFirstTurn = false;
                Console.Clear();
            }

            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Chyba! Neplatná volba.");
                Console.ResetColor();
            }
        }

        // Pokud hráč provedl split, spustí se tah pro druhou ruku
        if (splitActive)
        {
            PlaySecondHand(secondHand, splitBet);
        }
        
        bool mainHandAlive = player.GetHandScore() <= 21;
        bool secondHandAlive = splitActive && secondHand.GetHandScore() <= 21;

        // Dealer hraje jen tehdy, když má smysl pokračovat
        if ((mainHandAlive && !hasMainBlackjack) || secondHandAlive)
        {
            // Dealer líže karty, dokud má méně než 17 bodů
            while (dealer.ShouldHit())
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("--- Dealer líže kartu... ---");
                Console.ResetColor();
                Console.WriteLine();
                
                PrintTableState(secondHand, splitActive);
                System.Threading.Thread.Sleep(1500);
                dealer.AddCard(deck.DrawCard());
            }
        }
        
        Console.Clear();
        PrintRoundResult(secondHand, splitActive);

        // Kontrola, jestli má dealer Blackjack.
        bool dealerHasBlackjack = dealer.Hand.Count == 2 && dealer.GetHandScore() == 21;
        
        if (splitActive) Console.Write("[1. HLAVNÍ RUKA] -> ");
        EvaluateHandResult(player.GetHandScore(), dealer.GetHandScore(), player.CurrentBet, hasMainBlackjack, dealerHasBlackjack);

        // Pokud byl split, vyhodnotí se i druhá ruka.
        if (splitActive)
        {
            Console.Write("[2. DRUHÁ RUKA]  -> ");
            EvaluateHandResult(secondHand.GetHandScore(), dealer.GetHandScore(), splitBet, false, dealerHasBlackjack);
        }
        
        player.Lose();
        Console.WriteLine();
    }

// Metoda kteráse stará o zadání a kontrolu sázky
private int GetBet()
{
    int bet;
    
    while (true)
    {
        Console.Write("Zadej svoji sázku: ");
        string input = Console.ReadLine();
        
        if (int.TryParse(input, out bet))
        {

            if (player.PlaceBet(bet))
            {
                // Aktuální sázka se uloží do hráče
                player.CurrentBet = bet;
                return bet;
            }
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Neplatná částka! Nemáš dost žetonů.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Chyba! Musíš zadat celé číslo.");
            Console.ResetColor();
        }
    }
}

// Metoda řeší tah druhé ruky po splitu
private void PlaySecondHand(Player secondHand, int splitBet)
{
    Console.Clear();
    
    while (secondHand.GetHandScore() < 21)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"=== DRUHÁ RUKA === (Sázka: {splitBet} žetonů)");
        Console.ResetColor();
        Console.WriteLine();
        
        dealer.PrintVisibleHand();
        
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("Hlavní ruka [Ukončeno]");
        Console.ResetColor();
        Console.Write(" má: ");
        player.PrintHand();
        Console.WriteLine($"(Skóre: {player.GetHandScore()})");
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Druhá ruka");
        Console.ResetColor();
        Console.Write(" má: ");
        secondHand.PrintHand();
        Console.WriteLine($"(Skóre: {secondHand.GetHandScore()})");
        
        Console.Write("Co chceš udělat pro druhou ruku? ('hit' nebo 'stand'): ");
        string choice = Console.ReadLine()?.ToLower().Trim();
        
        if (choice == "hit")
        {
            secondHand.AddCard(deck.DrawCard());
            Console.Clear();
        }
        else if (choice == "stand")
        {
            break;
        }
        else
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Chyba! Neplatná volba.");
            Console.ResetColor();
        }
    }
}

// Metoda, která vypíše aktualní stav
private void PrintTableState(Player secondHand, bool splitActive)
{
    // dealer
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write("Dealer");
    Console.ResetColor();
    Console.Write(" má: ");
    dealer.PrintHand();
    Console.WriteLine($"(Skóre: {dealer.GetHandScore()})");

    // hrac
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write(splitActive ? "Hlavní ruka" : "Player");
    Console.ResetColor();
    Console.Write(" má: ");
    player.PrintHand();
    Console.WriteLine($"(Skóre: {player.GetHandScore()})");

    // Kdyby byl split, vypíše se i druhá ruka
    if (splitActive)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Druhá ruka");
        Console.ResetColor();
        Console.Write(" má: ");
        secondHand.PrintHand();
        Console.WriteLine($"(Skóre: {secondHand.GetHandScore()})");
    }

    Console.WriteLine();
}

private void PrintRoundResult(Player secondHand, bool splitActive)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("=== VÝSLEDKY KOLA ===");
    Console.ResetColor();
    Console.WriteLine();

    // Samotný výpis dealerových a hráčových karet je v PrintTableState metodě
    PrintTableState(secondHand, splitActive);
}

// Metoda porovnává skóre hráče a dealera
private void EvaluateHandResult(int playerScore, int dealerScore, int betAmount, bool playerBlackjack, bool dealerBlackjack)
{
    totalGames++;
        
    if (playerScore > 21)
    {
        dealerWins++;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Přetažení! Prohráváš {betAmount} žetonů.");
        Console.ResetColor();
    }
    // BJ hráče
    else if (playerBlackjack && !dealerBlackjack)
    {
        playerWins++;
        playerBlackjacks++;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"BLACKJACK! Vyhráváš {(int)(betAmount * 1.5)} žetonů.");
        Console.ResetColor();
        
        player.CurrentBet = betAmount;
        player.Win(1.5);
    }
    
    // Remíza
    else if (playerScore == dealerScore && playerBlackjack == dealerBlackjack)
    {
        draws++;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Remíza. Vrací se ti {betAmount} žetonů.");
        Console.ResetColor();

        // Sázka se hráči vrátí zpět.
        player.CurrentBet = betAmount;
        player.Draw();
    }
    // Výhra hráče
    else if (dealerScore > 21 || playerScore > dealerScore)
    {
        playerWins++;

        Console.ForegroundColor = ConsoleColor.Green;

        if (dealerScore > 21)
        {
            Console.WriteLine($"Dealer přetáhl! Vyhráváš {betAmount} žetonů.");
        }
        else
        {
            Console.WriteLine($"Máš víc bodů než dealer! Vyhráváš {betAmount} žetonů.");
        }

        Console.ResetColor();
        
        player.CurrentBet = betAmount;
        player.Win();
    }
    
    // Ve všech ostatních případech vyhrává dealer.
    else
    {
        dealerWins++;

        Console.ForegroundColor = ConsoleColor.Red;

        if (dealerBlackjack && !playerBlackjack)
        {
            Console.WriteLine($"Dealer má Blackjack. Prohráváš {betAmount} žetonů.");
        }
        else
        {
            Console.WriteLine($"Dealer má víc bodů. Prohráváš {betAmount} žetonů.");
        }

        Console.ResetColor();
    }
}

// Metoda vypíše finální statistiky 
private void PrintFinalStats()
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("=================================");
    Console.WriteLine("  Hra skončila. Děkujeme za hru! ");
    Console.WriteLine("=================================");
    Console.ResetColor();

    // Výpis všech uložených statistik.
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("==================================================");
    Console.WriteLine($"  Celkem odehraných kol:  {totalGames}");
    Console.WriteLine($"  Tvoje výhry:            {playerWins}");
    Console.WriteLine($"  Výhry dealera:          {dealerWins}");
    Console.WriteLine($"  Remízy:                 {draws}");
    Console.WriteLine($"  Počet tvých Blackjacků: {playerBlackjacks}");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("==================================================");
    Console.ResetColor();
}
}