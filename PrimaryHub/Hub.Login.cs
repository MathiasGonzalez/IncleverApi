
using Entities;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA = DataEntitiesAcces;

namespace PrimaryHub
{
    using Enums;
    using Parameters;
    public partial class Hub
    {

        public LogInOut LogIn(LogInIn input, string IP = null)
        {
            DA.CommonEntities.User newUser = new DA.CommonEntities.User();


            //newItems = items.Select(x => TinyMapper.Map<DA.CommonEntities.Movie>(x)).ToList();
            TinyMapper.Bind<User, DA.CommonEntities.User>(config =>
            {
                // config.Ignore(item => item.Genres);
            });

            //foreach (var x in items)
            {
                newUser = TinyMapper.Map<DA.CommonEntities.User>(input.user);
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
                    input.user.email = usuario.email;
                    input.user.userid = usuario.userid;
                    input.user.UID = usuario.UID;

                    #region Nueva Session
                    NewSession(input.user, IP);
                    #endregion

                    return new LogInOut { user = input.user, result = "OK" };
                    #endregion
                }
                else
                {
                    if (input.FireBaseForce && newUser.email != null)
                    {

                        #region Si no existe un usuario con ese email lo creo
                        if (!db.Usuarios.Any(user =>
                                user.email == newUser.email
                        ))
                        {
                            SignUp(new SignUpIn()
                            {
                                user = input.user
                            });
                        }
                        #endregion

                        usuario = db.Usuarios.Where(user => user.email == newUser.email).SingleOrDefault();

                        TinyMapper.Bind<DA.CommonEntities.User, User>(config => { });

                        var ruser = TinyMapper.Map<User>(usuario);

                        #region Nueva Session
                        NewSession(ruser, IP);
                        #endregion

                        return new LogInOut() { result = "FIREBASE", user = ruser };
                    }
                    else
                    {
                        return new LogInOut() { result = newUser.userName == null && newUser.email == null ? "NULLDATA" : "FAIL" };
                    }
                }


            }
        }

        public SignUpOut SignUp(SignUpIn input)
        {
            SignUpOut result = new SignUpOut { result = "Error" };


            if (input.user == null)
            {
                return result;
            }

            TinyMapper.Bind<User, DA.CommonEntities.User>(config =>
            {

            });

            var newUser = TinyMapper.Map<DA.CommonEntities.User>(input.user);

            using (var db = new DA.db())
            {

                if (db.Usuarios.Any(user =>
                user.email == newUser.email
                || user.userName == newUser.userName
                ))
                {

                    return new SignUpOut { result = "YA EXISTE" };
                }

                using (var transaccion = db.Database.BeginTransaction())
                {
                    try
                    {
                        #region hasheo password
                        if (newUser.password != null)
                        {
                            newUser.password = Hasher.GetHashString(newUser.password);
                        }
                        #endregion

                        #region add new user
                        newUser.status = UserStatus.Activo;
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


                        #region No existe el grupo defecto > crearlo

                        #region Agregar Grupo por Defecto  
                        //permisoPrivado = GroupPermissionMapper(permisoPrivadoDB);
                        var nuevoGrupoPrivado = new DA.CommonEntities.Group()
                        {
                            date = DateTime.Now,
                            description = "PRIVATE",
                            isPrivate = true,
                            title = "PRIVATE"
                        };

                        db.Groups.Add(nuevoGrupoPrivado);
                        db.SaveChanges();
                        #endregion

                        #region Agregar permiso para el grupo nuevo
                        //grupo creado recientemente
                        var permisoPorDefectoDB = new DA.CommonEntities.GroupPermission()
                        {
                            isprivate = true,
                            isdefault = true,
                            userid = newUser.userid,
                            groupid = nuevoGrupoPrivado.groupid
                        };
                        db.GroupPermissions.Add(permisoPorDefectoDB);
                        db.SaveChanges();
                        #endregion

                        #endregion

                        transaccion.Commit();
                        TinyMapper.Bind<DA.CommonEntities.User, User>(config => { });
                        result = new SignUpOut() { result = "OK", user = TinyMapper.Map<User>(addedUser) };
                    }
                    catch (Exception ex)
                    {

                        transaccion.Rollback();
                        result = new SignUpOut { result = "Error transaccion DA : " + ex.Message };
                    }
                }
            }
            return result;


        }


        /// <summary>
        /// crea una nueva session
        /// </summary>
        /// <param name="user">El usuario</param>
        /// <param name="IP">Dirección IP</param>
        private void NewSession(User user, string IP)
        {
            NewSession(new NewSessionIn
            {
                session = new Entities.Auth.Session
                {
                    ip = IP,
                    userid = user.userid
                }
            });
        }
    }
}
