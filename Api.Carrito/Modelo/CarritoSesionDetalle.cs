using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Carrito.Modelo
{
    public class CarritoSesionDetalle
    {
        public int CarritoSesionDetalleId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string ProductoSeleccionado { get; set; }

        // La siguiente propiedad es una ancla de vínculo que declara la propiedad de la clave primaria
        // de la entidad CarritoSesion, a fin de que sea generada su llave foránea al momento de crear
        // la base de datos.
        public int CarritoSesionId { get; set; }

        // Ahora, a nivel de objeto, se tiene que agregar la entidad que soporta la llave foránea anterior
        public CarritoSesion CarritoSesion { get; set; }
    }
}
