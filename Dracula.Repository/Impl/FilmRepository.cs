using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dracula.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dracula.Repository.Impl
{
    public class FilmRepository : IFilmRepository
    {
        private readonly DraculaDbContext _dbContext;

        public FilmRepository(DraculaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Film film)
        {
            await _dbContext.Film.AddAsync(film);
        }

        public async Task<Film> GetById(Guid id)
        {
            return await _dbContext.Film
                .Include(f => f.Country)
                .FirstAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Film>> Get(int begin, int amount)
        {
            return await _dbContext.Film
                .Include(f => f.Country)
                .Skip(begin).Take(amount).ToListAsync();
        }

        public async Task<IEnumerable<Film>> GetFilmeography(Actor actor)
        {
            return await _dbContext.Casting
                .Where(c => c.Actor == actor)
                .Select(c => c.Film)
                .Include(f => f.Country)
                .ToListAsync();
        }
    }
}