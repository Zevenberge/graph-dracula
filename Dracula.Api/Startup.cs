using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dracula.Api.Schema;
using Dracula.Repository;
using Dracula.Repository.Impl;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Dracula.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<DraculaDbContext>(options =>
                {
                    options.UseSqlServer(Configuration["ConnectionString"], sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(DraculaDbContext).GetTypeInfo().Assembly.GetName().Name);
                    });
                });


            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IFilmRepository, FilmRepository>();

            services.AddGraphQL(sp => HotChocolate.Schema.Create(c =>
            {
                c.RegisterServiceProvider(sp);
                c.RegisterQueryType<QueryType>();
                c.RegisterMutationType<MutationType>();
                c.RegisterType<ActorType>();
                c.RegisterType<FilmType>();
            }), builder =>
                builder.Use<TransactionMiddleware>()
                    .UseDefaultPipeline()
                    .AddErrorFilter(LogError)
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDeveloperExceptionPage();
            app.UseGraphQL();
            app.UsePlayground();
        }

        private static IError LogError(IError error)
        {
            var exception = error.Exception;
            if (exception != null)
            {
                Console.WriteLine(exception);
            }
            return error;
        }
    }
}
