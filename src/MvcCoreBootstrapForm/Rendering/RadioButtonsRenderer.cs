using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class RadioButtonsRenderer<TModel> : ControlRenderer<TModel, bool>
    {
        public IHtmlContent Render(RadioButtonsConfig<TModel> config, IHtmlHelper<TModel> htmlHelper)
        {
            TextWriter markup = new StringWriter();

            foreach(RadioButtonConfig<TModel> radioButtonConfig in config.RadioButtons)
            {
                TagBuilder container = config.Horizontal ? null : new TagBuilder("div");
                TagBuilder label = new TagBuilder("label");
                TagBuilder radioButton = this.TagBuilderFromHtmlContent(htmlHelper
                    .RadioButtonFor(radioButtonConfig.Expression, radioButtonConfig.Value), false);

                if(container != null)
                {
                    if(radioButtonConfig.Disabled)
                    {
                        container.AddCssClass("disabled");
                        radioButton.Attributes.Add("disabled", null);
                    }
                    container.AddCssClass("radio");
                    container.InnerHtml.AppendHtml(label);
                    this.AddCssClasses(container, config.CssClasses);
                }
                label.InnerHtml.AppendHtml(radioButton);
                if(config.Horizontal)
                {
                    label.AddCssClass("radio-inline");
                }
                if(!string.IsNullOrEmpty(radioButtonConfig.Label))
                {
                    label.InnerHtml.Append(radioButtonConfig.Label);
                }

                if(container != null)
                {
                    container.WriteTo(markup, HtmlEncoder.Default);
                }
                else
                {
                    label.WriteTo(markup, HtmlEncoder.Default);
                }
            }

            return(new HtmlString(markup.ToString()));
        }
    }
}