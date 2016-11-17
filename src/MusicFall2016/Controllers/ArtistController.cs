using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicFall2016.Models;
using MusicFall2016.Data;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Controllers
{
    [Authorize]
    public class ArtistController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtistController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        [Authorize]
        public IActionResult Index()
        {
            var artists = _context.Artists.ToList();
            return View(artists);
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(Artist artist)
        {
            if (ModelState.IsValid)
            {
                if (_context.Artists.Any(ac => ac.Name.Equals(artist.Name)))
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    _context.Artists.Add(artist);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return View(artist);
        }
        [Authorize]
        public IActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Artist artist = _context.Artists.SingleOrDefault(a => a.ArtistID == id);
            if (artist == null)
            {
                return NotFound();
            }
            ViewBag.AlbumsList = _context.Albums.Where(a => a.ArtistID == artist.ArtistID);
            return View(artist);

        }
        [Authorize]
        public IActionResult Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Artist artist = _context.Artists.SingleOrDefault(a => a.ArtistID == id);
            if (artist == null)
            {
                return NotFound();
            }
            ViewBag.AlbumsList = _context.Albums.Where(a => a.ArtistID == artist.ArtistID);
            return View(artist);

        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(Artist artist)
        {
            if (ModelState.IsValid)
            {
                _context.Artists.Update(artist);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistID = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.GenreID = new SelectList(_context.Genres, "GenreID", "Name");
            return View(artist);
        }
    }
}

