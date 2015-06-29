using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClienteMVC.Entity;

namespace ClienteMVC.Models
{
    public class TipoEnderecoModel
    {

        private MVCTesteEntities db = new MVCTesteEntities();

        public List<TipoEndereco> todosTipoEnderecos()
        {
            var lista = from e in db.TipoEndereco
                        select e;
            return lista.ToList();
        }

        public List<TipoEndereco> listarTipoEnderecos(string pesquisa)
        {
            var lista = from e in db.TipoEndereco
                        where e.Descricao.Contains(pesquisa)
                        select e;
            return lista.ToList();
        }

        public string adicionarTipoEndereco(TipoEndereco e)
        {
            string erro = null;
            try
            {
                db.TipoEndereco.AddObject(e);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public TipoEndereco obterTipoEndereco(int idTipoEndereco)
        {
            var lista = from e in db.TipoEndereco
                        where e.IdTipoEndereco == idTipoEndereco
                        select e;
            return lista.ToList().FirstOrDefault();
        }

        public string editarTipoEndereco(TipoEndereco e)
        {
            string erro = null;
            try
            {
                if (e.EntityState == System.Data.EntityState.Detached)
                {
                    db.TipoEndereco.Attach(e);
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

        public string excluirTipoEndereco(TipoEndereco e)
        {
            string erro = null;
            try
            {
                db.TipoEndereco.DeleteObject(e);
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