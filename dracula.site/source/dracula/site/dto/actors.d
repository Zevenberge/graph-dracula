module dracula.site.dto.actors;

import std.datetime;
import std.typecons;
import std.uuid;
import vibe.data.serialization;
import dracula.site.dto.countries;

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

class PlayDto
{
    UUID id;
    string role;
    FilmDto film;
}

class FilmDto
{
    UUID id;
    string name;
    int releaseYear;
}

class CreateActorDto
{
    string name;
    Date dateOfBirth;
    string nationality;
}

class ChangeActorDto
{
    UUID id;
    @optional Nullable!string name;
    @optional Nullable!Date dateOfBirth;
    @optional Nullable!string nationality;
}