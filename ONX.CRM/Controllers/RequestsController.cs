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
    public class RequestsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RequestsController> _logger;
        private readonly IStudentService _studentService;
        private readonly IEntityService<Course> _courseService;
        private readonly IStudentRequestService _studentRequestService;
        private readonly RequestsListViewModel _requests;
        private readonly IGroupService _groupService;
        private readonly IEntityService<Specialization> _specializationService;

        public RequestsController(IStudentRequestService studentRequestsService,
            IStudentService studentService, IEntityService<Course> courseService,
            IMapper mapper, ILogger<RequestsController> logger, RequestsListViewModel requests, 
            IGroupService groupService, IEntityService<Specialization> specializationService)
        {
            _specializationService = specializationService;
            _groupService = groupService;
            _requests = requests;
            _requests.RequestsList = new List<StudentRequestViewModel>();
            _mapper = mapper;
            _logger = logger;
            _courseService = courseService;
            _studentService = studentService;
            _studentRequestService = studentRequestsService;
        }
        [HttpGet]
        //[Authorize(Roles = "manager")]
        public async Task<IActionResult> Index(int? id)
        {
            try
            {
                var requests = await _studentRequestService.GetAllAsync();
                var coursesForDropMenu = new Dictionary<int, string>();
                var studentRequests = requests.ToList();
                foreach (var course in studentRequests.Select(_ => _.Course))
                {
                    coursesForDropMenu[course.Id] = course.Title;
                }
                ViewBag.Courses = coursesForDropMenu;
                if (id.HasValue && id != 0)
                {
                    foreach (var request in _mapper.Map<IEnumerable<StudentRequestViewModel>>(studentRequests.Where(_ => _.CourseId == id)))
                    {
                        _requests.RequestsList.Add(request);
                    }

                    var groups = _mapper.Map<IEnumerable<GroupViewModel>>(await _groupService.GetAllAsync()).Where(g => g.CourseId == id);

                    if (groups.Count() != 0)
                    {
                        ViewBag.AllRequestsShow = false;
                        ViewBag.Groups = groups;
                        ViewBag.GroupExists = true;
                        ViewBag.CheckingAllowed = true;
                        return View(_requests);
                    }
                    else
                    {
                        ViewBag.AllRequestsShow = false;
                        ViewBag.CheckingAllowed = true;
                        ViewBag.GroupExists = false;
                        return View(_requests);
                    }
                }
                foreach (var request in _mapper.Map<IEnumerable<StudentRequestViewModel>>(studentRequests))
                {
                    _requests.RequestsList.Add(request);
                }

                ViewBag.AllRequestsShow = true;
                ViewBag.CheckingAllowed = false;
                return View(_requests);
            }
            catch (Exception e)
            {
                _logger.LogError($"Method didn't work({e.Message}), {e.TargetSite}, {DateTime.Now}");
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Index(RequestsListViewModel model)
        {
            try
            {
                var requests = _mapper.Map<IEnumerable<StudentRequest>>(model.RequestsList.Where(r => r.Selected));
                if (requests.Count() == 0)
                {
                    return RedirectToAction("Index", "Requests", new { id = model.RequestsList.FirstOrDefault().CourseId});
                }


                _studentRequestService.AssignRequestToGroups(requests, model.GroupId);

                return RedirectToAction("RequestsForCourses", "Requests");
            }
            catch (Exception e)
            {
                _logger.LogError($"Method didn't work({e.Message}), {e.TargetSite}, {DateTime.Now}");
                return RedirectToAction("Error", "Home");
            }

        }
        [HttpGet]
        //[Authorize(Roles = "manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                ViewBag.Courses = _mapper.Map<IEnumerable<CourseViewModel>>(await _courseService.GetAllAsync());
                ViewBag.Students = _mapper.Map<IEnumerable<StudentViewModel>>(await _studentService.GetAllAsync());
                return View(id.HasValue
                    ? _mapper.Map<StudentRequestViewModel>(_studentRequestService.GetEntityById(id.Value))
                    : new StudentRequestViewModel() { Created = null });
            }
            catch (Exception e)
            {
                _logger.LogError($"Method didn't work({e.Message}), {e.TargetSite}, {DateTime.Now}");
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(StudentRequestViewModel studentRequest)
        {
            try
            {
                ViewBag.CoursesSelected = false;
                ViewBag.Courses = _mapper.Map<IEnumerable<CourseViewModel>>(await _courseService.GetAllAsync());
                ViewBag.Students = _mapper.Map<IEnumerable<StudentViewModel>>(await _studentService.GetAllAsync());
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
                    _studentRequestService.Update(_mapper.Map<StudentRequest>(studentRequest));
                else
                    _studentRequestService.Create(_mapper.Map<StudentRequest>(studentRequest));

                if (User.IsInRole("manager"))
                {
                    return RedirectToAction("RequestsForCourses");
                }
                return RedirectToAction("Index", "Specializations");
            }
            catch (Exception e)
            {
                _logger.LogError($"Method didn't work({e.Message}), {e.TargetSite}, {DateTime.Now}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditFromQuery(int id)
        {
            try
            {
                
                var course = _courseService.GetEntityById(id);
                ViewBag.SpecializationId = course.SpecializationId;


                ViewBag.SpecializationTitle = _specializationService.GetEntityById(course.SpecializationId).Title;


                ViewBag.CourseName = course.Title;
                ViewBag.Courses = _mapper.Map<IEnumerable<CourseViewModel>>(await _courseService.GetAllAsync());
                ViewBag.Students = _mapper.Map<IEnumerable<StudentViewModel>>(await _studentService.GetAllAsync());
                return View("Edit", new StudentRequestViewModel() { Created = null, CourseId = id });
            }
            catch (Exception e)
            {
                _logger.LogError($"Method didn't work({e.Message}), {e.TargetSite}, {DateTime.Now}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        //[Authorize(Roles = "manager")]
        public IActionResult Delete(int id)
        {
            try
            {
                _studentRequestService.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError($"Method didn't work({e.Message}), {e.TargetSite}, {DateTime.Now}");
                return RedirectToAction("Error", "Home");
            }
        }


        [HttpGet]
        public async Task<IActionResult> RequestsForCourses(int? id)
        {
            try
            {
                var requests = _mapper.Map<IEnumerable<StudentRequestViewModel>>(await _studentRequestService.GetAllAsync()).ToList();
                var courses = _mapper.Map<IEnumerable<CourseViewModel>>(await _courseService.GetAllAsync()).ToList();
                var allRequestsCount = 0;
                foreach (var course in courses)
                {
                    var count = requests.Count(r => r.CourseId == course.Id);
                    allRequestsCount += count;
                    course.RequestsCount = count;
                }
                ViewBag.RequestsCount = allRequestsCount;
                return View(courses.Where(c => c.RequestsCount != 0));
            }
            catch (Exception e)
            {
                _logger.LogError($"Method didn't work({e.Message}), {e.TargetSite}, {DateTime.Now}");
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
