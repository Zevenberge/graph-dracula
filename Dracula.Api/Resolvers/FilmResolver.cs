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
        public async Task<IEnumerable<Film>> GetAll([Service] IFilmRepository repository)
        {
            return await repository.Get(0, int.MaxValue);
        }

        public async Task<Film> GetById(Guid id, [Service]IFilmRepository repository)
        {
            return await repository.GetById(id);
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

        public async Task<Film> Edit(EditFilm data,
            [Service]IFilmRepository repository,
            [Service]ICountryRepository countries)
        {
            Country country = null;
            if (!string.IsNullOrWhiteSpace(data.CountryIso))
            {
                country = await countries.GetByIso(data.CountryIso);
            }
            var film = await repository.GetById(data.Id);
            film.CorrectInformation(data.Name, data.ReleaseYear, country);
            return film;
        }
    }
}