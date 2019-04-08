using System.Collections.Generic;
using System.Threading.Tasks;
using Dracula.Domain;

namespace Dracula.Repository
{
    public interface ICastingRepository
    {
        Task<IEnumerable<Casting>> GetCast(Film film);
        Task<IEnumerable<Casting>> GetPlays(Actor actor);
    }
}
