using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dracula.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dracula.Repository.Impl
{
    public class ActorRepository : IActorRepository
    {
        private readonly DraculaDbContext _dbContext;

        public ActorRepository(DraculaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Actor actor)
        {
            await _dbContext.Actor.AddAsync(actor);
        }

        public async Task<Actor> GetById(Guid id)
        {
            return await _dbContext.Actor
                .Include(a => a.Nationality)
                .FirstAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Actor>> Get(int begin, int amount)
        {
            return await _dbContext.Actor
                .Include(f => f.Nationality)
                .Skip(begin).Take(amount).ToListAsync();
        }

        public async Task<IEnumerable<Actor>> GetCast(Film film)
        {
            return await _dbContext.Casting
                .Where(c => c.Film == film)
                .Select(c => c.Actor)
                .Include(a => a.Nationality)
                .ToListAsync();
        }
    }
}