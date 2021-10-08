using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.ViewModel;
using ONX.CRM.ViewModel.Search;

namespace ONX.CRM.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;
        private readonly ILogger<TeachersController> _logger;
        public TeachersController(ITeacherService teacherService,
            IMapper mapper, ILogger<TeachersController> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _teacherService = teacherService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string query)
        {
            try
            {
                if (!string.IsNullOrEmpty(query))
                {
                    ViewBag.Teachers = _mapper.Map<IEnumerable<TeacherViewModel>>(await _teacherService
                        .SearchTeachers(query));
                    return View(new TeacherViewModel { Search = new SearchTeacherViewModel { Query = query } });
                }
                ViewBag.Teachers = _mapper.Map<IEnumerable<TeacherViewModel>>(await _teacherService.GetAllAsync());
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError($"Method didn't work({e.Message}), {e.TargetSite}, {DateTime.Now}");
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            try
            {
                return View(id.HasValue
                    ? _mapper.Map<TeacherViewModel>(_teacherService.GetEntityById(id.Value))
                    : new TeacherViewModel());
            }
            catch (Exception e)
            {
                _logger.LogError($"Method didn't work({e.Message}), {e.TargetSite}, {DateTime.Now}");
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public IActionResult Edit(TeacherViewModel teacher)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(teacher);
                }
                if (teacher.Id != 0)
                    _teacherService.Update(_mapper.Map<Teacher>(teacher));
                else
                    _teacherService.Create(_mapper.Map<Teacher>(teacher));


                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError($"Method didn't work({e.Message}), {e.TargetSite}, {DateTime.Now}");
                return RedirectToAction("Error", "Home");
            }
        }
        public IActionResult Delete(int id)
        {
            try
            {
                _teacherService.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError($"Method didn't work({e.Message}), {e.TargetSite}, {DateTime.Now}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public IActionResult SearchTeachers(TeacherViewModel model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.Search.Query))
                {
                    return RedirectToAction("Index", "Teachers", new { query = model.Search.Query }, null);
                }
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
