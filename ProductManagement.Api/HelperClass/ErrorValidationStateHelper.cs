
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace ProductManagement.Api.HelperClass
{
    public static class ErrorValidationStateHelper
    {
        public static string GetErrors(this ModelStateDictionary ModelState)
        {
            return $"One or several validation errors were found: " + string.Concat(ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
        }
    }
}
