﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Visi.Repository;
using Vonk.Core.Configuration;
using Vonk.Core.Operations.Crud;
using Vonk.Core.Operations.Search;
using Vonk.Core.Operations.Validation;
using Vonk.Core.Support;
using Vonk.Fhir.R3;
using Vonk.Smart;

namespace Vonk.Facade.Starter
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnv;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnv)
        {
            Check.NotNull(configuration, nameof(configuration));
            Check.NotNull(hostingEnv, nameof(hostingEnv));
            _configuration = configuration;
            _hostingEnv = hostingEnv;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFhirR3FacadeServices(_configuration)
                .AddSmartServices(_configuration, _hostingEnv)
                .AddVonkMinimalServices()
                .AddSearchServices()
                .AddReadServices()
                .AddCreateServices()
                .AddUpdateServices()
                .AddDeleteServices()
                .AddViSiServices(_configuration)
                .AddInstanceValidationServices(_configuration)
                .AddValidationServices(_configuration)
             ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseVonkMinimal()
                .UseSmartAuthorization()
                .UseSearch()
                .UseRead()
                .UseCreate()
                .UseUpdate()
                .UseDelete()
                .UseValidation()
                .UseInstanceValidation()
            ;
        }
    }
}
