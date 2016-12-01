using System.IO;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class GroupRenderer
    {
        private readonly IHtmlContent[] _contents;
        private readonly TextWriter _contentWriter;

        public GroupRenderer(IHtmlContent[] contents, TextWriter contentWriter)
        {
            _contents = contents;
            _contentWriter = contentWriter;
        }

        public void Render()
        {
            TagBuilder group = new TagBuilder("div") {TagRenderMode = TagRenderMode.StartTag};

            group.AddCssClass("form-group");
            _contentWriter.WriteLine(group.AsString());
            foreach(IHtmlContent content in _contents)
            {
                _contentWriter.WriteLine(content.AsString());
            }
            group.TagRenderMode = TagRenderMode.EndTag;
            _contentWriter.WriteLine(group.AsString());
        }
    }
}