using System;

namespace Todo.Todos.Dto;

public class GetTodoListDto
{
    public Guid? CreatorId { get; set; }
    public string? FilterText { get; set; }
    public Priority? Priority { get; set; }
    public DateTime? DueDateFrom { get; set; }
    public DateTime? DueDateTo { get; set; }
    public string? Sorting { get; set; }
    public int? SkipCount { get; set; }
    public int? MaxResultCount { get; set; }
}
