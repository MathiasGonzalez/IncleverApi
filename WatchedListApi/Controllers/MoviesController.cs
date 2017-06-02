using Entities;
using Parameters;
using PrimaryHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IncleverApi
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MoviesController : ApiController
    {
        private Hub _primaryHub = null;
        public Hub PrimaryHub
        {
            get
            {
                if (_primaryHub == null)
                {
                    _primaryHub = new Hub();
                }
                return _primaryHub;
            }
        }

        //[HttpGet]
        //public MovieData MovieData([FromUri]MovieData input)
        //{
        //    return PrimaryHub.MovieData(input);
        //}
        //[HttpGet]
        //public MoviesMessage MoviesByDevice([FromUri]MoviesMessage input)
        //{
        //    return PrimaryHub.MoviesByDevice(input);
        //}

        //[HttpPost]
        //public AddMovies AddMovies([FromBody]AddMovies input)
        //{
        //    return PrimaryHub.AddMovies(input);
        //}
    }
}