using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GenAITech.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace GenAITech.Models
{
    public class GenAIsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

    
        public GenAIsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: GenAIs
        public async Task<IActionResult> Index()
        {
              return _context.GenAI != null ? 
                          View(await _context.GenAI.OrderByDescending(x=>x.Like).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.GenAI'  is null.");
        }

        // GET: GenAIs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GenAI == null)
            {
                return NotFound();
            }

            var genAI = await _context.GenAI
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genAI == null)
            {
                return NotFound();
            }

            return View(genAI);
        }

        // GET: GenAIs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GenAIs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GenAI genAI)
        {
            if (ModelState.IsValid)
            {
                if (genAI.ImageFile != null)
                {
                    var uniqueFileName = GetUniqueFileName(genAI.ImageFile.FileName);
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    genAI.ImageFilename = uniqueFileName;
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await genAI.ImageFile.CopyToAsync(fileStream);
                    }
                }
                genAI.AnchorLink = "Home/GenAiSites#Bard_tumbinal";
                _context.Add(genAI);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genAI);
        }
        private string GetUniqueFileName(string fileName)
{
    fileName = Path.GetFileName(fileName);
    return Path.GetFileNameWithoutExtension(fileName)
           + "_"
           + Guid.NewGuid().ToString().Substring(0, 4)
           + Path.GetExtension(fileName);
}

        // GET: GenAIs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GenAI == null)
            {
                return NotFound();
            }

            var genAI = await _context.GenAI.FindAsync(id);
            if (genAI == null)
            {
                return NotFound();
            }
            return View(genAI);
        }

        // POST: GenAIs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GenAI genAI)
        {
            if (id != genAI.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (genAI.ImageFile != null)
                    {
                        var uniqueFileName = GetUniqueFileName(genAI.ImageFile.FileName);
                        var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        var filePath = Path.Combine(uploads, uniqueFileName);
                        genAI.ImageFilename = uniqueFileName;
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await genAI.ImageFile.CopyToAsync(fileStream);
                        }
                    }
                    genAI.AnchorLink = "Home/GenAiSites#Bard_tumbinal";
                    _context.Update(genAI);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenAIExists(genAI.Id))
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
            return View(genAI);
        }

        // GET: GenAIs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GenAI == null)
            {
                return NotFound();
            }

            var genAI = await _context.GenAI
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genAI == null)
            {
                return NotFound();
            }

            return View(genAI);
        }

        // POST: GenAIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GenAI == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GenAI'  is null.");
            }
            var genAI = await _context.GenAI.FindAsync(id);
            if (genAI != null)
            {
                _context.GenAI.Remove(genAI);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> IncrementLike(int id, int newLikeCount)
        {
            var genAI = await _context.GenAI.FindAsync(id);

            if (genAI == null)
            {
                return NotFound();
            }

            genAI.Like = newLikeCount;
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        private bool GenAIExists(int id)
        {
          return (_context.GenAI?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
