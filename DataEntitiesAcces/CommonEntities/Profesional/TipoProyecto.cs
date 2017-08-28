using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntitiesAcces.CommonEntities.Profesional
{
    [Table("TipoProyecto")]
    public class TipoProyecto
    {
        [Key]
        public int TipoProyectoId { get; set; }

        public string Tipo { get; set; }

        public int Nivel { get; set; }
    }
}
