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
    public class SancionController : Controller
    {
        private tbl_sancion sancion = new tbl_sancion();
        // GET: Admin/sanciones
        public ActionResult Index(int id = 0)
        {
            ViewData["id_estacion"] = id;
            return View();
        }

        public JsonResult Listar(AnexGRID grid)
        {
            return Json(sancion.Listar(grid));
        }

        public ActionResult Ver(int id)
        {
            return View(sancion.ObtenerPorId(id));
        }

        public ActionResult Crud(int id = 0)
        {
            return View(
                   id == 0 ? new tbl_sancion() : sancion.ObtenerPorId(id)
                );
        }

        [HttpPost]
        public JsonResult Guardar(tbl_sancion model)
        {
            var rm = new ResponseModel();
            model.estado = "A";
            if (ModelState.IsValid)
            {
                rm = model.SaveSancion();

                if (rm.response)
                {
                    rm.href = Url.Content("~/admin/sancion/");
                }
            }
            return Json(rm);
        }


        public ActionResult Eliminar(int id)
        {
            return Json(sancion.EliminarSancion(id));
        }
    }
}