using SummerCamp.DataModels.Models;

namespace SummerCamp.Models
{
    public class CompetitionTeamViewModel
    {
        public int Id { get; set; }

        public int? TeamId { get; set; }

        public int? CompetitionId { get; set; }

        public int TotalPoints { get; set; }

        public virtual Competition? Competition { get; set; }

        public virtual Team? Team { get; set; }

    }
}
