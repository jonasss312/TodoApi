namespace todo_api.Data
{
    public static class Constants
    {
        public const string ERROR_INVALID_LOGIN_DETAILS = "Username or password invalid.";
        public const string ERROR_USER_DOES_NOT_EXISTS = "Email not found.";
        public const string ERROR_PASSWORD_TOO_SHORT = "Password must be atleast 12 symbols.";
        public const string ERROR_CANNOT_CREATE_NEW_USER = "Username or password invalid.";
        public const string ERROR_ASSIGNMENT_NOT_FOUND = "Assignment not found.";
        public const string ERROR_BAD_USER = "You have no permission.";
        public const string ERROR_NOT_ASSIGNMENT_CREATOR = "This is not your assignment.";
        public const string ERROR_ASSIGNMENT_VALIDATION = "Assignment name and status required.";
        public const string ERROR_INVALID_EMAIL = "Email is not valid.";
        public const string ERROR_SENDING_EMAIL = "Email was not sent.";
        public const string SUCCESS_EMAIL_SENT = "Email was sent.";

        public const string API_PATH = "api/";
        public const string USERS_PATH = "users/";
        public const string ASSIGNMENTS_PATH = "assignments/";

        public const string USER_ID = "{userId}";
        public const string ASSIGNMENT_ID = "{assignmentId}";

        public const string LOGIN_COMMAND = "login";
        public const string REGISTER_COMMAND = "register";
        public const string RESET_PASSWORD_COMMAND = "resetPassword";

        public const string ADMIN_ROLE = "admin";
        public const string USER_ROLE = "user";
    }
}
