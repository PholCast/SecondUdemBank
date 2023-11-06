using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondUdemBank
{
    public class udemBankBD
    {
        public static void CrearBanco ()
        {
            using var db = new Contexto(); //Conexión a la BD --> contexto

            db.udemBanks.Add(new udemBank { comision = 0 });
            db.SaveChanges();
        }

        public static int ContarBancos()
        {
            using var db = new Contexto();
            int CantidadBancos = db.udemBanks.Count();
            return CantidadBancos;
        }

        public static void IngresarComisiones(double Comision)
        {
            using var db = new Contexto();

            var udemBank = db.udemBanks.FirstOrDefault(ud => ud.id == 1);
            udemBank.comision += Comision;
            db.SaveChanges();
        }
    }
}
