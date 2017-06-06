﻿using Entities;
using Nelibur.ObjectMapper;
using PrimaryHub.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using DA = DataEntitiesAcces;
using System.Data.Entity;


namespace PrimaryHub
{
    public partial class Hub
    {
        public FirstSnippetsOut FirstSnippets(FirstSnippetsIn input)
        {
            var result = new FirstSnippetsOut();



            //newItems = items.Select(x => TinyMapper.Map<DA.CommonEntities.Movie>(x)).ToList();
            //TinyMapper.Bind<DA.CommonEntities.Snippet, Snippet>(config =>
            //{
            //    config.Ignore(item => item.fields);
            //});


            using (var db = new DA.db())
            {
                var usuario = db.Usuarios.Where(user =>
                 user.userid == input.user.userid).SingleOrDefault();

                List<DA.CommonEntities.Snippet> snippets = new List<DA.CommonEntities.Snippet>();
                if (input.user != null)
                {
                    var permissions = db.GroupPermissions.Where(gPermission => gPermission.userid == input.user.userid).Select(g => g.groupid).ToList();

                    if (permissions != null && permissions.Count > 0)
                    {
                        snippets = db.Snippets.Include(s => s.fields).Join(permissions, s => s.groupid, per => per, (s, g) => s).ToList();
                    }

                    snippets.ForEach(s =>
                    {
                        s.fields = db.Fields.Where(f => f.snipettid == s.snipetid).ToList();
                    });

                    result.snippets = snippets.Select(s => { return SnippetFromDB(s); }).ToList();

                }
            }
            return result;
        }

        public AddSnippetOut AddSnippet(AddSnippetIn input)
        {
            var result = new AddSnippetOut();



            //TinyMapper.Bind<Snippet, DA.CommonEntities.Snippet>(config =>
            //{
            //    config.Ignore(sn => sn.fields);
            //});


            using (var db = new DA.db())
            {
                var usuario = db.Usuarios.Where(user =>
                 user.password == input.user.password
                 && (user.userName == input.user.userName || user.email == input.user.email)).SingleOrDefault();



                if (input.snippet.groupid == null)
                {
                    #region Agregar a Privados
                    var grupoPrivado = db.Groups.Where(grp => grp.isPrivate == true).SingleOrDefault();
                    #region No existe el grupo privado > crearlo
                    if (grupoPrivado == null)
                    {
                        var addedGroup = db.Groups.Add(new DA.CommonEntities.Group()
                        {
                            date = DateTime.Now,
                            description = "PRIVADO",
                            isPrivate = true,
                            title = "PRIVADO"
                        });
                        db.SaveChanges();
                        #region Agregar permiso
                        grupoPrivado = db.Groups.Where(grp => grp.isPrivate == true).SingleOrDefault();
                        db.GroupPermissions.Add(new DA.CommonEntities.GroupPermission()
                        {
                            userid = usuario.userid,
                            groupid = grupoPrivado.groupid
                        });
                        db.SaveChanges();
                        #endregion
                    }
                    input.snippet.groupid = grupoPrivado.groupid;
                    #endregion
                    #endregion
                }


                DA.CommonEntities.Snippet newSnippet = SnippetFromDB(input.snippet);  

                db.Snippets.Add(newSnippet);

                db.SaveChanges();
            }
            return result;
        }

        Snippet SnippetFromDB(DA.CommonEntities.Snippet snippet)
        {
            var ret = new Snippet();
            ret.fields = new List<Field>();
            TinyMapper.Bind<DA.CommonEntities.Field, Field>(config => { config.Ignore(f => f.snipett); });
            if (snippet.fields != null)
                snippet.fields.ForEach(orgField => { ret.fields.Add(TinyMapper.Map<Field>(orgField)); });
            ret.description = snippet.description;
            ret.groupid = snippet.groupid;
            ret.snipetid = snippet.snipetid;
            ret.date = snippet.date;
            ret.title = snippet.title;
            return ret;
        }
        DA.CommonEntities.Snippet SnippetFromDB(Snippet snippet)
        {
            var ret = new DA.CommonEntities.Snippet();
            TinyMapper.Bind<Field, DA.CommonEntities.Field>(config => { config.Ignore(f => f.snipett); });
            ret.fields = new List<DA.CommonEntities.Field>();
            if (snippet.fields != null)
                snippet.fields.ForEach(orgField => { ret.fields.Add(TinyMapper.Map<DA.CommonEntities.Field>(orgField)); });
            ret.description = snippet.description;
            ret.groupid = snippet.groupid;
            ret.snipetid = snippet.snipetid;
            ret.date = snippet.date;
            ret.title = snippet.title;
            return ret;
        }

    }



}