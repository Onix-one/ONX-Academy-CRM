using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.Filters;
using ONX.CRM.ViewModel;
using ONX.CRM.ViewModel.Search;

namespace ONX.CRM.Controllers
{
    [TypeFilter(typeof(LocalExceptionFilter))]
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
        public async Task<IActionResult> Index(string query, string status)
        {
            try
            {
                if (CheckingForSearchOrSorting(query, status))
                {
                    
                    if (!string.IsNullOrEmpty(query))
                    {
                        ViewBag.Groups = _mapper.Map<IEnumerable<GroupViewModel>>(await _groupService
                            .GetGroupsByQuery(query));
                        return View(new GroupViewModel() { Search = new SearchGroupViewModel() { Query = query } });
                    }

                    if (!string.IsNullOrEmpty(status))
                    {
                        ViewBag.Groups = _mapper.Map<IEnumerable<GroupViewModel>>(await _groupService
                            .GetGroupsByStatus(status));
                        return View(new GroupViewModel { Search = new SearchGroupViewModel() });
                    }
                }

                ViewBag.Groups = _mapper.Map<IEnumerable<GroupViewModel>>(await _groupService.GetAllAsync());
                return View(new GroupViewModel { Search = new SearchGroupViewModel() });


            }
            catch (Exception exception)
            {
                _logger.LogError($"Method didn't work({exception.Message}), {exception.TargetSite}, {DateTime.Now}");
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                ViewBag.Teachers = _mapper.Map<IEnumerable<TeacherViewModel>>(await _teacherService.GetAllAsync());
                ViewBag.Courses = _mapper.Map<IEnumerable<CourseViewModel>>(await _courseService.GetAllAsync());
                return View(id.HasValue
                    ? _mapper.Map<GroupViewModel>(_groupService.GetEntityById(id.Value))
                    : new GroupViewModel());
            }
            catch (Exception exception)
            {
                _logger.LogError($"Method didn't work({exception.Message}), {exception.TargetSite}, {DateTime.Now}");
                throw;
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
            catch (Exception exception)
            {
                _logger.LogError($"Method didn't work({exception.Message}), {exception.TargetSite}, {DateTime.Now}");
                throw;
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
            catch (Exception exception)
            {
                _logger.LogError($"Method didn't work({exception.Message}), {exception.TargetSite}, {DateTime.Now}");
                throw;
            }
        }

        public IActionResult SearchGroups(GroupViewModel model)
        {
            try
            {
                if (CheckingForSearchOrSorting(model.Search.Query, model.Search.Status))
                {
                    return RedirectToAction("Index", "Groups", new
                    {
                        query = model.Search.Query,
                        status = model.Search.Status
                    }, null);
                }
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                _logger.LogError($"Method didn't work({exception.Message}), {exception.TargetSite}, {DateTime.Now}");
                throw;
            }
        }
        private bool CheckingForSearchOrSorting(string query, string status)
        {
            if (!string.IsNullOrEmpty(query) || !string.IsNullOrEmpty(status))
            {
                return true;
            }
            return false;
        }
    }
}
