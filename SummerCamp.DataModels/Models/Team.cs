using System;
using System.Collections.Generic;

namespace SummerCamp.DataModels.Models;

public partial class Team
{
    public int Id { get; set; }

    public string? NickName { get; set; }

    public string? Name { get; set; }

    public int? CoachId { get; set; }

    public virtual Coach? Coach { get; set; }

    public virtual ICollection<CompetitionMatch> CompetitionMatchAwayTeams { get; set; } = new List<CompetitionMatch>();

    public virtual ICollection<CompetitionMatch> CompetitionMatchHomeTeams { get; set; } = new List<CompetitionMatch>();

    public virtual ICollection<CompetitionTeam> CompetitionTeams { get; set; } = new List<CompetitionTeam>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    public virtual ICollection<TeamSponsor> TeamSponsors { get; set; } = new List<TeamSponsor>();
}
