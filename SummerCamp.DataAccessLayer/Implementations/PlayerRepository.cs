using Microsoft.EntityFrameworkCore;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataAccessLayer.Repositories;
using SummerCamp.DataModels.Models;
using System.Linq.Expressions;

namespace SummerCamp.DataAccessLayer.Implementations
{
    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(SummerCampDbContext dbContext) : base(dbContext)
        {
        }

        public override IList<Player> Get(Expression<Func<Player, bool>> expression)
        {
            return dbContext.Players.Include(c => c.Team).Where(expression).ToList();
        }
        public override IList<Player> GetAll()
        {
            return dbContext.Players.Include(c => c.Team).ToList();
        }
    }
}
