namespace MyAuth_lib.Constants
{
    public static class AuthConstants
    {
        //Authentication
        internal const string ENCRYPTION_KEY = "Yq3t6w9z$C&F)J@NcRfTjWnZr4u7x!A%D*G-KaPdSgVkYp2s5v8y/B?E(H+MbQeT";

        internal const string AUTH_COOKIE = "MyTodoToken";

        internal const string AUDIENCE = "MyTodoJWTServiceClient";
        internal const string ISSUER = "MyTodoJWTServiceServer";
        internal const string TOKEN_SUBJECT = "MyTodoJWTServiceToken";

        internal const string CLIENT_AUTHENTICATION_SCHEMA = "DefaultAuth";

        //Authorization
        public const string VALIDATION_POLICY = "MyTodoRequestValidation";

        public const string QUERY_POLICY = "policy";

        public const string WITHOUT_POLICY = "none";
    }
}
