using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace  Parameters
{
    public class MoviesMessage
    {
        public string uuid { get; set; }
        public DateTime? searchDate { get; set; }
        public int? inicio { get; set; }
        public int? cantidad { get; set; }
        public string title { get; set; }
        public List<Movie> Movies { get; set; }
    }

    public class AddMovies
    {
        public string uuid { get; set; }
        public bool ok { get; set; }      
        public List<Movie> newMovies { get; set; }
    }
    public class MovieData{
        public string uuid { get; set; }
        public Movie data { get; set; }
    }



}



