namespace ComputerShopApi.Models.DTOs
{
    public class NewComputerDto
    {
        public string Brand { get; set; }
        public double Display { get; set; }
        public int Memory { get; set; }
        public Guid OsId { get; set; }
    }
}
