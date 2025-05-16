using LibraryManagement.Data.Entities;
using LibraryManagement.Repository.Contracts;
using LibraryManagement.Services.Contracts;
using LibraryManagement.ViewModels.Books;

namespace LibraryManagement.Services
{
    public class BookManagementService : IBookManagementService
    {
        private readonly IRepositoryBase<Book, long> booksRepository;
        private readonly IRepositoryBase<BookAuthor, long> bookAuthorRepository;

        public BookManagementService(
            IRepositoryBase<Book, long> booksRepository,
            IRepositoryBase<BookAuthor, long> bookAuthorRepository)
        {
            this.booksRepository = booksRepository;
            this.bookAuthorRepository = bookAuthorRepository;
        }

        public async Task<IEnumerable<GetBookVM>> GetAllBooksAsync()
        {
            var books = await booksRepository.GetAllAsync();
            return books.Select(book => new GetBookVM
            {
                Id = book.Id,
                Title = book.Title,
                Genre = book.Genre,
                IsTaken = book.IsTaken,
                Authors = book.BookAuthors.Select(ba => new GetBookAuthorVM
                {
                    AuthorId = ba.AuthorId,
                    AuthorFullName = ba.Author.FirstName + " " + ba.Author.LastName
                }).ToList(),
            });
        }

        public async Task<GetBookVM> GetBookByIdAsync(long id)
        {
            var book = await booksRepository.GetByIdAsync(id);
            return new GetBookVM
            {
                Id = book.Id,
                Title = book.Title,
                Genre = book.Genre,
                IsTaken = book.IsTaken,
                Authors = book.BookAuthors.Select(ba => new GetBookAuthorVM
                {
                    AuthorId = ba.AuthorId,
                    AuthorFullName = ba.Author.FirstName + " " + ba.Author.LastName
                }).ToList(),
            };
        }

        public async Task AddBookAsync(CreateBookVM book)
        {
            var newBook = new Book
            {
                Title = book.Title,
                Genre = book.Genre,
            };

            await booksRepository.AddAsync(newBook);

            var bookAuthors = book.AuthorIds.Select(authorId => new BookAuthor
            {
                BookId = newBook.Id,
                AuthorId = authorId
            }).ToList();

            await bookAuthorRepository.AddRangeAsync(bookAuthors);
        }

        public async Task UpdateBookAsync(EditBookVM book)
        {
            var existingBook = await booksRepository.GetByIdAsync(book.Id);

            if (existingBook == null) throw new KeyNotFoundException($"Book with ID {book.Id} not found.");

            existingBook.Title = book.Title;
            existingBook.Genre = book.Genre;

            await booksRepository.UpdateAsync(existingBook);

            var existingAuthorIds = existingBook.BookAuthors.Select(ba => ba.AuthorId).ToList();

            var authorsIdsToAdd = book.AuthorIds.Except(existingAuthorIds).ToList();

            var authorsIdsToRemove = existingAuthorIds.Except(book.AuthorIds).ToList();

            if (authorsIdsToRemove.Any())
            {
                var bookAuthorsToRemove = existingBook.BookAuthors
                    .Where(x => authorsIdsToRemove.Contains(x.AuthorId))
                    .ToList();

                await bookAuthorRepository.DeleteRangeAsync(bookAuthorsToRemove);
            }

            if (authorsIdsToAdd.Any())
            {
                var newBookAuthors = authorsIdsToAdd.Select(authorId => new BookAuthor
                {
                    BookId = book.Id,
                    AuthorId = authorId
                }).ToList();

                await bookAuthorRepository.AddRangeAsync(newBookAuthors);
            }
        }

        public async Task DeleteBookAsync(long bookId)
        {
            var book = await booksRepository.GetByIdAsync(bookId);

            var bookAuthors = book.BookAuthors.Select(ba => new BookAuthor
            {
                BookId = book.Id,
                AuthorId = ba.AuthorId
            }).ToList();

            await booksRepository.DeleteAsync(book);
        }
    }
}
