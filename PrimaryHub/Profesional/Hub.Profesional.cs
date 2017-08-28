using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA = DataEntitiesAcces;
using DCE = DataEntitiesAcces.CommonEntities.Profesional;
namespace PrimaryHub
{
    using Entities.Profesional;
    using Enums;
    using Nelibur.ObjectMapper;
    using Parameters;
    public partial class Hub
    {

        public AddProfesionalOut AddProfesional(AddProfesionalIn input)
        {
            var response = new AddProfesionalOut();
            using (var db = new DA.db())
            {
                TinyMapper.Bind<Profesional, DCE.Profesional>(config =>
                {
                    // config.Ignore(item => item.Genres);
                });

                var electricista = TinyMapper.Map<DCE.Profesional>(input.Profesional);

                var nuevo_prof = db.Profesionales.Add(electricista);

                db.SaveChanges();

                input.Profesional.userid = nuevo_prof.userid;
                response.Profesional = input.Profesional;


            }
            return response;
        }

    }
}
