using System.ComponentModel.DataAnnotations;

namespace WebApp123.Models
{
    [Display(Name = "Owner")]
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Range(18, 100, ErrorMessage = "Please enter an age between 18 and 100.")]
        public int Age { get; set; }
        public List<Pet> Pets { get; set; } = new List<Pet>();

        public string GetFullName()
        {
            return $"{Name} {Surname}";
        }
    }
}
