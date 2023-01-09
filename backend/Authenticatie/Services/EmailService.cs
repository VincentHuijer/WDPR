using System.Net;
using System.Net.Mail;
using Newtonsoft.Json;
namespace backend.Authenticatie;
public class EmailService : IEmailService{

    public async Task Send(string email, string content){
        string jsonString = File.ReadAllText("emailconfig.json");
        EmailConfiguration configuration = JsonConvert.DeserializeObject<EmailConfiguration>(jsonString);
        string smtpServer = "smtp.gmail.com"; // SMTP server nog aanmaken
        string username = configuration.Email; // username vanuit aparte file halen
        string password = configuration.Wachtwoord; // password vanuit aparte file halen

        string from = configuration.Email; // Email nog aanmaken
        string subject = "Email Verification";

        MailMessage message = new MailMessage(from, email, subject, content);

        SmtpClient client = new SmtpClient(smtpServer, 587);

        client.Credentials = new NetworkCredential(configuration.Email, configuration.Wachtwoord);
        client.EnableSsl = true;
        
        await client.SendMailAsync(message);
    }



}