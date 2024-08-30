using System.ComponentModel.DataAnnotations;

namespace API_Rest_Simple.Models
{
    public class Professional_exp
    {
        public int id { get; set; }

        [Required]
        [MaxLength(100)]
        public string name { get; set; }
    }
}
