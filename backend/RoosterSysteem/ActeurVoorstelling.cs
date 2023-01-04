using backend.Authenticatie;
public class ActeurVoorstelling
{
    public string voorstellingTitel { get; set; }
    public Voorstelling Voorstelling { get; set; }
    public string klantId { get; set; }
    public Klant Acteur { get; set; }
}