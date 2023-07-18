using Microsoft.EntityFrameworkCore;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataAccessLayer.Repositories;
using SummerCamp.DataModels.Models;
using SummerCamp.Models;
using System.Linq.Expressions;

namespace SummerCamp.DataAccessLayer.Implementations
{
    public class CompetitionRepository : GenericRepository<Competition>, ICompetitionRepository
    {
        public CompetitionRepository(SummerCampDbContext dbContext) : base(dbContext)
        {
        }

        public override IList<Competition> Get(Expression<Func<Competition, bool>> expression)
        {
            return dbContext.Competitions
                .Include(c => c.Sponsor)
                .Include(c => c.CompetitionTeams)
                    .ThenInclude(ct => ct.Team)
                .Where(expression)
                .ToList();
        }
        public override IList<Competition> GetAll()
        {
            return dbContext.Competitions.Include(c => c.Sponsor).ToList();
        }
        public override Competition? GetById(int id)
        {
            var result = dbContext.Competitions
                .Include(c => c.CompetitionTeams).ThenInclude(c => c.Team)
                .Include(c => c.CompetitionMatches)
                .FirstOrDefault(c => c.Id == id);

            return result;
        }
        public List<SelectTeamViewModel> GetSelectedTeams(int id)
        {
            var query = from cmp in dbContext.Competitions
                        join cmpt in dbContext.CompetitionTeams on cmp.Id equals cmpt.CompetitionId
                        join team in dbContext.Teams on cmpt.TeamId equals team.Id
                        select new SelectTeamViewModel() { Id = team.Id, NickName = team.NickName };

            return query.ToList();

        }
    }
}
