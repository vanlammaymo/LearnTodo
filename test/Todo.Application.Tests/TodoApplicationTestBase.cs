using Volo.Abp.Modularity;

namespace Todo;

public abstract class TodoApplicationTestBase<TStartupModule> : TodoTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
