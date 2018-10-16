using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    public class DtoEmpleado
    {
        public int id_empleado { get; set; }
        public int id_cargo { get; set; }
        public int id_estacion { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string direccion { get; set; }
        public DateTime fecha_nac { get; set; }
        public int telefono { get; set; }
        public string nit { get; set; }
        public string correo { get; set; }
        public string estado { get; set; }
        public int dpi { get; set; }
        public string estacion { get; set; }
        public string cargo { get; set; }
        public decimal? suelgo { get; set; }
    }

    public class DtoRegistro
    {
        public int id_empleado { get; set; }
        public int id_registro_pago { get; set; }
        public string empleado { get; set; }
        public decimal total { get; set; }
        public decimal faltante { get; set; }
        public int cierre { get; set; }
        public string num_planilla { get; set; }
    }

    public class DtoAbono
    {
        public int id_abono { get; set; }
        public decimal monto { get; set; }
        public string detalle { get; set; }
    }
}
