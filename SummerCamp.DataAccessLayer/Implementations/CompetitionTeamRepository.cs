using Microsoft.EntityFrameworkCore;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataAccessLayer.Repositories;
using SummerCamp.DataModels.Models;
using System.Linq.Expressions;

namespace SummerCamp.DataAccessLayer.Implementations
{
    public class CompetitionTeamRepository : GenericRepository<CompetitionTeam>, ICompetitionTeamRepository
    {
        public CompetitionTeamRepository(SummerCampDbContext dbContext) : base(dbContext)
        {
        }
        public override IList<CompetitionTeam> Get(Expression<Func<CompetitionTeam, bool>> expression)
        {
            return dbContext.Set<CompetitionTeam>().Include(x => x.Team).Where(expression).ToList();

        }
        public bool RemoveRange(List<CompetitionTeam> competitionTeams)
        {
            dbContext.CompetitionTeams.RemoveRange(competitionTeams);
            return dbContext.SaveChanges() > 0;
        }
    }
}