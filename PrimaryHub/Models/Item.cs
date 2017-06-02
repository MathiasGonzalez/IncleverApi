using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryHub.Models
{
    public class Item
    {
        public Item() { }
        public string Nombre { get; set; }
        public List<Propiedad> propiedades { get; set; }
    }
}
