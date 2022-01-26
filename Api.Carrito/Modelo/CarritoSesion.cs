using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Carrito.Modelo
{
    public class CarritoSesion
    {
        public int CarritoSesionId { get; set; }
        public DateTime? FechaCreacion { get; set; }

        // Representa una relación de uno a muchos, donde CarritoSesion es el lado de "uno"
        // y CarritoSesionDetalle es el lado de "muchos"
        public ICollection<CarritoSesionDetalle> ListaDetalle { get; set; }
    }
}
