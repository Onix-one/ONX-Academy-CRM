using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Hosting;

namespace ONX.CRM.Filters
{
    public class LocalExceptionFilter : Attribute, IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public LocalExceptionFilter(
            IWebHostEnvironment hostingEnvironment,
            IModelMetadataProvider modelMetadataProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            if (!_hostingEnvironment.IsDevelopment())
            {
                return;
            }

            var result = new ViewResult { ViewName = "Error" };
            result.ViewData = new ViewDataDictionary(_modelMetadataProvider,
                context.ModelState);
            result.ViewData.Add("Exception", context.Exception);
            result.ViewData.Add("ExceptionMessage", $"<b style='color:red;'>Message: {context.Exception} <br/><br/> StackTrace: {context.Exception}</b>");
            // TODO: Pass additional detailed data via ViewData
            context.Result = result;
        }
    }
}
