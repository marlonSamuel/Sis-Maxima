namespace SCP_MAXIMA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_abono
    {
        [Key]
        public int id_abono { get; set; }

        public int id_prestamo { get; set; }

        public decimal monto { get; set; }

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

        [StringLength(1)]
        public string estado { get; set; }

        public virtual tbl_prestamo tbl_prestamo { get; set; }
    }
}
