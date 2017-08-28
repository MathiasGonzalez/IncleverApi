using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntitiesAcces.CommonEntities.Profesional
{
    [Table("Electricistas")]
    public class Profesional 
    {
        [Key]
        public Guid userid { get; set; }

        [ForeignKey("userid")]
        public User user { get; set; }
        //         1)VISIBLE PARA NOSOTROS
        //Documento de Identidad.
        public int? DocumentTypeId { get; set; }

        [ForeignKey("DocumentTypeId")]
        public DocumentType DocumentType { get; set; }

        public string DocumentNumber { get; set; }
        //Domicilio Particular/Fiscal.
        public string Domicilio { get; set; }
        //Ciudad /Barrio.
        public string Ciudad { get; set; }
        public string Barrio { get; set; }
        //Estudios.
        public string Estudios { get; set; }
        //Firma en UTE
        public bool? FirmaUte { get; set; }
        //Capacidades Tecnicas.
        public string CapacidadesTecnicas { get; set; }
        //Disponibilidad Horaria.
        //Vehiculo Propio.
        public bool? Vehiculo { get; set; }
        //Tarifa.
        public decimal? Tarifa { get; set; }
        //Facturacion ---SI/NO 
        public bool? Facturacion { get; set; }
        //----------------------------------------------------------
        //VISIBLE PARA EL CLIENTE: 
        //Capacidades.
        //Teléfono.
        public string Telefono { get; set; }
        public string Telefono2 { get; set; }
        //Tarifa.
        //METADATA
        public string ExtraInfo { get; set; }
    }
}
