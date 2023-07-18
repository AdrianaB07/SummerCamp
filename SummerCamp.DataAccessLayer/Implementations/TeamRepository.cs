using Microsoft.EntityFrameworkCore;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataAccessLayer.Repositories;
using SummerCamp.DataModels.Models;
using System.Linq.Expressions;

namespace SummerCamp.DataAccessLayer.Implementations
{
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository(SummerCampDbContext dbContext) : base(dbContext)
        {
        }

        public override IList<Team> Get(Expression<Func<Team, bool>> expression)
        {
            return dbContext.Teams.Include(c => c.Coach).Include(x => x.CompetitionTeams).Where(expression).ToList();
        }
        public override IList<Team> GetAll()
        {
            return dbContext.Teams.Include(c => c.Coach).ToList();
        }
    }
}
