namespace f1_api.Models
{
	public class Team
	{
        public int Id { get; set; }
        public required string Manufacturer { get; set; }
        public IFormFile Image { get; set; }
        public required Driver Driver1 { get; set; }
        public required Driver Driver2 { get; set; }
    }
}

