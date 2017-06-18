using Entities;
using Nelibur.ObjectMapper;
using PrimaryHub.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using DaEntities = DataEntitiesAcces.CommonEntities;
using System.Data.Entity;


namespace PrimaryHub
{
    public partial class Hub
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public FirstSnippetsOut FirstSnippets(FirstSnippetsIn input)
        {
            var result = new FirstSnippetsOut();



            //newItems = items.Select(x => TinyMapper.Map<DA.CommonEntities.Movie>(x)).ToList();
            //TinyMapper.Bind<DA.CommonEntities.Snippet, Snippet>(config =>
            //{
            //    config.Ignore(item => item.fields);
            //});


            using (var db = new DataEntitiesAcces.db())
            {
                var usuario = db.Usuarios.Where(user =>
                 user.userid == input.user.userid).SingleOrDefault();

                List<DaEntities.Snippet> snippets = new List<DaEntities.Snippet>();
                if (input.user != null)
                {
                    var permissions = db.GroupPermissions.Where(gPermission => gPermission.userid == input.user.userid).Select(g => g.groupid).ToList();

                    if (permissions != null && permissions.Count > 0)
                    {
                        snippets = db.Snippets.Include(x => x.fields).Join(permissions, s => s.groupid, per => per, (s, g) => s).ToList();
                    }

                    //snippets.ForEach(s =>
                    //{
                    //    s.fields = db.Fields.Where(f => f.snipettid == s.snipetid).ToList();
                    //});

                    result.snippets = snippets.Select(s => { return SnippetMapper(s); }).ToList();

                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public AddSnippetOut AddSnippet(AddSnippetIn input)
        {
            var result = new AddSnippetOut();



            //TinyMapper.Bind<Snippet, DA.CommonEntities.Snippet>(config =>
            //{
            //    config.Ignore(sn => sn.fields);
            //});

            try
            {
                using (var db = new DataEntitiesAcces.db())
                {
                    using (var transaccion = db.Database.BeginTransaction())
                    {

                        try
                        {

                            var usuario = db.Usuarios.Where(user =>
                            user.password == input.user.password
                            && (user.userName == input.user.userName || user.email == input.user.email)).SingleOrDefault();



                            #region No se especifica grupo > agregar a grupo por defecto
                            if (input.snippet.groupid == null)
                            {
                                #region Agregar a Defecto                        

                                var permisoPorDefectoDB = db.GroupPermissions.AsNoTracking().Where(gp => gp.userid == usuario.userid && gp.isdefault == true).SingleOrDefault();

                                #region No existe el grupo defecto > crearlo
                                if (permisoPorDefectoDB == null)
                                {


                                    //permisoPrivado = GroupPermissionMapper(permisoPrivadoDB);
                                    var nuevoGrupoPrivado = new DaEntities.Group()
                                    {
                                        date = DateTime.Now,
                                        description = "PRIVATE",
                                        isPrivate = true,
                                        title = "PRIVATE"
                                    };

                                    db.Groups.Add(nuevoGrupoPrivado);

                                    db.SaveChanges();


                                    #region Agregar permiso para el grupo privado
                                    //grupo creado recientemente
                                    permisoPorDefectoDB = new DaEntities.GroupPermission()
                                    {
                                        isprivate = true,
                                        isdefault = true,
                                        userid = usuario.userid,
                                        groupid = nuevoGrupoPrivado.groupid
                                    };
                                    db.GroupPermissions.Add(permisoPorDefectoDB);
                                    db.SaveChanges();
                                    #endregion


                                }
                                input.snippet.groupid = permisoPorDefectoDB.groupid;
                                #endregion

                                #endregion
                            }
                            #endregion

                            DaEntities.Snippet newSnippet = SnippetMapper(input.snippet);

                            db.Snippets.Add(newSnippet);

                            db.SaveChanges();

                            transaccion.Commit();

                            input.snippet.id = newSnippet.id;

                            result = new AddSnippetOut() { user = input.user, snippet = input.snippet, result = "OK" };
                        }
                        catch (Exception ex)
                        {
                            transaccion.Rollback();
                            result = new AddSnippetOut() { result = ex.Message };
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                result = new AddSnippetOut() { result = ex.Message };
            }

            return result;
        }

        public AddSnippetOut EditSnippet(AddSnippetIn input)
        {
            var result = new AddSnippetOut();
            try
            {
                using (var db = new DataEntitiesAcces.db())
                {
                    using (var transaccion = db.Database.BeginTransaction())
                    {

                        try
                        {

                            var usuario = db.Usuarios.Where(user =>
                             user.password == input.user.password
                             && (user.userName == input.user.userName || user.email == input.user.email)).SingleOrDefault();

                            DaEntities.Snippet editedSnippet = db.Snippets.Include(snp => snp.fields).Where(snp => snp.id == input.snippet.id).SingleOrDefault();
                            //trato de eliminar los fields que de todas formas se agreagaran
                            db.Fields.RemoveRange(editedSnippet.fields);

                            db.SaveChanges();

                            SnippetMapper(input.snippet, editedSnippet);

                            db.SaveChanges();

                            transaccion.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaccion.Rollback();
                            result = new AddSnippetOut() { result = ex.Message };
                        }
                    }

                    result = new AddSnippetOut() { user = input.user, snippet = input.snippet, result = "OK" };
                }
            }
            catch (Exception ex)
            {
                result = new AddSnippetOut() { result = ex.Message };
            }
            return result;
        }


        Snippet SnippetMapper(DaEntities.Snippet snippet)
        {
            if (snippet == null)
            {
                return null;
            }
            var ret = new Snippet();
            ret.fields = new List<Field>();
            TinyMapper.Bind<DaEntities.Field, Field>(config => { config.Ignore(f => f.snipett); });
            if (snippet.fields != null)
                snippet.fields.ForEach(orgField => { ret.fields.Add(TinyMapper.Map<Field>(orgField)); });
            ret.description = snippet.description;
            ret.groupid = snippet.groupid;
            ret.snipetid = snippet.snipetid;
            ret.date = snippet.date;
            ret.title = snippet.title;
            return ret;
        }
        DaEntities.Snippet SnippetMapper(Snippet snippet, DaEntities.Snippet mod = null)
        {
            if (snippet == null)
            {
                return null;
            }
            var ret = mod ?? new DaEntities.Snippet();

            ret.snipetid = snippet.snipetid;

            if (mod != null) ret.snipetid = mod.snipetid;

            TinyMapper.Bind<Field, DaEntities.Field>(config => { config.Ignore(f => f.snipett); });
            ret.fields = new List<DaEntities.Field>();
            if (snippet.fields != null)
            {
                snippet.fields.ForEach(orgField => { ret.fields.Add(TinyMapper.Map<DaEntities.Field>(orgField)); });
            }
            ret.description = snippet.description;
            ret.groupid = snippet.groupid;

            ret.date = snippet.date;
            ret.title = snippet.title;
            return ret;
        }

        GroupPermission GroupPermissionMapper(DaEntities.GroupPermission per)
        {
            if (per == null)
            {
                return null;
            }
            var result = new GroupPermission();
            result.isprivate = per.isprivate;
            result.groupid = per.groupid;
            result.sticky = per.sticky;
            result.userid = per.userid;
            //result.group = per.group;   
            TinyMapper.Bind<User, DaEntities.User>(config => { });
            result.user = TinyMapper.Map<User>(per.user);
            return result;
        }
        DaEntities.GroupPermission GroupPermissionMapper(GroupPermission per)
        {
            if (per == null)
            {
                return null;
            }
            var result = new DaEntities.GroupPermission();
            result.isprivate = per.isprivate;
            result.groupid = per.groupid;
            result.sticky = per.sticky;
            result.userid = per.userid;
            TinyMapper.Bind<DaEntities.User, User>(config => { });
            result.user = TinyMapper.Map<DaEntities.User>(per.user);
            return result;
        }

    }



}
