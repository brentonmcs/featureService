namespace WH.FeatureService.Api.Common
{
    public interface ICache
    {
        T Get<T>(string key);

        void Set<T>(string key, T value);
    }
}