using System.Text.Json.Serialization;

public class Show{
    public Show(){
        
    }
    public Show(int zaalnummer, DateTime datumEnTijd, int voorstellingId, int kalenderId){
        Zaalnummer = zaalnummer;
        Datum = datumEnTijd;
        VoorstellingId = voorstellingId;
        KalenderId = kalenderId;
    }
    public int ShowId {set; get;}
    public Zaal Zaal {set; get;}
    public int Zaalnummer {set; get;}

    [JsonIgnore]
    public Voorstelling Voorstelling {set; get;}
    public int VoorstellingId {set; get;}
    public List<ActeurVoorstelling>? Acteur { get; set; }
    public int KalenderId {set; get;}
    public Kalender? Kalender { get; set; }
    public DateTime Datum { get; set; }
    [JsonIgnore]
    public ArtiestGroep? ArtiestGroep {set; get;}
    public int? ArtiestGroepId {set; get;}
}