using Api.Libro.Modelo;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Libro.Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LibreriaMaterial, LibroMaterialDto>();
        }
    }
}
