using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClienteMVC.Entity;

namespace ClienteMVC.Models
{
    public class UsuarioModel
    {

        private MVCTesteEntities db = new MVCTesteEntities();

        public List<Usuario> todosUsuarios()
        {
            var lista = from c in db.Usuario
                        select c;
            return lista.ToList();
        }

        public string adicionarUsuario(Usuario u)
        {
            string erro = null;
            try
            {
                db.Usuario.AddObject(u);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public Usuario obterUsuario(int idUsuario)
        {
            var lista = from u in db.Usuario
                        where u.IdUsuario == idUsuario
                        select u;
            return lista.ToList().FirstOrDefault();
        }

        public Usuario obterUsuarioPorLogin(string login)
        {
            var lista = from u in db.Usuario
                        where u.Login == login
                        select u;
            return lista.ToList().FirstOrDefault();
        }

        public string editarUsuario(Usuario u)
        {
            string erro = null;
            try
            {
                if (u.EntityState == System.Data.EntityState.Detached)
                {
                    db.Usuario.Attach(u);
                }
                db.ObjectStateManager.ChangeObjectState(u, System.Data.EntityState.Modified);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public string excluirUsuario(Usuario u)
        {
            string erro = null;
            try
            {
                db.Usuario.DeleteObject(u);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

    }
}