namespace SCP_MAXIMA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_usuario
    {
        [Key]
        public int id_usuario { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre_usuario { get; set; }

        [Required]
        [StringLength(550)]
        public string contraseña { get; set; }

        [Required]
        [StringLength(50)]
        public string correo { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        public int? id_rol { get; set; }

        public int? id_estacion { get; set; }

        public virtual tbl_rol tbl_rol { get; set; }
    }
}
