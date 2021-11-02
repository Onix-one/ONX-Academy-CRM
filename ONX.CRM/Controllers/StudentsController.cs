using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.DAL.Models;
using ONX.CRM.Extensions;
using ONX.CRM.Filters;
using ONX.CRM.ViewModel;
using ONX.CRM.ViewModel.PageInfo;
using ONX.CRM.ViewModel.Search;

namespace ONX.CRM.Controllers
{
    [TypeFilter(typeof(LocalExceptionFilter))]
    //[Authorize(Roles = "manager")]
    public class StudentsController : BaseController
    {
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentsController> _logger;
        private readonly PageInfoViewModel _pageInfo;
        public StudentsController(IStudentService studentService, IGroupService groupService,
            IMapper mapper, ILogger<StudentsController> logger, PageInfoViewModel pageInfo)
        {
            _pageInfo = pageInfo;
            _mapper = mapper;
            _logger = logger;
            _studentService = studentService;
            _groupService = groupService;
        }
        public async Task<IActionResult> Index(string query, int courseId, int type, int pageSize, int pageNumber)
        {
            PageInfoViewModel pageInfo;
            int skip;
            int take;
            ViewBag.Courses = await _studentService.GetActiveCoursesIdTitle();
            if (CheckingForSearchOrSorting(query, courseId, type))
            {
                pageInfo = _pageInfo.CheckingPageInfo(pageSize, pageNumber, await _studentService
                        .GetNumberOfStudentsByParameters(query, courseId, type));
                skip = (pageInfo.PageNumber - 1) * pageInfo.PageSize;
                take = pageInfo.PageSize;

                var listOfStudentsAfterSearchByQuery = _mapper.Map<IEnumerable<StudentViewModel>>(await _studentService
                     .GetListOfStudentsByParameters(query, courseId, type, skip, take)).ToList();

                ViewBag.Students = listOfStudentsAfterSearchByQuery;

                return View(new StudentViewModel
                {
                    Search = new SearchStudentViewModel
                    {
                        Query = query,
                        CourseId = courseId,
                        Type = type
                    },
                    PageInfo = pageInfo
                });
            }

            pageInfo = _pageInfo.CheckingPageInfo(pageSize, pageNumber, await _studentService.GetNumberOfStudents());
            skip = (pageInfo.PageNumber - 1) * pageInfo.PageSize;
            take = pageInfo.PageSize;

            var allStudents = _mapper
                .Map<IEnumerable<StudentViewModel>>(await _studentService.GetStudentsWithSkipAndTakeAsync(skip, take)).ToList();

            ViewBag.Students = allStudents;

            return View(new StudentViewModel
            {
                Search = new SearchStudentViewModel(),
                PageInfo = pageInfo
            });
        }
        [HttpPost]
        public async Task<IActionResult> Pagination(StudentViewModel model)
        {
            return RedirectToAction("Index", "Students", new
            {
                pageSize = model.PageInfo.PageSize,
                pageNumber = model.PageInfo.PageNumber,
                query = model.Search.Query,
                courseId = model.Search.CourseId,
                type = model.Search.Type
            }, null);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Groups = _mapper.Map<IEnumerable<GroupViewModel>>(await _groupService.GetAllAsync());
            return View(id.HasValue
                ? _mapper.Map<StudentViewModel>(await _studentService.GetEntityByIdAsync(id.Value))
                : new StudentViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(StudentViewModel student)
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
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _studentService.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult SearchStudents(StudentViewModel model)
        {
            return RedirectToAction("Index", "Students", new
            {
                query = model.Search.Query,
                courseId = model.Search.CourseId,
                type = model.Search.Type
            }, null);
        }
        private bool CheckingForSearchOrSorting(string query, int courseId, int type)
        {
            if (!string.IsNullOrEmpty(query) || courseId != 0 || type != 0)
            {
                return true;
            }
            return false;
        }
    }
}
