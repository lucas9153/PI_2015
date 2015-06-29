using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClienteMVC.Entity;

namespace ClienteMVC.Models
{
    public class PerfilModel
    {

        private MVCTesteEntities db = new MVCTesteEntities();

        public List<Perfil> todosPerfis()
        {
            var lista = from p in db.Perfil
                        select p;
            return lista.ToList();
        }

        public List<Perfil> listarPerfisPorUsuario(int idUsuario)
        {
            var lista = from p in db.Perfil
                        join pu in db.PerfilUsuario
                        on p.IdPerfil equals pu.IdPerfil
                        where pu.IdUsuario == idUsuario
                        select p;
            return lista.ToList();
        }

    }
}