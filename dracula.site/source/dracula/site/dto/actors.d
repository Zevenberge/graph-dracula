module dracula.site.dto.actors;

import std.datetime;
import std.typecons;
import std.uuid;
import vibe.data.serialization;

class ActorDetailsDto
{
    UUID id;
    string name;
    Date dateOfBirth;
    CountryDto nationality;
    PlayDto[] films;
}

class ActorListItemDto
{
    UUID id;
    string name;
}

class CountryDto
{
    string iso;
    string name;
}

class PlayDto
{
    string role;
    FilmDto film;
}

class FilmDto
{
    UUID id;
    string name;
    int releaseYear;
}

class ChangeActorDto
{
    UUID id;
    @optional Nullable!string name;
    @optional Nullable!Date dateOfBirth;
    @optional Nullable!string nationality;
}