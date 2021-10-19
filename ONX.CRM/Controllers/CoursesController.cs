using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.Filters;
using ONX.CRM.ViewModel;

namespace ONX.CRM.Controllers
{
    [TypeFilter(typeof(LocalExceptionFilter))]
    public class CoursesController : Controller
    {
        private readonly ISpecializationService _specializationService;
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;
        private readonly ILogger<CoursesController> _logger;
        public CoursesController(ICourseService courseService,
            ISpecializationService specializationService, 
            IMapper mapper, ILogger<CoursesController> logger)
        {
            _specializationService = specializationService;
            _courseService = courseService;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IActionResult> Index(int id)
        {
            var specializationTitle = await _specializationService.GetEntityByIdAsync(id);
            ViewBag.SpecializationTitle = specializationTitle.Title;
            var courses = await _courseService.GetAllAsync();
            return View(_mapper
                .Map<IEnumerable<CourseViewModel>>(courses.Where(_ => _.SpecializationId == id)).ToList());
        }
    }
}
