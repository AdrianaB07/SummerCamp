using SummerCamp.DataModels.Models;

namespace SummerCamp.Models
{
    public class PlayerViewModel
    {
        public int Id { get; set; }

        public string? FullName { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? Adress { get; set; }

        public int? Position { get; set; }

        public int? ShirtNumber { get; set; }

        public int? TeamId { get; set; }
        public virtual Team? Team { get; set; }
        //public IEnumerable<Team> Teams { get; set; }
        //public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
    }
}
