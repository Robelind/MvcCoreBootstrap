using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace MvcCoreBootstrapForm.Rendering
{
    public class BootstrapForm : MvcForm
    {
        private readonly ViewContext _viewContext;

        public BootstrapForm(ViewContext viewContext, HtmlEncoder htmlEncoder)
        : base(viewContext, htmlEncoder)
        {
            _viewContext = viewContext;
            //viewContext.Writer = new ViewBufferTextWriter();
        }

        protected override void GenerateEndForm()
        {
            base.GenerateEndForm();
            ViewBufferTextWriter a = _viewContext.Writer as ViewBufferTextWriter;
            foreach(ViewBufferValue viewBufferValue in a.Buffer.Pages[0].Buffer)
            {
                string s = viewBufferValue.ToString();
            }
            //string s = _viewContext.Writer.ToString();
            //_viewContext.Writer = new StringWriter();
            //_viewContext.Writer.Write(s);
        }
    }
}
