using Helper;
using Model;
using System;
using System.Web.Mvc;
using Transport.Areas.Admin.Filters;

namespace SCP_MAXIMA.Areas.Admin.Controllers
{
    [Autenticado]
    public class EmpleadoController : Controller
    {
        private tbl_empleado empleado = new tbl_empleado();
        private tbl_usuario usuario = new tbl_usuario();

        int userId = SessionHelper.GetUser();
        int idEstacion;

        // GET: Admin/empleado
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Listar(AnexGRID grid)
        {
            return Json(empleado.Listar(grid));
        }

        public ActionResult Ver(int id)
        {
            return View(empleado.ObtenerPorId(id));
        }

        public ActionResult Crud(int id = 0, int id_estacion = 0)
        {
            return View(
                   id == 0 ? new tbl_empleado() : empleado.ObtenerPorId(id)
                );
        }

        [HttpPost]
        public JsonResult Guardar(tbl_empleado model)
        {
            var rm = new ResponseModel();

            var user = usuario.ObtenerUsuario(userId);
            

            model.estado = "A";
            if (ModelState.IsValid)
            {
                rm = model.SaveEmpleado();

                if (rm.response)
                {
                    rm.href = Url.Content("~/admin/empleado/index?id_estacion="+model.id_estacion);
                }
            }
            return Json(rm);
        }


        public ActionResult Eliminar(int id)
        {
            return Json(empleado.EliminarEmpleado(id));
        }
    }
}