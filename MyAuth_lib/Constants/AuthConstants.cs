namespace MyAuth_lib.Constants
{
    public static class AuthConstants
    {
        //Authentication
        internal static string ENCRYPTION_KEY = "Yq3t6w9z$C&F)J@NcRfTjWnZr4u7x!A%D*G-KaPdSgVkYp2s5v8y/B?E(H+MbQeT";

        internal static string AUTH_COOKIE = "MyTodoToken";

        internal static string AUDIENCE = "MyTodoJWTServiceClient";
        internal static string ISSUER = "MyTodoJWTServiceServer";

        //Authorization
        public static string VALIDATION_POLICY = "MyTodoRequestValidation";

        public static string QUERY_POLICY = "policy";

        public static string WITHOUT_POLICY = "none";

        internal static int CLIENT_TOKEN_VALIDATION_CACHE_EXPIRATION = 5;
    }
}
