using Volo.Abp.Modularity;

namespace Todo;

/* Inherit from this class for your domain layer tests. */
public abstract class TodoDomainTestBase<TStartupModule> : TodoTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
