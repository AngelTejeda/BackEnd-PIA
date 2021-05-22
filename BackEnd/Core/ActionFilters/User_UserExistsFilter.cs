using Core.BackEnd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.ActionFilters
{
    // Verifica que el usuario exista en la Base de Datos.
    public class User_UserExistsFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            string username = context.ActionArguments["username"] as string;
            
            if (!new UserSC().UserExists(username))
            {
                context.ModelState.AddModelError("username", "The user does not exist.");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
