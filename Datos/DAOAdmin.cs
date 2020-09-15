using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios;

namespace Datos
{
    public class DAOAdmin
    {
        //METODO PARA INSERTAR EMPLEADO 
        public void InsertarEmpleado(UEncapUsuario user)
        {
            using (var db = new Mapeo())
            {
                db.usuario.Add(user);
                db.SaveChanges();
            }
        }
    }

    
}
