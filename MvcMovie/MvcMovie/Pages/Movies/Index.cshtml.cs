using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly MvcMovie.Models.MovieContext _context;

        public IndexModel(MvcMovie.Models.MovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get; set; }
        public SelectList Geners;
        public string MovieGener { get; set; }

        public async Task OnGetAsync(string movieGener,string searchString)
        {
            //if (String.IsNullOrEmpty(strSearch))
            //    Movie = await _context.Movie.ToListAsync();
            //Movie = await _context.Movie.Where(r => r.Title.Contains(strSearch)).ToListAsync();

            IQueryable<string> generQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie
                         select m;

            if (!string.IsNullOrEmpty(searchString))
                movies = movies.Where(r => r.Title.Contains(searchString));

            if (!String.IsNullOrEmpty(movieGener))
                movies = movies.Where(r => r.Genre == movieGener);

            Geners = new SelectList(await generQuery.Distinct().ToListAsync());
            Movie = await movies.ToListAsync();
        }
    }
}
