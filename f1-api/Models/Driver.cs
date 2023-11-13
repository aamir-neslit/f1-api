namespace f1_api.Models
{
	public class Driver
	{
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int Age { get; set; }
        public required string Nationality { get; set; }
        public IFormFile Image { get; set; }
    }
}

