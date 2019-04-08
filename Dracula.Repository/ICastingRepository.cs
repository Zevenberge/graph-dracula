using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dracula.Domain;

namespace Dracula.Repository
{
    public interface ICastingRepository
    {
        Task<Casting> GetById(Guid id);
        Task<IEnumerable<Casting>> GetCast(Film film);
        Task<IEnumerable<Casting>> GetPlays(Actor actor);
        Task Add(Casting casting);
        Task DeleteById(Guid id);
    }
}
