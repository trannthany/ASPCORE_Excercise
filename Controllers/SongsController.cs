using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication_Practice.Data;
using WebApplication_Practice.Models;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication_Practice.Controllers
{
    public class SongsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Songs
        public async Task<IActionResult> Index(string searchString)
        {
            var songs = from s in _context.Song select s;
            if (!String.IsNullOrEmpty(searchString)) 
            {
                songs = songs.Where(m => m.Title.Contains(searchString));
            }

            //var applicationDbContext = _context.Song.Include(s => s.MyAlbum);
            //// return View(await applicationDbContext.ToListAsync());
            return View(await songs.ToListAsync());
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song
                .Include(s => s.MyAlbum)
                .FirstOrDefaultAsync(m => m.SongID == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["MyAlbumID"] = new SelectList(_context.Album, "AlbumID", "Title");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SongID,Title,MyAlbumID,SongUrl")] Song song)
        {
            System.IO.File.Move(song.SongUrl, "~/");

            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MyAlbumID"] = new SelectList(_context.Album, "AlbumID", "Title", song.MyAlbumID);
            return View(song);
        }

        // GET: Songs/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            ViewData["MyAlbumID"] = new SelectList(_context.Album, "AlbumID", "Title", song.MyAlbumID);
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SongID,Title,MyAlbumID,SongUrl")] Song song)
        {
            if (id != song.SongID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.SongID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MyAlbumID"] = new SelectList(_context.Album, "AlbumID", "Title", song.MyAlbumID);
            return View(song);
        }

        // GET: Songs/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song
                .Include(s => s.MyAlbum)
                .FirstOrDefaultAsync(m => m.SongID == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Song.FindAsync(id);
            _context.Song.Remove(song);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.Song.Any(e => e.SongID == id);
        }
    }
}
