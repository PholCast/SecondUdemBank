using SecondUdemBank;

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
            UsuarioBD.CrearCuenta(nombre, clave);

            using var db = new Contexto();
            Usuario usuario = db.Usuarios.SingleOrDefault(u => u.clave == clave);
            int id = usuario.id;
            
            
            double Saldo = 4681.26;

            CuentaDeAhorroBD.CrearCuentaDeAhorro(id,Saldo);

            
            CuentaDeAhorro cuenta = db.CuentasDeAhorros.SingleOrDefault(c => c.id_propietario == id);

            Assert.IsNotNull(cuenta);

            db.CuentasDeAhorros.Remove(cuenta);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
        }

        }
    }
