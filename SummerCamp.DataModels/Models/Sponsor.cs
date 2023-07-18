using System;
using System.Collections.Generic;

namespace SummerCamp.DataModels.Models;

public partial class Sponsor
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public virtual ICollection<Competition> Competitions { get; set; } = new List<Competition>();

    public virtual ICollection<TeamSponsor> TeamSponsors { get; set; } = new List<TeamSponsor>();
}
