public interface IEmailService{

    Task Send(string email, string content);
    
}