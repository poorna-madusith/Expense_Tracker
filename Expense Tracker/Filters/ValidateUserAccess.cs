using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Expense_Tracker.Filters
{
    public class ValidateUserAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("id", out var idObject) && idObject is int id)
            {
                var controller = context.Controller as Controller;
                if (controller == null)
                {
                    context.Result = new BadRequestResult();
                    return;
                }

                // The actual validation will be done in the controller action
                // This is just a placeholder to ensure the id parameter exists
                base.OnActionExecuting(context);
            }
        }
    }
}
