using Core.BackEnd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.ActionFilters
{
    // Verifica que exista un producto con el ID especificado.
    public class Product_IdExistsActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            int id = (int)context.ActionArguments["id"];

            if (new ProductSC().GetProductById(id) == null)
            {
                context.ModelState.AddModelError("id", $"There is no product with id {id}.");
                context.Result = new NotFoundObjectResult(context.ModelState);
            }
                
        }
    }
}
