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
    public class AbonoController : Controller
    {
        private tbl_abono abono = new tbl_abono();
        // GET: Admin/Abono
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Listar(AnexGRID grid)
        {
            return Json(abono.Listar(grid));
        }

        public ActionResult Ver(int id)
        {
            return View(abono.ObtenerPorId(id));
        }

        public ActionResult Crud(int id = 0)
        {
            return View(
                   id == 0 ? new tbl_abono() : abono.ObtenerPorId(id)
                );
        }

        [HttpPost]
        public JsonResult Guardar(tbl_abono model)
        {
            var rm = new ResponseModel();
            model.estado = "A";
            if (ModelState.IsValid)
            {
                rm = model.SaveAbono();

                if (rm.response)
                {
                    rm.href = Url.Content("~/admin/abono/");
                }
            }
            return Json(rm);
        }


        public ActionResult Eliminar(int id)
        {
            return Json(abono.EliminarAbono(id));
        }
    }
}