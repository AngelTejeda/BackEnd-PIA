using Core.BackEnd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.ActionFilters
{
    // Verifica que exista un empleado con el ID especificado.
    public class Employee_IdExistsActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            int id = (int)context.ActionArguments["id"];

            if (new EmployeeSC().GetEmployeeById(id) == null)
            {
                context.ModelState.AddModelError("id", $"There is no employee with id {id}.");
                context.Result = new NotFoundObjectResult(context.ModelState);
            }   
        }
    }
}
