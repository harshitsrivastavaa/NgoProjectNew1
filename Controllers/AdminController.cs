using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NgoProjectNew1.Models;

namespace NgoProjectNew1.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly NgoDbContext _context;

        public AdminController(NgoDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        
        public async Task<IActionResult> Index()
        {
            var ngoDbContext = _context.NgoRegMembers.Include(n => n.Role);
            return View(await ngoDbContext.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ngoRegMember = await _context.NgoRegMembers
                .Include(n => n.Role)
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (ngoRegMember == null)
            {
                return NotFound();
            }

            return View(ngoRegMember);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.NgoUserRoles, "RoleId", "RoleId");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,Name,Gender,Address,ContactNo,RoleId,IsActive,AdminComments,CreatedBy,CreatedDate,UpdatedDate,ModifiedBy,Username,Password")] NgoRegMember ngoRegMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ngoRegMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.NgoUserRoles, "RoleId", "RoleId", ngoRegMember.RoleId);
            return View(ngoRegMember);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ngoRegMember = await _context.NgoRegMembers.FindAsync(id);
            if (ngoRegMember == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.NgoUserRoles, "RoleId", "RoleId", ngoRegMember.RoleId);
            return View(ngoRegMember);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,Name,Gender,Address,ContactNo,RoleId,IsActive,AdminComments,CreatedBy,CreatedDate,UpdatedDate,ModifiedBy,Username,Password")] NgoRegMember ngoRegMember)
        {
            if (id != ngoRegMember.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ngoRegMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NgoRegMemberExists(ngoRegMember.MemberId))
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
            ViewData["RoleId"] = new SelectList(_context.NgoUserRoles, "RoleId", "RoleId", ngoRegMember.RoleId);
            return View(ngoRegMember);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ngoRegMember = await _context.NgoRegMembers
                .Include(n => n.Role)
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (ngoRegMember == null)
            {
                return NotFound();
            }

            return View(ngoRegMember);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ngoRegMember = await _context.NgoRegMembers.FindAsync(id);
            _context.NgoRegMembers.Remove(ngoRegMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NgoRegMemberExists(int id)
        {
            return _context.NgoRegMembers.Any(e => e.MemberId == id);
        }
    }
}
