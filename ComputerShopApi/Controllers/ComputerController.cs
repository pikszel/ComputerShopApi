using ComputerShopApi.Models;
using ComputerShopApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShopApi.Controllers
{
    [Route("computers")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        public ComputerShopDbContext _context = new ComputerShopDbContext();

        [HttpGet("all")]
        public object GetAll()
        {
            var computerz = _context.Computers.ToList();

            if (computerz != null)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés!", result = computerz });
            }
            else if (computerz.Count == 0)
            {
                return StatusCode(404, new { message = "Sikeres lekérdezés!", result = computerz });
            }
            else
            {
                return new { message = "Hiba a lekérdezésben!", result = computerz };
            }
        }

        [HttpGet("byId")]
        public object GetById([FromQuery] Guid id)
        {
            var computer = _context.Computers.SingleOrDefault(c => c.Id == id);

            if (computer != null)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés!", result = computer });
            }

            return StatusCode(404, new { message = "Nincs ilyen Id!", result = computer });
        }

        [HttpPost("newComputer")]
        public object NewComputer([FromBody] NewComputerDto newComputerDto)
        {
            var osExists = _context.Osystems.Any(o => o.Id == newComputerDto.OsId);

            if (!osExists)
            {
                return StatusCode(404, new { message = "Nincs ilyen operációs rendszer!", result = (object?)null });
            }

            var computer = new Computer
            {
                Id = Guid.NewGuid(),
                Brand = newComputerDto.Brand,
                Display = newComputerDto.Display,
                Memory = newComputerDto.Memory,
                CreatedTime = DateTime.Now,
                OsId = newComputerDto.OsId
            };

            _context.Computers.Add(computer);
            _context.SaveChanges();

            return StatusCode(201, new { message = "Sikeres hozzáadás!", result = computer });
        }




    }
}
