using System.Net;
using System.Net.Mail;
public class EmailService : IEmailService{

    public async Task Send(string email, string content){
        string smtpServer = "example.com"; // SMTP server nog aanmaken
        string username = "USERNAME"; // username vanuit aparte file halen
        string password = "PASSWORD"; // password vanuit aparte file halen

        string from = "ONS-EMAIL@GMAIL.COM"; // Email nog aanmaken
        string subject = "Email Verification";

        MailMessage message = new MailMessage(from, email, subject, content);

        SmtpClient client = new SmtpClient(smtpServer);
        client.Credentials = new NetworkCredential(username, password);
        await client.SendMailAsync(message);
    }



}