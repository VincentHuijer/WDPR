public interface IEmailService{

    Task Send(string email, string content);
    Task<bool> CheckDomainIsDisposable(string email);
}