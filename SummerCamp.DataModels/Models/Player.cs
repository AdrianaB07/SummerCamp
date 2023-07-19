using System;
using System.Collections.Generic;

namespace SummerCamp.DataModels.Models;

public partial class Player
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Adress { get; set; }

    public int? Position { get; set; }

    public int? ShirtNumber { get; set; }

    public int? TeamId { get; set; }

    public virtual Team? Team { get; set; }
}
