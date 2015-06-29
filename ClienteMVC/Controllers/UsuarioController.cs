using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClienteMVC.Models;
using ClienteMVC.Entity;
using System.Web.Security;

namespace ClienteMVC.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioModel usuarioModel = new UsuarioModel();
        private PerfilModel perfilModel = new PerfilModel();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View(new Usuario());
        }

        [HttpPost]
        public ActionResult Login(Usuario u)
        {
            Usuario banco = usuarioModel.obterUsuarioPorLogin(u.Login);
            if (banco == null || banco == new Usuario())
            {
                ViewBag.Erro = "Usuário inexistente!";
                return View(u);
            }
            if (u.Senha != banco.Senha)
            {
                ViewBag.Erro = "Senha Incorreta!";
                return View(u);
            }
            //Roles.DeleteCookie();
            // Passar perfis do banco para a aplicação
            foreach (Perfil p in perfilModel.todosPerfis())
            {
                if (!Roles.RoleExists(p.Descricao)) // Testa se a role não existe
                {
                    Roles.CreateRole(p.Descricao); // adiciona a role
                }
            }
            // Adicionar perfis do usuario à classe Role
            foreach (Perfil p in perfilModel.listarPerfisPorUsuario(banco.IdUsuario))
            {
                // Testa se o usuario não está na role associada ao banco
                if (!Roles.IsUserInRole(u.Login, p.Descricao)) 
                {
                    Roles.AddUserToRole(u.Login, p.Descricao); // adiciona o usuario
                }
            }
            FormsAuthentication.SetAuthCookie(u.Login, true);
            return Redirect("/");
        }

        public ActionResult Logoff()
        {
            Usuario u = usuarioModel.obterUsuarioPorLogin(User.Identity.Name);
            // Remover todos os perfis do usuário
            foreach (Perfil p in perfilModel.listarPerfisPorUsuario(u.IdUsuario))
            {
                if (Roles.IsUserInRole(u.Login, p.Descricao))
                {
                    Roles.RemoveUserFromRole(u.Login, p.Descricao);
                }
            }
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

    }
}
