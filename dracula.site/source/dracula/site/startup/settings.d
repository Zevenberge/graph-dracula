module dracula.site.startup.settings;

import std.experimental.logger;
import vibe.d;

HTTPServerSettings configureSettings()
{
	import std.functional : toDelegate;
	info("Configuring settings");

	auto settings = new HTTPServerSettings;
	settings.port = 8080;
	settings.errorPageHandler = toDelegate(&errorPage);
	return settings;
}

private void errorPage(HTTPServerRequest req, 
	HTTPServerResponse res, 
	HTTPServerErrorInfo error)
{
	.error("Application error: ", error.debugMessage);
	res.render!("error.dt", req, error);
}

immutable string endpoint = "http://localhost:5000/graphql/..";
