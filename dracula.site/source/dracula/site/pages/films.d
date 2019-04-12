module dracula.site.pages.films;

import std.uuid;
import vibe.web.common;
import vibe.web.web;
import dracula.site.dependencies.films;
import dracula.site.dependencies.countries;
import dracula.site.dto.films;

@path("/film")
class FilmService
{
    void get()
    {
        auto films = getFilms;
        render!("films.dt", films);
    }

    @path(":id")
    void get(UUID _id)
    {
        import std.algorithm : sort;

        auto result = getFilm(_id);
        auto film = result.film;
        auto countries = result.countries.sort!((a, b) => a.name < b.name);
        render!("film-details.dt", film, countries);
    }

    void getNew()
    {
        auto countries = getCountries();
        render!("film-create.dt", countries);
    }
}

@path("/api/film")
interface IFilmApi
{
    UUID postCreate(CreateFilmDto dto);
    void postEdit(ChangeFilmDto dto);
}
class FilmApi : IFilmApi
{
    @bodyParam("dto")
    UUID postCreate(CreateFilmDto dto)
    {
        return createFilm(dto);
    }

    @bodyParam("dto")
    void postEdit(ChangeFilmDto dto)
    {
        editFilm(dto);
    }
}
