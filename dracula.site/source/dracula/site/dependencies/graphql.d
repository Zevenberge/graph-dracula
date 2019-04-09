module dracula.site.dependencies.graphql;

import vibe.core.log;
import vibe.data.json;
import vibe.http.client;
import dracula.site.startup.settings;

auto query(TQuery, string ql)()
{
    import std.conv : to;
    struct Query
    {
        string query;
    }
    auto response = requestHTTP(endpoint, (scope request) {
        request.method = HTTPMethod.POST;
        request.writeJsonBody(Query(ql));
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