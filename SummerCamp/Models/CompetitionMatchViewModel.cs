using SummerCamp.DataModels.Models;

namespace SummerCamp.Models
{
    public class CompetitionMatchViewModel
    {
        public int Id { get; set; }

        public int? CompetitionId { get; set; }

        public int? HomeTeamId { get; set; }

        public int? AwayTeamId { get; set; }

        public int? HomeTeamGoals { get; set; }

        public int? AwayTeamGoals { get; set; }

        public int? TeamId { get; set; }

        public virtual Team? HomeTeam { get; set; }

        public virtual Team? AwayTeam { get; set; }

    }
}
