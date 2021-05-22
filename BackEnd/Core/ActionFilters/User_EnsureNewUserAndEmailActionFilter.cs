using Core.BackEnd;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.ActionFilters
{
    // Verifia que el Usuario y el Correo no existan en la Base de Datos.
    public class User_EnsureNewUserAndEmailActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            bool isValid = true;
            
            UserPostDTO newUser = context.ActionArguments["newUser"] as UserPostDTO;
            
            if (new UserSC().UserExists(newUser.UserName))
            {
                context.ModelState.AddModelError("username", "The username is not available.");
                isValid = false;
            }

            if (new UserSC().EmailExists(newUser.Email))
            {
                context.ModelState.AddModelError("email", "The email has already been registered.");
                isValid = false;
            }
           
            if (!isValid)
                context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
