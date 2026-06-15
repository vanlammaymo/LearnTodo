namespace Todo.Errors;

public static class TodoErrorCodes
{
    public const string TodoTitleAlreadyExists = "TodoError:001";
    public const string TodoItemDoesNotExist = "TodoError:002";
    public const string DontHavePermission = "TodoError:003";
    public const string TaskDoesNotExist = "TodoError:004";
    public const string FieldIsRequired = "ValidateMessage:FieldIsRequried";
    public const string FieldMaxLengthError = "ValidateMessage:FieldMaxLengthError";
    public const string InvalidField = "ValidateMessage:InvalidField";
    public const string InvalidPriority = "ValidateMessage:InvalidPriority";
    public const string DueDateMustBeFuture = "ValidateMessage:DueDateMustBeFuture";
    public const string AtLeastOneFieldRequired = "ValidateMessage:AtLeastOneFieldRequired";
}
