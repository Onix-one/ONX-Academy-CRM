using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.DAL.Models;
using ONX.CRM.Filters;
using ONX.CRM.ViewModel;

namespace ONX.CRM.Controllers
{
    [Authorize]
    [TypeFilter(typeof(LocalExceptionFilter))]
    public class ProfileController : BaseController
    {
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly IManagerService _managerService;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public ProfileController(IManagerService managerService, ITeacherService teacherService, IStudentService studentService,
            UserManager<User> userManager, IMapper mapper, IUserService userService)
        {
            _teacherService = teacherService;
            _managerService = managerService;
            _userManager = userManager;
            _userService = userService;
            _studentService = studentService;

            _mapper = mapper;
        }
        public virtual async Task<IActionResult> Index()
        {
            if (User.Identity != null)
            {
                var userId = _userManager.GetUserId(User);
                if (User.IsInRole("manager"))
                {
                    var manager = await _managerService.FindByUserIdAsync(userId);
                    return View(_mapper.Map<ProfileViewModel>(manager.FirstOrDefault()));
                }
                if (User.IsInRole("teacher"))
                {
                    var teacher = await _teacherService.FindByUserIdAsync(userId);
                    return View(_mapper.Map<ProfileViewModel>(teacher.FirstOrDefault()));
                }
                if (User.IsInRole("student"))
                {
                    var student = await _studentService.FindByUserIdAsync(userId);
                    return View(_mapper.Map<ProfileViewModel>(student.FirstOrDefault()));
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (User.IsInRole("manager"))
            {
                await _userService.Update(model.UserId, model.Email, model.FirstName, model.LastName);

                _managerService.Update(_mapper.Map<Manager>(model));
            }
            if (User.IsInRole("student"))
            {
                await _userService.Update(model.UserId, model.Email, model.FirstName, model.LastName);
                _studentService.Update(_mapper.Map<Student>(model));
            }
            if (User.IsInRole("teacher"))
            {
                await _userService.Update(model.UserId, model.Email, model.FirstName, model.LastName);
                _teacherService.Update(_mapper.Map<Teacher>(model));
            }
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> ChangePassword(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(new ChangePasswordViewModel { Id = user.Id, Email = user.Email });
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {

            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Profile");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User is not found.");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(int id, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                if (uploadedFile.Length > 600000)
                {
                    var userId = _userManager.GetUserId(User);
                    if (User.IsInRole("manager"))
                    {
                        ModelState.AddModelError(string.Empty, "The image file is too large");
                        var manager = await _managerService.FindByUserIdAsync(userId);
                        return View("Index", _mapper.Map<ProfileViewModel>(manager.FirstOrDefault()));
                    }
                    if (User.IsInRole("student"))
                    {
                        ModelState.AddModelError(string.Empty, "The image file is too large");
                        var student = await _studentService.FindByUserIdAsync(userId);
                        return View("Index", _mapper.Map<ProfileViewModel>(student.FirstOrDefault()));
                    }
                    if (User.IsInRole("teacher"))
                    {
                        ModelState.AddModelError(string.Empty, "The image file is too large");
                        var teacher = await _teacherService.FindByUserIdAsync(userId);
                        return View("Index", _mapper.Map<ProfileViewModel>(teacher.FirstOrDefault()));
                    }
                }
                //save file to DB (Person)
                await using var memoryStream = new MemoryStream();

                await uploadedFile.CopyToAsync(memoryStream);

                var content = memoryStream.ToArray();
                if (User.IsInRole("manager"))
                {
                    _managerService.SaveImage(id, content);
                }
                if (User.IsInRole("teacher"))
                {
                    _teacherService.SaveImage(id, content);
                }
                if (User.IsInRole("student"))
                {
                    _studentService.SaveImage(id, content);
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult DeleteImage(int id)
        {
            if (User.IsInRole("manager"))
                _managerService.DeleteImage(id);
           
            if (User.IsInRole("student"))
                _studentService.DeleteImage(id);

            if (User.IsInRole("teacher"))
                _teacherService.DeleteImage(id);

            return RedirectToAction("Index");
        }
    }
}
