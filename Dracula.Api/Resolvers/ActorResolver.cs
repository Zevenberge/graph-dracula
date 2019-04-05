using System.Collections.Generic;
using System.Threading.Tasks;
using Dracula.Domain;
using Dracula.Repository;
using HotChocolate;

namespace Dracula.Api.Resolvers
{
    public class ActorResolver
    {
        public async Task<IEnumerable<Actor>> GetActors([Parent]Film film,
            [Service]IActorRepository repository)
        {
            return await repository.GetCast(film);
        } 
    }
}