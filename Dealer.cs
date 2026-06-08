public class Dealer : Participant
{ 
    public Dealer() : base()
    {
    }

    // Metoda - systém lízání pro dealera
    public bool ShouldHit()
    {
        if (GetHandScore() < 17)
        {
            return true;  // hit
        }
        else
        {
            return false; // stand
        }
    }
}