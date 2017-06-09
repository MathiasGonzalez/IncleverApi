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

                    result.snippets = snippets.Select(s => { return SnippetFromDB(s); }).ToList();

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

                    var usuario = db.Usuarios.Where(user =>
                     user.password == input.user.password
                     && (user.userName == input.user.userName || user.email == input.user.email)).SingleOrDefault();




                    if (input.snippet.groupid == null)
                    {
                        #region Agregar a Privados

                        GroupPermission permisoPrivado = null;

                        var permisoPrivadoDB = db.GroupPermissions.AsNoTracking().Where(gp => gp.userid == usuario.userid && gp.isprivate == true).SingleOrDefault();


                        #region No existe el grupo privado > crearlo
                        if (permisoPrivadoDB == null)
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
                            permisoPrivadoDB = new DaEntities.GroupPermission()
                            {
                                isprivate = true,
                                userid = usuario.userid,
                                groupid = nuevoGrupoPrivado.groupid
                            };
                            db.GroupPermissions.Add(permisoPrivadoDB);
                            db.SaveChanges();
                            #endregion


                        }
                        input.snippet.groupid = permisoPrivadoDB.groupid;
                        #endregion
                        #endregion
                    }


                    DaEntities.Snippet newSnippet = SnippetMapper(input.snippet);

                    db.Snippets.Add(newSnippet);
                    db.SaveChanges();
                    input.snippet.id = newSnippet.id;
                    return new AddSnippetOut() { user = input.user, snippet = input.snippet, result = "OK" };

                }
            }
            catch (Exception ex)
            {
                return new AddSnippetOut() { result = ex.Message };
            }
        }

        public AddSnippetOut EditSnippet(AddSnippetIn input)
        {
            var result = new AddSnippetOut();
            try
            {
                using (var db = new DataEntitiesAcces.db())
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

                    return new AddSnippetOut() { user = input.user, snippet = input.snippet, result = "OK" };
                }
            }
            catch (Exception ex)
            {
                return new AddSnippetOut() { result = ex.Message };
            }
        }


        Snippet SnippetFromDB(DaEntities.Snippet snippet)
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
            TinyMapper.Bind<Field, DaEntities.Field>(config => { config.Ignore(f => f.snipett); });
            ret.fields = new List<DaEntities.Field>();
            if (snippet.fields != null)
            {
                snippet.fields.ForEach(orgField => { ret.fields.Add(TinyMapper.Map<DaEntities.Field>(orgField)); });
            }
            ret.description = snippet.description;
            ret.groupid = snippet.groupid;
            ret.snipetid = snippet.snipetid;
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
