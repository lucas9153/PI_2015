using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClienteMVC.Entity;

namespace ClienteMVC.Models
{
    public class ClienteModel
    {

        private MVCTesteEntities db = new MVCTesteEntities();

        public List<Cliente> todosClientes()
        {
            var lista = from c in db.Cliente
                        select c;
            return lista.ToList();
        }

        public string adicionarCliente(Cliente c)
        {
            string erro = null;
            try
            {
                db.Cliente.AddObject(c);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public Cliente obterCliente(int idCliente)
        {
            var lista = from c in db.Cliente
                        where c.IdCliente == idCliente
                        select c;
            return lista.ToList().FirstOrDefault();
        }

        public string editarCliente(Cliente c)
        {
            string erro = null;
            try
            {
                if (c.EntityState == System.Data.EntityState.Detached)
                {
                    db.Cliente.Attach(c);
                }
                db.ObjectStateManager.ChangeObjectState(c, System.Data.EntityState.Modified);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public string excluirCliente(Cliente c)
        {
            string erro = null;
            try
            {
                db.Cliente.DeleteObject(c);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public string validarCliente(Cliente c)
        {
            if (c.Nome == null || c.Nome == "")
            {
                return "O nome não pode ser vazio!";
            }
            if (c.CPF == null || c.CPF.Length > 11)
            {
                return "CPF inválido";
            }
            if (c.DataNascimento == null || c.DataNascimento > DateTime.Now.Date)
            {
                return "Data de nascimento inválida";
            }
            if (c.Email == null || c.Email == "")
            {
                return "E-mail inválido";
            }
            return null;
        }

    }
}