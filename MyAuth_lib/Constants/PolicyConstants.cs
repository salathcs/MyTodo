namespace MyAuth_lib.Constants
{
    public static class PolicyConstants
    {
        public const string ADMIN_POLICY = "Admin";

        //Resource based policies

        public const string RESOURCE_BASED_PREFIX = "ResourceBased";

        public const string RESOURCE_BASED_TODO_POLICY = $"{RESOURCE_BASED_PREFIX}_Todo";
        public const string RESOURCE_BASED_USER_POLICY = $"{RESOURCE_BASED_PREFIX}_User";
    }
}
