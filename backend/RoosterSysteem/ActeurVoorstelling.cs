using backend.Authenticatie;
public class ActeurVoorstelling
{
    public int ShowId { get; set; }
    public Show Show { get; set; }
    public int ActeurId { get; set; }
    public Klant Acteur { get; set; }
}