using Microsoft.AspNetCore.Mvc.ModelBinding;
using MediumClone.Api.Common.Http;
using ErrorOr;

namespace MediumClone.Api.EndpointDefinitions;


public abstract class BaseEndpointDefinition
{
    protected IResult ResultsProblem(HttpContext context, List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Results.Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        context.Items[HttpContextItemKeys.Errors] = errors;

        return Problem(errors[0]);
    }

    private IResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Results.Problem(statusCode: statusCode, title: error.Description);
    }

    private IResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description);
        }
        var errorsDictionary = modelStateDictionary.ToDictionary(
       kvp => kvp.Key,
       kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());

        return Results.ValidationProblem(errorsDictionary);
    }

}