namespace WebApp123.Models
{
    public class Vaccine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Pet> Pets { get; set; } = new List<Pet>();
    }
}
