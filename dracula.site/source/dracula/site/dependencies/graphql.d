module dracula.site.dependencies.graphql;

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
        static if(TParams.length == 1) {
            TParams[0] variables;
        }
    }
    auto response = requestHTTP(endpoint, (scope request) {
        static if(TParams.length == 1) {
            auto q = Query(ql, params);
        } else {
            auto q = Query(ql);
        }
        request.method = HTTPMethod.POST;
        request.writeJsonBody(q);
    });
    struct Response
    {
        TQuery data;
    }
    auto json = response.readJson;
    scope(exit) response.disconnect;
    logInfo(json.toString);
    return json.deserializeJson!Response.data;
}