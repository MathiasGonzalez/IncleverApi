using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntitiesAcces.CommonEntities.Profesional
{
    [Table("Proyectos")]
    public class Proyecto
    {
        [Key]
        public int ProyectoId { get; set; }

        public Guid? userid { get; set; }

        public DateTime? fecha { get; set; }

        public TipoProyecto TipoProyecto { get; set; }

        public TipoProyecto TipoProyecto2 { get; set; }

        public string Detalles { get; set; }

        public string ExtraInfo { get; set; }
    }
}
