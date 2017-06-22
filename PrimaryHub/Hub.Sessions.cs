using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaEntities = DataEntitiesAcces.CommonEntities;
namespace PrimaryHub
{
    using Enums;
    using Parameters;
    public partial class Hub
    {

        public NewSessionOut NewSession(NewSessionIn input)
        {
            var result = new NewSessionOut();
            var userid = input.session.userid ?? input.user?.userid;
            try
            {
                using (var db = new DataEntitiesAcces.db())
                {
                    var session = db.Sessions.Where(sess => sess.userid == userid).SingleOrDefault();

                    #region Caso. primer session
                    if (session == null)
                    {
                        db.Sessions.Add(new DaEntities.Auth.Session()
                        {
                            userid = input.session.userid ?? input.user?.userid,
                            fechasession = DateTime.UtcNow,
                            ip = input.session.ip,
                            platform = input.session.platform,
                            token = input.session.token
                        });
                    }
                    #endregion
                    #region Caso. ya existe session
                    else
                    {

                    }
                    #endregion

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }


        public GetLastSessionOut GetLastSession(GetLastSessionIn input)
        {
            var result = new GetLastSessionOut();
            try
            {
                using (var db = new DataEntitiesAcces.db())
                {
                    var session = db.Sessions.Where(sess => input.user.userid == sess.userid).SingleOrDefault();
                    result.session =  new Entities.Auth.Session()
                    {
                        userid = session.userid ?? session.user?.userid,
                        fechasession = session.fechasession,
                        ip = session.ip,
                        platform = session.platform,
                        token = session.token
                    };
                }
            }
            catch (Exception ex)
            {
                result.result = ex.Message;
            }
            return result;
        }
    }
}
