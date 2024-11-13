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
    public class SessionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sessions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sessions.Include(s => s.EndTime).Include(s => s.SessionType).Include(s => s.StartTime);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sessions/Details/1/2024-11-12
        [HttpGet("Sessions/Details/{sessionTypeId}/{dateString}")]
        public async Task<IActionResult> Details(int sessionTypeId, string dateString)
        {
            // Convert date string to DateTime
            DateTime sessionDate;
            if (!DateTime.TryParse(dateString, out sessionDate))
                return BadRequest("Invalid session date, must be 'yyyy-mm-dd'.");

            // Check SessionType exists
            var sessionType = await _context.SessionTypes.FindAsync(sessionTypeId);
            if (sessionType == null)
                return NotFound("Session type not found.");

            // Find the Session
            var session = await _context.Sessions
                .Include(s => s.SessionType)
                .Include(s => s.StartTime)
                .Include(s => s.EndTime)
                .FirstOrDefaultAsync(s => s.SessionTypeId == sessionTypeId && s.Date == sessionDate);
            
            // Check if Session exists
            if (session == null)
                return NotFound("Session not found");

            // Return view
            return View(session);
        }

        private void PopulateViewData(DateTime? selectedStartTime, DateTime? selectedEndTime, int? selectedSessionTypeId)
        {
            ViewData["StartTimeId"] = new SelectList(_context.Timeslots, "Time", "TimeFormatted", selectedStartTime);
            ViewData["EndTimeId"] = new SelectList(_context.Timeslots, "Time", "TimeFormatted", selectedEndTime);
            ViewData["SessionTypeId"] = new SelectList(_context.SessionTypes, "Id", "Name", selectedSessionTypeId);
        }

        private void PopulateViewData()
        {
            PopulateViewData(null, null, null);
        }

        // GET: Sessions/Create
        public IActionResult Create()
        {
            //ViewData["StartTimeId"] = new SelectList(_context.Timeslots, "Time", "TimeFormatted");
            //ViewData["EndTimeId"] = new SelectList(_context.Timeslots, "Time", "TimeFormatted");
            //ViewData["SessionTypeId"] = new SelectList(_context.SessionTypes, "Id", "Name");
            PopulateViewData();
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,SessionTypeId,StartTimeId,EndTimeId,Status")] Session session)
        {
            // Check for existing session (primary key already exists - date + session type)
            if (SessionExists(session.Date, session.SessionTypeId))
            {
                //return BadRequest("session exists...");
                ModelState.AddModelError("Date", "Session already exists (same date and session type).");
            }

            if (ModelState.IsValid)
            {
                _context.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //ViewData["EndTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", session.EndTimeId);
            //ViewData["SessionTypeId"] = new SelectList(_context.SessionTypes, "Id", "Name", session.SessionTypeId);
            //ViewData["StartTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", session.StartTimeId);
            PopulateViewData(session.StartTimeId, session.EndTimeId, session.SessionTypeId);
            return View(session);
        }

        // GET: Sessions/Edit/5
        public async Task<IActionResult> Edit(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            //ViewData["EndTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", session.EndTimeId);
            //ViewData["SessionTypeId"] = new SelectList(_context.SessionTypes, "Id", "Name", session.SessionTypeId);
            //ViewData["StartTimeId"] = new SelectList(_context.Timeslots, "Time", "Time", session.StartTimeId);
            PopulateViewData(session.StartTimeId, session.EndTimeId, session.SessionTypeId);
            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime id, [Bind("Date,SessionTypeId,StartTimeId,EndTimeId,Status")] Session session)
        {
            if (id != session.Date)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(session);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(session.Date, session.SessionTypeId))
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

            PopulateViewData(session.StartTimeId, session.EndTimeId, session.SessionTypeId);
            return View(session);
        }

        // GET: Sessions/Delete/5
        public async Task<IActionResult> Delete(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.EndTime)
                .Include(s => s.SessionType)
                .Include(s => s.StartTime)
                .FirstOrDefaultAsync(m => m.Date == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session != null)
            {
                _context.Sessions.Remove(session);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionExists(DateTime date, int sessionTypeId)
        {
            return _context.Sessions.Any(s => s.Date == date && s.SessionTypeId == sessionTypeId);
        }
    }
}
