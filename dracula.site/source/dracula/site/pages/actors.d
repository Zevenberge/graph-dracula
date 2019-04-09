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
        auto actor = getActor(_id);
        render!("actor-details.dt", actor);
    }
}
