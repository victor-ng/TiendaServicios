using Api.Libro.Aplicacion;
using Api.Libro.Modelo;
using AutoMapper;

namespace Api.Libro.Tests
{
    class MappingTest : Profile
    {
        public MappingTest()
        {
            CreateMap<LibreriaMaterial, LibroMaterialDto>();
        }
    }
}
