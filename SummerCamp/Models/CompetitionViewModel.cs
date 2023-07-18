using SummerCamp.DataModels.Models;

namespace SummerCamp.Models
{
    public class CompetitionViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? NumberOfTeams { get; set; }

        public string? Adress { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? SponsorId { get; set; }
        public virtual Sponsor? Sponsor { get; set; }
        public List<int>? SelectedTeamIds { get; set; }
        public List<Team>? AvailableTeams { get; set; } = new List<Team>();
        public List<TeamViewModel>? SelectedTeams { get; set; }
        public List<CompetitionTeam>? CompetitionTeams { get; set; } = new List<CompetitionTeam>();
        public List<TeamViewModel>? Teams { get; set; }
    }
}
