using Todo.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Todo.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TodoEntityFrameworkCoreModule),
    typeof(TodoApplicationContractsModule)
)]
public class TodoDbMigratorModule : AbpModule
{
}
