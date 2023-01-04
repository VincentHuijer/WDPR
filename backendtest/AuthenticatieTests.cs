using Moq;

namespace backendtest;


public class AuthenticatieTests
{
    [Fact]
    public async void TestDomainDisposable()
    {
        Mock<IEmailService> emailServiceMock = new Mock<IEmailService>();
        GebruikerService gebruikerService = new GebruikerService(emailServiceMock.Object);
        emailServiceMock.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>()));

        var result = gebruikerService.CheckDomainIsDisposable("test@yopmail.com"); // Gebruik API, test werkt nog niet.
    
        Assert.True(await result);
    }
    //Afhankelijkheid van database in gebruikerservice aanpassen zodat dit te mocken is.
    
}