using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// The movies to display on the index page
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }

        /// <summary>
        /// Current search terms
        /// </summary>
        [BindProperty]
        public string SearchTerms { get; set; } = "";

        /// <summary>
        /// THe filtered MPAA ratings
        /// </summary>
        [BindProperty] 
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// The filtered genres
        /// </summary>
        [BindProperty] 
        public string[] Genres { get; set; }

        /// <summary>
        /// Gets and sets the IMDB minimium rating
        /// </summary>
        public double? IMDBMin { get; set; }

        /// <summary>
        /// Gets and sets the IMDB maximum rating
        /// </summary>
        public double? IMDBMax { get; set; }

        /// <summary>
        /// Gets and sets the Rotten Tomatoe minimum rating
        /// </summary>
        public double? RottenTomatoeMin { get; set; }

        /// <summary>
        /// Gets and sets the Rotten Tomatoe maximum rating
        /// </summary>
        public double? RottenTomatoeMax { get; set; }

        /// <summary>
        /// Does the response initialization for incoming GET requests
        /// </summary>
        public void OnGet(double? IMDBMin, double? IMDBMax, double? RottenTomatoeMin, double? RottenTomatoeMax)
        {
            // Nullable conversion workaround
            this.IMDBMin = IMDBMin;
            this.IMDBMax = IMDBMax;
            this.RottenTomatoeMin = RottenTomatoeMin;
            this.RottenTomatoeMax = RottenTomatoeMax;
            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenre(Movies, Genres);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.FilterByRottenTomatoeRating(Movies, RottenTomatoeMin, RottenTomatoeMax);
        }
    }
}
