public class Stoel{
    public int StoelID { get; set; }
    public bool IsGereserveerd { get; set; }
    public int Rang { get; set; }
    public double Prijs { get; set; }
    public Zaal Zaal { get; set; }
    public string stoelReserveren(){
        return "geen logica toegevoegd";
    }


}