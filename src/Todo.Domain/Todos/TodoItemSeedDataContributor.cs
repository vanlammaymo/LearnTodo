using System;

namespace Todo.Todos;

public class TodoItemSeedDataContributor
{
    public static TodoItem[] GetSeedData()
    {
        return new[]
        {
            new TodoItem(Guid.NewGuid(), "Buy groceries", "Milk, Bread, Eggs, Butter", false, DateTime.Now.AddDays(2), Priority.High),
            new TodoItem(Guid.NewGuid(), "Finish project report", "Complete the final report for the project", false, DateTime.Now.AddDays(5), Priority.Medium),
            new TodoItem(Guid.NewGuid(), "Call plumber", "Fix the leaking sink in the kitchen", false, DateTime.Now.AddDays(1), Priority.Low)
        };
    }
}