using LibraryManagement.Services.Contracts;
using LibraryManagement.ViewModels.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagement.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBookManagementService bookManagementService;
        private readonly ILoanManagementService loansManagementService;
        private readonly IAuthorManagementService authorManagementService;

        public BooksController(IBookManagementService bookManagementService, ILoanManagementService loansManagementService, IAuthorManagementService authorManagementService)
        {
            this.bookManagementService = bookManagementService;
            this.loansManagementService = loansManagementService;
            this.authorManagementService = authorManagementService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var books = await bookManagementService.GetAllBooksAsync();

            var loans = await loansManagementService.GetUserLoans(long.Parse(userId));

            var booksLoans = new List<GetBookLoanDataVM>();

            foreach (var book in books)
            {
                var bookLoanData = new GetBookLoanDataVM
                {
                    Id = book.Id,
                    Title = book.Title,
                    Genre = book.Genre,
                    IsTaken = book.IsTaken,
                    Authors = book.Authors
                };

                if (book.IsTaken)
                {
                    var bookLoan = loans.Items.FirstOrDefault(l => l.BookId == book.Id);

                    if (bookLoan != null)
                    {
                        bookLoanData.IsTakenByCurrentUser = true;
                        bookLoanData.BorrowerId = bookLoan.UserId;
                        bookLoanData.LoanId = bookLoan.Id;
                    }
                }

                booksLoans.Add(bookLoanData);
            }

            return View(booksLoans);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var authors = await authorManagementService.GetAllAuthorsAsync();

            var authorsSelectList = authors.Items.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.FirstName + " " + a.LastName
            });

            ViewBag.AuthorsSelectList = authorsSelectList;

            var viewModel = new CreateBookVM
            {
                AuthorIds = new HashSet<long>()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBookVM book)
        {
            if (ModelState.IsValid)
            {
                await bookManagementService.AddBookAsync(book);

                return RedirectToAction(nameof(Index));
            }

            var authors = await authorManagementService.GetAllAuthorsAsync();

            var authorsSelectList = authors.Items.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.FirstName + " " + a.LastName
            });

            ViewBag.AuthorsSelectList = authors;

            var viewModel = new CreateBookVM
            {
                AuthorIds = new HashSet<long>()
            };

            return View(book);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await bookManagementService.GetBookByIdAsync(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            var viewModel = new EditBookVM
            {
                Id = book.Id,
                Title = book.Title,
                Genre = book.Genre,
                AuthorIds = book.Authors.Select(a => a.AuthorId).ToHashSet()
            };

            var authors = await authorManagementService.GetAllAuthorsAsync();

            var authorsSelectList = authors.Items.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.FirstName + " " + a.LastName
            });

            ViewBag.AuthorsSelectList = authorsSelectList;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, EditBookVM viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await bookManagementService.UpdateBookAsync(viewModel);

                return RedirectToAction(nameof(Index));
            }

            var authors = await authorManagementService.GetAllAuthorsAsync();

            var authorsSelectList = authors.Items.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.FirstName + " " + a.LastName
            });

            ViewBag.AuthorsSelectList = authorsSelectList;

            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(long id)
        {
            if (id == default)
            {
                return NotFound();
            }

            var book = await bookManagementService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            await bookManagementService.DeleteBookAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName(nameof(BorrowBook))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrowBook(long bookId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            await loansManagementService.BorrowBook(bookId, long.Parse(userId));

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName(nameof(ReturnBook))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnBook(long loanId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            await loansManagementService.ReturnBook(loanId, long.Parse(userId));

            return RedirectToAction(nameof(Index));
        }
    }
}
