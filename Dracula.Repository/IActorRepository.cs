using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dracula.Domain;

namespace Dracula.Repository
{
    public interface IActorRepository
    {
        Task<Actor> GetById(Guid id);
        Task<IEnumerable<Actor>> Get(int begin, int amount);
        Task Add(Actor actor);
        Task<IEnumerable<Actor>> GetCast(Film film);
    }
}