using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Carrito.Remoto.Modelo
{
    public class LibroRemoto
    {
        public Guid? LibreriaMaterialId { get; set; }

        public string Titulo { get; set; }

        public DateTime? FechaPublicacion { get; set; }

        public Guid? AutorLibro { get; set; }
    }
}
