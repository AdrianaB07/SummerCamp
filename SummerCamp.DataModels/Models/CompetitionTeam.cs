using System;
using System.Collections.Generic;

namespace SummerCamp.DataModels.Models;

public partial class CompetitionTeam
{
    public int Id { get; set; }

    public int? TeamId { get; set; }

    public int? CompetitionId { get; set; }

    public int TotalPoints { get; set; }

    public virtual Competition? Competition { get; set; }

    public virtual Team? Team { get; set; }
}
