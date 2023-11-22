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
    public class CuentaDeAhorroBDTest
    {
        [TestMethod]
        public void CrearCuentaDeAhorro_Test()
        {
            // Arrange
            var mockSet = new Mock<DbSet<CuentaDeAhorro>>();
            var mockContext = new Mock<Contexto>();
            mockContext.Setup(m => m.CuentasDeAhorros).Returns(mockSet.Object);

            var cuentaDeAhorroBD = new CuentaDeAhorroBD(mockContext.Object);

            // Act
            cuentaDeAhorroBD.CrearCuentaDeAhorro(1, 1000); // Proporciona un saldo específico para la prueba

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<CuentaDeAhorro>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void IngresarCapital_Test()
        {
            // Arrange
            var mockSet = new Mock<DbSet<CuentaDeAhorro>>();
            var mockContext = new Mock<Contexto>();
            mockContext.Setup(m => m.CuentasDeAhorros).Returns(mockSet.Object);

            var cuentaDeAhorroBD = new CuentaDeAhorroBD(mockContext.Object);

            var cuentaDeAhorro = new CuentaDeAhorro { id = 1, id_propietario = 1, saldo = 500 }; // Supongamos que la cuenta tiene un saldo inicial de 500

            // Act
            cuentaDeAhorroBD.IngresarCapital(cuentaDeAhorro, 1000); // Ingresa 1000 como saldo

            // Assert
            Assert.AreEqual(1499.0, cuentaDeAhorro.saldo); // Verifica que el saldo se haya actualizado correctamente después de la comisión
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        /*[TestMethod]
        public void ObtenerHistorialCuentaDeAhorroTest()
        {
            // Arrange
            var data = new List<Transacciones>
    {
        new Transacciones { id = 1, id_cuentaDeAhorro = 1, CantidadTransaccion = 1000, fecha = DateOnly.FromDateTime(DateTime.Now), TipoTransaccion = "Deposito" },
        new Transacciones { id = 2, id_cuentaDeAhorro = 1, CantidadTransaccion = 500, fecha = DateOnly.FromDateTime(DateTime.Now), TipoTransaccion = "Retiro" },
        new Transacciones { id = 3, id_cuentaDeAhorro = 2, CantidadTransaccion = 2000, fecha = DateOnly.FromDateTime(DateTime.Now), TipoTransaccion = "Deposito" },
    }.AsQueryable();

            var mockSet = new Mock<DbSet<Transacciones>>();
            mockSet.As<IQueryable<Transacciones>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Transacciones>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Transacciones>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Transacciones>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.TransaccionesCuentaAhorros).Returns(mockSet.Object);

            // Act
            var cuentaDeAhorroBD = new CuentaDeAhorroBD(mockContext.Object);
            var result = cuentaDeAhorroBD.ObtenerHistorialCuentaDeAhorro(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Deposito", result[0].TipoTransaccion);
            Assert.AreEqual("Retiro", result[1].TipoTransaccion);
        }*/

    }

}