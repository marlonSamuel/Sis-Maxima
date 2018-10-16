namespace SCP_MAXIMA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_permiso
    {
        [Key]
        public int id_permiso { get; set; }

        public int id_tipo_permiso { get; set; }

        public int id_empleado { get; set; }

        [Required]
        [StringLength(20)]
        public string nombre { get; set; }

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

        public virtual tbl_tipo_permiso tbl_tipo_permiso { get; set; }
    }
}
