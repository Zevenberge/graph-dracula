using System.Collections.Generic;
using System.Threading.Tasks;
using Dracula.Api.Schema;
using Dracula.Domain;
using Dracula.Repository;
using HotChocolate;

namespace Dracula.Api.Resolvers
{
    public class ActorResolver
    {
       public async Task<IEnumerable<Actor>> GetAll(
            [Service]IActorRepository repository)
        {
            return await repository.Get(0, int.MaxValue);
        } 

        public async Task<IEnumerable<Actor>> GetActors([Parent]Film film,
            [Service]IActorRepository repository)
        {
            return await repository.GetCast(film);
        } 

        public async Task<Actor> Create(CreateActor data, 
            [Service]IActorRepository repository,
            [Service]ICountryRepository countries)
        {
            var country = await countries.GetByIso(data.Nationality);
            var actor = new Actor(data.Name, data.DateOfBirth, country);
            await repository.Add(actor);
            return actor;
        }

        public async Task<Actor> Edit(EditActor data,
            [Service]IActorRepository repository,
            [Service]ICountryRepository countries)
        {
            Country nationality = null;
            if(!string.IsNullOrWhiteSpace(data.Nationality))
            {
                nationality = await countries.GetByIso(data.Nationality);
            }
            var actor = await repository.GetById(data.Id);
            actor.CorrectInformation(data.Name, data.DateOfBirth, nationality);
            return actor;
        }
    }
}