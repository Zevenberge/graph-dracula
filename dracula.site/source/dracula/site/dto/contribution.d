module dracula.site.dto.contribution;

import std.typecons;
import std.uuid;
import vibe.data.json;

class CreateContributionDto
{
    UUID actor;
    UUID film;
    string role;
}

class EditContributionDto
{
    UUID id;
    @optional Nullable!UUID actor;
    @optional Nullable!UUID film;
    @optional Nullable!string role;
}

class EditCastDto
{
    CreateContributionDto[] newContributions;
    EditContributionDto[] editedContributions;
    UUID[] deletedContributions;
}

class CastDto
{
    UUID id;
    string name;
    ContributionDto[] actors;
}

class ContributionDto
{
    UUID id;
    string role;
    ActorDto actor;
}

class ActorDto
{
    UUID id;
    string name;
}