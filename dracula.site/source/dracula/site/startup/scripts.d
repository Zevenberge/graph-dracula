module dracula.site.startup.scripts;

import vibe.d;

void addScripts(URLRouter router)
{
    router.get("/scripts/*", &sendScript);
}


private void sendScript(HTTPServerRequest req, HTTPServerResponse res)
{
    import std.algorithm : joiner;
    import std.array : array;
    import std.conv : to;
    import std.file : exists;
    import std.stdio : File;
    auto path = req.requestPath;
    auto filename = "public/" ~ path.toString;
    if(filename.exists)
    {
        auto file = File(filename, "r");
        auto contents = file.byLine(Yes.keepTerminator).joiner("").array.to!string;
        res.writeBody(contents, "application/javascript");
    }
    else
    {
        res.statusCode = HTTPStatus.notFound;
        res.writeVoidBody;
    }
}