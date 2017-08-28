using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Profesional
{
    public class Proyecto
    {
        public int ProyectoId { get; set; }

        public Guid? userid { get; set; }

        public DateTime? fecha { get; set; }

        public TipoProyecto TipoProyecto { get; set; }

        public TipoProyecto TipoProyecto2 { get; set; }

        public string Detalles { get; set; }

        //METADATA
        public string ExtraInfo { get; set; }
    }
}
