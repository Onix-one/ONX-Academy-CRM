using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.Filters;
using ONX.CRM.ViewModel;

namespace ONX.CRM.Controllers
{
    [TypeFilter(typeof(LocalExceptionFilter))]
    public class SpecializationsController : Controller
    {
        private readonly IEntityService<Specialization> _specializationService;
        private readonly IMapper _mapper;

        public SpecializationsController(IEntityService<Specialization> specializationService, IMapper mapper)
        {
            _specializationService = specializationService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View(_mapper.
                Map<IEnumerable<SpecializationViewModel>>(await _specializationService.GetAllAsync()).ToList());
        }
    }
}
