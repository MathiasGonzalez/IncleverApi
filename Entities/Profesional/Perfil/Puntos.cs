using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Profesional.Perfil
{
    public class Puntos
    {
        public Guid? userid { get; set; }

        public int? MensajeId { get; set; }

        public Guid? userid_Origen { get; set; }

        public CategoriaPunto CategoriaPunto { get; set; }

        public int Puntaje { get; set; }
    }
}
