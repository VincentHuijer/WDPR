using Newtonsoft.Json;
using System.Net.Http;
public class EmailService : IEmailService{

    public async Task Send(string email, string content){
        //Hiervoor moeten we nog een emailservice vinden waarmee je echt emails kan versturen.
    }

    public async Task<bool> CheckDomainIsDisposable(string email){
        return false;
    }

}