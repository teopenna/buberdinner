using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Authorize]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(IList<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }
        
        if (errors.All(e => e.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }
        
        HttpContext.Items["errors"] = errors;

        var firstError = errors.First();

        return Problem(firstError);
    }

    private IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }

    private IActionResult ValidationProblem(IList<Error> errors)
    {
        var modelModelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelModelStateDictionary.AddModelError(error.Code, error.Description);
        }

        return ValidationProblem(modelModelStateDictionary);
    }
}