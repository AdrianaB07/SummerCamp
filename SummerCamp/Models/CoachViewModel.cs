using System.ComponentModel.DataAnnotations;

namespace SummerCamp.Models
{
    public class CoachViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Va rugam adaugati un nume!")]
        public string FullName { get; set; }
    }
}
