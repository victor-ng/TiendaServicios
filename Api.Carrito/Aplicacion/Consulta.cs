using Api.Carrito.Persistencia;
using Api.Carrito.Remoto.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Carrito.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoDto> {
            public int CarritoSesionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {
            private readonly CarritoContexto _contexto;
            private readonly ILibroServicio _libroServicio;

            public Manejador(CarritoContexto contexto, ILibroServicio libroServicio)
            {
                _contexto = contexto;
                _libroServicio = libroServicio;
            }

            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = 
                    await _contexto.CarritoSesiones
                    .FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSesionId);

                var carritoSesionDetalle =
                    await _contexto.CarritoSesionDetalles
                    .Where(x => x.CarritoSesionId == request.CarritoSesionId).ToListAsync();

                var listaDetalles = new List<CarritoDetalleDto>();

                foreach(var libro in carritoSesionDetalle)
                {
                    var response = await _libroServicio.GetLibro(new Guid(libro.ProductoSeleccionado));
                    
                    if (response.resultado)
                    {
                        var objetoLibro = response.libro;
                        var carritoDetalle = new CarritoDetalleDto
                        {
                            TituloLibro = objetoLibro.Titulo,
                            FechaPublicacion = objetoLibro.FechaPublicacion,
                            LibroId = objetoLibro.LibreriaMaterialId
                        };
                        listaDetalles.Add(carritoDetalle);
                    }
                }

                var carritoSesionDto = new CarritoDto
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    ListaProductos = listaDetalles
                };

                return carritoSesionDto;
            }
        }
    }
}
