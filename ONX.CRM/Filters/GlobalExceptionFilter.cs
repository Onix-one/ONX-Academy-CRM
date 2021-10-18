using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Hosting;

namespace ONX.CRM.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly IModelMetadataProvider _modelMetadataProvider;

    public GlobalExceptionFilter(
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

        var result = new ViewResult
        {
            ViewName = "Error",
            ViewData = new ViewDataDictionary(_modelMetadataProvider,
                context.ModelState) {{"Exception", context.Exception}}
        };
        // TODO: Pass additional detailed data via ViewData
        context.Result = result;
    }
    }
}
