module dracula.site.dependencies.actors;

import std.datetime;
import std.typecons;
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

alias ActorAndCountries = Tuple!(ActorDetails, "actor", Country[], "countries");

ActorAndCountries getActor(UUID id)
{
    auto result = query!(ActorQuery, q{
            query($id : Uuid!) {
                actor(id: $id) {
                    id
                    name
                    dateOfBirth
                    nationality {
                        iso
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
                countries {
                    iso
                    name
                }
            }
    })(IdParameter(id));
    return ActorAndCountries(result.actor, result.countries);
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
    string iso;
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
    Country[] countries;
}

struct IdParameter
{
    UUID id;
}
