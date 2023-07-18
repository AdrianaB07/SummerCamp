﻿using Microsoft.EntityFrameworkCore;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataAccessLayer.Repositories;
using SummerCamp.DataModels.Models;
using System.Linq.Expressions;

namespace SummerCamp.DataAccessLayer.Implementations
{
    public class CompetitionMatchRepository : GenericRepository<CompetitionMatch>, ICompetitionMatchRepository
    {
        public CompetitionMatchRepository(SummerCampDbContext dbContext) : base(dbContext)
        {
        }
        public override IList<CompetitionMatch> Get(Expression<Func<CompetitionMatch, bool>> expression)
        {
            return dbContext.CompetitionMatches.Include(c => c.HomeTeam).Include(c => c.AwayTeam).Where(expression).ToList();
        }
        public override IList<CompetitionMatch> GetAll()
        {
            return dbContext.CompetitionMatches.Include(c => c.HomeTeam).Include(c => c.AwayTeam).ToList();
        }
    }
}

