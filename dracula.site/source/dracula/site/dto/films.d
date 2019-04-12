module dracula.site.dto.films;

import std.typecons;
import std.uuid;
import vibe.data.serialization;
import dracula.site.dto.countries;

class FilmDetailsDto
{
    UUID id;
    string name;
    int releaseYear;
    CountryDto country;
    PlayDto[] actors;
}

class FilmListItemDto
{
    UUID id;
    string name;
}

class PlayDto
{
    string role;
    ActorDto actor;
}

class ActorDto
{
    UUID id;
    string name;
    Tuple!(string, "name") nationality;
}

class CreateFilmDto
{
    string name;
    int releaseYear;
    string country;
}

class ChangeFilmDto
{
    UUID id;
    @optional Nullable!string name;
    @optional Nullable!int releaseYear;
    @optional Nullable!string country;
}