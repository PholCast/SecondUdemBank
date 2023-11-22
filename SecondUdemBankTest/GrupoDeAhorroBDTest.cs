using Microsoft.EntityFrameworkCore;
using Moq;
using SecondUdemBank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SecondUdemBankTest
{
    [TestClass]
    public class GrupoDeAhorroBDTest
    {
        [TestMethod]
        public void CrearGrupoDeAhorroTest()
        {
            // Arrange
            var usuario = new Usuario { id = 1, nombre = "Usuario1", clave = "Clave1" };
            var nombreGrupo = "GrupoTest";

            var mockSet = new Mock<DbSet<GrupoDeAhorro>>();
            var mockContext = new Mock<Contexto>();
            mockContext.Setup(m => m.GruposDeAhorros).Returns(mockSet.Object);

            // Act
            var grupoDeAhorroBD = new GrupoDeAhorroBD(mockContext.Object);
            grupoDeAhorroBD.CrearGrupoDeAhorro(usuario, nombreGrupo);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<GrupoDeAhorro>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        /*[TestMethod]
        public void ObtenerGruposAhorroTest()
        {
            // Arrange
            var mockSet = new Mock<DbSet<GrupoDeAhorro>>();
            var mockContext = new Mock<Contexto>();
            mockContext.Setup(m => m.GruposDeAhorros).Returns(mockSet.Object);

            var grupoAhorro1 = new GrupoDeAhorro { id = 1, NombreGrupo = "Grupo1", SaldoGrupo = 100 };
            var grupoAhorro2 = new GrupoDeAhorro { id = 2, NombreGrupo = "Grupo2", SaldoGrupo = 200 };

            var listaIds = new List<int> { 1, 2 };

            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.Provider).Returns(new List<GrupoDeAhorro> { grupoAhorro1, grupoAhorro2 }.AsQueryable().Provider);
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.Expression).Returns(new List<GrupoDeAhorro> { grupoAhorro1, grupoAhorro2 }.AsQueryable().Expression);
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.ElementType).Returns(new List<GrupoDeAhorro> { grupoAhorro1, grupoAhorro2 }.AsQueryable().ElementType);
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.GetEnumerator()).Returns(() => new List<GrupoDeAhorro> { grupoAhorro1, grupoAhorro2 }.AsQueryable().GetEnumerator());

            // Act
            var grupoDeAhorroBD = new GrupoDeAhorroBD(mockContext.Object);
            var result = grupoDeAhorroBD.ObtenerGruposAhorro(listaIds);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Grupo1", result[0].NombreGrupo);
            Assert.AreEqual("Grupo2", result[1].NombreGrupo);
        }*/

        [TestMethod]
        public void ObtenerGrupoAhorroPorIdTest()
        {
            // Arrange
            var idGrupo = 1;

            var data = new List<GrupoDeAhorro>
        {
            new GrupoDeAhorro { id = 1, id_CreadorGrupo = 1, SaldoGrupo = 0, NombreGrupo = "Grupo1" },
            new GrupoDeAhorro { id = 2, id_CreadorGrupo = 2, SaldoGrupo = 0, NombreGrupo = "Grupo2" },
        }.AsQueryable();

            var mockSet = new Mock<DbSet<GrupoDeAhorro>>();
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.GruposDeAhorros).Returns(mockSet.Object);

            // Act
            var grupoDeAhorroBD = new GrupoDeAhorroBD(mockContext.Object);
            var result = grupoDeAhorroBD.ObtenerGrupoAhorroPorId(idGrupo);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.id);
        }

        [TestMethod]
        public void QuitarSaldoTest()
        {
            // Arrange
            var data = new List<GrupoDeAhorro>
    {
        new GrupoDeAhorro { id = 1, SaldoGrupo = 1000 },
        new GrupoDeAhorro { id = 2, SaldoGrupo = 2000 },
    }.AsQueryable();

            var mockSet = new Mock<DbSet<GrupoDeAhorro>>();
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.GruposDeAhorros).Returns(mockSet.Object);

            // Act
            var grupoDeAhorroBD = new GrupoDeAhorroBD(mockContext.Object);
            grupoDeAhorroBD.QuitarSaldo(1, 500);

            // Assert
            var grupo = mockContext.Object.GruposDeAhorros.SingleOrDefault(u => u.id == 1);
            Assert.IsNotNull(grupo);
            Assert.AreEqual(500, grupo.SaldoGrupo);
        }

        [TestMethod]
        public void IncrementarSaldoTest()
        {
            // Arrange
            var data = new List<GrupoDeAhorro>
    {
        new GrupoDeAhorro { id = 1, SaldoGrupo = 1000 },
        new GrupoDeAhorro { id = 2, SaldoGrupo = 2000 },
    }.AsQueryable();

            var mockSet = new Mock<DbSet<GrupoDeAhorro>>();
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.GruposDeAhorros).Returns(mockSet.Object);

            // Act
            var grupoDeAhorroBD = new GrupoDeAhorroBD(mockContext.Object);
            grupoDeAhorroBD.IncrementarSaldo(1, 500);

            // Assert
            var grupo = mockContext.Object.GruposDeAhorros.SingleOrDefault(u => u.id == 1);
            Assert.IsNotNull(grupo);
            Assert.AreEqual(1500, grupo.SaldoGrupo);
        }

        /*[TestMethod]
        public void IngresarUsuarioAGrupoDeAhorroTest()
        {
            // Arrange
            var mockSet = new Mock<DbSet<UsuarioXGrupoAhorro>>();
            var mockContext = new Mock<Contexto>();
            mockContext.Setup(m => m.UsuariosXGruposAhorros).Returns(mockSet.Object);

            var service = new GrupoDeAhorroBD(mockContext.Object);
            var usuario = new Usuario { id = 1, nombre = "Usuario1", clave = "Clave1" };
            var usuarioInvitado = new Usuario { id = 2, nombre = "Usuario2", clave = "Clave2" };
            var grupoDeAhorro = new GrupoDeAhorro { id = 1, NombreGrupo = "Grupo1", SaldoGrupo = 1000 };

            // Act
            service.IngresarUsuarioAGrupoDeAhorro(usuario, usuarioInvitado, grupoDeAhorro);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<UsuarioXGrupoAhorro>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }*/

        [TestMethod]
        public void ObtenerSaldoGrupoTest()
        {
            // Arrange
            var data = new List<GrupoDeAhorro>
    {
        new GrupoDeAhorro { id = 1, NombreGrupo = "Grupo1", SaldoGrupo = 1000 },
        new GrupoDeAhorro { id = 2, NombreGrupo = "Grupo2", SaldoGrupo = 2000 },
    }.AsQueryable();

            var mockSet = new Mock<DbSet<GrupoDeAhorro>>();
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.GruposDeAhorros).Returns(mockSet.Object);

            // Act
            var grupoDeAhorroBD = new GrupoDeAhorroBD(mockContext.Object);
            var result = grupoDeAhorroBD.ObtenerSaldoGrupo(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1000, result);
        }

        [TestMethod]
        public void ObtenerCantidadGruposDeAhorroTest()
        {
            // Arrange
            var data = new List<UsuarioXGrupoAhorro>
    {
        new UsuarioXGrupoAhorro { id_ParticipanteGrupo = 1, id_GrupoAhorro = 1, PerteneceAlGrupo = true },
        new UsuarioXGrupoAhorro { id_ParticipanteGrupo = 1, id_GrupoAhorro = 2, PerteneceAlGrupo = true },
        new UsuarioXGrupoAhorro { id_ParticipanteGrupo = 2, id_GrupoAhorro = 1, PerteneceAlGrupo = true },
    }.AsQueryable();

            var mockSet = new Mock<DbSet<UsuarioXGrupoAhorro>>();
            mockSet.As<IQueryable<UsuarioXGrupoAhorro>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<UsuarioXGrupoAhorro>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<UsuarioXGrupoAhorro>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<UsuarioXGrupoAhorro>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.UsuariosXGruposAhorros).Returns(mockSet.Object);

            // Act
            var grupoDeAhorroBD = new GrupoDeAhorroBD(mockContext.Object);
            var result = grupoDeAhorroBD.ObtenerCantidadGruposDeAhorro(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void ObtenerCantidadUsuariosTest()
        {
            // Arrange
            var data = new List<UsuarioXGrupoAhorro>
    {
        new UsuarioXGrupoAhorro { id_ParticipanteGrupo = 1, id_GrupoAhorro = 1, PerteneceAlGrupo = true },
        new UsuarioXGrupoAhorro { id_ParticipanteGrupo = 2, id_GrupoAhorro = 1, PerteneceAlGrupo = true },
        new UsuarioXGrupoAhorro { id_ParticipanteGrupo = 3, id_GrupoAhorro = 1, PerteneceAlGrupo = true },
    }.AsQueryable();

            var mockSet = new Mock<DbSet<UsuarioXGrupoAhorro>>();
            mockSet.As<IQueryable<UsuarioXGrupoAhorro>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<UsuarioXGrupoAhorro>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<UsuarioXGrupoAhorro>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<UsuarioXGrupoAhorro>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.UsuariosXGruposAhorros).Returns(mockSet.Object);

            var grupoDeAhorro = new GrupoDeAhorro { id = 1, NombreGrupo = "Grupo1", SaldoGrupo = 1000 };

            // Act
            var grupoDeAhorroBD = new GrupoDeAhorroBD(mockContext.Object);
            var result = grupoDeAhorroBD.ObtenerCantidadUsuarios(grupoDeAhorro);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IngresarFidelizacionTest()
        {
            // Arrange
            var data = new List<GrupoDeAhorro>
    {
        new GrupoDeAhorro { id = 1, NombreGrupo = "Grupo1", SaldoGrupo = 1000 },
        new GrupoDeAhorro { id = 2, NombreGrupo = "Grupo2", SaldoGrupo = 2000 },
    }.AsQueryable();

            var mockSet = new Mock<DbSet<GrupoDeAhorro>>();
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<GrupoDeAhorro>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.GruposDeAhorros).Returns(mockSet.Object);

            // Act
            var grupoDeAhorroBD = new GrupoDeAhorroBD(mockContext.Object);
            var result = grupoDeAhorroBD.IngresarFidelizacion();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2200, result.SaldoGrupo);
        }

    }
}
