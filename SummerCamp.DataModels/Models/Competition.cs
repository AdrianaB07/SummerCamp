using System;
using System.Collections.Generic;

namespace SummerCamp.DataModels.Models;

public partial class Competition
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? NumberOfTeams { get; set; }

    public string? Adress { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? SponsorId { get; set; }

    public virtual ICollection<CompetitionMatch> CompetitionMatches { get; set; } = new List<CompetitionMatch>();

    public virtual ICollection<CompetitionTeam> CompetitionTeams { get; set; } = new List<CompetitionTeam>();

    public virtual Sponsor? Sponsor { get; set; }
}
