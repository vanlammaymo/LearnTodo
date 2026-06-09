using Xunit;

namespace Todo.EntityFrameworkCore;

[CollectionDefinition(TodoTestConsts.CollectionDefinitionName)]
public class TodoEntityFrameworkCoreCollection : ICollectionFixture<TodoEntityFrameworkCoreFixture>
{

}
