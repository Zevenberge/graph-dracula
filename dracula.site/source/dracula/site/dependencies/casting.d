module dracula.site.dependencies.casting;

import std.uuid;
import std.typecons;
import vibe.core.log;
import vibe.data.json;
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

CastDto getCast(UUID filmId)
{
    auto result = query!(Tuple!(CastDto, "film"), q{
        query($id: Uuid!) {
            film(id: $id) {
                id
                name
                actors {
                    id
                    role
                    actor {
                        id
                        name
                    }
                }
            }
        }
    })(IdParameter(filmId));
    return result.film;
}

void editCast(EditCastDto dto)
{
    import std.algorithm, std.conv, std.format;
    string[] params;
    string[] operations;
    Parameter[string] data;
    foreach(size_t i, add; dto.newContributions)
    {
        logInfo("Addition:" ~ serializeToJson(add).toPrettyString);
        string paramName = "createData" ~ i.to!string;
        params ~= "$" ~ paramName ~": CreateCastingInput!";
        operations ~= q{
            create%s: createCasting(data: $%s) {
                id
            }
        }.format(i, paramName);
        data[paramName] = add.toGenericParameter;
    }

    foreach(size_t i, edit; dto.editedContributions) 
    {
        logInfo("Mutation:" ~ serializeToJson(edit).toPrettyString);
        string paramName = "editData" ~ i.to!string;
        params ~= "$" ~ paramName ~": EditCastingInput!";
        operations ~= q{
            edit%s: editCasting(data: $%s) {
                id
            }
        }.format(i, paramName);
        data[paramName] = edit.toGenericParameter;
    }

    foreach(size_t i, deletion; dto.deletedContributions)
    {
        string paramName = "deleteData" ~ i.to!string;
        params ~= "$" ~ paramName ~": Uuid!";
        operations ~= q{
            delete%s: deleteCasting(id: $%s)
        }.format(i, paramName);
        data[paramName] = deletion.toGenericParameter;
    }

    auto queryString = q{
        mutation(%s) {
            %s
        }
    }.format(
        params.joiner(", "),
        operations.joiner("\r\n")
    );

    variableQuery(queryString, data);
}