using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClienteMVC.Entity;

namespace ClienteMVC.Models
{
    public class EstadoModel
    {

        private MVCTesteEntities db = new MVCTesteEntities();

        public List<Estado> todosEstados()
        {
            var lista = from e in db.Estado
                        select e;
            return lista.ToList();
        }

        public string adicionarEstado(Estado e)
        {
            string erro = null;
            try
            {
                db.Estado.AddObject(e);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public Estado obterEstado(string UF)
        {
            var lista = from e in db.Estado
                        where e.UF == UF
                        select e;
            return lista.ToList().FirstOrDefault();
        }

        public string editarEstado(Estado e)
        {
            string erro = null;
            try
            {
                if (e.EntityState == System.Data.EntityState.Detached)
                {
                    db.Estado.Attach(e);
                }
                db.ObjectStateManager.ChangeObjectState(e, System.Data.EntityState.Modified);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public string excluirEstado(Estado e)
        {
            string erro = null;
            try
            {
                db.Estado.DeleteObject(e);
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