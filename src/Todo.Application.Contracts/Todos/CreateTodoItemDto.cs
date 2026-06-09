using System;
using Todo.Todos;

public class CreateTodoItemDto
{
    public string Title { get; set; }
    public bool IsDone { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public DateTime DueDate { get; set; }
}
