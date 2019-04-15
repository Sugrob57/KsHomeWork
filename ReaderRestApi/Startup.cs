using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Serilog;
using ReaderRestApi.Providers;

namespace CoreTestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            WorkPath = Configuration["WorkPath"]; // получение рабочей директории
            System.IO.Directory.CreateDirectory(WorkPath);
            InitLogger(WorkPath); // инициализация логгера

            DBProvider _db = new DBProvider();
            _db.InitializeDB();
        }

        public IConfiguration Configuration { get; }
        public static string WorkPath { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Reader Rest API",
                    Description = "ASP.NET Core Web API"
                });
                c.IncludeXmlComments(GetXmlCommentsPath());
            });
        }

        private static string GetXmlCommentsPath()
        {
            return String.Format(@"{0}\ReaderRestApi.xml", AppDomain.CurrentDomain.BaseDirectory);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }         

            app.UseDefaultFiles(new DefaultFilesOptions
            {
                DefaultFileNames = new
                List<string> { "index.html" }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "index.html");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReaderRestApi V1");
                c.RoutePrefix = string.Empty;
            });
        }

        private static void InitLogger(string log_path)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(log_path + "ReaderApi_.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Service started...");
        }
    }
}
