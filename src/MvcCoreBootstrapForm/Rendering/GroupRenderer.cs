using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class GroupRenderer
    {
        private readonly GroupConfig _config;
        private readonly IHtmlHelper _htmlHelper;

        public GroupRenderer(GroupConfig config, IHtmlHelper htmlHelper)
        {
            _config = config;
            _htmlHelper = htmlHelper;
        }

        public void Render()
        {
            TagBuilder group = new TagBuilder("div") {TagRenderMode = TagRenderMode.StartTag};

            group.AddCssClass("form-group");
            _htmlHelper.ViewContext.Writer.WriteLine(group.AsString());
            foreach(IHtmlContent content in _config.Contents)
            {
                _htmlHelper.ViewContext.Writer.WriteLine(content.AsString());
            }
            group.TagRenderMode = TagRenderMode.EndTag;
            _htmlHelper.ViewContext.Writer.WriteLine(group.AsString());
        }
    }
}