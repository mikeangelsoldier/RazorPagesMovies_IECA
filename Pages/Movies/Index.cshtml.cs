using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovies.Models;
using RazorPagesMovies.Data;
using Microsoft.AspNetCore.Mvc.Rendering; // -

namespace RazorPagesMovies.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovies.Data.MovieContext _context;

        public IndexModel(RazorPagesMovies.Data.MovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get; set; } = default!; // -

        [BindProperty(SupportsGet = true)]// -
        public string? SearchString { get; set; }// -

        public SelectList? Genres { get; set; }// - Será para leer generos de bd y usarlos para select en html

        [BindProperty(SupportsGet = true)]// -
        public string? MovieGenre { get; set; }// -


        public async Task OnGetAsync()
        {



            IQueryable<string> genreQuery = from m in _context.Movies orderby m.Genre select m.Genre; //-





            var movies = from n in _context.Movies select n; // -  consulta a bd

            if (!string.IsNullOrEmpty(SearchString)) //-
            {
                movies = movies.Where(s => s.Title.Contains(SearchString));//- Se filtran de acuerdo a un campo de busqueda
            }

            if (!string.IsNullOrEmpty(MovieGenre)) //-
            {
                movies = movies.Where(x => x.Genre == MovieGenre);//- Se filtran de acuerdo a un campo de busqueda
            }



            if (_context.Movies != null)
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()); //-
                Movie = await movies.ToListAsync();
            }

            /*
                        if (_context.Movies != null)
                        {
                            Movie = await _context.Movies.ToListAsync();
                        }
                        */
        }
    }
}
