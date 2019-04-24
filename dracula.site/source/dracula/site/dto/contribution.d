module dracula.site.dto.contribution;

import std.uuid;

class CreateContributionDto
{
    UUID actor;
    UUID film;
    string role;
}