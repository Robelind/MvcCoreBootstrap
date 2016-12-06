using Microsoft.AspNetCore.Mvc.ModelBinding;
using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrapForm.Config
{
    internal class ValidationSummaryConfig : ConfigBase
    {
        public ModelStateDictionary ModelState { get; set; }
    }
}
