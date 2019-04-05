using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dracula.Api.Schema;
using Dracula.Domain;
using Dracula.Repository;
using HotChocolate;

namespace Dracula.Api.Resolvers
{
    public class FilmResolver
    {
        public async Task<IEnumerable<Film>> GetFilms([Parent]Actor actor,
            [Service]IFilmRepository repository)
        {
            return await repository.GetFilmeography(actor);
        }

        public async Task<IEnumerable<Film>> GetAll([Service] IFilmRepository repository)
        {
            return await repository.Get(0, int.MaxValue);
        }

        public async Task<Film> GetById(string id, [Service]IFilmRepository repository)
        {
            Console.WriteLine($"Getting film with ID {id}");
            var guid = Guid.Parse(id);
            return await repository.GetById(guid);
        }

        public async Task<Film> Create(CreateFilm data, 
            [Service]IFilmRepository repository,
            [Service]ICountryRepository countries)
        {
            var country = await countries.GetByIso(data.CountryIso);
            var film = new Film(data.Name, data.ReleaseYear, country);
            await repository.Add(film);
            return film;
        }
    }
}