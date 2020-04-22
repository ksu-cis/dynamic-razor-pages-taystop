﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public static class MovieDatabase
    {
        private static List<Movie> movies = new List<Movie>();

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        static MovieDatabase() {
            
            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }

            HashSet<string> genreSet = new HashSet<string>();
            foreach (Movie movie in movies)
            {
                if (movie.MajorGenre != null)
                {
                    genreSet.Add(movie.MajorGenre);
                }
            }
            genres = genreSet.ToArray();
        }

        /// <summary>
        /// Gets all the movies in the database
        /// </summary>
        public static IEnumerable<Movie> All { get { return movies; } }

        /// <summary>
        /// A list of MPAA Ratings
        /// </summary>
        public static string[] MPAARatings
        {
            get => new string[]
            {
                "G",
                "PG",
                "PG-13",
                "R",
                "NC-17"
            };
        }

        //Private backing genres
        private static string[] genres;
        /// <summary>
        /// gets the movie genres represented in the database
        /// </summary>
        public static string[] Genres => genres;

        /// <summary>
        /// Searches the database for matching movies
        /// </summary>
        /// <param name="terms">The terms to search for</param>
        /// <returns>A collection of movies</returns>
        public static IEnumerable<Movie> Search(string terms)
        {
            List<Movie> results = new List<Movie>();
            if (terms == null) return All;

            foreach(Movie movie in All)
            {
                if(movie.Title != null && movie.Title.Contains(terms, StringComparison.InvariantCultureIgnoreCase))
                {
                    results.Add(movie);
                }
            }
            return results;
        }

        /// <summary>
        /// Filters the provided collection of movies
        /// </summary>
        /// <param name="movies">The collection of movies to filter</param>
        /// <param name="ratings">The ratings to include</param>
        /// <returns>A collection containing only movies that match the filter</returns>
        public static IEnumerable<Movie> FilterByMPAARating(IEnumerable<Movie> movies, IEnumerable<string> ratings)
        {
            if (ratings == null || ratings.Count() == 0) return movies;
            List<Movie> results = new List<Movie>();
            foreach(Movie movie in movies)
            {
                if(movie.MPAARating != null && ratings.Contains(movie.MPAARating))
                {
                    results.Add(movie);
                }
            }
            return results;
        }

        /// <summary>
        /// Filters the provided collection of movies
        /// </summary>
        /// <param name="movies">The collection of movies to filter</param>
        /// <param name="genres">The Genres to include</param>
        /// <returns>A collection containing only movies that match the filter</returns>
        public static IEnumerable<Movie> FilterByGenre(IEnumerable<Movie> movies, IEnumerable<string> genres)
        {
            if (genres == null || genres.Count() == 0) return movies;
            List<Movie> results = new List<Movie>();
            foreach(Movie movie in movies)
            {
                if(movie.MajorGenre != null && genres.Contains(movie.MajorGenre))
                {
                    results.Add(movie);
                }
            }
            return results;
        }

        /// <summary>
        /// Filters the provided collection of movies
        /// </summary>
        /// <param name="movies">The collection of movies to filter</param>
        /// <param name="min">The min score rating</param>
        /// <param name="max">The max score rating</param>
        /// <returns>A collection containing only movies that match the filter</returns>
        public static IEnumerable<Movie> FilterByIMDBRating(IEnumerable<Movie> movies, double? min, double? max)
        {
            if (min == null && max == null) return movies;
            List<Movie> results = new List<Movie>();
            if (max == null)
            {
                foreach (Movie movie in movies)
                {
                    if (movie.IMDBRating <= max) results.Add(movie);
                }
            }
            if (min == null)
            {
                foreach (Movie movie in movies)
                {
                    if (movie.IMDBRating >= min) results.Add(movie);
                }
            }
            foreach (Movie movie in movies)
            {
                if (movie.IMDBRating >= min && movie.IMDBRating <= max)
                {
                    results.Add(movie);
                }
            }
            return results;
        }

        /// <summary>
        /// Filters the provided collection of movies
        /// </summary>
        /// <param name="movies">the collection of movies to filter</param>
        /// <param name="min">the min score rating</param>
        /// <param name="max">the max score rating</param>
        /// <returns>A collection containing only movies that match the filter</returns>
        public static IEnumerable<Movie> FilterByRottenTomatoeRating(IEnumerable<Movie> movies, double? min, double? max)
        {
            if (min == null && max == null) return movies;
            List<Movie> results = new List<Movie>();
            if(max == null)
            {
                foreach(Movie movie in movies)
                {
                    if (movie.RottenTomatoesRating <= max) results.Add(movie);
                }
            }
            if(min == null)
            {
                foreach(Movie movie in movies)
                {
                    if (movie.RottenTomatoesRating >= min) results.Add(movie);
                }
            }
            foreach (Movie movie in movies)
            {
                if (movie.RottenTomatoesRating >= min && movie.RottenTomatoesRating <= max)
                {
                    results.Add(movie);
                }
            }
            return results;
        }
    }
}
