using Microsoft.AspNetCore.Mvc.ModelBinding;
using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrapAlert.Config
{
    internal class AlertConfig : ConfigBase
    {
        public string Text { get; set; }
        public bool Dismissable { get; set; }
        public ModelStateDictionary ModelState { get; set; }
    }
}
