import vibe.d;
import dracula.site.startup.routing;
import dracula.site.startup.settings;

shared static this()
{
	auto router = configureRouter;
	auto settings =	configureSettings;
	listenHTTP(settings, router);
}
