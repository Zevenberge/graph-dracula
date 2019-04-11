module dracula.site.pages.actors;

import std.uuid;
import vibe.web.common;
import vibe.web.web;
import dracula.site.dependencies.actors;

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
}
