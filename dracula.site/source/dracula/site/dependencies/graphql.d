module dracula.site.dependencies.graphql;

import std.uuid;
import vibe.core.log;
import vibe.data.json;
import vibe.http.client;
import dracula.site.startup.settings;

auto query(TQuery, string ql, TParams...)(TParams params)
{
    import std.conv : to;

    struct Query
    {
        string query;
        static if (TParams.length == 1)
        {
            TParams[0] variables;
        }
    }

    static if (TParams.length == 1)
    {
        auto q = Query(ql, params);
    }
    else
    {
        auto q = Query(ql);
    }

    auto json = sendRequest(q);
    static if (!is(TQuery == void))
    {
        struct Response
        {
            TQuery data;
        }

        return json.deserializeJson!Response.data;
    }
}

void variableQuery(string ql, Parameter[string] params)
{
    struct Query
    {
        string query;
        Parameter[string] variables;
    }

    sendRequest(Query(ql, params));
}

private auto sendRequest(Query)(Query query)
{
    auto response = requestHTTP(endpoint, (scope request) {
        request.method = HTTPMethod.POST;
        logInfo("Outgoing JSON:" ~ serializeToJson(query).toPrettyString);
        request.writeJsonBody(query);
    });
    scope (exit) response.disconnect;
    auto json = response.readJson;
    logInfo("Incoming JSON: " ~ json.toPrettyString);
    auto error = json["errors"];
    if(error.type != Json.Type.undefined)
    {
        throw new Exception(error.toPrettyString);
    }
    return json;
}

struct IdParameter
{
    UUID id;
}

struct DataParameter(TData)
{
    TData data;
}

class Parameter
{
    Json toJson() const
    {
        return Json.init;
    }

    static Parameter fromJson(Json src)
    {
        assert(false);
    }
}

Parameter toGenericParameter(T)(T data)
{
    return new BoxedParameter!T(data);
}

class BoxedParameter(T) : Parameter
{
    this(T data)
    {
        _data = data;
    }
    private T _data;

    override Json toJson() const
    {
        return serializeToJson(_data);
    }

    static BoxedParameter!T fromJson(Json src)
    {
        return new BoxedParameter!T(deserializeJson!T(src));
    }
}