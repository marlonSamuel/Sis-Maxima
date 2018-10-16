using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    public class ResumenPlanillaDto
    {
        public string num_planilla { get; set; }
        public decimal total { get; set; }
    }

    public class ResumenPlanillaNumDto
    {
        public string num_planilla { get; set; }
        public string empleado { get; set; }
        public decimal total_empleado { get; set; }
        public int no_recibo { get; set; }
        public string estacion { get; set; }
        public decimal total { get; set; }
        public string cargo { get; set; }
    }

    public class ResumenEmpleadoDto
    {
        public string empleado { get; set; }
        public string cargo { get; set; }
        public int prestamos { get; set; }
        public int sanciones { get; set; }
        public int anticipos { get; set; }
        public string estacion { get; set; }
    }
}
