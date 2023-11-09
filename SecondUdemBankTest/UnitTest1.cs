using SecondUdemBank;
using Moq.EntityFrameworkCore;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace SecondUdemBankTest
{
    [TestClass]
    public class UsuarioBDTest
    {
        [TestMethod]
        public void CrearCuentaTest()
        {
            /*string nombre = "Arnulfo";
            string clave = "Arfnulfo123";

            var contextMock = new Mock<Contexto>();


            UsuarioBD usuarioBD = new UsuarioBD(contextMock.Object);*/


            var mockSet = new Mock<DbSet<Usuario>>();

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(m => m.Usuarios).Returns(mockSet.Object);

            var service = new UsuarioBD(mockContext.Object);
            service.CrearCuenta("Avirama", "DIORO");

            mockSet.Verify(m => m.Add(It.IsAny<Usuario>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());


        }

        [TestMethod]
        public void ObtenerUsuarioPorIdTest()
        {
            // Arrange
            var data = new List<Usuario>
        {
            new Usuario { id = 1, nombre = "Usuario1", clave = "Clave1" },
            new Usuario { id = 2, nombre = "Usuario2", clave = "Clave2" },
        }.AsQueryable();

            var mockSet = new Mock<DbSet<Usuario>>();
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.Usuarios).Returns(mockSet.Object);

            // Act
            var usuarioBD = new UsuarioBD(mockContext.Object);
            var result = usuarioBD.ObtenerUsuarioPorId(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Usuario1", result.nombre);
        }

    }

}
