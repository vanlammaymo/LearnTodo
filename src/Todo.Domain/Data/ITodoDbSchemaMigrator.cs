using System.Threading.Tasks;

namespace Todo.Data;

public interface ITodoDbSchemaMigrator
{
    Task MigrateAsync();
}
