using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using System;

namespace WebApp123.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Range(0, 50, ErrorMessage = "Please enter an age between 0 and 50.")]
        public int Age { get; set; }
        [Display(Name = "Owner")]
        public int? PersonId { get; set; }
        public Person? Person { get; set; }
        public List<Vaccine> Vaccines { get; set; } = new List<Vaccine>();
        [NotMapped]
        public List<int> VaccinesParams { get; set; }
       
    }
}
