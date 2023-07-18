using System;
using System.Collections.Generic;

namespace SummerCamp.DataModels.Models;

public partial class CompetitionMatch
{
    public int Id { get; set; }

    public int CompetitionId { get; set; }

    public int HomeTeamId { get; set; }

    public int AwayTeamId { get; set; }

    public int HomeTeamGoals { get; set; }

    public int AwayTeamGoals { get; set; }

    public virtual Team AwayTeam { get; set; } = null!;

    public virtual Competition Competition { get; set; } = null!;

    public virtual Team HomeTeam { get; set; } = null!;
}
