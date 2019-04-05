using System;
using System.Collections.Generic;
using Dracula.Domain;
using Dracula.Repository;
using HotChocolate;

namespace Dracula.Api.Resolvers
{
    public class FilmResolver
    {
        public IEnumerable<Film> GetFilms([Parent]Actor actor,
            [Service]IFilmRepository repository)
        {
            return repository.GetFilmeography(actor)
                .ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public IEnumerable<Film> GetAll([Service] IFilmRepository repository)
        {
            return repository.Get(0, int.MaxValue)
                .ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public Film GetById(string id, [Service]IFilmRepository repository)
        {
            var guid = Guid.Parse(id);
            return repository.GetById(guid)
                .ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}