using System.ComponentModel.DataAnnotations;

namespace API_Rest_Simple.Models
{
    public class Person
    {
        public int id { get; set; }

        [MinLength(5)]
        public required string name { get; set; }
        [Required]
        [Range(1,150)]
        public required int age { get; set; }

        public List<Professional_exp> professional_exp { get; set; }
    }
}
