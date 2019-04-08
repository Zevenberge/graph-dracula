using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dracula.Api.Schema;
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

        public async Task<Casting> Create(CreateCasting data,
            [Service]ICastingRepository repository,
            [Service]IActorRepository actors,
            [Service]IFilmRepository films)
        {
            var actor = await actors.GetById(data.Actor);
            var film = await films.GetById(data.Film);
            var casting = new Casting(film, actor, data.Role);
            await repository.Add(casting);
            return casting;
        }

        public async Task<Casting> Edit(EditCasting data,
            [Service]ICastingRepository repository,
            [Service]IActorRepository actors,
            [Service]IFilmRepository films)
        {
            var casting = await repository.GetById(data.Id);
            Actor actor = null;
            if(data.Actor != null)
            {
                actor = await actors.GetById(data.Actor.Value);
            }
            Film film = null;
            if(data.Film != null)
            {
                film = await films.GetById(data.Film.Value);
            }
            casting.CorrectData(film, actor, data.Role);
            return casting;
        }

        public async Task<Guid> Delete(Guid id, [Service]ICastingRepository repository)
        {
            await repository.DeleteById(id);
            return id;
        }
    }
}