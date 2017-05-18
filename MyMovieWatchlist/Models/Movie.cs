using System.ComponentModel.DataAnnotations.Schema;

namespace MyMovieWatchlist.Models

{
    public class Movie
    {
        public int Id { get; set; }

        //public int? ParentId { get; set; }

        public int SiteMenuId { get; set; }
        public virtual SiteMenu SiteMenu { get; set; }

        [NotMapped]
        public string Title { get; set; }

        [NotMapped]
        public string Year { get; set; }

        [NotMapped]
        public string Rated { get; set; }

        [NotMapped]
        public string Released { get; set; }

        [NotMapped]
        public string Runtime { get; set; }

        [NotMapped]
        public string Genre { get; set; }

        [NotMapped]
        public string Director { get; set; }

        [NotMapped]
        public string Writer { get; set; }

        [NotMapped]
        public string Actors { get; set; }

        [NotMapped]
        public string Plot { get; set; }

        [NotMapped]
        public string Language { get; set; }

        [NotMapped]
        public string Country { get; set; }

        [NotMapped]
        public string Awards { get; set; }

        [NotMapped]
        public string Poster { get; set; }

        [NotMapped]
        public Rating[] Ratings { get; set; }

        [NotMapped]
        public string Metascore { get; set; }

        [NotMapped]
        public string imdbRating { get; set; }

        [NotMapped]
        public string imdbVotes { get; set; }

        public string imdbID { get; set; }

        [NotMapped]
        public string Type { get; set; }

        [NotMapped]
        public string DVD { get; set; }

        [NotMapped]
        public string BoxOffice { get; set; }

        [NotMapped]
        public string Production { get; set; }

        [NotMapped]
        public string Website { get; set; }
    }

    public class Rating
    {
        public string Source { get; set; }
        public string Value { get; set; }
    }
}