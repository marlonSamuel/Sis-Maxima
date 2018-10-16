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
    public class vacacionController : Controller
    {
        private tbl_vacacion vacacion = new tbl_vacacion();
        // GET: Admin/vacaciones
        public ActionResult Index(int id = 0)
        {
            ViewData["id_estacion"] = id;
            return View();
        }

        public JsonResult Listar(AnexGRID grid)
        {
            return Json(vacacion.Listar(grid));
        }

        public ActionResult Ver(int id)
        {
            return View(vacacion.ObtenerPorId(id));
        }

        public ActionResult Crud(int id = 0)
        {
            return View(
                   id == 0 ? new tbl_vacacion() : vacacion.ObtenerPorId(id)
                );
        }

        [HttpPost]
        public JsonResult Guardar(tbl_vacacion model)
        {
            var rm = new ResponseModel();
            model.estado = "A";
            if (ModelState.IsValid)
            {
                rm = model.SaveVacacion();

                if (rm.response)
                {
                    rm.href = Url.Content("~/admin/vacacion/");
                }
            }
            return Json(rm);
        }


        public ActionResult Eliminar(int id)
        {
            return Json(vacacion.EliminarVacacion(id));
        }
    }
}