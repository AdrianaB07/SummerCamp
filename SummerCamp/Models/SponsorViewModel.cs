using System.ComponentModel.DataAnnotations;

namespace SummerCamp.Models
{
    public class SponsorViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please add name!")]
        public string FullName { get; set; }
    }

}