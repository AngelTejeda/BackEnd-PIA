using Core.BackEnd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.ActionFilters
{
    // Verifica que exista un cliente con el ID especificado.
    public class Customer_IdExistsActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            string id = context.ActionArguments["id"] as string;

            if (new CustomerSC().GetCustomerById(id) == null)
            {
                context.ModelState.AddModelError("id", $"There is no customer with id {id}.");
                context.Result = new NotFoundObjectResult(context.ModelState);
            }   
        }
    }
}
