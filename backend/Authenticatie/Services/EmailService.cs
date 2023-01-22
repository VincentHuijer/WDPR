using System.Net;
using System.Net.Mail;
using Newtonsoft.Json;
namespace backend.Authenticatie;
public class EmailService : IEmailService{

    public async Task Send(string email, string content, string subject){
        string jsonString = File.ReadAllText("emailconfig.json");
        EmailConfiguration configuration = JsonConvert.DeserializeObject<EmailConfiguration>(jsonString);
        string smtpServer = "smtp.gmail.com"; 
        string username = configuration.Email; 
        string password = configuration.Wachtwoord; 

        string from = configuration.Email; 
        string _subject = subject;

        MailMessage message = new MailMessage(from, email, _subject, content);

        SmtpClient client = new SmtpClient(smtpServer, 587);

        client.Credentials = new NetworkCredential(configuration.Email, configuration.Wachtwoord);
        client.EnableSsl = true;
        
        await client.SendMailAsync(message);
    }



}