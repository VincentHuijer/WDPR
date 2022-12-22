public interface IVoorstelling{
    string BetrokkenPersonen { get; set; }
    List<Klant> Kaartjeshouders { get; set; }
    
    Task Send(string email, string content);

}