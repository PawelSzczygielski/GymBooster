using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GymBooster.Common.Utils
{
    public class DateTimeModelBinder : IModelBinder
    {
        public static readonly Type[] SupportedTypes = new[] { typeof(DateTime), typeof(DateTime?) };

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            if (!SupportedTypes.Contains(bindingContext.ModelType))
                return Task.CompletedTask;

            string modelName = GetModelName(bindingContext);

            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            string dateToParse = valueProviderResult.FirstValue;
            if (string.IsNullOrWhiteSpace(dateToParse))
                return Task.CompletedTask;

            DateTime? parsedDateTime = ParseDate(bindingContext, dateToParse);
            bindingContext.Result = ModelBindingResult.Success(parsedDateTime);
            return Task.CompletedTask;
        }

        private DateTime? ParseDate(ModelBindingContext bindingContext, string dateToParse)
        {
            DateTimeModelBinderAttribute attribute = GetDateTimeModelBinderAttribute(bindingContext);
            string dateFormat = attribute?.DateFormat;

            if (string.IsNullOrWhiteSpace(dateFormat))
                return dateToParse.ParseDateTime();

            return dateToParse.ParseDateTime(new[] { dateFormat });
        }

        private DateTimeModelBinderAttribute GetDateTimeModelBinderAttribute(ModelBindingContext bindingContext)
        {
            string modelName = GetModelName(bindingContext);

            var paramDescriptor = bindingContext.ActionContext.ActionDescriptor.Parameters
                .Where(p => p.ParameterType == typeof(DateTime?))
                .Where(p =>
                {
                    var paramModelName = p.BindingInfo?.BinderModelName ?? p.Name;
                    return paramModelName == modelName;
                })
                .FirstOrDefault();

            var ctrlParamDescriptor = paramDescriptor as ControllerParameterDescriptor;
            if (ctrlParamDescriptor == null)
                return null;

            var attribute = ctrlParamDescriptor.ParameterInfo.GetCustomAttributes(typeof(DateTimeModelBinderAttribute), false).FirstOrDefault();
            return (DateTimeModelBinderAttribute)attribute;
        }

        private string GetModelName(ModelBindingContext bindingContext)
        {
            if (!string.IsNullOrEmpty(bindingContext.BinderModelName))
            {
                return bindingContext.BinderModelName;
            }

            return bindingContext.ModelName;
        }
    }
}
