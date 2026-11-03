using ComputerShopApi.Models;
using ComputerShopApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShopApi.Controllers
{
    [Route("osystems")]
    [ApiController]
    public class OsystemController : ControllerBase
    {
        public ComputerShopDbContext _context = new ComputerShopDbContext();

        [HttpGet("all")]
        public object GetAll()
        {
            var osystems = _context.Osystems.ToList();

            if (osystems != null)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés!", result = osystems });
            }
            else if (osystems.Count == 0)
            {
                return StatusCode(404, new { message = "Sikeres lekérdezés!", result = osystems });
            }
            else
            {
                return new { message = "Hiba a lekérdezésben!", result = osystems };
            }

        }
        [HttpGet("byId")]
        public object GetById([FromQuery] Guid id)
        {
            var osystem = _context.Osystems.SingleOrDefault(o => o.Id == id);

            if (osystem != null)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés!", result = osystem });
            }

            return StatusCode(404, new { message = "Nincs ilyen Id!", result = osystem });
        }

        [HttpPost("newOs")]

        public object NewOs([FromBody] NewOsystemDto newOsystemDto)
        {
            var osystem = new Osystem
            {
                Id = Guid.NewGuid(),
                Name = newOsystemDto.Name,
                Version = newOsystemDto.Version,
                RegisterTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };

            if (osystem != null)
            {
                _context.Osystems.Add(osystem);
                _context.SaveChanges();

                return StatusCode(201, new { message = "Sikeres hozzáadás!", result = osystem });
            }
            else
            {
                return StatusCode(400, new { message = "Hiba a hozzáadásban!", result = osystem });
            }
        }
    }
}
