using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors.Definitions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

public class ValidateInputMiddleware
{
    private readonly FieldDelegate _next;
    private readonly IServiceProvider serviceProvider;

    public ValidateInputMiddleware(FieldDelegate next, IServiceProvider serviceProvider)
    {
        _next = next;
        this.serviceProvider = serviceProvider;
    }

    public async Task Invoke(IMiddlewareContext context)
    {
        if (context.FieldSelection.Arguments.Count == 0)
        {
            await _next(context);
            return;
        }

        var errors = context.FieldSelection.Arguments
            .Select(a => context.Argument<object>(a.Name.Value))
            .SelectMany(ValidateObject);

        if (errors.Any())
        {
            foreach (var error in errors)
            {
                context.ReportError(ErrorBuilder.New()
                    .SetCode(error.ErrorCode)
                    .SetMessage(error.ErrorMessage)
                    .SetExtension("memberNames", error.PropertyName)
                    .AddLocation(context.FieldSelection.Location.Line, context.FieldSelection.Location.Column)
                    .SetPath(context.Path)
                    .Build());
            }

            context.Result = null;

        }
        else
        {
            await _next(context);
        }

        IEnumerable<FluentValidation.Results.ValidationFailure> ValidateObject(object argument)
        {
            try
            {
                var actualObjectType = argument.GetType();
                var validatorGenericType = typeof(IValidator<>);
                var validatorType = validatorGenericType.MakeGenericType(actualObjectType);
                var validator = context.Service(validatorType); 
                if (validator == null) return null;

                var validateMethodInfo = validatorType.GetMethod(nameof(IValidator.Validate), new[] { actualObjectType });
                var results = validateMethodInfo.Invoke(validator, new[] { argument }) as FluentValidation.Results.ValidationResult;

                return results.Errors.AsEnumerable();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to validate with error '{ex.Message}'.");
            }
        }
    }
}
