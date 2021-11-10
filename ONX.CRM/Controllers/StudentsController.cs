using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly ILessonService _lessonService;
        public StudentsController(IStudentService studentService, IGroupService groupService,
            IMapper mapper, IUserService userService,
            ILogger<StudentsController> logger, PageInfoViewModel pageInfo,
            UserManager<User> userManager, ILessonService lessonService)
        {
            _pageInfo = pageInfo;
            _mapper = mapper;
            _logger = logger;
            _studentService = studentService;
            _groupService = groupService;
            _userService = userService;
            _userManager = userManager;
            _lessonService = lessonService;
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
        public async Task<IActionResult> Edit(StudentViewModel model)
        {
            ViewBag.Groups = _mapper.Map<IEnumerable<GroupViewModel>>(await _groupService.GetAllAsync());
            if (!ModelState.IsValid)
                return View(model);

            if (model.Id != 0)
            {
                await _userService.Update(model.UserId, model.Email, model.FirstName, model.LastName);
                _studentService.Update(_mapper.Map<Student>(model));
            }
            else
            {
                if (await _studentService.CheckIfManagerExists(model.Email))
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
                await _userService.CreateAsync(user, "student");

                model.UserId = user.Id;
                _studentService.Create(_mapper.Map<Student>(model));
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetEntityByIdAsync(id);
            await _userService.Delete(student.UserId);

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
        public async Task<IActionResult> Schedule()
        {
            if (User.Identity != null)
            {
                var userId = _userManager.GetUserId(User);
                var student = _studentService.FindByUserIdAsync(userId).Result.FirstOrDefault();
                if (student != null)
                {
                    ViewBag.Lessons = _mapper
                        .Map<IEnumerable<LessonViewModel>>(await _lessonService.GetLessonsByGroupId(student.GroupId.Value));

                    return View(new LessonViewModel ());
                }
            }
            
            return View();
        }
        public async Task<IActionResult> VideoMaterials()
        {
            if (User.Identity != null)
            {
                var userId = _userManager.GetUserId(User);
                var student = _studentService.FindByUserIdAsync(userId).Result.FirstOrDefault();
                if (student != null)
                {
                    ViewBag.Lessons = _mapper
                        .Map<IEnumerable<LessonViewModel>>(await _lessonService.GetLessonsByGroupId(student.GroupId.Value));

                    return View(new LessonViewModel());
                }
            }

            return View();
        }
        public IActionResult Home()
        {
            return RedirectToAction("Schedule");
        }
    }
}
