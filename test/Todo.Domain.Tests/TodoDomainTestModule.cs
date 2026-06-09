using Volo.Abp.Modularity;

namespace Todo;

[DependsOn(
    typeof(TodoDomainModule),
    typeof(TodoTestBaseModule)
)]
public class TodoDomainTestModule : AbpModule
{

}
