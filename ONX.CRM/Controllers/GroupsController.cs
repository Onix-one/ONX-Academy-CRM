using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.ViewModel;

namespace ONX.CRM.Controllers
{
    //[Authorize(Roles = "manager")]
    public class GroupsController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        private readonly ILogger<GroupsController> _logger;
        private readonly ITeacherService _teacherService;
        private readonly IEntityService<Course> _courseService;
        public GroupsController(IGroupService groupService, ITeacherService teacherService, 
            IEntityService<Course> courseService, IMapper mapper, ILogger<GroupsController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _groupService = groupService;
            _teacherService = teacherService;
            _courseService = courseService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(_mapper.Map<IEnumerable<GroupViewModel>>(await _groupService.GetAllAsync()));
            }
            catch (Exception e)
            {
                _logger.LogError($"Method didn't work({e.Message}), {e.TargetSite}, {DateTime.Now}");
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                ViewBag.Teachers = _mapper.Map<IEnumerable<TeacherViewModel>>( await _teacherService.GetAllAsync());
                ViewBag.Courses = _mapper.Map<IEnumerable<CourseViewModel>>(await _courseService.GetAllAsync());
                return View(id.HasValue
                    ? _mapper.Map<GroupViewModel>(_groupService.GetEntityById(id.Value))
                    : new GroupViewModel());
            }
            catch (Exception e)
            {
                _logger.LogError($"Method didn't work({e.Message}), {e.TargetSite}, {DateTime.Now}");
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(GroupViewModel group)
        {
            try
            {
                ViewBag.Teachers = _mapper.Map<IEnumerable<TeacherViewModel>>(await _teacherService.GetAllAsync());
                ViewBag.Courses = _mapper.Map<IEnumerable<CourseViewModel>>(await _courseService.GetAllAsync());
                if (!ModelState.IsValid)
                    return View(group);

                if (group.Id != 0)
                    _groupService.Update(_mapper.Map<Group>(group));
                else
                    _groupService.Create(_mapper.Map<Group>(group));

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError($"Method didn't work({e.Message}), {e.TargetSite}, {DateTime.Now}");
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                _groupService.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError($"Method didn't work({e.Message}), {e.TargetSite}, {DateTime.Now}");
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
