using System.Collections.Generic;
using Dracula.Domain;
using Dracula.Repository;
using HotChocolate;

namespace Dracula.Api.Resolvers
{
    public class ActorResolver
    {
        public IEnumerable<Actor> GetActors([Parent]Film film,
            [Service]IActorRepository repository)
        {
            return repository.GetCast(film)
                .ConfigureAwait(false).GetAwaiter().GetResult();
        } 
    }
}