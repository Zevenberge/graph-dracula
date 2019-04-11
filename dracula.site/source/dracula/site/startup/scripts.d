module dracula.site.startup.scripts;

import vibe.d;

void addScripts(URLRouter router)
{
    router.get("/scripts/*", &sendScript);
    router.get("/styles/*", &sendCss);
}

private void sendScript(HTTPServerRequest req, HTTPServerResponse res)
{
    sendFile(req, res, "application/javascript");
}

private void sendCss(HTTPServerRequest req, HTTPServerResponse res)
{
    sendFile(req, res, "text/css");
}

private void sendFile(HTTPServerRequest req, HTTPServerResponse res, string contentType)
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
        res.writeBody(contents, contentType);
    }
    else
    {
        res.statusCode = HTTPStatus.notFound;
        res.writeVoidBody;
    }
}