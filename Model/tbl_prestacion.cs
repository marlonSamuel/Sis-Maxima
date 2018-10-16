namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class tbl_prestacion
    {
        [Key]
        public int id_prestacion { get; set; }

        public int id_empleado { get; set; }

        public int id_tipo { get; set; }

        public decimal? monto { get; set; }


        [Required]
        [StringLength(20)]
        public string nombre_prestacion { get; set; }

        [StringLength(500)]
        public string detalle_prestacion { get; set; }

        [StringLength(50)]
        public string usuarioCreo { get; set; }

        public DateTime? fechaCreo { get; set; }

        [StringLength(50)]
        public string usuarioActualizo { get; set; }

        public DateTime? fechaActualizo { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        public virtual tbl_empleado tbl_empleado { get; set; }

        public virtual tbl_tipo_prestacion tbl_tipo_prestacion { get; set; }

        //metodo para obtener tipo empleado por id
        public List<tbl_prestacion> ObtenerEmpleadoId(int id)
        {
            var prestacion = new List<tbl_prestacion>();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    prestacion = ctx.tbl_prestacion.Where(x => x.id_empleado == id).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return prestacion;
        }
    }
}
