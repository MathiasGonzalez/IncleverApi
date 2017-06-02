using DataEntitiesAcces;
using Entities;
using Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryHub
{
    public partial class Hub
    {

        

        public Hub()
        {
            
        }

        public MovieData MovieData(MovieData input)
        {
            return DbHub.MovieData(input);

        }

            

        public MoviesMessage MoviesByDevice(MoviesMessage input)
        {
            return  DbHub.MoviesByDevice(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public AddMovies AddMovies(AddMovies input) {
           return DbHub.AddMovies(input);
        }

        
    }
}
