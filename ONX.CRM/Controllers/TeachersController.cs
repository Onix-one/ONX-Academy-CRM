using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.Extensions;
using ONX.CRM.Filters;
using ONX.CRM.ViewModel;
using ONX.CRM.ViewModel.PageInfo;
using ONX.CRM.ViewModel.Search;

namespace ONX.CRM.Controllers
{
    [TypeFilter(typeof(LocalExceptionFilter))]
    public class TeachersController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;
        private readonly ILogger<TeachersController> _logger;
        private readonly PageInfoViewModel _pageInfo;
        public TeachersController(ITeacherService teacherService, IMapper mapper,
            ILogger<TeachersController> logger, PageInfoViewModel pageInfo)
        {
            _pageInfo = pageInfo;
            _logger = logger;
            _mapper = mapper;
            _teacherService = teacherService;
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
        public IActionResult Edit(TeacherViewModel teacher)
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
        public IActionResult Delete(int id)
        {
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
    }
}
