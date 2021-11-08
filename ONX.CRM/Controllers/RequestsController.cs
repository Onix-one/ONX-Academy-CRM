using System;
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
    public class RequestsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RequestsController> _logger;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private readonly IStudentRequestService _studentRequestService;
        private readonly IGroupService _groupService;
        private readonly ISpecializationService _specializationService;
        private readonly PageInfoViewModel _pageInfo;

        public RequestsController(IStudentRequestService studentRequestsService,
            IStudentService studentService, ICourseService courseService,
            IMapper mapper, ILogger<RequestsController> logger, IGroupService groupService,
            ISpecializationService specializationService, PageInfoViewModel pageInfo)
        {
            _pageInfo = pageInfo;
            _specializationService = specializationService;
            _groupService = groupService;
            _mapper = mapper;
            _logger = logger;
            _courseService = courseService;
            _studentService = studentService;
            _studentRequestService = studentRequestsService;
        }
        [HttpGet]
        //[Authorize(Roles = "manager")]
        public async Task<IActionResult> Index(int courseId, int pageSize, int pageNumber)
        {

            PageInfoViewModel pageInfo;
            int skip;
            int take;
            //Получаем список курсов по которым есть заявки
            ViewBag.CoursesList = await _studentRequestService.GetActiveCoursesIdTitle();

            if (courseId != 0)
            {
                ViewBag.AllRequestsShow = false;

                pageInfo = _pageInfo.CheckingPageInfo(pageSize, pageNumber,
                    await _studentRequestService.GetNumberOfRequestsByCourseId(courseId));
                skip = (pageInfo.PageNumber - 1) * pageInfo.PageSize;
                take = pageInfo.PageSize;
                var requestsList = _mapper.Map<IEnumerable<StudentRequestViewModel>>(await _studentRequestService
                    .GetRequestsByCourseId(courseId, skip, take));

                //Если у заявок отфильтрованых по фильтру есть группа то разрешаем добавить студента в эту группу 
                var groups = _mapper.Map<IEnumerable<GroupViewModel>>(await _groupService.GetAllAsync())
                    .Where(g => g.CourseId == courseId);
                if (groups.Count() != 0)
                {
                    ViewBag.GroupExists = true;
                    ViewBag.CheckingAllowed = true;
                    ViewBag.Groups = groups;
                    return View(new RequestsListViewModel
                    {
                        Search = new SearchRequestViewModel { CourseId = courseId },
                        PageInfo = pageInfo,
                        RequestsList = requestsList.ToList()
                    });
                }
                //Иначе предложим создать новую группу
                ViewBag.GroupExists = false;
                ViewBag.CheckingAllowed = false;
                return View(new RequestsListViewModel
                {
                    Search = new SearchRequestViewModel { CourseId = courseId },
                    PageInfo = pageInfo,
                    RequestsList = requestsList.ToList()
                });
            }
            //Запуск Index без фильтрации по курсам
            pageInfo = _pageInfo.CheckingPageInfo(pageSize, pageNumber, await _studentRequestService.GetNumberOfRequests());
            skip = (pageInfo.PageNumber - 1) * pageInfo.PageSize;
            take = pageInfo.PageSize;
            var requests = _mapper.Map<IEnumerable<StudentRequestViewModel>>(await _studentRequestService.GetRequestsWithSkipAndTakeAsync(skip, take));

            ViewBag.AllRequestsShow = true;
            ViewBag.CheckingAllowed = false;

            return View(new RequestsListViewModel()
            {
                Search = new SearchRequestViewModel(),
                PageInfo = pageInfo,
                RequestsList = requests.ToList()
            });
        }
        [HttpPost]
        public async Task<IActionResult> Index(RequestsListViewModel model)
        {
            if (model.RequestsList != null)
            {
                var requests = _mapper.Map<IEnumerable<StudentRequest>>(model.RequestsList.Where(r => r.Selected));
                foreach (var request in requests)
                {
                    if (await _studentService.CheckIfManagerExists(request.Email))
                    {
                        ModelState.AddModelError("Email", "A user with this address is already registered.");
                        return View(model);
                    }
                }
                _studentRequestService.AssignRequestToGroups(requests, model.GroupId);
                //Если заявку была одна и мы добавили ее в группу то преадресовываем на все заявки
                if (model.RequestsList.Count == 1)
                {
                    return RedirectToAction("Index", "Requests");
                }
            }
            if (model.PageInfo == null)
            {
                return RedirectToAction("Index", "Requests", new
                {
                    courseId = model.Search.CourseId
                }, null);
            }
            return RedirectToAction("Index", "Requests", new
            {
                pageSize = model.PageInfo.PageSize,
                pageNumber = model.PageInfo.PageNumber,
                courseId = model.Search.CourseId,
            }, null);
        }
        [HttpGet]
        //[Authorize(Roles = "manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Courses = _mapper.Map<IEnumerable<CourseViewModel>>(await _courseService.GetAllAsync());
            ViewBag.Students = _mapper.Map<IEnumerable<StudentViewModel>>(await _studentService.GetAllAsync());
            return View(id.HasValue
                ? _mapper.Map<StudentRequestViewModel>(await _studentRequestService.GetEntityByIdAsync(id.Value))
                : new StudentRequestViewModel() { Created = null });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(StudentRequestViewModel studentRequest)
        {
            ViewBag.CoursesSelected = false;
            ViewBag.Courses = _mapper.Map<IEnumerable<CourseViewModel>>(await _courseService.GetAllAsync());
            ViewBag.Students = _mapper.Map<IEnumerable<StudentViewModel>>(await _studentService.GetAllAsync());
            var course = await _courseService.GetEntityByIdAsync(studentRequest.CourseId);
            ViewBag.CourseName = course.Title;
            if (studentRequest.Created == null)
            {
                studentRequest.Created = DateTime.Now;
            }
            if (!ModelState.IsValid)
                if (studentRequest.Id == 0)
                    return View(new StudentRequestViewModel());
                else
                    return View(studentRequest);
            if (studentRequest.Id != 0)
            {
                if (await _studentService.CheckIfManagerExists(studentRequest.Email))
                {
                    ModelState.AddModelError("Email", "A user with this address is already registered.");
                    return View(studentRequest);
                }
                _studentRequestService.Update(_mapper.Map<StudentRequest>(studentRequest));
            }
            else
            {
                if (await _studentService.CheckIfManagerExists(studentRequest.Email))
                {
                    ModelState.AddModelError("Email", "A user with this address is already registered.");
                    return View(studentRequest);
                }
                _studentRequestService.Create(_mapper.Map<StudentRequest>(studentRequest));
            }

            if (User.IsInRole("manager") || User.IsInRole("admin"))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Specializations");
        }
        [HttpGet]
        public async Task<IActionResult> EditFromQuery(int id)
        {
            var course = await _courseService.GetEntityByIdAsync(id);
            ViewBag.CourseName = course.Title;
            ViewBag.SpecializationId = course.SpecializationId;
            var specialization = await _specializationService.GetEntityByIdAsync(course.SpecializationId);
            ViewBag.SpecializationTitle = specialization.Title;
           
            ViewBag.Courses = _mapper.Map<IEnumerable<CourseViewModel>>(await _courseService.GetAllAsync());
            ViewBag.Students = _mapper.Map<IEnumerable<StudentViewModel>>(await _studentService.GetAllAsync());
            return View("Edit", new StudentRequestViewModel() { Created = null, CourseId = id });
        }
        [HttpPost]
        //[Authorize(Roles = "manager")]
        public IActionResult Delete(int id)
        {
            _studentRequestService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
