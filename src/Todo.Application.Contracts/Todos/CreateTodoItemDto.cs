using System;
using System.ComponentModel.DataAnnotations;


public class CreateTodoItemDto
{
    [Required]
    [MaxLength(256)]
    public string Title { get; set; } = "Title";

    [Required]
    [MaxLength(1024)]
    public string Description { get; set; } = "Description";

    public bool IsDone { get; set; }

    public string Priority { get; set; } = "Medium";

    public DateTime DueDate { get; set; } = DateTime.Now.AddDays(7);
}
