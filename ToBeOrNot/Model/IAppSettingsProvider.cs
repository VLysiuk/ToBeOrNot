namespace ToBeOrNot.Model
{
    public interface IAppSettingsProvider
    {
        string GetAppVersion();
        string GetAuthorEmail();
    }
}
