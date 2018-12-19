using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrap.Rendering
{
    internal interface ITooltipRenderer
    {
        void Render(TagBuilder element, TooltipConfig config);
    }

    internal class TooltipRenderer : ITooltipRenderer
    {
        public void Render(TagBuilder element, TooltipConfig config)
        {
            if(config != null)
            {
                element.Attributes.Add("data-toggle", "tooltip");
                element.Attributes.Add("data-html", "true");
                element.Attributes.Add("title", config.Content);
                element.Attributes.Add("data-placement", config.Placement.ToString().ToLower());
                element.Attributes.Add("data-trigger", config.Trigger.ToString().ToLower());
                element.Attributes.Add("data-delay", $"{{ \"show\": {config.ShowDelay}, \"hide\": {config.HideDelay}  }}");
            }
        }
    }
}
