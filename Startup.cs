using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using ProyectoPolizas.Repositories;
using ProyectoPolizas.Controllers;
using System.Security.Cryptography;

namespace ProyectoPolizas
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configurar la conexión a MongoDB
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("local");

            // Registrar la instancia de la base de datos en el contenedor de dependencias
            services.AddSingleton<IMongoDatabase>(database);

            // Registra el repositorio
            services.AddSingleton<PolizaRepository>();

            // Otros servicios y configuraciones
            services.AddControllers();
            // Configura la inyección de dependencias para AppSettings
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            // Agrega autenticación con JWT
            var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            // Configurar el sistema de registro (logger)
            services.AddLogging(builder =>
            {
                builder.AddConsole(); // Agrega el registro en la consola
                builder.AddDebug();   // Agrega el registro de depuración
                                      // Agrega otros proveedores de registro según tus necesidades
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "auth",
                pattern: "api/auth/{action}",
                defaults: new { controller = "Auth" });
                // Otra rutas
            });
        }
    }
}
