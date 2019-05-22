# Graph Dracula

A small GraphQL proof-of-concept application. It explores the various capabilities of GraphQL in a minimal application.

## Architecture

Graph Dracula is built using a layered architecture. The core (written in C#) consists of a `Dracula.Domain` project, which defines the business entities. As it is mostly a CRUD application, this part is not very interesting. On top of the domain, we have `Dracula.Repository`, which takes care of the database interactions using EntityFrameworkCore. Finally, we have `Dracula.Api` which provides the GraphQL web interface using AspNetCore and [Hot Chocolate](https://github.com/ChilliCream/hotchocolate).

For the client side, I created a [vibe.d](http://vibed.org/) web site in `dracula.site` (written in D). It's a simple web server that serves semi static web pages made interactive with a little manual Javascript hacking. It communicates with `Dracula.Api`.

To persist the data, a simple off-the-shelf MSSQL Docker container is used.

## Running Graph Dracula

Graph Dracula consists of three parts. Firstly, the database. To use the Docker container, simply type
```bash
docker-compose up
```
in the root of the project and the database should start. If you don't have Docker, simply edit the connection string in Dracula.Api/appsettings.json to the connection of your local SQL instance.

The GraphQL endpoint can be stated using
```bash
dotnet run --project Dracula.Api/Dracula.Api.csproj
```
from the root of the project. It requires the `dotnet` command line tool (DotNetCore 2.1) to run.

Finally, the client website can be started using
```bash
dub
```
from the dracula.site folder. I have dub version 1.15 and dmd (D compiler) version 2.086, but it should run with slightly older versions as well.

## Websites

* The main website runs on http://localhost:8080/
* The GraphQL endpoint runs on http://localhost:5000/
* An interactive playground for writing manual GraphQL queries provided by Hot Chocolate runs on http://localhost:5000/playground/

## Ethymology

"Graph" means "graaf" in Dutch. "Graaf" is the Dutch word for "Count".
