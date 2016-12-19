using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;
using MvcCoreBootstrapForm.Extensions;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class RowRenderer
    {
        private readonly RowConfig _config;

        public RowRenderer(RowConfig config)
        {
            _config = config;
        }

        public IHtmlContent Render()
        {
            TagBuilder row = new TagBuilder("div") {TagRenderMode = TagRenderMode.StartTag};
            //IList<TagBuilder> columns = new List<TagBuilder>();
            TextWriter writer = new StringWriter();

            row.AddCssClass("row");
            row.WriteTo(writer, HtmlEncoder.Default);
            for(var i = 0; i < _config.Columns.Count; i++)
            {
                ColumnConfig columnConfig = _config.Columns.ElementAt(i);
                TagBuilder column = new TagBuilder("div");

                column.AddCssClass(columnConfig.Width.CssClass());
                column.InnerHtml.AppendHtml(columnConfig.Content);
                column.WriteTo(writer, HtmlEncoder.Default);
            }

            /*foreach(ColumnConfig columnConfig in _config.Columns)
            {
                TagBuilder column = new TagBuilder("div") {TagRenderMode = TagRenderMode.StartTag};

                column.AddCssClass(columnConfig.Width.CssClass());
                columns.Add(column);
                row.WriteTo(writer, HtmlEncoder.Default);
            }

            //_contentWriter.WriteLine(row.AsString());
            for(var i = 0; i < columns.Count; i++)
            {
                TagBuilder column = columns.ElementAt(i);

                _contentWriter.WriteLine(column.AsString());
                _contentWriter.WriteLine(_config.Columns.ElementAt(i).Content.AsString());
                column.TagRenderMode = TagRenderMode.EndTag;
                _contentWriter.WriteLine(column.AsString());
            }*/
            row.TagRenderMode = TagRenderMode.EndTag;
            row.WriteTo(writer, HtmlEncoder.Default);
            //_contentWriter.WriteLine(row.AsString());
            return(new HtmlString(writer.ToString()));
        }
    }
}