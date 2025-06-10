using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using Expense_Tracker.Filters;

namespace Expense_Tracker.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<CategoryController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            _logger.LogInformation($"Getting categories for user {userId}");
            return _context.Categories != null ?
                        View(await _context.Categories.Where(c => c.UserId == userId).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Categories' is null.");
        }

        // GET: Category/AddOrEdit
        [ValidateUserAccess]
        public IActionResult AddOrEdit(int id = 0)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Challenge();
            }

            if (id == 0)
            {
                return View(new Category { Type = "Expense", UserId = userId });
            }

            var category = _context.Categories
                .FirstOrDefault(c => c.CategoryId == id && c.UserId == userId);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/AddOrEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateUserAccess]
        public async Task<IActionResult> AddOrEdit([Bind("CategoryId,Title,Icon,Type")] Category category)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                {
                    _logger.LogError("User ID is null when trying to save category");
                    return Challenge();
                }

                _logger.LogInformation($"Processing category - Title: {category.Title}, Type: {category.Type}, UserId: {userId}");

                if (!ModelState.IsValid)
                {
                    foreach (var modelStateEntry in ModelState.Values)
                    {
                        foreach (var error in modelStateEntry.Errors)
                        {
                            _logger.LogError($"Model validation error: {error.ErrorMessage}");
                        }
                    }
                    return View(category);
                }

                if (category.CategoryId == 0)
                {
                    _logger.LogInformation("Adding new category");
                    category.UserId = userId;
                    _context.Categories.Add(category);
                }
                else
                {
                    var existingCategory = await _context.Categories
                        .FirstOrDefaultAsync(c => c.CategoryId == category.CategoryId && c.UserId == userId);

                    if (existingCategory == null)
                    {
                        _logger.LogWarning($"Category {category.CategoryId} not found or doesn't belong to user {userId}");
                        return NotFound();
                    }

                    existingCategory.Title = category.Title;
                    existingCategory.Icon = category.Icon;
                    existingCategory.Type = category.Type;
                    _context.Update(existingCategory);
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation("Category saved successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving category: {ex.Message}");
                ModelState.AddModelError("", "An error occurred while saving the category.");
                return View(category);
            }
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ValidateUserAccess]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var category = await _context.Categories
                    .FirstOrDefaultAsync(c => c.CategoryId == id && c.UserId == userId);

                if (category == null)
                {
                    return NotFound();
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting category {id}: {ex.Message}");
                return Problem($"Error deleting category {id}");
            }
        }
    }
}
