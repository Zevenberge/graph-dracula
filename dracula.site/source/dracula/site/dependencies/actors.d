module dracula.site.dependencies.actors;

import std.uuid;
import dracula.site.dependencies.graphql;

class ActorListItem
{
    UUID id;
    string name;
}

class ActorQuery
{
    ActorListItem[] actors;
}

ActorListItem[] getActors()
{
    return query!(ActorQuery, q{
            query {
                actors {
                    id
                    name
                }
            }
    }).actors;
}
