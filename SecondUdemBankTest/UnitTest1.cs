using SecondUdemBank;
using Moq.EntityFrameworkCore;
using Moq;
namespace SecondUdemBankTest
{
    [TestClass]
    public class CuentaDeAhorroBDTest
    {
        [TestMethod]
        public void CrearCuentaDeAhorro()
        {
            string nombre = "Arnulfo";
            string clave = "Arfnulfo123";

            var contextMock = new Mock<Contexto>();


            UsuarioBD usuarioBD = new UsuarioBD(contextMock.object);

            
        }

        }
    }
