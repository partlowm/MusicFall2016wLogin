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
    public class PlaylistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlaylistsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        [Authorize]
        public IActionResult Index()
        {
            var playlists = _context.Playlist.ToList();
            return View(playlists);
        }
        [Authorize]
        public IActionResult addPlaylistAlbums(int? id)
        {
            
            if (id != null)
            {
                ViewBag.playlists = new SelectList(_context.Playlist.Where(i => i.UserID == User.Identity.Name), "PlaylistID", "Name");
                PlayListAlbums pa = new PlayListAlbums();
                pa.AlbumID = (int)id;
                return View(pa);
            }
            else
            {
                return View("Index");
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult addPlaylistAlbum(PlayListAlbums pa)
        {
            
            if (_context.PlayListAlbums.FirstOrDefault(i => i.AlbumID == pa.AlbumID && i.PlaylistID == pa.PlaylistID) == null)
            {
                _context.PlayListAlbums.Add(pa);
                _context.SaveChanges();

            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                if (_context.Playlist.Any(ac => ac.PlaylistName.Equals(playlist.PlaylistName)))
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    _context.Playlist.Add(playlist);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return View(playlist);
        }
        [Authorize]
        public IActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            Playlist playlist = _context.Playlist.SingleOrDefault(a => a.PlaylistID == id);
            if (playlist == null)
            {
                return NotFound();
            }
            ViewBag.AlbumsList = _context.Playlist.Where(a => a.PlaylistID == playlist.PlaylistID);
            return View(playlist);

        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                _context.Playlist.Update(playlist);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(playlist);
        }
        [Authorize]
        public IActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            PlayListAlbums playlistalbums = _context.PlayListAlbums.SingleOrDefault(a => a.PlaylistID == id);
            if (playlistalbums == null)
            {
                return NotFound();
            }
            ViewBag.AlbumsList = _context.PlayListAlbums.Where(a => a.PlaylistID == playlistalbums.PlaylistID);
            return View(playlistalbums);

        }
        [Authorize]
        public IActionResult Delete(int? id)
        {
            ViewBag.ArtistID = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.GenreID = new SelectList(_context.Genres, "GenreID", "Name");
            if (id == null)
            {
                return NotFound();
            }
            Playlist playlist = _context.Playlist.SingleOrDefault(a => a.PlaylistID == id);
            if (playlist == null)
            {
                return NotFound();
            }
            ViewBag.AlbumsList = _context.Playlist.Where(a => a.PlaylistID == playlist.PlaylistID);
            return View(playlist);

        }
        [Authorize]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Playlist playlist = _context.Playlist.Find(id);
            _context.Playlist.Remove(playlist);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
