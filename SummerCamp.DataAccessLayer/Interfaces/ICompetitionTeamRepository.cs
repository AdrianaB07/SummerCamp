using SummerCamp.DataModels.Models;

namespace SummerCamp.DataAccessLayer.Interfaces
{
    public interface ICompetitionTeamRepository : IGenericRepository<CompetitionTeam>
    {
        bool RemoveRange(List<CompetitionTeam> competitionTeams);
    }
}