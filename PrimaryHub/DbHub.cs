
using DA = DataEntitiesAcces;
using Entities;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PrimaryHub
{
    public static class DbHub
    {
        //#region PRIVATE
        //private static List<DA.CommonEntities.Movie> AddMovies(List<Movie> items)
        //{
        //    List<DA.CommonEntities.Movie> newItems = new List<DA.CommonEntities.Movie>();


        //    //newItems = items.Select(x => TinyMapper.Map<DA.CommonEntities.Movie>(x)).ToList();
        //    TinyMapper.Bind<Movie, DA.CommonEntities.Movie>(config =>
        //                 {
        //                     // config.Ignore(item => item.Genres);

        //                 });

        //    foreach (var x in items)
        //    {
        //        newItems.Add(TinyMapper.Map<DA.CommonEntities.Movie>(x));
        //    }

        //    using (var db = new DA.db())
        //    {
        //        using (var transaction = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                foreach (var movie in newItems)
        //                {
        //                    var mov = db.Movies.Where(m => m.id == movie.id && m.uuid == movie.uuid).SingleOrDefault();
        //                    if (mov == null)
        //                        db.Movies.Add(movie);
        //                    else
        //                        mov.seen = true;
        //                    db.SaveChanges();
        //                }





        //                transaction.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                transaction.Rollback();
        //                throw ex;
        //            }
        //        }
        //    }
        //    return newItems;
        //}
        //#endregion

        //internal static MovieData MovieData(MovieData input)
        //{
        //    MovieData output = new Parameters.MovieData();
        //    using (var database = new DA.db())
        //    {
        //        var res = database.Movies.AsNoTracking().Where(m => m.uuid == input.uuid && m.id == input.data.id).SingleOrDefault();
        //        if (res != null)
        //            output.data = TinyMapper.Map<Movie>(res);
        //    }
        //    return output;
        //}

        //internal static AddMovies AddMovies(AddMovies input)
        //{
        //    try
        //    {
        //        DbHub.AddMovies(input.newMovies);
        //        input.ok = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        input.ok = false;
        //    }
        //    return input;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>       
        //internal static MoviesMessage MoviesByDevice(MoviesMessage input)
        //{
        //    MoviesMessage output = new MoviesMessage();
        //    List<Movie> result = new List<Movie>();

        //    #region parametros

        //    string title = input.title;
        //    int? skip = input.inicio;
        //    int? take = input.cantidad;
        //    #endregion

        //    using (var database = new DA.db())
        //    {
        //        var res = title != null ?
        //            database.Movies
        //            .Where(x => x.uuid == input.uuid)
        //            .OrderBy(x => x.id)
        //            .Skip(skip ?? 0)
        //            .Take(take ?? 5)
        //            .ToList() :
        //            database.Movies
        //            .Where(x => x.uuid == input.uuid)
        //            .OrderBy(x => x.id)
        //            .Skip(skip ?? 0)
        //            .Take(take ?? 5)
        //            .ToList();


        //        if (res != null)
        //        {
        //            if (res.Count == 0)
        //            {
        //                //res = AddMovies(new List<Movie>()
        //                //{
        //                //    new Movie() {
        //                //        title =title ?? "title",
        //                //        uuid="3dsfs654fs65d4",
        //                //        release = DateTime.Today ,
        //                //        original_title = "Lorem ipsum dolor sit amet",   
        //                //        popularity =8,                           
        //                //        //Genres = new List<Genre>() {
        //                //        //    new Genre() {  title="drama" }
        //                //        //},
        //                //        Html ="",
        //                //        //Sections = new List<VisualSection>() {
        //                //        //    new VisualSection() { content =" Lorem ipsum dolor sit amet consectetur adipiscing elit. Fusce blandit magna quis turpis."+title, title="Section 1 "+title},
        //                //        //     new VisualSection() { content ="Section 2 Lorem ipsum dolor sit amet consectetur adipiscing elit. "+title, title="Section 2 "+title},
        //                //        //      new VisualSection() { content =$"var ${title} = 2; "+title, title="code"}
        //                //        //}
        //                //    }
        //                //});
        //            }
        //            res.ForEach(x =>
        //            {
        //                TinyMapper.Bind<DA.CommonEntities.Movie, Movie>(config =>
        //                {
        //                    //config.Ignore(item => item.Genres);
        //                });
        //                result.Add(TinyMapper.Map<Movie>(x));
        //            });
        //        }
        //        output.Movies = result;
        //        output.cantidad = result.Count;

        //        return output;
        //    }

        //}






    }
}
