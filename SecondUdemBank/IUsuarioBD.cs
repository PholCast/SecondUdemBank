using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondUdemBank
{
    public interface IUsuarioBD
    {
        void CrearCuenta(String Nombre = "", String Clave = "");
    }
}
