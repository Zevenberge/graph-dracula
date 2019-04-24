module dracula.site.pages.actors;

import std.uuid;
import vibe.web.common;
import vibe.web.web;
import dracula.site.dependencies.actors;
import dracula.site.dependencies.casting;
import dracula.site.dependencies.countries;
import dracula.site.dependencies.films;
import dracula.site.dependencies.role;
import dracula.site.dto.actors;
import dracula.site.dto.contribution;

@path("/actor")
class ActorService
{
    void get()
    {
        auto actors = getActors;
        render!("actors.dt", actors);
    }

    @path(":id")
    void get(UUID _id)
    {
        import std.algorithm : sort;

        auto result = getActor(_id);
        auto actor = result.actor;
        auto countries = result.countries.sort!((a, b) => a.name < b.name);
        render!("actor-details.dt", actor, countries);
    }

    void getNew()
    {
        auto countries = getCountries();
        render!("actor-create.dt", countries);
    }

    @path("/add")
    void getNewFilm()
    {
        auto films = getFilms;
        auto roles = getRoles;
        render!("actor-new-contribution.dt", films, roles);
    }
}

@path("/api/actor")
interface IActorApi
{
    UUID postCreate(CreateActorDto dto);
    void postEdit(ChangeActorDto dto);
    UUID postContribution(CreateContributionDto dto);
}

class ActorApi : IActorApi
{
    @bodyParam("dto")
    UUID postCreate(CreateActorDto dto)
    {
        return createActor(dto);
    }

    @bodyParam("dto")
    void postEdit(ChangeActorDto dto)
    {
        editActor(dto);
    }

    @bodyParam("dto")
    UUID postContribution(CreateContributionDto dto)
    {
        createContribution(dto);
        return dto.actor;
    }
}
