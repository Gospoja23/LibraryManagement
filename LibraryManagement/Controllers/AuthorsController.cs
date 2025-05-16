using LibraryManagement.Services.Contracts;
using LibraryManagement.ViewModels.Authors;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorManagementService authorManagementService;

        public AuthorsController(IAuthorManagementService authorManagementService)
        {
            this.authorManagementService = authorManagementService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await authorManagementService.GetAllAuthorsAsync());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAuthorVM author)
        {
            if (ModelState.IsValid)
            {
                await authorManagementService.AddAuthorAsync(author);

                return RedirectToAction(nameof(Index));
            }

            return View(author);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(long id)
        {
            if (id == default)
            {
                return NotFound();
            }

            var author = await authorManagementService.GetAuthorByIdAsync(id);

            if (author is null)
            {
                return NotFound();
            }

            var editAuthor = new EditAuthorVM
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            };

            return View(editAuthor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditAuthorVM author)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authorManagementService.UpdateAuthorAsync(author);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return View(author);
                }
            }

            return View(author);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(long id)
        {
            if (id == default)
            {
                return NotFound();
            }

            var author = await authorManagementService.GetAuthorByIdAsync(id);

            if (author is null)
            {
                return NotFound();
            }

            return View(author);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            await authorManagementService.DeleteAuthorAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
