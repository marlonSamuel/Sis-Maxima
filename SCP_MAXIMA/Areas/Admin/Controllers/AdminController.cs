using Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transport.Areas.Admin.Filters;

namespace SCP_MAXIMA.Areas.Admin.Controllers
{
    [Autenticado]
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        private readonly tbl_estacion estacion = new tbl_estacion();
        private readonly tbl_usuario usuario = new tbl_usuario();
        public ActionResult Index()
        {
            var userId = SessionHelper.GetUser();

            var user = usuario.ObtenerUsuario(userId);

            if(user != null)
            {
              if(user.id_estacion != null || user.id_estacion > 0)
                {
                    return Redirect("~/Admin/AdminEstacion/index?id_estacion=" + user.id_estacion);
                }
            }

            return View();
        }
    }
}