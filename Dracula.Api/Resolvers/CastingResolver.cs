using System.Collections.Generic;
using System.Threading.Tasks;
using Dracula.Domain;
using Dracula.Repository;
using HotChocolate;

namespace Dracula.Api.Resolvers
{
    public class CastingResolver
    {
        public async Task<IEnumerable<Casting>> GetCast([Parent]Film film,
            [Service]ICastingRepository repository)
        {
            return await repository.GetCast(film);
        }

        public async Task<IEnumerable<Casting>> GetPlays([Parent]Actor actor,
            [Service]ICastingRepository repository)
        {
            return await repository.GetPlays(actor);
        }
    }
}