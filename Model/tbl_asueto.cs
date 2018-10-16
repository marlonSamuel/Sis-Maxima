namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class tbl_asueto
    {
        [Key]
        public int id_dia { get; set; }

        [Required]
        [StringLength(20)]
        public string nombre { get; set; }

        public int? id_empleado { get; set; }

        [StringLength(50)]
        public string usuarioCreo { get; set; }

        public DateTime? fechaCreo { get; set; }

        [StringLength(50)]
        public string usuarioActualizo { get; set; }

        public DateTime? fechaActualizo { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        public virtual tbl_empleado tbl_empleado { get; set; }

        //metodo para obtener tipo empleado por id
        public List<tbl_asueto> ObtenerEmpleadoId(int id)
        {
            var asueto = new List<tbl_asueto>();
            try
            {
                using (var ctx = new MaximaContext())
                {
                    asueto = ctx.tbl_asueto.Where(x => x.id_empleado == id).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return asueto;
        }
    }
}
