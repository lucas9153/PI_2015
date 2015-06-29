using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClienteMVC.Entity;

namespace ClienteMVC.Models
{
    public class EnderecoModel
    {

        private MVCTesteEntities db = new MVCTesteEntities();

        public List<Endereco> todosEnderecos()
        {
            var lista = from e in db.Endereco
                        select e;
            return lista.ToList();
        }

        public string adicionarEndereco(Endereco e)
        {
            string erro = null;
            try
            {
                db.Endereco.AddObject(e);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public Endereco obterEndereco(int idEndereco)
        {
            var lista = from e in db.Endereco
                        where e.IdEndereco == idEndereco
                        select e;
            return lista.ToList().FirstOrDefault();
        }

        public string editarEndereco(Endereco e)
        {
            string erro = null;
            try
            {
                if (e.EntityState == System.Data.EntityState.Detached)
                {
                    db.Endereco.Attach(e);
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

        public string excluirEndereco(Endereco e)
        {
            string erro = null;
            try
            {
                db.Endereco.DeleteObject(e);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public List<Endereco> obterEnderecosCliente(int idCliente)
        {
            var lista = from e in db.Endereco
                        where e.IdCliente == idCliente
                        select e;

            return lista.ToList();
        }

    }
}