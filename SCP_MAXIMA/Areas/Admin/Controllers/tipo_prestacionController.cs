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
    public class tipo_prestacionController : Controller
    {
        private tbl_tipo_prestacion tipo_prestacion = new tbl_tipo_prestacion();
        // GET: Admin/tipo_´restacopm
        public ActionResult Index(int id = 0)
        {
            ViewData["id_estacion"] = id;
            return View();
        }

        public JsonResult Listar(AnexGRID grid)
        {
            return Json(tipo_prestacion.Listar(grid));
        }

        public ActionResult Ver(int id)
        {
            return View(tipo_prestacion.ObtenerPorId(id));
        }

        public ActionResult Crud(int id = 0)
        {
            return View(
                   id == 0 ? new tbl_tipo_prestacion () : tipo_prestacion.ObtenerPorId(id)
                );
        }

        [HttpPost]
        public JsonResult Guardar(tbl_tipo_prestacion model)
        {
            var rm = new ResponseModel();
            model.estado = "A";
            if (ModelState.IsValid)
            {
                rm = model.Savetipo_prestacion();

                if (rm.response)
                {
                    rm.href = Url.Content("~/admin/tipo_prestacion/");
                }
            }
            return Json(rm);
        }


        public ActionResult Eliminar(int id)
        {
            return Json(tipo_prestacion.Eliminartipo_prestacion(id));
        }
    }
}