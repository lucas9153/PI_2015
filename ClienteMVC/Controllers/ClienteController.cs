using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClienteMVC.Entity;
using ClienteMVC.Models;
using System.Web.Security;

namespace ClienteMVC.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private ClienteModel clienteModel = new ClienteModel();
        private EnderecoModel enderecoModel = new EnderecoModel();
        private EstadoModel estadoModel = new EstadoModel();
        private CidadeModel cidadeModel = new CidadeModel();

        public ActionResult Index()
        {
            if (!Roles.IsUserInRole(User.Identity.Name, "Administrador"))
            {
                return Redirect("/Shared/Error");
            }
            return View(clienteModel.todosClientes());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Cliente c)
        {
            clienteModel.adicionarCliente(c);
            return RedirectToAction("Index");
        }

        // localhost:8080/Cliente/Edit/8
        public ActionResult Edit(int id)
        {
            Cliente c;
            if (id == 0)
            {
                c = new Cliente();
            } 
            else 
            {
                c = clienteModel.obterCliente(id);
            }
            return View(c);
        }

        [HttpPost]
        public ActionResult Edit(Cliente c)
        {
            string erro = clienteModel.validarCliente(c);
            if (erro == null)
            {
                if (c.IdCliente == 0)
                {
                    erro = clienteModel.adicionarCliente(c);
                }
                else
                {
                    erro = clienteModel.editarCliente(c);
                }
            }

            if (erro == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = erro;
                return View(c);
            }
        }

        // localhost:8080/Cliente/Delete/8
        public ActionResult Delete(int id)
        {
            Cliente c = clienteModel.obterCliente(id);
            clienteModel.excluirCliente(c);
            return RedirectToAction("Index");
        }

        public ActionResult ListaEnderecos(int idCliente)
        {
            ViewBag.IdCliente = idCliente;
            Cliente c = clienteModel.obterCliente(idCliente);
            ViewBag.NomeCliente = c.Nome;
            return View(enderecoModel.obterEnderecosCliente(idCliente));
        }

        public ActionResult EditEndereco(int idEndereco, int idCliente)
        {
            Endereco e = new Endereco();
            e.IdCliente = idCliente;
            if (idEndereco != 0)
            {
                e = enderecoModel.obterEndereco(idEndereco);
            }
            
            string estadoSelecionado = "MG";
            int cidadeSelecionada = 1; // 1 = Patos de Minas

            if (idEndereco != 0)
            {
                estadoSelecionado = e.Cidade.UF;
                cidadeSelecionada = e.IdCidade;
            }

            ViewBag.Estados 
                = new SelectList(estadoModel.todosEstados(), "UF", "Descricao", 
                    estadoSelecionado);
            ViewBag.IdCidade
                = new SelectList(cidadeModel.obterCidadesPorEstado(estadoSelecionado), 
                    "IdCidade", "Descricao", cidadeSelecionada);
            
            return View(e);
        }

        [HttpPost]
        public ActionResult EditEndereco(Endereco e)
        {
            string erro = null;
            if (e.IdEndereco == 0)
            {
                erro = enderecoModel.adicionarEndereco(e);
            }
            else
            {
                erro = enderecoModel.editarEndereco(e);
            }
            if (erro == null)
            {
                return RedirectToAction("ListaEnderecos", new { idCliente = e.IdCliente });
            }
            else
            {
                ViewBag.Erro = erro;
                return View(e);
            }
        }

        public ActionResult DeleteEndereco(int idEndereco)
        {
            Endereco e = enderecoModel.obterEndereco(idEndereco);
            enderecoModel.excluirEndereco(e);
            return RedirectToAction("ListaEnderecos", new { idCliente = e.IdCliente });
        }

        public JsonResult ListaCidades(string estado)
        {
            var cidades 
                = new SelectList(cidadeModel.obterCidadesPorEstado(estado), "IdCidade", "Descricao");
            return Json(new { cidades = cidades });
        }

    }
}
