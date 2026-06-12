using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Todo.Errors;
using Todo.Localization;

namespace Todo.Todos.Dto;

public class UpdateTodoItemDto : IValidatableObject
{
    [Required]
    public Guid Id { get; set; }

    [MaxLength(256)]
    public string? Title { get; set; }

    [MaxLength(1024)]
    public string? Description { get; set; }

    public Priority? Priority { get; set; }

    public DateTime? DueDate { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var L = validationContext.GetRequiredService<IStringLocalizer<TodoResource>>();

        List<ValidationResult> errors = new List<ValidationResult>();

        if (Title is null &&
            Description is null &&
            Priority is null &&
            DueDate is null)
        {
            errors.Add(new ValidationResult(
                L[TodoErrorCodes.AtLeastOneFieldRequired]
            ));
        }

        if (Priority.HasValue &&
            !Enum.IsDefined(typeof(Priority), Priority))
        {
            errors.Add(new ValidationResult(
                L[TodoErrorCodes.InvalidPriority],
                [nameof(Priority)]
            ));
        }

        if (DueDate.HasValue &&
            DueDate.Value <= DateTime.UtcNow)
        {
            errors.Add(new ValidationResult(
                L[TodoErrorCodes.DueDateMustBeFuture],
                [nameof(DueDate)]
            ));
        }

        return errors;
    }
}
