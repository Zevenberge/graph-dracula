module dracula.site.dependencies.actors;

import std.datetime;
import std.uuid;
import dracula.site.dependencies.graphql;

class ActorListItem
{
    UUID id;
    string name;
}

ActorListItem[] getActors()
{
    return query!(ActorsQuery, q{
            query {
                actors {
                    id
                    name
                }
            }
    }).actors;
}

ActorDetails getActor(UUID id)
{
    return query!(ActorQuery, q{
            query($id : Uuid!) {
                actor(id: $id) {
                    id
                    name
                    dateOfBirth
                    nationality {
                        name
                    }
                    films {
                        role
                        film {
                            id
                            name
                            releaseYear
                        }
                    }
                }
            }
    })(IdParameter(id)).actor;
}

class ActorDetails
{
    UUID id;
    string name;
    Date dateOfBirth;
    Country nationality;
    Play[] films;
}

class Country
{
    string name;
}

class Play
{
    string role;
    Film film;
}

class Film
{
    UUID id;
    string name;
    int releaseYear;
}

private:

class ActorsQuery
{
    ActorListItem[] actors;
}

class ActorQuery
{
    ActorDetails actor;
}

struct IdParameter
{
    UUID id;
}
