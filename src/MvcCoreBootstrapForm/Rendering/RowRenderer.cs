using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class RowRenderer
    {
        private readonly RowConfig _config;
        private readonly IHtmlHelper _htmlHelper;

        public RowRenderer(RowConfig config, IHtmlHelper htmlHelper)
        {
            _config = config;
            _htmlHelper = htmlHelper;
        }

        public void Render()
        {
            TagBuilder row = new TagBuilder("div") {TagRenderMode = TagRenderMode.StartTag};
            IList<TagBuilder> columns = new List<TagBuilder>();

             row.AddCssClass("row");
            foreach(ColumnConfig columnConfig in _config.Columns)
            {
                TagBuilder column = new TagBuilder("div") {TagRenderMode = TagRenderMode.StartTag};

                column.AddCssClass(columnConfig.Width.CssClass());
                columns.Add(column);
            }

            _htmlHelper.ViewContext.Writer.WriteLine(row.AsString());
            for(var i = 0; i < columns.Count; i++)
            {
                TagBuilder column = columns.ElementAt(i);

                _htmlHelper.ViewContext.Writer.WriteLine(column.AsString());
                _htmlHelper.ViewContext.Writer.WriteLine(_config.Columns.ElementAt(i).Content.AsString());
                column.TagRenderMode = TagRenderMode.EndTag;
                _htmlHelper.ViewContext.Writer.WriteLine(column.AsString());
            }
            row.TagRenderMode = TagRenderMode.EndTag;
            _htmlHelper.ViewContext.Writer.WriteLine(row.AsString());
        }
    }
}