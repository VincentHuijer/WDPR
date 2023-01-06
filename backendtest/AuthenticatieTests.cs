using Moq;
using Microsoft.EntityFrameworkCore;
namespace backendtest;


public class AuthenticatieTests
{
    [Fact]
    public async void TestDomainDisposable()
    {
        Mock<IEmailService> emailServiceMock = new Mock<IEmailService>();
        GebruikerService gebruikerService = new GebruikerService(emailServiceMock.Object);
        emailServiceMock.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>()));

        var result = gebruikerService.CheckDomainIsDisposable("test@yopmail.com");
    
        Assert.True(await result);
    }
    
    [Fact]
    public async void TestRegistreerValid()
    {
        var options = new DbContextOptionsBuilder<GebruikerContext>()
                            .UseInMemoryDatabase("MyInMemoryDb").Options;

        var context = new GebruikerContext(options);
        Mock<IEmailService> emailServiceMock = new Mock<IEmailService>();
        GebruikerService gebruikerService = new GebruikerService(emailServiceMock.Object);
        emailServiceMock.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>()));
        var result = gebruikerService.Registreer("TestVoornaam", "TestAchternaam", "unique1@gmail.com", "TestWachtwoord", new VerificatieToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(3)}, context);
        var expected = "Success";
        Assert.Equal(expected, await result);
    }

    [Fact]
    public async void TestRegistreerInvalidEmail()
    {
        var options = new DbContextOptionsBuilder<GebruikerContext>()
                            .UseInMemoryDatabase("MyInMemoryDb").Options;

        var context = new GebruikerContext(options);
        Mock<IEmailService> emailServiceMock = new Mock<IEmailService>();
        GebruikerService gebruikerService = new GebruikerService(emailServiceMock.Object);
        emailServiceMock.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>()));
        var result = gebruikerService.Registreer("TestVoornaam", "TestAchternaam", "unique2@yopmail.com", "TestWachtwoord", new VerificatieToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(3)}, context);
        var expected = "DisposableMailError";
        Assert.Equal(expected, await result);
    }

    [Fact]
    public async void TestRegistreerEmailInUse()
    {
        var options = new DbContextOptionsBuilder<GebruikerContext>()
                            .UseInMemoryDatabase("MyInMemoryDb").Options;

        var context = new GebruikerContext(options);
        Mock<IEmailService> emailServiceMock = new Mock<IEmailService>();
        GebruikerService gebruikerService = new GebruikerService(emailServiceMock.Object);
        emailServiceMock.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>()));
        await gebruikerService.Registreer("TestVoornaam", "TestAchternaam", "unique3@gmail.com", "TestWachtwoord", new VerificatieToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(3)}, context);
        var result = gebruikerService.Registreer("TestVoornaam", "TestAchternaam", "unique3@gmail.com", "TestWachtwoord", new VerificatieToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(3)}, context);
        var expected = "EmailInUseError";
        Assert.Equal(expected, await result);
    }

    [Fact]
    public async void TestLoginSucces()
    {
        var options = new DbContextOptionsBuilder<GebruikerContext>()
                            .UseInMemoryDatabase("MyInMemoryDb").Options;

        var context = new GebruikerContext(options);
        
        Mock<IEmailService> emailServiceMock = new Mock<IEmailService>();
        GebruikerService gebruikerService = new GebruikerService(emailServiceMock.Object);
        emailServiceMock.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>()));
        await gebruikerService.Registreer("TestVoornaam", "TestAchternaam", "unique4@gmail.com", "TestWachtwoord", new VerificatieToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(3)}, context);
        Klant klant = context.Klanten.First(k => k.Email == "unique4@gmail.com");
        klant.VerificatieToken = null;
        klant.TokenId = null;
        await context.SaveChangesAsync();
        var result = gebruikerService.Login("unique4@gmail.com", "TestWachtwoord", context);
        var expected = "Success";
        Assert.Equal(expected, await result);
    }
        [Fact]
    public async void TestLoginNotVerified()
    {
        var options = new DbContextOptionsBuilder<GebruikerContext>()
                            .UseInMemoryDatabase("MyInMemoryDb").Options;

        var context = new GebruikerContext(options);
        
        Mock<IEmailService> emailServiceMock = new Mock<IEmailService>();
        GebruikerService gebruikerService = new GebruikerService(emailServiceMock.Object);
        emailServiceMock.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>()));
        await gebruikerService.Registreer("TestVoornaam", "TestAchternaam", "unique5@gmail.com", "TestWachtwoord", new VerificatieToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(3)}, context);
        var result = gebruikerService.Login("unique5@gmail.com", "TestWachtwoord", context);
        var expected = "NotVerifiedError";
        Assert.Equal(expected, await result);
    }
    [Fact]
    public async void TestLoginInvalidCredentials()
    {
        var options = new DbContextOptionsBuilder<GebruikerContext>()
                            .UseInMemoryDatabase("MyInMemoryDb").Options;

        var context = new GebruikerContext(options);
        
        Mock<IEmailService> emailServiceMock = new Mock<IEmailService>();
        GebruikerService gebruikerService = new GebruikerService(emailServiceMock.Object);
        emailServiceMock.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>()));
        await gebruikerService.Registreer("TestVoornaam", "TestAchternaam", "unique6@gmail.com", "TestWachtwoord", new VerificatieToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(3)}, context);
        Klant klant = context.Klanten.First(k => k.Email == "unique6@gmail.com");
        klant.VerificatieToken = null;
        klant.TokenId = null;
        await context.SaveChangesAsync();
        var result = gebruikerService.Login("unique6@gmail.com", "fout", context);
        var expected = "InvalidCredentialsError";
        Assert.Equal(expected, await result);
    }

    [Fact]
    public async void TestLoginInvalidCredentialsUserDoesNotExist()
    {
        var options = new DbContextOptionsBuilder<GebruikerContext>()
                            .UseInMemoryDatabase("MyInMemoryDb").Options;

        var context = new GebruikerContext(options);
        
        Mock<IEmailService> emailServiceMock = new Mock<IEmailService>();
        GebruikerService gebruikerService = new GebruikerService(emailServiceMock.Object);
        emailServiceMock.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>()));
        var result = gebruikerService.Login("unique7@gmail.com", "fout", context);
        var expected = "InvalidCredentialsError";
        Assert.Equal(expected, await result);
    }

    [Fact]
    public async void TestLoginUserBlocked()
    {
        var options = new DbContextOptionsBuilder<GebruikerContext>()
                            .UseInMemoryDatabase("MyInMemoryDb").Options;

        var context = new GebruikerContext(options);
        
        Mock<IEmailService> emailServiceMock = new Mock<IEmailService>();
        GebruikerService gebruikerService = new GebruikerService(emailServiceMock.Object);
        emailServiceMock.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>()));
        await gebruikerService.Registreer("TestVoornaam", "TestAchternaam", "unique8@gmail.com", "TestWachtwoord", new VerificatieToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(3)}, context);
        Klant klant = context.Klanten.First(k => k.Email == "unique8@gmail.com");
        klant.IsBlocked = true;
        await context.SaveChangesAsync();
        var result = gebruikerService.Login("unique8@gmail.com", "fout", context);
        var expected = "UserBlockedError";
        Assert.Equal(expected, await result);
    }

    [Fact]
    public async void TestVerifieerSuccesvol()
    {
        var options = new DbContextOptionsBuilder<GebruikerContext>()
                            .UseInMemoryDatabase("MyInMemoryDb").Options;

        var context = new GebruikerContext(options);
        
        Mock<IEmailService> emailServiceMock = new Mock<IEmailService>();
        GebruikerService gebruikerService = new GebruikerService(emailServiceMock.Object);
        emailServiceMock.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>()));


        await gebruikerService.Registreer("TestVoornaam", "TestAchternaam", "unique9@gmail.com", "TestWachtwoord", new VerificatieToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(3)}, context);
        Klant klant = context.Klanten.First(k => k.Email == "unique9@gmail.com");
        string token = klant.VerificatieToken.Token;
    
        var result = gebruikerService.Verifieer("unique9@gmail.com", token, context);
        var expected = "Success";
        Assert.Equal(expected, await result);
    }

    [Fact]
    public async void TestVerifieerAlreadyVerified()
    {
        var options = new DbContextOptionsBuilder<GebruikerContext>()
                            .UseInMemoryDatabase("MyInMemoryDb").Options;

        var context = new GebruikerContext(options);
        
        Mock<IEmailService> emailServiceMock = new Mock<IEmailService>();
        GebruikerService gebruikerService = new GebruikerService(emailServiceMock.Object);
        emailServiceMock.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>()));


        await gebruikerService.Registreer("TestVoornaam", "TestAchternaam", "unique10@gmail.com", "TestWachtwoord", new VerificatieToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(3)}, context);
        Klant klant = context.Klanten.First(k => k.Email == "unique10@gmail.com");
        string token = klant.VerificatieToken.Token;
        klant.TokenId = null;
        klant.VerificatieToken = null;
        await context.SaveChangesAsync();
        var result = gebruikerService.Verifieer("unique10@gmail.com", token, context);
        var expected = "AlreadyVerifiedError";
        Assert.Equal(expected, await result);
    }

    [Fact]
    public async void TestVerifieerExpiredToken()
    {
        var options = new DbContextOptionsBuilder<GebruikerContext>()
                            .UseInMemoryDatabase("MyInMemoryDb").Options;

        var context = new GebruikerContext(options);
        
        Mock<IEmailService> emailServiceMock = new Mock<IEmailService>();
        GebruikerService gebruikerService = new GebruikerService(emailServiceMock.Object);
        emailServiceMock.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>()));


        await gebruikerService.Registreer("TestVoornaam", "TestAchternaam", "unique11@gmail.com", "TestWachtwoord", new VerificatieToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(3)}, context);
        Klant klant = context.Klanten.First(k => k.Email == "unique11@gmail.com");
        VerificatieToken verificatieToken = klant.VerificatieToken;
        verificatieToken.VerloopDatum = DateTime.Now.AddDays(-1);
        await context.SaveChangesAsync();
        var result = gebruikerService.Verifieer("unique11@gmail.com", verificatieToken.Token, context);
        var expected = "ExpiredTokenError";
        Assert.Equal(expected, await result);
    }
    [Fact]
    public async void TestVerifieerError()
    {
        var options = new DbContextOptionsBuilder<GebruikerContext>()
                            .UseInMemoryDatabase("MyInMemoryDb").Options;

        var context = new GebruikerContext(options);
        
        Mock<IEmailService> emailServiceMock = new Mock<IEmailService>();
        GebruikerService gebruikerService = new GebruikerService(emailServiceMock.Object);
        emailServiceMock.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>()));


        await gebruikerService.Registreer("TestVoornaam", "TestAchternaam", "unique12@gmail.com", "TestWachtwoord", new VerificatieToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(3)}, context);
        Klant klant = context.Klanten.First(k => k.Email == "unique12@gmail.com");

        var result = gebruikerService.Verifieer("unique12@gmail.com", "niet-bestaand-token", context);
        var expected = "ExpiredTokenError";
        Assert.Equal(expected, await result);
    }


}