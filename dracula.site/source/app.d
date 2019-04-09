import std.stdio;

import vibe.d;

shared static this()
{
	import std.functional : toDelegate;
	auto router = new URLRouter;
	router.get("/", &index);
	auto settings = new HTTPServerSettings;
	settings.port = 8080;
	settings.errorPageHandler = toDelegate(&errorPage);
	listenHTTP(settings, router);
}

void index(HTTPServerRequest req, HTTPServerResponse res)
{
	res.render!("index.dt", req);
}

void errorPage(HTTPServerRequest req,
	HTTPServerResponse res,
	HTTPServerErrorInfo error)
{
	res.render!("error.dt", req, error);
}