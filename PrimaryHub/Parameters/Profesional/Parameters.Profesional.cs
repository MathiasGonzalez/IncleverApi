using Entities.Profesional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryHub.Parameters
{
    public class AddProfesionalIn : BaseParameter
    {
        public Profesional Profesional { get; set; }
    }

    public class AddProfesionalOut : BaseParameter
    {
        public Profesional Profesional { get; set; }
    }
}
