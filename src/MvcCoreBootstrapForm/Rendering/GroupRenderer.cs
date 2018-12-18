using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class GroupRenderer
    {
        private readonly IEnumerable<IHtmlContent> _contents;
        private readonly string _label;

        public GroupRenderer(IEnumerable<IHtmlContent> contents, string label)
        {
            _contents = contents;
            _label = label;
        }

        public IHtmlContent Render()
        {
            TagBuilder group = new TagBuilder("div") {TagRenderMode = TagRenderMode.StartTag};
            TextWriter writer = new StringWriter();

            group.AddCssClass("form-group");
            group.WriteTo(writer, HtmlEncoder.Default);
            if(_label != null)
            {
                TagBuilder label = new TagBuilder("label");

                label.InnerHtml.Append(_label);
                label.WriteTo(writer, HtmlEncoder.Default);
            }
            
            foreach(IHtmlContent content in _contents)
            {
                content.WriteTo(writer, HtmlEncoder.Default);
            }
            group.TagRenderMode = TagRenderMode.EndTag;
            group.WriteTo(writer, HtmlEncoder.Default);

            return(new HtmlString(writer.ToString()));
        }
    }
}