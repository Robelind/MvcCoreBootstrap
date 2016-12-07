using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using MvcCoreBootstrapForm.Extensions;

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
            //IHtmlContentBuilder builder = new HtmlContentBuilder();
            TagBuilder group = new TagBuilder("div") {TagRenderMode = TagRenderMode.StartTag};
            TextWriter writer = new StringWriter();

            group.AddCssClass("form-group");
            group.WriteTo(writer, HtmlEncoder.Default);
            if(_label != null)
            {
                TagBuilder label = new TagBuilder("label");

                label.InnerHtml.Append(_label);
                //group.InnerHtml.AppendHtml(label);
                label.WriteTo(writer, HtmlEncoder.Default);
            }
            //builder.AppendHtml(group);
            foreach(IHtmlContent content in _contents)
            {
                //builder.AppendHtml(content);
                content.WriteTo(writer, HtmlEncoder.Default);
            }
            group.TagRenderMode = TagRenderMode.EndTag;
            group.WriteTo(writer, HtmlEncoder.Default);
            //builder.AppendHtml(group);

            return(new HtmlString(writer.ToString()));
        }
    }
}