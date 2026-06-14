namespace Todo.Permissions;

public static class TodoPermissions
{
    public const string GroupName = "Todo";

    public static class Todos
    {
        public const string Default = GroupName + ".Todos";
        public const string View = Default + ".View";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
        public const string MarkAsDone = Default + ".MarkAsDone";
    }
}
