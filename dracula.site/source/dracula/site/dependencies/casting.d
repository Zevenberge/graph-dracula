module dracula.site.dependencies.casting;

import std.uuid;
import std.typecons;
import dracula.site.dependencies.graphql;
import dracula.site.dto.contribution;

void createContribution(CreateContributionDto dto)
{
    auto result = query!(Tuple!(IdParameter, "createCasting"), q{
        mutation($data: CreateCastingInput!) {
            createCasting(data: $data) {
                id
            }
        }
    })(DataParameter!CreateContributionDto(dto));
}

void deleteContribution(UUID id)
{
    query!(void, q{
        mutation($id: Uuid!) {
            deleteCasting(id: $id)
        }
    })(IdParameter(id));
}