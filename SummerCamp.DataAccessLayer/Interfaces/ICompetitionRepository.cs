using SummerCamp.DataModels.Models;
using SummerCamp.Models;

namespace SummerCamp.DataAccessLayer.Interfaces
{
    public interface ICompetitionRepository : IGenericRepository<Competition>
    {
        public List<SelectTeamViewModel> GetSelectedTeams(int id);
    }
}
