using Volo.Abp.Modularity;

namespace Todo;

[DependsOn(
    typeof(TodoApplicationModule),
    typeof(TodoDomainTestModule)
)]
public class TodoApplicationTestModule : AbpModule
{

}
