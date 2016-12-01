using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;
using MvcCoreBootstrapForm.Extensions;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class RowRenderer
    {
        private readonly RowConfig _config;
        private readonly TextWriter _contentWriter;

        public RowRenderer(RowConfig config, TextWriter contentWriter)
        {
            _config = config;
            _contentWriter = contentWriter;
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

            _contentWriter.WriteLine(row.AsString());
            for(var i = 0; i < columns.Count; i++)
            {
                TagBuilder column = columns.ElementAt(i);

                _contentWriter.WriteLine(column.AsString());
                _contentWriter.WriteLine(_config.Columns.ElementAt(i).Content.AsString());
                column.TagRenderMode = TagRenderMode.EndTag;
                _contentWriter.WriteLine(column.AsString());
            }
            row.TagRenderMode = TagRenderMode.EndTag;
            _contentWriter.WriteLine(row.AsString());
        }
    }
}