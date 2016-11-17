using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicFall2016.Models;
using MusicFall2016.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Controllers
{
    [Authorize]
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GenreController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        [Authorize]
        public IActionResult Index()
        {
            var genres = _context.Genres.ToList();
            return View(genres);
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if (_context.Genres.Any(ac => ac.Name.Equals(genre.Name)))
            {

                return RedirectToAction("Index");
            }
            else
            {
                _context.Genres.Add(genre);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genre);
        }

        [Authorize]
        public IActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Genre genre = _context.Genres.SingleOrDefault(a => a.GenreID == id);
            if (genre == null)
            {
                return NotFound();
            }
            ViewBag.AlbumsList = _context.Albums.Where(a => a.GenreID == genre.GenreID);
            return View(genre);

        }
        [Authorize]
        public IActionResult Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Genre genre = _context.Genres.SingleOrDefault(a => a.GenreID == id);
            if (genre == null)
            {
                return NotFound();
            }
            ViewBag.AlbumsList = _context.Albums.Where(a => a.GenreID == genre.GenreID);
            return View(genre);

        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _context.Genres.Update(genre);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistID = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.GenreID = new SelectList(_context.Genres, "GenreID", "Name");
            return View(genre);
        }

    }
}
