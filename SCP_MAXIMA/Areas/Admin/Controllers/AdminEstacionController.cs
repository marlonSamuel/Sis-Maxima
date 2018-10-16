using Helper;
using Model;
using System;
using System.Web.Mvc;
using Transport.Areas.Admin.Filters;

namespace SCP_MAXIMA.Areas.Admin.Controllers
{
    [Autenticado]
    public class AdminEstacionController : Controller
    {
        // GET: Admin/AdminEstacion
        private readonly tbl_estacion estacion = new tbl_estacion();
        private readonly tbl_usuario usuario = new tbl_usuario();

        int userId = SessionHelper.GetUser();
        public ActionResult Index()
        {
            var id = Convert.ToInt32(Request.Params["id_estacion"]);

            return View(
                id == 0 ? new tbl_estacion(): estacion.ObtenerPorId(id)
                );
        }
    }
}