using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MvcCoreBootstrapTable.Rendering
{
    public class TableBinder : IModelBinder
    {
        Task IModelBinder.BindModelAsync(ModelBindingContext bindingContext)
        {
            TableState tableState = new TableStateParser().Parse(bindingContext.HttpContext);

            bindingContext.Result = ModelBindingResult.Success(new TableUpdater(tableState));

            return(Task.FromResult(1));
        }
    }
}
