using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using ONX.CRM.Controllers;

namespace ONX.CRM.Filters
{
    public class ViewBagActionFilter : ActionFilterAttribute
    {
        public ViewBagActionFilter() { }
        public override void OnResultExecuting(ResultExecutingContext context)
        {

            if (context.Controller is Controller contextController)
            {
                contextController.ViewBag.Theme = BaseController.Theme ?? "dark-theme";
            }
            base.OnResultExecuting(context);
        }
    }
}
