namespace SCP_MAXIMA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_prestacion_empleado
    {
        [Key]
        public int id_prestacion_empleado { get; set; }

        public int id_empleado { get; set; }

        public int id_prestacion { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha { get; set; }

        [StringLength(500)]
        public string detalle { get; set; }

        [StringLength(50)]
        public string usuarioCreo { get; set; }

        public DateTime? fechaCreo { get; set; }

        [StringLength(50)]
        public string usuarioActualizo { get; set; }

        public DateTime? fechaActualizo { get; set; }

        public virtual tbl_empleado tbl_empleado { get; set; }

        public virtual tbl_prestacion tbl_prestacion { get; set; }
    }
}
