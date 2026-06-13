using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Todo.Errors;
using Todo.Localization;

namespace Todo.Todos.Dto;

public class CreateTodoItemDto : IValidatableObject
{
    [Required]
    [MaxLength(256)]
    [DefaultValue("Task 1")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(1024)]
    [DefaultValue("Task 1 description")]
    public string Description { get; set; } = string.Empty;

    public Priority? Priority { get; set; }

    public DateTime? DueDate { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var localization = validationContext.GetRequiredService<IStringLocalizer<TodoResource>>();

        List<ValidationResult> errors = new List<ValidationResult>();

        if (Priority.HasValue &&
            !Enum.IsDefined(typeof(Priority), Priority))
        {
            errors.Add(new ValidationResult(
                localization[TodoErrorCodes.InvalidPriority],
                [nameof(Priority)]
            ));
        }

        if (DueDate.HasValue &&
            DueDate.Value <= DateTime.Now)
        {
            errors.Add(new ValidationResult(
                localization[TodoErrorCodes.DueDateMustBeFuture],
                [nameof(DueDate)]
            ));
        }

        return errors;
    }
}
