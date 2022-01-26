using Api.Carrito.Modelo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Carrito.Persistencia
{
    public class CarritoContexto : DbContext
    {
        public CarritoContexto(DbContextOptions<CarritoContexto> options) : base(options)
        {
        }

        public DbSet<CarritoSesion> CarritoSesiones { get; set; }
        public DbSet<CarritoSesionDetalle> CarritoSesionDetalles { get; set; }
    }
}
