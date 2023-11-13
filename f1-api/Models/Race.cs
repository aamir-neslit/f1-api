namespace f1_api.Models
{
	public class Race
	{
        public int Id { get; set; }
        public required string WinnerName { get; set; }
        public required TimeOnly WinnerTime { get; set; }
        public required string GrandPrix { get; set; }
        public required int NumberOfLaps { get; set; }
    }
}

