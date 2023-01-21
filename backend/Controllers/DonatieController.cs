using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Authenticatie;
namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DonatieController : ControllerBase
{

    private readonly GebruikerContext _context;
    public DonatieController(GebruikerContext context)
    {
        _context = context;
    }

    [HttpPost("permission")] //DONE
    public async Task<ActionResult> NieuweDonatie([FromForm] string token){
        return Redirect("https://theater-laak.netlify.app/donatie?token=" + token);
    }

}
