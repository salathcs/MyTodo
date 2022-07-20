namespace MyAuth_lib.Interfaces
{
    public interface IAuthClientSupplier
    {
        string GetValidationUrl();

        int GetCacheExpiration();
    }
}
