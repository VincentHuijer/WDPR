using backend.Authenticatie;
public class Kaartjeshouders
{
    public int VoorstellingId { get; set; }
    public Voorstelling Voorstelling { get; set; }
    public int KlantId { get; set; }
    public Klant Klant { get; set; }
}