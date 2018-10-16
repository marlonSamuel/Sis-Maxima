namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class tbl_vacacion
    {
        [Key]
        public int id_vacacion { get; set; }

        public int id_empleado { get; set; }

        [Required]
        [StringLength(20)]
        public string nombre { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha_inicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha_fin { get; set; }

        [StringLength(500)]
        public string detalle { get; set; }

        
        [StringLength(50)]
        public string usuarioCreo { get; set; }

        public DateTime? fechaCreo { get; set; }

       
        [StringLength(50)]
        public string usuarioActualizo { get; set; }

        public DateTime? fechaActualizo { get; set; }

        
        [StringLength(1)]
        public string estado { get; set; }

        public virtual tbl_empleado tbl_empleado { get; set; }

        //metodo para listar
        public AnexGRIDResponde Listar(AnexGRID agrid)
        {
            try
            {
                using (var ctx = new MaximaContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    agrid.Inicializar();

                    var query = ctx.tbl_vacacion.Where(x => x.id_vacacion > 0).Where(x => x.estado == "A");

                    // Ordenar 
                    if (agrid.columna == "id") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.id_vacacion)
                                                        : query.OrderBy(x => x.id_vacacion);

                    if (agrid.columna == "nombre") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.nombre)
                                                        : query.OrderBy(x => x.nombre);

                    if (agrid.columna == "fecha_inicio") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.fecha_inicio)
                                                        : query.OrderBy(x => x.fecha_inicio);

                    if (agrid.columna == "fecha_fin") query = agrid.columna_orden == "DESC"
                                    ? query.OrderByDescending(x => x.fecha_fin)
                                    : query.OrderBy(x => x.fecha_fin);

                    if (agrid.columna == "detalle") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.detalle)
                                                        : query.OrderBy(x => x.detalle);



                    var vacaciones = query.Skip(agrid.pagina)
                                            .Take(agrid.limite)
                                            .ToList();

                    var total = query.Count();

                    agrid.SetData(vacaciones, total);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return agrid.responde();
        }

        //metodo para obtener tipo empleado por id
        public List<tbl_vacacion> ObtenerEmpleadoId(int id)
        {
            var vacacion = new List<tbl_vacacion>();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    vacacion = ctx.tbl_vacacion.Where(x => x.id_empleado == id).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return vacacion;
        }

        //metodo para obtener empleado por id
        public tbl_vacacion ObtenerPorId(int id)
        {
            var vacacion = new tbl_vacacion();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    vacacion = ctx.tbl_vacacion.Where(x => x.id_vacacion == id).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return vacacion;
        }

        //metodo para insertar o actualizar
        public ResponseModel SaveVacacion()
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    this.estado = "A";
                    if (this.id_vacacion > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;

                    ctx.SaveChanges();

                    rm.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                rm.SetResponse(false, "no se ha podido insertar las vacaciones");
            }

            return rm;
        }

        //metodo para elimiar un cargo a nivel logico 
        public ResponseModel EliminarVacacion(int id)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    var vacacion = ctx.tbl_vacacion.Where(x => x.id_vacacion == id).SingleOrDefault();
                    vacacion.estado = "I";

                    ctx.Entry(vacacion).State = EntityState.Modified;
                    ctx.SaveChanges();
                    rm.SetResponse(true);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return rm;
        }
    }
}
