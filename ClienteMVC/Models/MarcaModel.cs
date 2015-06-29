﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClienteMVC.Entity;

namespace ClienteMVC.Models
{
    public class MarcaModel
    {

        private MVCTesteEntities db = new MVCTesteEntities();

        public List<Marca> todasMarcas()
        {
            var lista = from m in db.Marca
                        select m;
            return lista.ToList();
        }

        public Marca obterMarca(int idMarca)
        {
            var lista = from m in db.Marca
                        where m.IdMarca == idMarca
                        select m;
            return lista.ToList().FirstOrDefault();
        }

        public string adicionarMarca(Marca m)
        {
            string erro = null;
            try
            {
                db.Marca.AddObject(m);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public string editarMarca(Marca m)
        {
            string erro = null;
            try
            {
                if (m.EntityState == System.Data.EntityState.Detached)
                {
                    db.Marca.Attach(m);
                }
                db.ObjectStateManager.ChangeObjectState(m,
                    System.Data.EntityState.Modified);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public string excluirMarca(Marca m)
        {
            string erro = null;
            try
            {
                db.DeleteObject(m);
                db.SaveChanges();
            } catch (Exception ex) 
            {
                erro = ex.Message;
            }
            return erro;
        }

    }
}