module dracula.site.dependencies.films;

import std.typecons;
import std.uuid;
import dracula.site.dependencies.graphql;
import dracula.site.dto.countries;
import dracula.site.dto.films;

FilmListItemDto[] getFilms()
{
    return query!(FilmsQuery, q{
            query {
                films {
                    id
                    name
                }
            }
    }).films;
}

alias FilmAndCountriesDto = Tuple!(FilmDetailsDto, "film", CountryDto[], "countries");

FilmAndCountriesDto getFilm(UUID id)
{
    auto result = query!(FilmQuery, q{
            query($id : Uuid!) {
                film(id: $id) {
                    id
                    name
                    releaseYear 
                    country {
                        iso
                        name
                    }
                    actors {
                        role
                        actor {
                            id
                            name
                            nationality {
                                name
                            }
                        }
                    }
                }
                countries {
                    iso
                    name
                }
            }
    })(IdParameter(id));
    return FilmAndCountriesDto(result.film, result.countries);
}

UUID createFilm(CreateFilmDto dto)
{
    auto result = query!(Tuple!(IdParameter, "createFilm"), q{
        mutation($data: CreateFilmInput!) {
            createFilm(data: $data) {
                id
            }
        }
    })(DataParameter!CreateFilmDto(dto));
    return result.createFilm.id;
}

void editFilm(ChangeFilmDto dto)
{
    auto result = query!(Tuple!(IdParameter, "editFilm"), q{
        mutation($data: EditFilmInput!) {
            editFilm(data: $data) {
                id
            }
        }
    })(DataParameter!ChangeFilmDto(dto));
}

private:

class FilmsQuery
{
    FilmListItemDto[] films;
}

class FilmQuery
{
    FilmDetailsDto film;
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