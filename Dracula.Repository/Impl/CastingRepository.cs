using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dracula.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dracula.Repository.Impl
{
    public class CastingRepository : ICastingRepository
    {
        private readonly DraculaDbContext _dbContext;

        public CastingRepository(DraculaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Casting>> GetCast(Film film)
        {
            return await Casting.Where(c => c.Film == film).ToListAsync();
        }

        public async Task<IEnumerable<Casting>> GetPlays(Actor actor)
        {
            return await Casting.Where(c => c.Actor == actor).ToListAsync();
        }

        public async Task<Casting> GetById(Guid id)
        {
            return await Casting.FirstAsync(x => x.Id == id);
        }

        public async Task Add(Casting casting)
        {
            await _dbContext.Casting.AddAsync(casting);
        }

        public async Task DeleteById(Guid id)
        {
            var entity = await GetById(id);
            _dbContext.Casting.Remove(entity);
        }

        private IQueryable<Casting> Casting => _dbContext.Casting
            .Include(x => x.Film)
            .Include(x => x.Actor);
    }
}