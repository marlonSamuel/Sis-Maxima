namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class tbl_sancion
    {
        [Key]
        public int id_sancion { get; set; }

        public int id_empleado { get; set; }


        [Required]
        [StringLength(50)]
        public string nombre_sancion { get; set; }

        [StringLength(500)]
        public string detalle { get; set; }

        public decimal monto { get; set; }

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

                    var query = ctx.tbl_sancion.Where(x => x.id_sancion > 0).Where(x => x.estado == "A");

                    // Ordenar 
                    if (agrid.columna == "id") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.id_sancion)
                                                        : query.OrderBy(x => x.id_sancion);

                    if (agrid.columna == "nombre") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.nombre_sancion)
                                                        : query.OrderBy(x => x.nombre_sancion);


                    if (agrid.columna == "detalle") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.detalle)
                                                        : query.OrderBy(x => x.detalle);

                    if (agrid.columna == "monto") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.monto)
                                                        : query.OrderBy(x => x.monto);



                    var sanciones = query.Skip(agrid.pagina)
                                            .Take(agrid.limite)
                                            .ToList();

                    var total = query.Count();

                    agrid.SetData(sanciones, total);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return agrid.responde();
        }

        //metodo para obtener tipo empleado por id
        public List<tbl_sancion> ObtenerEmpleadoId(int id)
        {
            var sancion = new List<tbl_sancion>();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    sancion = ctx.tbl_sancion.Where(x => x.id_empleado == id).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return sancion;
        }

        //metodo para obtener sancion por id
        public tbl_sancion ObtenerPorId(int id)
        {
            var sancion = new tbl_sancion();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    sancion = ctx.tbl_sancion.Where(x => x.id_sancion == id).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return sancion;
        }

        //metodo para insertar o actualizar
        public ResponseModel SaveSancion()
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    this.estado = "A";
                    if (this.id_sancion > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;

                    ctx.SaveChanges();

                    rm.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                rm.SetResponse(false, "no se ha podido insertar la sancion");
            }

            return rm;
        }

        //metodo para elimiar un cargo a nivel logico 
        public ResponseModel EliminarSancion(int id)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    var sancion = ctx.tbl_sancion.Where(x => x.id_sancion == id).SingleOrDefault();
                    sancion.estado = "I";

                    ctx.Entry(sancion).State = EntityState.Modified;
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
