public bestelling bestelling;
public boolean isBetaald;
public double korting;

class Betaling
{
    public isBetaald betalen(Klantbestelling klantbestelling)
    {
        console.log("Klant heeft betaald!");
    }

    public isBetaald betalen(DonateurBestelling donateurBestelling)
    {
        console.log("Donateur heeft betaald!");
    }
}

