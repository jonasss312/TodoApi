namespace todo_api.Data
{
    public static class Constants
    {
        public const string ERROR_INVALID_LOGIN_DETAILS = "Username or password invalid";
        public const string ERROR_CANNOT_CREATE_NEW_USER = "Username or password invalid";
        public const string ERROR_ASSIGNMENT_NOT_FOUND = "Assignment not found.";
        public const string ERROR_ASSIGNMENT_VALIDATION = "Assignment name and status required";
        public const string ERROR_INVALID_EMAIL = "Email is not valid";

        public const string API_PATH = "api/";
        public const string USERS_PATH = "users/";
        public const string ASSIGNMENTS_PATH = "assignments/";

        public const string USER_ID = "{userId}";
        public const string ASSIGNMENT_ID = "{assignmentId}";

    }
}
