using Todo.Samples;
using Xunit;

namespace Todo.EntityFrameworkCore.Applications;

[Collection(TodoTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<TodoEntityFrameworkCoreTestModule>
{

}
