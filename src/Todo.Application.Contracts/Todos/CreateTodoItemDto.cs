using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Todo.Todos;


public class CreateTodoItemDto : IValidatableObject
{
    [Required]
    [MaxLength(256)]
    [DefaultValue("Task 1")]
    public string Title { get; set; }

    [Required]
    [MaxLength(1024)]
    [DefaultValue("Task 1 description")]
    public string Description { get; set; }

    [Required]
    public Priority Priority { get; set; }

    [Required]
    public DateTime DueDate { get; set; }


    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        if (!Enum.IsDefined(typeof(Priority), Priority))
        {
            yield return new ValidationResult(
                "Priority must be Low, Medium or High",
                new[] { nameof(Priority) });
        }
    }
}
