using TechTalk.SpecFlow;

[Binding]
public class CommonStep{

    protected GebruikerContext _context;

    public CommonStep(){
        var options = new DbContextOptionsBuilder<GebruikerContext>()
                           .UseInMemoryDatabase("MyInMemoryDb").Options;

        var context = new GebruikerContext(options);
        _context = context;
    }

}