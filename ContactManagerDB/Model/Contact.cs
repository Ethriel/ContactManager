using System.ComponentModel.DataAnnotations;

namespace ContactManagerDB.Model
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public bool Married { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public decimal Salary { get; set; }
    }
}
