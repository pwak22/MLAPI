namespace MobileLegendsAPI.Models
{
    public class Heroes
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; }
        public string Role { get; set; }
        public int Price { get; set; }
        public bool Enabled { get; set; }

    }
}
