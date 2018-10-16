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
    public class LoginController : Controller
    {
        // GET: Admin/Login
        tbl_usuario usuario_session = new tbl_usuario();
        [NoLogin]
        public ActionResult Index()
        {
            return View();
        }

        //metodo para acceder al sistema
        public JsonResult Acceder(string login, string clave)
        {
            var rm = usuario_session.Acceder(login, clave);

            if (rm.response)
            {
                rm.href = Url.Content("~/Admin/Admin");
            }

            return Json(rm);
        }

        public ActionResult Logout()
        {
            SessionHelper.DestroyUserSession();
            return Redirect("~/Admin/Login");
        }
    }
}