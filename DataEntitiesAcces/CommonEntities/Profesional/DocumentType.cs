using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntitiesAcces.CommonEntities.Profesional
{
    public class DocumentType
    {
		[Key]
        public int DocumentTypeId { get; set; }

        public string DocumentName { get; set; }
    }
}
