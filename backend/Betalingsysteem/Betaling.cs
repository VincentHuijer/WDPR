using System;
using System.Collections.Generic;

class Betaling
{
    public iBestelling bestelling;
    public bool isBetaald;
    public double korting;
    public bool betalen(Klantbestelling klantbestelling)
    {
        System.Console.WriteLine("Klant heeft betaald!");
        return isBetaald = true;
    }

    public bool betalen(DonateurBestelling donateurBestelling)
    {
        System.Console.WriteLine("Donateur heeft betaald!");
        return isBetaald = true;
    }
}

