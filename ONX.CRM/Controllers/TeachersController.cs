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
    public class TeachersController : BaseController
    {
        private readonly ITeacherService _teacherService;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        private readonly ILogger<TeachersController> _logger;
        private readonly PageInfoViewModel _pageInfo;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IStudentService _studentService;
        private readonly ILessonService _lessonService;
        public TeachersController(ITeacherService teacherService, IMapper mapper,
            ILogger<TeachersController> logger, IUserService userService,
            UserManager<User> userManager, PageInfoViewModel pageInfo,
            IStudentService studentService, ILessonService lessonService, IGroupService groupService)
        {
            _pageInfo = pageInfo;
            _logger = logger;
            _mapper = mapper;
            _teacherService = teacherService;
            _userService = userService;
            _userManager = userManager;
            _groupService = groupService;
            _studentService = studentService;
            _lessonService = lessonService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string query, int pageSize, int pageNumber)
        {
            PageInfoViewModel pageInfo;
            int skip;
            int take;
            if (!string.IsNullOrEmpty(query))
            {
                pageInfo = _pageInfo.CheckingPageInfo(pageSize, pageNumber, await _teacherService
                    .GetNumberOfTeachersByQuery(query));
                skip = (pageInfo.PageNumber - 1) * pageInfo.PageSize;
                take = pageInfo.PageSize;
                ViewBag.Teachers = _mapper.Map<IEnumerable<TeacherViewModel>>(await _teacherService
                    .GetTeachersByQuery(query, skip, take));
                return View(new TeacherViewModel
                {
                    Search = new SearchTeacherViewModel
                    {
                        Query = query
                    },
                    PageInfo = pageInfo
                });
            }
            pageInfo = _pageInfo.CheckingPageInfo(pageSize, pageNumber, await _teacherService
                .GetNumberOfTeachers());
            skip = (pageInfo.PageNumber - 1) * pageInfo.PageSize;
            take = pageInfo.PageSize;
            ViewBag.Teachers = _mapper
                .Map<IEnumerable<TeacherViewModel>>(await _teacherService.GetTeachersWithSkipAndTakeAsync(skip, take));
            return View(new TeacherViewModel
            {
                Search = new SearchTeacherViewModel(),
                PageInfo = pageInfo
            });
        }
        [HttpPost]
        public async Task<IActionResult> Pagination(TeacherViewModel model)
        {
            return RedirectToAction("Index", "Teachers", new
            {
                pageSize = model.PageInfo.PageSize,
                pageNumber = model.PageInfo.PageNumber,
                query = model.Search.Query
            }, null);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return View(id.HasValue
                ? _mapper.Map<TeacherViewModel>(await _teacherService.GetEntityByIdAsync(id.Value))
                : new TeacherViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TeacherViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Id != 0)
            {
                await _userService.Update(model.UserId, model.Email, model.FirstName, model.LastName);
                _teacherService.Update(_mapper.Map<Teacher>(model));
            }
            else
            {
                if (await _teacherService.CheckIfManagerExists(model.Email))
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
                await _userService.CreateAsync(user, "teacher");

                model.UserId = user.Id;
                _teacherService.Create(_mapper.Map<Teacher>(model));
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _teacherService.GetEntityByIdAsync(id);
            await _userService.Delete(teacher.UserId);

            _teacherService.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult SearchTeachers(TeacherViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Search.Query))
            {
                return RedirectToAction("Index", "Teachers", new
                {
                    query = model.Search.Query
                }, null);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Groups()
        {
            if (User.Identity != null)
            {
                var userId = _userManager.GetUserId(User);
                var teacher = _teacherService.FindByUserIdAsync(userId).Result.FirstOrDefault();
                if (teacher != null)
                {
                    ViewBag.Groups = _mapper
                        .Map<IEnumerable<GroupViewModel>>(await _groupService.GetGroupsByTeacherId(teacher.Id));
                    return View(new GroupViewModel
                    {
                        Search = new SearchGroupViewModel(),
                        PageInfo = new PageInfoViewModel()
                    });
                }
            }
            ViewBag.Groups = new List<GroupViewModel>().AsEnumerable();
            return View(new GroupViewModel
            {
                Search = new SearchGroupViewModel(),
                PageInfo = new PageInfoViewModel()
            });
        }
        public async Task<IActionResult> GroupInfo(int id, string error)
        {
            ViewBag.Students = _mapper
                .Map<IEnumerable<StudentViewModel>>(await _studentService.GetStudentsByGroupId(id));
            ViewBag.Lessons = _mapper
                .Map<IEnumerable<LessonViewModel>>(await _lessonService.GetLessonsByGroupId(id));

            ViewBag.CurrentGroup = _mapper
                .Map<GroupViewModel>(await _groupService.GetEntityByIdAsync(id));

            if (error != null)
            {
                ModelState.AddModelError(string.Empty, "The pdf file is too large");
                return View(new LessonViewModel { GroupId = id });
            }
            return View(new LessonViewModel{GroupId = id});
        }
        public IActionResult Home()
        {
            return RedirectToAction("Groups");
        }
    }
}
