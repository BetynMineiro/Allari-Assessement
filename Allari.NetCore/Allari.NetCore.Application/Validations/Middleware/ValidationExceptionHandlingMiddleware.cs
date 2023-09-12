using System.Text.Json;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Allari.NetCore.Application.Validations.Middleware;

public class ValidationExceptionHandlingMiddleware : IMiddleware
  {
    private readonly ILogger<ValidationExceptionHandlingMiddleware> _logger;

    public ValidationExceptionHandlingMiddleware(
      ILogger<ValidationExceptionHandlingMiddleware> logger)
    {
      this._logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
      try
      {
        await next(context);
      }
      catch (ValidationException ex)
      {
        await this.HandleExceptionAsync(context, ex);
      }
      catch (Exception ex)
      {
        await this.HandleGenericExceptionAsync(context, ex);
      }
    }

    public static Task HandleForbiddenToken(ForbiddenContext ctx)
    {
      ProblemDetails problemDetails = new ProblemDetails();
      problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3";
      problemDetails.Title = "Forbidden";
      problemDetails.Status = new int?(403);
      problemDetails.Extensions["errors"] = (object) new Dictionary<string, string>()
      {
        {
          "Server",
          "Request is invalid"
        }
      };
      ctx.Response.ContentType = "application/json";
      ctx.Response.StatusCode = problemDetails.Status.Value;
      return ctx.Response.WriteAsync(JsonSerializer.Serialize<ProblemDetails>(problemDetails));
    }

    public static Task HandleFailToken(AuthenticationFailedContext ctx)
    {
      ProblemDetails problemDetails = new ProblemDetails()
      {
        Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
        Title = "Unauthorized",
        Status = new int?(401)
      };
      problemDetails.Extensions["errors"] = (object) new Dictionary<string, string>()
      {
        {
          "Server",
          ctx.Exception.Message
        }
      };
      ctx.Response.ContentType = "application/json";
      ctx.Response.StatusCode = problemDetails.Status.Value;
      return ctx.Response.WriteAsync(JsonSerializer.Serialize<ProblemDetails>(problemDetails));
    }

    private Task HandleExceptionAsync(HttpContext httpContext, ValidationException exception)
    {
      ProblemDetails problemDetails1 = new ProblemDetails()
      {
        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
        Title = "Bad Request",
        Status = new int?(400)
      };
      problemDetails1.Extensions["errors"] = (object) ValidationExceptionHandlingMiddleware.GetErrors(exception);
      ProblemDetails problemDetails2 = problemDetails1;
      httpContext.Response.ContentType = "application/json";
      httpContext.Response.StatusCode = problemDetails2.Status.Value;
      string text = JsonSerializer.Serialize<ProblemDetails>(problemDetails2);
      this._logger.LogError("{obj}", (object) text);
      return httpContext.Response.WriteAsync(text);
    }

    private Task HandleGenericExceptionAsync(HttpContext httpContext, Exception exception)
    {
      ProblemDetails problemDetails1 = new ProblemDetails()
      {
        Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
        Title = "Internal Server Error",
        Status = new int?(500)
      };
      problemDetails1.Extensions["errors"] = (object) new Dictionary<string, string>()
      {
        {
          "Server",
          exception.Message
        }
      };
      ProblemDetails problemDetails2 = problemDetails1;
      httpContext.Response.ContentType = "application/json";
      httpContext.Response.StatusCode = problemDetails2.Status.Value;
      string text = JsonSerializer.Serialize<ProblemDetails>(problemDetails2);
      this._logger.LogError("{obj}", (object) text);
      return httpContext.Response.WriteAsync(text);
    }

    private static Dictionary<string, IEnumerable<string>> GetErrors(ValidationException exception) => exception.Errors.GroupBy<ValidationFailure, string>((Func<ValidationFailure, string>) (e => e.PropertyName)).ToDictionary<IGrouping<string, ValidationFailure>, string, IEnumerable<string>>((Func<IGrouping<string, ValidationFailure>, string>) (g => g.Key), (Func<IGrouping<string, ValidationFailure>, IEnumerable<string>>) (g => g.Select<ValidationFailure, string>((Func<ValidationFailure, string>) (e => e.ErrorMessage))));
  }