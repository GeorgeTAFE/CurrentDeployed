using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DreamTimeS224.Data;
using DreamTimeS224.Models;

namespace DreamTimeS224.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Books.ToListAsync());

            // Get the list of books with their associated genres
            // This basically creates an INNER JOIN between Books and Genres
            return View(await _context.Books.Include(b => b.Genre).ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(string id)
        {
            // Check if no ID given
            if (id == null) return NotFound("Must give a book ID.");

            // Find the book in the database (with associated Genre)
            var book = await _context.Books
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.ISBN == id);

            // Check if book does not exist
            if (book == null) return NotFound("Book not found.");

            // Load the Details view
            return View(book);
        }

        //// GET: Books/Create
        //public IActionResult Create()
        //{
        //    // Get a list of genres to populate a dropdown list and pass through using ViewData
        //    ViewData["GenreList"] = new SelectList(_context.Genres, "Id", "Name");

        //    // Load the Create view
        //    return View();
        //}

        // GET: Books/Create/[id?]
        //[HttpGet("Books/Create/{id?}")]
        public async Task<IActionResult> Create(string? id)
        {
            // Get a list of genres to populate a dropdown list and pass through using ViewData
            ViewData["GenreList"] = new SelectList(_context.Genres, "Id", "Name");

            // If no ISBN (id) has been supplied, just load the view
            if (id == null) return View();

            // Get book from the ISBN (id) passed in
            Book? book = await _context.Books.FindAsync(id);

            // Check if the book does NOT exist
            if (book == null)
            {
                // Option 1: Display a nasty 404 Not Found error
                //return NotFound("No book with that ISBN was found.");

                // Option 2: Redirect back to the book listing/index
                return RedirectToAction(nameof(Index));
            }

            // Load the Create view with the book model
            return View(book);
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ISBN,Title,Author,Pages,Description,ImageFilename,Copies,Publisher,DatePublished,Edition,GenreId")] Book book)
        {
            // Turn genre ID into a Genre object
            Genre? genre = await _context.Genres.FindAsync(book.GenreId);

            //// Check if genre is null and default to the first genre (or some other default)
            //genre = genre ?? await _context.Genres.FirstAsync();

            // Check if genre doesn't exist and throw 404 error (or something nicer)
            if (genre == null)
            {
                return NotFound("Oi! Cut it out! Stop messing with the genre ID!");
            }

            // Assign genre to the book (previously null)
            book.Genre = genre;

            // Option 1: Manually revalidate the model (to include the assigned genre)
            //ModelState.Clear();
            //TryValidateModel(book);
            ////await TryUpdateModelAsync(book);

            // Option 2: Remove/ignore validation for the Genre property
            ModelState.Remove("Genre");

            // Check if ISBN is already in use (book exists)
            if (null != await _context.Books.FindAsync(book.ISBN))
            {
                // Option 1: Bad UX
                //return BadRequest("ISBN already exists.");

                // Option 2: Pass back a custom validation error message
                ModelState.AddModelError("ISBN", "ISBN already exists.");
            }

            // Check if model is valid
            if (ModelState.IsValid)
            {
                // Add book to EF DbSet and save changes in database
                _context.Add(book);
                await _context.SaveChangesAsync();

                // Redirect back to Index listing
                return RedirectToAction(nameof(Index));
            }

            // Get a list of genres to populate a dropdown list and pass through using ViewData
            ViewData["GenreList"] = new SelectList(_context.Genres, "Id", "Name");

            // Display the Create view with error messages
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            // Get a list of genres to populate a dropdown list and pass through using ViewData
            ViewData["GenreList"] = new SelectList(_context.Genres, "Id", "Name");

            // Load the Create view
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ISBN,Title,Author,Pages,Description,ImageFilename,Copies,Publisher,DatePublished,Edition,GenreId")] Book book)
        {
            // Check if ISBN in URL does not match the book's ISBN
            if (id != book.ISBN) return BadRequest("ISBNs do not match.");

            // Turn genre ID into a Genre object
            Genre? genre = await _context.Genres.FindAsync(book.GenreId);

            // Check if genre doesn't exist and throw 404 error (or something nicer)
            if (genre == null) return NotFound("Oi! Cut it out! Stop messing with the genre ID!");

            // Assign genre to the book (previously null)
            book.Genre = genre;

            // Remove/ignore validation for the Genre property
            ModelState.Remove("Genre");

            // Check if model is valid
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.ISBN))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Redirect the user to the Index action (listing of models)
                return RedirectToAction(nameof(Index));
                return RedirectToAction("Index");
            }

            // Get a list of genres to populate a dropdown list and pass through using ViewData
            ViewData["GenreList"] = new SelectList(_context.Genres, "Id", "Name");

            // Display the Edit view with error messages
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.ISBN == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(string id)
        {
            return _context.Books.Any(e => e.ISBN == id);
        }

        // GET: Books/ViewAll
        public async Task<IActionResult> ViewAll()
        {
            // Get the list of books with their associated genres
            // This basically creates an INNER JOIN between Books and Genres
            return View(await _context.Books.Include(b => b.Genre).ToListAsync());
        }
    }
}
