

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;

namespace ProjeCoreOrnekOzellikler.Filters
{
    public class ExceptionFilterAttirbute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {

            var result = new ViewResult { ViewName = "Errorr" };
            var model = new EmptyModelMetadataProvider();
            result.ViewData = new ViewDataDictionary(model, context.ModelState);
            result.ViewData.Add("commonFilter", context.Exception);
            context.Result = result;
            context.ExceptionHandled = true;

        }
    }
}
