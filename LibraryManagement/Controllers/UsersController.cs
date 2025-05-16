using LibraryManagement.Services.Contracts;
using LibraryManagement.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    //аннотация ,которая позволяет разрешить доступ к методам контроллера только определенным пользователям(в данном случае с ролью админ)
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserManagementService userManagementService;

        public UsersController(IUserManagementService userManagementService)
        {
            this.userManagementService = userManagementService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await userManagementService.GetAllUsersAsync());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateUserVM user)
        {
            if (ModelState.IsValid)
            {
                await userManagementService.AddUserAsync(user);

                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(long id)
        {
            if (id == default)
            {
                return NotFound();
            }

            var user = await userManagementService.GetUserByIdAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            var editUserVM = new EditUserVM
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password
            };

            return View(editUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserVM user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await userManagementService.UpdateUserAsync(user);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return View(user);
                }
            }

            return View(user);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(long id)
        {
            if (id == default)
            {
                return NotFound();
            }

            var user = await userManagementService.GetUserByIdAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            await userManagementService.DeleteUserAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
