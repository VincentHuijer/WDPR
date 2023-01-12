using backend.Authenticatie;
public class Kaartjeshouders
{
    public int ShowId { get; set; }
    public Show Show { get; set; }
    public int KlantId { get; set; }
    public Klant Klant { get; set; }
}