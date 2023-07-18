using SummerCamp.DataModels.Models;
using System.ComponentModel.DataAnnotations;

namespace SummerCamp.Models
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please add Nickname!")]
        public string? NickName { get; set; }
        [Required(ErrorMessage = "Please add Name!")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please select a sponsor.")]
        public int? CoachId { get; set; }
        public virtual Coach? Coach { get; set; }
        public int? CompetitionTeamId { get; set; }
        public List<int>? SelectedPlayerIds { get; set; }
        public List<PlayerViewModel>? AvailablePlayers { get; set; } = new List<PlayerViewModel>();
        public List<PlayerViewModel>? SelectedPlayers { get; set; }

    }
}
