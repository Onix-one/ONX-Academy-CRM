using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IActionResult> Index(int? id)
        {
            try
            {
                var students = await _studentService.GetAllAsync();
                return View(id.HasValue 
                    ? _mapper.Map<IEnumerable<StudentViewModel>>(students.Where(_ => _.GroupId == id))
                    : _mapper.Map<IEnumerable<StudentViewModel>>(students));
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
    }
}
