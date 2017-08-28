using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Profesional.Perfil
{
    public class Mensaje
    {
        public int MensajeId { get; set; }

        public Guid? userid { get; set; }

        public Guid? userid_origen { get; set; }

        public bool? EsComentario { get; set; }
        
        public List<Puntos> Puntos { get; set; }

        public string ContenidoMensaje { get; set; } 
        
        public DateTime? Fecha { get; set; }

        public string ExtraInfo { get; set; }
    }
}
