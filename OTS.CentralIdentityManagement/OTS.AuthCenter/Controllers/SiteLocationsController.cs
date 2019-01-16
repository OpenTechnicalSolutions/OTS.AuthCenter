using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OTS.AuthCenter.Data;
using OTS.AuthCenter.Models;

namespace OTS.AuthCenter.Controllers
{
    public class SiteLocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SiteLocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SiteLocations
        [Authorize("Administrator,AuthCenterAdministrator,SiteLocationManager")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.siteLocations.ToListAsync());
        }

        // GET: SiteLocations/Details/5
        [Authorize("Administrator,AuthCenterAdministrator,SiteLocationManager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteLocation = await _context.siteLocations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siteLocation == null)
            {
                return NotFound();
            }

            return View(siteLocation);
        }

        // GET: SiteLocations/Create
        [Authorize("Administrator,AuthCenterAdministrator,SiteLocationManager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: SiteLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("Administrator,AuthCenterAdministrator,SiteLocationManager")]
        public async Task<IActionResult> Create([Bind("Id,SiteName,MainOfficeAddress,MainOfficeAddress2,SiteDescription")] SiteLocation siteLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siteLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(siteLocation);
        }

        // GET: SiteLocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteLocation = await _context.siteLocations.FindAsync(id);
            if (siteLocation == null)
            {
                return NotFound();
            }
            return View(siteLocation);
        }

        // POST: SiteLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("Administrator,AuthCenterAdministrator,SiteLocationManager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SiteName,MainOfficeAddress,MainOfficeAddress2,SiteDescription")] SiteLocation siteLocation)
        {
            if (id != siteLocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siteLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteLocationExists(siteLocation.Id))
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
            return View(siteLocation);
        }

        // GET: SiteLocations/Delete/5
        [Authorize("Administrator,AuthCenterAdministrator,SiteLocationManager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteLocation = await _context.siteLocations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siteLocation == null)
            {
                return NotFound();
            }

            return View(siteLocation);
        }

        // POST: SiteLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize("Administrator,AuthCenterAdministrator,SiteLocationManager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var siteLocation = await _context.siteLocations.FindAsync(id);
            _context.siteLocations.Remove(siteLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiteLocationExists(int id)
        {
            return _context.siteLocations.Any(e => e.Id == id);
        }
    }
}
