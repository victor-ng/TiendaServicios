﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Carrito.Aplicacion
{
    public class CarritoDetalleDto
    {
        public Guid? LibroId { get; set; }
        public string TituloLibro { get; set; }
        public string AutorLibro { get; set; }
        public DateTime? FechaPublicacion { get; set; }
    }
}
