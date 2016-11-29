using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class RadioButtonsRenderer<TModel, TResult> : ControlRenderer<TModel, bool>
    {
        public IHtmlContent Render(RadioButtonsConfig<TModel, TResult> config, IHtmlHelper<TModel> htmlHelper)
        {
            TextWriter markup = new StringWriter();

            foreach(RadioButtonConfig<TModel, TResult> radioButtonConfig in config.RadioButtons)
            {
                TagBuilder container = new TagBuilder("div");
                TagBuilder label = new TagBuilder("label");
                TagBuilder radioButton = this.TagBuilderFromHtmlContent(htmlHelper
                        .RadioButtonFor(radioButtonConfig.Expression, radioButtonConfig.Value), false);

                if(radioButtonConfig.Disabled)
                {
                    container.AddCssClass("disabled");
                    radioButton.Attributes.Add("disabled", null);
                }
                container.AddCssClass(config.Horizontal ? "radio-inline" : "radio");
                container.InnerHtml.AppendHtml(label);
                this.AddCssClasses(container, config.CssClasses);
                label.InnerHtml.AppendHtml(radioButton);
                label.AddCssClass("control-label");
                if(!string.IsNullOrEmpty(radioButtonConfig.Label))
                {
                    label.InnerHtml.Append(radioButtonConfig.Label);
                }

                container.WriteTo(markup, HtmlEncoder.Default);
            }

            return(new HtmlString(markup.ToString()));
        }
    }
}