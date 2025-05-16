using LibraryManagement.Data.Entities;
using LibraryManagement.Repository.Contracts;
using LibraryManagement.Services.Contracts;
using LibraryManagement.ViewModels.Authors;

namespace LibraryManagement.Services
{
    public class AuthorManagementService : IAuthorManagementService
    {
        private readonly IRepositoryBase<Author, long> authorsRepository;

        public AuthorManagementService(IRepositoryBase<Author, long> authorsRepository)
        {
            this.authorsRepository = authorsRepository;
        }
        public async Task<GetAuthorsVM> GetAllAuthorsAsync()
        {
            var authors = await authorsRepository.GetAllAsync();
            var authorsToReturn = authors.Select(author => new GetAuthorVM
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            }).ToList();

            var authorsVM = new GetAuthorsVM
            {
                Items = authorsToReturn
            };

            return authorsVM;
        }

        public async Task<GetAuthorVM> GetAuthorByIdAsync(long id)
        {
            var author = await authorsRepository.GetByIdAsync(id);

            return new GetAuthorVM
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            };
        }

        public async Task AddAuthorAsync(CreateAuthorVM author)
        {
            var newAuthor = new Author
            {
                FirstName = author.FirstName,
                LastName = author.LastName
            };

            await authorsRepository.AddAsync(newAuthor);
        }


        public async Task UpdateAuthorAsync(EditAuthorVM author)
        {
            await authorsRepository.UpdateAsync(new Author
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            });
        }

        public async Task DeleteAuthorAsync(long authorId)
        {
            var authorToDelete = await authorsRepository.GetByIdAsync(authorId);

            await authorsRepository.DeleteAsync(authorToDelete);
        }
    }
}
