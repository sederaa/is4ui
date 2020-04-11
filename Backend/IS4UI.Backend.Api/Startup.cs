using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore;
using IS4UI.Backend.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using IS4UI.Backend.Api.GraphQL.QueryTypes;

namespace IS4UI.Backend.Api
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
            services.AddDbContext<ApplicationDbContext>(optionsAction=>{
                optionsAction.UseSqlServer("Server=.; Database=IdentityServer; User Id=sa; Password=Passw0rd;");
            });
            
            //TODO: scan for all IValidators and register
            services.AddTransient<IValidator<CreateClientInput>, CreateClientInputValidator>();
            services.AddTransient<IValidator<CreateClientSecretInput>, CreateClientSecretInputValidator>();

            services
                .AddDataLoaderRegistry()
                .AddGraphQL(SchemaBuilder.New()
                    .AddQueryType<RootQueryType>()
                    .AddMutationType<RootMutationType>()
                    .Use<ValidateInputMiddleware>()
                    .Create()
                );

                services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseGraphQL("/graphql");

             app.UseEndpoints(endpoints =>
             {
                 endpoints.MapRazorPages();
             });
        }
    }
}
