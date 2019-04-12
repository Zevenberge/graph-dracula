module dracula.site.startup.routing;

import vibe.d;
import dracula.site.pages.actors;
import dracula.site.pages.films;
import dracula.site.startup.scripts;

URLRouter configureRouter()
{
	auto router = new URLRouter;
	router.get("/", &index);
	router.addScripts;
    router.registerWebInterface(new ActorService);
	router.registerRestInterface(new ActorApi);
	router.registerWebInterface(new FilmService);
	router.registerRestInterface(new FilmApi);
    return router;
}

private void index(HTTPServerRequest req, HTTPServerResponse res)
{
	res.render!("index.dt", req);
}