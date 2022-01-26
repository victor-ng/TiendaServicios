using Api.Carrito.Aplicacion;
using Api.Carrito.Persistencia;
using Api.Carrito.Remoto.Interface;
using Api.Carrito.Remoto.Servicio;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Carrito
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
            services.AddScoped<ILibroServicio, LibrosServicio>();
            services.AddControllers();

            services.AddDbContext<CarritoContexto>(options => {
                options.UseMySQL(Configuration.GetConnectionString("ConexionDatabase"));
            });
            services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
            // Se agrega el HttpClient como servicio para poder llamar a los otros microservicios
            services.AddHttpClient("Libros", config => {
                config.BaseAddress = new Uri(Configuration["Services:Libros"]);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
