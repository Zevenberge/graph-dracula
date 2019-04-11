module dracula.site.pages.actors;

import std.uuid;
import vibe.web.common;
import vibe.web.web;
import dracula.site.dependencies.actors;
import dracula.site.dependencies.countries;
import dracula.site.dto.actors;

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
}

@path("/api/actor")
interface IActorApi
{
    UUID postCreate(CreateActorDto dto);
    void postEdit(ChangeActorDto dto);
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
}
