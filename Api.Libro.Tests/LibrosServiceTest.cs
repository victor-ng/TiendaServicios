using Api.Libro.Aplicacion;
using Api.Libro.Modelo;
using Api.Libro.Persistencia;
using AutoMapper;
using Moq;
using GenFu;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Api.Libro.Tests
{
    public class LibrosServiceTest
    {
        // Generar datos de prueba con la libreria GenFu
        private IEnumerable<LibreriaMaterial> ObtenerDatosPrueba()
        {
            // Indica el tipo de datos a generar
            A.Configure<LibreriaMaterial>()
                .Fill(x => x.Titulo).AsArticleTitle()
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); } );

            // Crea 30 datos mock de tipo LibreriaMaterial
            var lista = A.ListOf<LibreriaMaterial>(30);
            // Asigna un valor 0 al primer elemento de la lista
            lista[0].LibreriaMaterialId = Guid.Empty;

            return lista;
        }

        private Mock<ContextoLibreria> CrearContexto()
        {
            var datosPrueba = ObtenerDatosPrueba().AsQueryable();

            var dbSet = new Mock<DbSet<LibreriaMaterial>>();
            // Configurar la clase como data entity
            dbSet.As<IQueryable<LibreriaMaterial>>()
                .Setup(x => x.Provider)
                .Returns(datosPrueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>()
                .Setup(x => x.Expression)
                .Returns(datosPrueba.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>()
                .Setup(x => x.ElementType)
                .Returns(datosPrueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>()
                .Setup(x => x.GetEnumerator())
                .Returns(datosPrueba.GetEnumerator());

            dbSet
                .As<IAsyncEnumerable<LibreriaMaterial>>()
                .Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<LibreriaMaterial>(datosPrueba.GetEnumerator()));

            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(datosPrueba.Provider));

            var contexto = new Mock<ContextoLibreria>();
            contexto.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);

            return contexto;
        }

        [Fact]
        public async void GetLibroPorId()
        {
            var mockContexto = CrearContexto();
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();

            var request = new ConsultaFiltro.LibroUnico();
            request.LibroId = Guid.Empty;

            var manejador = new ConsultaFiltro.Manejador(mockContexto.Object, mapper);

            var libro = await manejador.Handle(request, new CancellationToken());

            Assert.NotNull(libro);
            Assert.True(libro.LibreriaMaterialId == Guid.Empty);
        }

        [Fact]
        public async void GetLibros()
        {
            // System.Diagnostics.Debugger.Launch();

            // Crear mock de DbContext
            var mockContexto = CrearContexto();

            // Crear mock de IMapper
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mockMapper = mapConfig.CreateMapper();

            // Instanciar clase Manejador, pasando los mock como parametros.
            Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object, mockMapper);

            Consulta.Ejecuta request = new Consulta.Ejecuta();

            var lista = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(lista.Any());
        }

        [Fact]
        public async void GuardarLibro()
        {
            System.Diagnostics.Debugger.Launch();

            // Configura la bd en memoria
            var options = new DbContextOptionsBuilder<ContextoLibreria>()
                .UseInMemoryDatabase(databaseName: "BaseDatosLibro")
                .Options;

            // Crea el contexto
            var contexto = new ContextoLibreria(options);

            var request = new Nuevo.Ejecuta()
            {
                Titulo = "Libro de Microservices",
                AutorLibro = Guid.Empty,
                FechaPublicacion = DateTime.Now
            };

            var manejador = new Nuevo.Manejador(contexto);

            var libro = await manejador.Handle(request, new CancellationToken());

            Assert.True(libro != null);
        }
    }
}
