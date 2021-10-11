using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ONX.CRM.BLL.Enums;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.ViewModel;
using ONX.CRM.ViewModel.Search;

namespace ONX.CRM.Controllers
{
    //[Authorize(Roles = "manager")]
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentsController> _logger;
        public StudentsController(IStudentService studentService, IGroupService groupService,
            IMapper mapper, ILogger<StudentsController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _studentService = studentService;
            _groupService = groupService;
        }

        public async Task<IActionResult> Index(string query, int courseId, StudentType type)
        {
            try
            {
                ViewBag.Courses = await _studentService.GetCoursesForDropdown();
                if (CheckingForSearchOrSorting(query, courseId, type.ToString()))
                {
                    ViewBag.Students = _mapper.Map<IEnumerable<StudentViewModel>>(await _studentService
                            .SearchStudents(query, courseId, type));
                    return View(new StudentViewModel { Search = new SearchStudentViewModel { Query = query } });
                }

                ViewBag.Students = _mapper.Map<IEnumerable<StudentViewModel>>(await _studentService.GetAllAsync());
                return View(new StudentViewModel { Search = new SearchStudentViewModel() });
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
                ViewBag.Groups = _mapper.Map<IEnumerable<GroupViewModel>>(await _groupService.GetAllAsync());
                return View(id.HasValue
                    ? _mapper.Map<StudentViewModel>(_studentService.GetEntityById(id.Value))
                    : new StudentViewModel());
            }
            catch (Exception e)
            {
                _logger.LogError($"Method didn't work({e.Message}), {e.TargetSite}, {DateTime.Now}");
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(StudentViewModel student)
        {
            try
            {
                ViewBag.Groups = _mapper.Map<IEnumerable<GroupViewModel>>(await _groupService.GetAllAsync());
                if (!ModelState.IsValid)
                    return View(student);

                if (student.Id != 0)
                    _studentService.Update(_mapper.Map<Student>(student));
                else
                    _studentService.Create(_mapper.Map<Student>(student));

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
                _studentService.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError($"Method didn't work({e.Message}), {e.TargetSite}, {DateTime.Now}");
                return RedirectToAction("Error", "Home");
            }
        }
        public IActionResult SearchStudents(StudentViewModel model)
        {
            try
            {
                if (CheckingForSearchOrSorting(model.Search.Query, model.Search.CourseId, model.Search.Type))
                {
                    return RedirectToAction("Index", "Students", new
                    {
                        query = model.Search.Query,
                        courseId = model.Search.CourseId,
                        type = model.Search.Type
                    }, null);
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError($"Method didn't work({e.Message}), {e.TargetSite}, {DateTime.Now}");
                return RedirectToAction("Error", "Home");
            }
        }
        private bool CheckingForSearchOrSorting(string query, int courseId, string type)
        {
            if (!string.IsNullOrEmpty(query) || courseId != 0 || !string.IsNullOrEmpty(type))
            {
                return true;
            }
            return false;
        }
    }
}
