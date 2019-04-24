module dracula.site.dependencies.role;

import std.algorithm;
import std.array;
import std.typecons;
import dracula.site.dependencies.graphql;

string[] getRoles()
{
    return query!(SchemaQuery, q{
        query {
            __schema {
                types {
                    name
                    enumValues {
                        name
                    }
                }
            }
        }
    }).__schema.types.filter!(t => t.name == "Role").front
        .enumValues.map!(ev => ev.name).array;
}

private:
alias EnumValue = Tuple!(string, "name");
alias Type = Tuple!(string, "name", Nullable!(EnumValue[]), "enumValues");
alias Schema = Tuple!(Type[], "types");
alias SchemaQuery = Tuple!(Schema, "__schema");