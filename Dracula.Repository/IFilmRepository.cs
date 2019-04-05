using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dracula.Domain;

namespace Dracula.Repository
{
    public interface IFilmRepository
    {
        Task<Film> GetById(Guid id);
        Task Add(Film film);
        Task<IEnumerable<Film>> GetFilmeography(Actor actor);
        Task<IEnumerable<Film>> Get(int begin, int amount);
    }
}
