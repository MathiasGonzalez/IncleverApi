
using Entities;
using Nelibur.ObjectMapper;
using Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA = DataEntitiesAcces;

namespace PrimaryHub
{
    public partial class Hub
    {

        public LogInOut LogIn(LogInIn input)
        {
            DA.CommonEntities.User newUser = new DA.CommonEntities.User();


            //newItems = items.Select(x => TinyMapper.Map<DA.CommonEntities.Movie>(x)).ToList();
            TinyMapper.Bind<User, DA.CommonEntities.User>(config =>
            {
                // config.Ignore(item => item.Genres);
            });

            //foreach (var x in items)
            {
                newUser = TinyMapper.Map<DA.CommonEntities.User>(input.User);
            }

            using (var db = new DA.db())
            {
                if (newUser.password != null)
                {
                    newUser.password = Hasher.GetHashString(newUser.password);
                }
                var usuario = db.Usuarios.Where(user =>
                 user.password == newUser.password
                 && (user.userName == newUser.userName || user.email == newUser.email)).SingleOrDefault();


                if (usuario != null)
                {
                    #region usuario registrado 
                    input.User.email = usuario.email;
                    input.User.userid = usuario.userid;
                    return new LogInOut { User = input.User, result = "OK" };
                    #endregion
                }
                else
                {
                    if (input.FireBaseForce)
                    {

                        #region Si no existe un usuario con ese email lo creo
                        if (!db.Usuarios.Any(user =>
                                user.email == newUser.email
                        ))
                        {
                            SignUp(new SignUpIn()
                            {
                                User = input.User
                            });
                        }
                        #endregion
                        usuario = db.Usuarios.Where(user =>
                            (user.userName == newUser.userName || user.email == newUser.email)).SingleOrDefault();
                        TinyMapper.Bind<DA.CommonEntities.User, User>(config =>
                         {
                         });
                        return new LogInOut() { result = "FIREBASE", User = TinyMapper.Map<User>(usuario) };

                    }
                    else
                    {
                        return new LogInOut() { result = "FAIL" };
                    }
                }


            }
        }

        public SignUpOut SignUp(SignUpIn input)
        {
            if (input.User == null)
            {
                return new SignUpOut { result = "Error" };
            }

            TinyMapper.Bind<User, DA.CommonEntities.User>(config =>
            {

            });

            var newUser = TinyMapper.Map<DA.CommonEntities.User>(input.User);

            using (var db = new DA.db())
            {

                if (db.Usuarios.Any(user =>
                user.email == newUser.email
                || user.userName == newUser.userName
                ))
                {

                    return new SignUpOut { result = "YA EXISTE" };
                }

                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (newUser.password != null)
                        {
                            newUser.password = Hasher.GetHashString(newUser.password);
                        }

                        #region add new user
                        db.Usuarios.Add(newUser);
                        db.SaveChanges();
                        #endregion

                        #region add new Account for the user
                        var addedUser = db.Usuarios.Where(user => user.email == newUser.email || user.userName == newUser.userName).Single();
                        db.Accounts.Add(new DA.CommonEntities.Account()
                        {
                            onlyPrivate = false,
                            seePublic = true,
                            logins = 0,
                            userid = addedUser.userid
                        });
                        db.SaveChanges();
                        #endregion

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {

                        transaction.Rollback();
                        return new SignUpOut { result = "Error transaccion DA" };
                    }
                }
            }
            return new SignUpOut { result = "Error" };


        }

    }
}
