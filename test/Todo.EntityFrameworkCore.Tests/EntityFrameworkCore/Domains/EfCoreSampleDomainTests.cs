using Todo.Samples;
using Xunit;

namespace Todo.EntityFrameworkCore.Domains;

[Collection(TodoTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<TodoEntityFrameworkCoreTestModule>
{

}
