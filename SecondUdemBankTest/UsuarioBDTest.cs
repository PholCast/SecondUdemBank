using SecondUdemBank;
using Moq.EntityFrameworkCore;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System;

namespace SecondUdemBankTest
{
    [TestClass]
    public class UsuarioBDTest
    {
        [TestMethod]
        public void CrearCuentaTest()
        {
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


        [TestMethod]
        public void ObtenerUsuariosTest()
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
            var result = usuarioBD.ObtenerUsuarios();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            CollectionAssert.AreEquivalent(data.ToList(), result.ToList());

        }


        [TestMethod]
        public void MostrarInformacionCuenta_Test()
        {
            // Arrange
            var dataUsuarios = new List<Usuario>
            {
                new Usuario { id = 1, nombre = "Usuario1", clave = "Clave1" },
                new Usuario { id = 2, nombre = "Usuario2", clave = "Clave2" },
                
            }.AsQueryable();

            var dataCuentas = new List<CuentaDeAhorro>
            {
                new CuentaDeAhorro {id = 1, id_propietario = 1, saldo = 45689},
                new CuentaDeAhorro {id = 2, id_propietario = 2, saldo = 984915}
            }.AsQueryable();



            var usuario = new Usuario{id = 1,nombre = "Usuario1", clave = "Clave1"};
            var cuentaDeAhorro = new CuentaDeAhorro { id = 1, id_propietario = 1, saldo = 45689 };

            var mockSetUsuario = new Mock<DbSet<Usuario>>();
            mockSetUsuario.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(dataUsuarios.Provider);
            mockSetUsuario.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(dataUsuarios.Expression);
            mockSetUsuario.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(dataUsuarios.ElementType);
            mockSetUsuario.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(() => dataUsuarios.GetEnumerator());

           
           
            var mockSetCuentas = new Mock<DbSet<CuentaDeAhorro>>();
            mockSetCuentas.As<IQueryable<CuentaDeAhorro>>().Setup(m => m.Provider).Returns(dataCuentas.Provider);
            mockSetCuentas.As<IQueryable<CuentaDeAhorro>>().Setup(m => m.Expression).Returns(dataCuentas.Expression);
            mockSetCuentas.As<IQueryable<CuentaDeAhorro>>().Setup(m => m.ElementType).Returns(dataCuentas.ElementType);
            mockSetCuentas.As<IQueryable<CuentaDeAhorro>>().Setup(m => m.GetEnumerator()).Returns(() => dataCuentas.GetEnumerator());

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.Usuarios).Returns(mockSetUsuario.Object);
            mockContext.Setup(c => c.CuentasDeAhorros).Returns(mockSetCuentas.Object);

            // Act
            //var usuarioBD = new UsuarioBD(mockContext.Object);
            //suarioBD.MostrarInformacionCuenta(new Usuario { id = 1, nombre = "Usuario1", clave = "Clave1" });

            // Assert
            //mockContext.Verify(c => c.CuentasDeAhorros, Times.Once);


            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw); // Redirige Console.WriteLine a StringWriter

                var usuarioBD = new UsuarioBD(mockContext.Object);

                
                // Act
                usuarioBD.MostrarInformacionCuenta(new Usuario { id = 1, nombre = "Usuario1", clave = "Clave1" });

                // Assert
                var expectedOutput = $"El propietario de la cuenta es: {usuario.nombre}" + Environment.NewLine +
                                     $"El saldo de la cuenta es: {cuentaDeAhorro.saldo}" + Environment.NewLine;
                Assert.AreEqual(expectedOutput, sw.ToString());
            }

        }

    }

}
