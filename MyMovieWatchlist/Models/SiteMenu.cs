using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyMovieWatchlist.Models
{
    public class SiteMenu
    {
        [Key]
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string NavUrl { get; set; }
        public int ParentMenuId { get; set; }
        public List<Movie> Movies { get; set; }
    }
}