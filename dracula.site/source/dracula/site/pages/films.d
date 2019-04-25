module dracula.site.pages.films;

import std.uuid;
import vibe.web.common;
import vibe.web.web;
import dracula.site.dependencies.actors;
import dracula.site.dependencies.casting;
import dracula.site.dependencies.countries;
import dracula.site.dependencies.films;
import dracula.site.dependencies.role;
import dracula.site.dto.contribution;
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

    @path(":id/edit-cast")
    void getEditCast(UUID _id)
    {
        auto data = getCast(_id);
        auto contributions = data.actors;
        auto id = _id;
        auto name = data.name;
        auto actors = getActors;
        auto roles = getRoles;
        render!("film-edit-cast.dt", id, name, contributions, actors, roles);
    }
}

@path("/api/film")
interface IFilmApi
{
    UUID postCreate(CreateFilmDto dto);
    void postEdit(ChangeFilmDto dto);
    @path("edit-cast")
    void postEditCast(EditCastDto dto);
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

    @bodyParam("dto")
    void postEditCast(EditCastDto dto)
    {
        editCast(dto);
    }
}
