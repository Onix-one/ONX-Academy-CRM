using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.DAL.Models;
using ONX.CRM.Filters;
using ONX.CRM.ViewModel;

namespace ONX.CRM.Controllers
{
    [TypeFilter(typeof(LocalExceptionFilter))]
    public class ManagersController : BaseController
    {
        private readonly IManagerService _managerService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        public ManagersController(IManagerService managerService, IMapper mapper,
            IUserService userService, INotificationService notificationService)
        {
            _mapper = mapper;
            _managerService = managerService;
            _userService = userService;
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            ViewBag.Managers = _mapper.Map<IEnumerable<ManagerViewModel>>(await _managerService.GetAllAsync());
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return View(id.HasValue
                ? _mapper.Map<ManagerViewModel>(await _managerService.GetEntityByIdAsync(id.Value))
                : new ManagerViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ManagerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Id != 0)
            {
                await _userService.Update(model.UserId, model.Email, model.FirstName, model.LastName);
                _managerService.Update(_mapper.Map<Manager>(model));
            }
            else
            {
                if (await _managerService.CheckIfManagerExists(model.Email))
                {
                    ModelState.AddModelError("Email", "A user with this address is already registered.");
                    return View(model);
                }
                User user = new User
                {
                    Email = model.Email,
                    UserName = model.Email,
                    LastName = model.LastName,
                    FirstName = model.FirstName
                };
                await _userService.CreateAsync(user, "manager");
                model.UserId = user.Id;
                _managerService.Create(_mapper.Map<Manager>(model));
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var manager = await _managerService.GetEntityByIdAsync(id);
            await _userService.Delete(manager.UserId);

            _managerService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
