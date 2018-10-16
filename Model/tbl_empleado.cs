namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class tbl_empleado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_empleado()
        {
            tbl_anticipo = new HashSet<tbl_anticipo>();
            tbl_asueto = new HashSet<tbl_asueto>();
            tbl_permiso = new HashSet<tbl_permiso>();
            tbl_prestacion = new HashSet<tbl_prestacion>();
            tbl_prestamo = new HashSet<tbl_prestamo>();
            tbl_registro_pago = new HashSet<tbl_registro_pago>();
            tbl_sancion = new HashSet<tbl_sancion>();
            tbl_vacacion = new HashSet<tbl_vacacion>();
        }

        [Key]
        public int id_empleado { get; set; }

        public int id_cargo { get; set; }

        public int id_estacion { get; set; }

        [Required]
        [StringLength(20)]
        public string primer_nombre { get; set; }

        [StringLength(20)]
        public string segundo_nombre { get; set; }

        [StringLength(20)]
        public string tercer_nombre { get; set; }

        [Required]
        [StringLength(20)]
        public string primer_apellido { get; set; }

        [StringLength(20)]
        public string segundo_apellido { get; set; }

        [Required]
        [StringLength(50)]
        public string direccion { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha_nac { get; set; }

        public int telefono { get; set; }

        [Required]
        [StringLength(10)]
        public string nit { get; set; }

        [StringLength(50)]
        public string correo { get; set; }

        [StringLength(50)]
        public string usuarioCreo { get; set; }

        public DateTime? fechaCreo { get; set; }

        [StringLength(50)]
        public string usarioActualizo { get; set; }

        public DateTime? fechaActualizo { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        public int dpi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_anticipo> tbl_anticipo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_asueto> tbl_asueto { get; set; }

        public virtual tbl_cargo tbl_cargo { get; set; }

        public virtual tbl_estacion tbl_estacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_permiso> tbl_permiso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_prestacion> tbl_prestacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_prestamo> tbl_prestamo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_registro_pago> tbl_registro_pago { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_sancion> tbl_sancion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_vacacion> tbl_vacacion { get; set; }

        //obtener datos empleado a listar
        public List<tbl_empleado> GetAll()
        {
            var list = new List<tbl_empleado>();

            try
            {
                using(var ctx = new MaximaContext())
                {
                    list = ctx.tbl_empleado.Where(x => x.estado == "A").ToList();
                }

            }catch(Exception e)
            {
                throw;
            }

            return list;
        }



        //metodo para listar
        public AnexGRIDResponde Listar(AnexGRID agrid)
        {
            try
            {
                using (var ctx = new MaximaContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    agrid.Inicializar();

                    var query = ctx.tbl_empleado.Where(x => x.id_empleado > 0).Where(x => x.estado == "A");

                    // Ordenar 
                    if (agrid.columna == "id") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.id_empleado)
                                                        : query.OrderBy(x => x.id_empleado);

                    if (agrid.columna == "primer_nombre") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.primer_nombre)
                                                        : query.OrderBy(x => x.primer_nombre);

                    if (agrid.columna == "segundo_nombre") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.segundo_nombre)
                                                        : query.OrderBy(x => x.segundo_nombre);

                    if (agrid.columna == "primer_apellido") query = agrid.columna_orden == "DESC"
                                    ? query.OrderByDescending(x => x.primer_apellido)
                                    : query.OrderBy(x => x.primer_apellido);

                    if (agrid.columna == "segundo_apellido") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.segundo_nombre)
                                                        : query.OrderBy(x => x.segundo_apellido);

                    if (agrid.columna == "telefono") query = agrid.columna_orden == "DESC"
                                    ? query.OrderByDescending(x => x.telefono)
                                    : query.OrderBy(x => x.telefono);

                    if (agrid.columna == "direccion") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.direccion)
                                                        : query.OrderBy(x => x.direccion);

                    if (agrid.columna == "dpi") query = agrid.columna_orden == "DESC"
                                    ? query.OrderByDescending(x => x.dpi)
                                    : query.OrderBy(x => x.dpi);

                    if (agrid.columna == "correo") query = agrid.columna_orden == "DESC"
                                                        ? query.OrderByDescending(x => x.correo)
                                                        : query.OrderBy(x => x.correo);
                    

                    var empleados = query.Skip(agrid.pagina)
                                            .Take(agrid.limite)
                                            .ToList();

                    var total = query.Count();

                    agrid.SetData(empleados, total);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return agrid.responde();
        }

        //metodo para obtener empleado por id
        public tbl_empleado ObtenerPorId(int id)
        {
            var empleado = new tbl_empleado();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    empleado = ctx.tbl_empleado.Where(x => x.id_empleado == id).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return empleado;
        }

        //metodo para insertar o actualizar
        public ResponseModel SaveEmpleado()
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    this.estado = "A";
                    if (this.id_empleado > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;

                    ctx.SaveChanges();

                    rm.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                rm.SetResponse(false, "no se ha podido insertar el empleado");
            }

            return rm;
        }

        //metodo para elimiar un cargo a nivel logico 
        public ResponseModel EliminarEmpleado(int id)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    var empleado = ctx.tbl_empleado.Where(x => x.id_empleado == id).SingleOrDefault();
                    empleado.estado = "I";

                    ctx.Entry(empleado).State = EntityState.Modified;
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
