using ComputerShopApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopApi.Controllers
{
    [Route("oscomp")]
    [ApiController]
    public class OsCompController : ControllerBase
    {
        public ComputerShopDbContext _context = new ComputerShopDbContext();

        // Az öösszes OS a hozzá tartozó computerekkel
        [HttpGet("allOsAndComputer")]
        public object GetAllOsAndComputer()
        {
            var result = _context.Osystems.Include(o => o.Computers);
            return new { message = result };
        }

        // Adott OS a hozzá tartozó computerekkel
        [HttpGet("osByIdWithComputers")]
        public object osByIdWithComputers([FromQuery] Guid id)
        {
            var result = _context.Osystems.Include(o => o.Computers)
                .Select(o => new
                {
                    o.Id,
                    o.Name,
                    o.Version,
                    Computer = o.Computers
                .Select(c => new { c.Brand, c.Display })
                })
                .SingleOrDefault(o => o.Id == id);
            return new { message = result };
        }
    }
}
