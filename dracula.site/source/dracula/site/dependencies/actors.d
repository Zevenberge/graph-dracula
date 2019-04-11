module dracula.site.dependencies.actors;

import std.typecons;
import std.uuid;
import dracula.site.dependencies.graphql;
import dracula.site.dto.actors;

ActorListItemDto[] getActors()
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

alias ActorAndCountriesDto = Tuple!(ActorDetailsDto, "actor", CountryDto[], "countries");

ActorAndCountriesDto getActor(UUID id)
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
    return ActorAndCountriesDto(result.actor, result.countries);
}

void editActor(ChangeActorDto dto)
{
    auto result = query!(Tuple!(IdParameter, "editActor"), q{
        mutation($data: EditActorInput!) {
            editActor(data: $data) {
                id
            }
        }
    })(DataParameter!ChangeActorDto(dto));
}

private:

class ActorsQuery
{
    ActorListItemDto[] actors;
}

class ActorQuery
{
    ActorDetailsDto actor;
    CountryDto[] countries;
}

struct IdParameter
{
    UUID id;
}

struct DataParameter(TData)
{
    TData data;
}