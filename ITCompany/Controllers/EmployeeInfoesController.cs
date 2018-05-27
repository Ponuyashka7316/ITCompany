using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITCompany.Models;
using X.PagedList;

namespace ITCompany.Controllers
{
    public class EmployeeInfoesController : Controller
    {
        private readonly KvadroContext _context;

        public EmployeeInfoesController(KvadroContext context)
        {
            _context = context;
        }

        // GET: EmployeeInfoes
        public async Task<IActionResult> Index(int? page)
        {
            
            var kvadroContext = _context.EmployeeInfo.Include(e => e.Employee).Include(e => e.Project);
            var pageNumber = page ?? 1;
            var onePage = kvadroContext.ToPagedList(pageNumber, 25);
            ViewBag.OnePage = onePage;
            return View();
           // return View(await kvadroContext.ToListAsync());
            //return View(await PaginatedList<EmployeeInfo>.CreateAsync(kvadroContext.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: EmployeeInfoes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeInfo = await _context.EmployeeInfo
                .Include(e => e.Employee)
                .Include(e => e.Project)
                .SingleOrDefaultAsync(m => m.EmployeeId == id);
            if (employeeInfo == null)
            {
                return NotFound();
            }

            return View(employeeInfo);
        }

        // GET: EmployeeInfoes/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.SalaryInfo, "EmployeeId", "EmployeeId");
            ViewData["ProjectId"] = new SelectList(_context.ProjectInfo, "ProjectId", "Chief");
            return View();
        }

        // POST: EmployeeInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,EmployeeName,Adress,District,Experiance,Year,Language,Base,Comment,BonusAll,ProjectId")] EmployeeInfo employeeInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.SalaryInfo, "EmployeeId", "EmployeeId", employeeInfo.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.ProjectInfo, "ProjectId", "Chief", employeeInfo.ProjectId);
            return View(employeeInfo);
        }

        // GET: EmployeeInfoes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeInfo = await _context.EmployeeInfo.SingleOrDefaultAsync(m => m.EmployeeId == id);
            if (employeeInfo == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.SalaryInfo, "EmployeeId", "EmployeeId", employeeInfo.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.ProjectInfo, "ProjectId", "Chief", employeeInfo.ProjectId);
            return View(employeeInfo);
        }

        // POST: EmployeeInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("EmployeeId,EmployeeName,Adress,District,Experiance,Year,Language,Base,Comment,BonusAll,ProjectId")] EmployeeInfo employeeInfo)
        {
            if (id != employeeInfo.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeInfoExists(employeeInfo.EmployeeId))
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
            ViewData["EmployeeId"] = new SelectList(_context.SalaryInfo, "EmployeeId", "EmployeeId", employeeInfo.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.ProjectInfo, "ProjectId", "Chief", employeeInfo.ProjectId);
            return View(employeeInfo);
        }

        // GET: EmployeeInfoes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeInfo = await _context.EmployeeInfo
                .Include(e => e.Employee)
                .Include(e => e.Project)
                .SingleOrDefaultAsync(m => m.EmployeeId == id);
            if (employeeInfo == null)
            {
                return NotFound();
            }

            return View(employeeInfo);
        }

        // POST: EmployeeInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var employeeInfo = await _context.EmployeeInfo.SingleOrDefaultAsync(m => m.EmployeeId == id);
            _context.EmployeeInfo.Remove(employeeInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeInfoExists(long id)
        {
            return _context.EmployeeInfo.Any(e => e.EmployeeId == id);
        }
    }
}
