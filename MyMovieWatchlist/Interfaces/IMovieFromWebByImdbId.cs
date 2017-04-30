using MyMovieWatchlist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMovieWatchlist.Interfaces
{
    public interface IMovieFromWebByImdbId
    {
        Movie GetMovie(string imdbId);
    }
}