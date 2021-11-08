using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.DAL.Models;
using ONX.CRM.Filters;
using ONX.CRM.ViewModel;

namespace ONX.CRM.Controllers
{
    [TypeFilter(typeof(LocalExceptionFilter))]
    public class LessonsController : BaseController
    {
        private readonly ILessonService _lessonService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
       
        public LessonsController(ILessonService lessonService, IMapper mapper,
            IUserService userService)
        {
            _mapper = mapper;
            _lessonService = lessonService;
            _userService = userService;
           
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(int? id, int groupId)
        {
            return View(id.HasValue
                ? _mapper.Map<LessonViewModel>(await _lessonService.GetEntityByIdAsync(id.Value))
                : new LessonViewModel{GroupId = groupId});
        }
        [HttpPost]
        public async Task<IActionResult> Edit(LessonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Id != 0)
            {
                _lessonService.Update(_mapper.Map<Lesson>(model));
            }
            else
            {
                _lessonService.Create(_mapper.Map<Lesson>(model));
            }
            return RedirectToAction("GroupInfo", "Teachers", new { id = model.GroupId});
        }
        public async Task<IActionResult> Delete(int id)
        {
           var groupId =  _lessonService.GetEntityByIdAsync(id).Result.GroupId;
            _lessonService.Delete(id);
            return RedirectToAction("GroupInfo", "Teachers", new { id = groupId });
        }
    }
}
