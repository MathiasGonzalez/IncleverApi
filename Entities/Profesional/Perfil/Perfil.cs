using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Profesional.Perfil
{
    public class Perfil
    {
        public Guid? userid { get; set; }   

        public List<Puntos> Puntos { get; set; }

        public List<Mensaje> Mensajes { get; set; }

        public string ExtraInfo { get; set; }
    }
}
