using System;
using System.Collections.Generic;

namespace SummerCamp.DataModels.Models;

public partial class TeamSponsor
{
    public int Id { get; set; }

    public int? TeamId { get; set; }

    public int? SponsorId { get; set; }

    public virtual Sponsor? Sponsor { get; set; }

    public virtual Team? Team { get; set; }
}
