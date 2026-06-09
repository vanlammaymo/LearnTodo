namespace Todo.Permissions;

public static class TodoPermissions
{
    public const string GroupName = "Todos";

    public static class TodosPermissions
    {
        public const string Default = GroupName + ".Todos";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
