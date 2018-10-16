namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class tbl_estacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_estacion()
        {
            tbl_empleado = new HashSet<tbl_empleado>();
        }

        [Key]
        public int id_estacion { get; set; }

        [Required]
        [StringLength(20)]
        public string nombre_estacion { get; set; }

        [Required]
        [StringLength(50)]
        public string ubicacion { get; set; }

        [Required]
        [StringLength(20)]
        public string telefono { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_empleado> tbl_empleado { get; set; }

        //listar todos las estaciones
        public List<tbl_estacion> Listar()
        {
            var list = new List<tbl_estacion>();
            try
            {
                using(var ctx = new MaximaContext())
                {
                    list = ctx.tbl_estacion.Where(x => x.estado == "A").ToList();
                }
            }
            catch(Exception e)
            {
                throw e;
            }

            return list;
        }

        //metodo para obtener estacion por id
        public tbl_estacion ObtenerPorId(int id)
        {
            var estacion = new tbl_estacion();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    estacion = ctx.tbl_estacion.Where(x => x.id_estacion == id).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return estacion;
        }

        //metodo para insertar o actualizar
        public ResponseModel SaveEstacion()
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    this.estado = "A";
                    if (this.id_estacion > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;

                    ctx.SaveChanges();

                    rm.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                rm.SetResponse(false, "no se ha podido insertar el cargo");
            }

            return rm;
        }

        //metodo para elimiar un cargo a nivel logico 
        public ResponseModel EliminarEstacion(int id)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    var estacion = ctx.tbl_estacion.Where(x => x.id_estacion == id).SingleOrDefault();
                    estacion.estado = "I";

                    ctx.Entry(estacion).State = EntityState.Modified;
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
