using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ONX.CRM.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly ILogger<GlobalExceptionFilter> _logger;
        public GlobalExceptionFilter(
        IWebHostEnvironment hostingEnvironment,
        IModelMetadataProvider modelMetadataProvider,
        ILogger<GlobalExceptionFilter> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            _logger.LogError($"Method didn't work({context.Exception.Message}), " +
                             $"{context.Exception.TargetSite}, {DateTime.Now}");
            if (!_hostingEnvironment.IsDevelopment())
            {
                return;
            }
            var result = new ViewResult
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(_modelMetadataProvider,
                    context.ModelState) { { "Exception", context.Exception } }
            };
            // TODO: Pass additional detailed data via ViewData
            context.Result = result;
        }
    }
}
