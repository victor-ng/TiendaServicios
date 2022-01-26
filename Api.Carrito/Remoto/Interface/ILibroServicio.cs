using Api.Carrito.Remoto.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Carrito.Remoto.Interface
{
    public interface ILibroServicio
    {
        Task<(bool resultado, LibroRemoto libro, string errorMessage)> GetLibro(Guid LibroId);
    }
}
