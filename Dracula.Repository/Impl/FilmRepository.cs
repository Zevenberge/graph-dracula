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
            return await _dbContext.Film.FindAsync(id);
        }

        public async Task<IEnumerable<Film>> GetFilmeography(Actor actor)
        {
            return await _dbContext.Casting
                .Where(c => c.Actor == actor)
                .Select(c => c.Film).ToListAsync();
        }
    }
}