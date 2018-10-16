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
    public class tipo_permisoController : Controller
    {
        private tbl_tipo_permiso tipo_permiso = new tbl_tipo_permiso();
        // GET: Admin/tipo_permiso
        public ActionResult Index(int id = 0)
        {
            ViewData["id_estacion"] = id;
            return View();
        }

        public JsonResult Listar(AnexGRID grid)
        {
            return Json(tipo_permiso.Listar(grid));
        }

        public ActionResult Ver(int id)
        {
            return View(tipo_permiso.ObtenerPorId(id));
        }

        public ActionResult Crud(int id = 0)
        {
            return View(
                   id == 0 ? new tbl_tipo_permiso() : tipo_permiso.ObtenerPorId(id)
                );
        }

        [HttpPost]
        public JsonResult Guardar(tbl_tipo_permiso model)
        {
            var rm = new ResponseModel();
            model.estado = "A";
            if (ModelState.IsValid)
            {
                rm = model.Savetipo_permiso();

                if (rm.response)
                {
                    rm.href = Url.Content("~/admin/tipo_permiso/");
                }
            }
            return Json(rm);
        }


        public ActionResult Eliminar(int id)
        {
            return Json(tipo_permiso.Eliminartipo_permiso(id));
        }
    }
}